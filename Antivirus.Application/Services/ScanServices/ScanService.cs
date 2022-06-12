using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanServices;

public class ScanService : IScanService
{
    private readonly object _locker = new();

    /// <summary>
    ///     Current scanning file.
    /// </summary>
    public string CurrentFile { get; private set; } = string.Empty;

    /// <summary>
    ///     Scans directory.
    /// </summary>
    /// <param name="path"> Path to directory. </param>
    /// <returns> Scan result. </returns>
    public ScanStatus Scan(string path)
    {
        var res = new ScanStatus
        {
            FilesToProcess = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Count()
        };
        CurrentFile = path;
        Task.Run(() =>
        {
            ScanDirectory(path, res);
            res.FinishTime = DateTime.Now;
            res.IsFinished = true;
            return res;
        });

        return res;
    }

    /// <summary>
    ///     Scans directory.
    /// </summary>
    /// <param name="path"> Path to directory. </param>
    /// <returns> Scan result. </returns>
    private void ScanDirectory(string path, ScanStatus status)
    {
        CurrentFile = path;
        try
        {
            var files = Directory.GetFiles(path);
            ScanFiles(files, status);
            var subdirectories = Directory.GetDirectories(path);
            Parallel.ForEach(subdirectories, subDirectory =>
            {
                ScanDirectory(subDirectory, status);
            });
        }
        catch (Exception)
        {
            lock (_locker)
            {
                status.TotalErrors++;
            }
        }
    }

    /// <summary>
    ///     Scans files.
    /// </summary>
    /// <param name="files"> Paths to files. </param>
    /// <returns> Scan result. </returns>
    private void ScanFiles(string[] files, ScanStatus status)
    {
        status.ProcessedFiles += files.Length;
        Parallel.ForEach(files, file =>
        {
            try
            {
                CurrentFile = file;
                var isJs = Path.GetExtension(file).Equals(".js");
                Parallel.ForEach(File.ReadLines(file),
                    line => { ProcessMalwareType(status, LineAnalyzer.Analyze(line, isJs)); });
            }
            catch (Exception)
            {
                lock (_locker)
                {
                    status.TotalErrors++;
                }
            }
        });
    }

    /// <summary>
    ///     Processes malware type, change scan result if needed.
    /// </summary>
    /// <param name="status"> Scan result. </param>
    /// <param name="malwareType"> Malware type. </param>
    private void ProcessMalwareType(ScanStatus status, Malware malwareType)
    {
        lock (_locker)
        {
            switch (malwareType)
            {
                case Malware.EvilJs:
                    status.TotalEvilJsDetects++;
                    break;
                case Malware.Remover:
                    status.TotalRemoveDetects++;
                    break;
                case Malware.DllRunner:
                    status.TotalRunDllDetects++;
                    break;
            }
        }
    }
}