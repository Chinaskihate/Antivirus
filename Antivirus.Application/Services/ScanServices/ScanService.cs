using System.Diagnostics;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanServices;

public class ScanService : IScanService
{
    private readonly object _locker = new();

    /// <summary>
    ///     Scans directory asynchronously.
    /// </summary>
    /// <param name="path"> Path to directory. </param>
    /// <returns> Scan result(task). </returns>
    public ScanResult ScanAsync(string path)
    {
        var watch = Stopwatch.StartNew();
        var res = new ScanResult();
        Task.Run(() =>
        {
            ScanDirectory(path, res);
            watch.Stop();
            res.FinishTime=DateTime.Now;
        });

        return res;
    }

    /// <summary>
    ///     Scans directory.
    /// </summary>
    /// <param name="path"> Path to directory. </param>
    /// <returns> Scan result. </returns>
    private void ScanDirectory(string path, ScanResult res)
    {
        try
        {
            var files = Directory.GetFiles(path);
            ScanFiles(files, res);
            var subdirectories = Directory.GetDirectories(path);
            Parallel.ForEach(subdirectories, subDirectory =>
            {
                ScanDirectory(subDirectory, res);
            });
        }
        catch (Exception ex)
        {
            res.TotalErrors++;
            res.ErrorMessages.Add(ex.Message);
        }
    }

    /// <summary>
    ///     Scans files.
    /// </summary>
    /// <param name="files"> Paths to files. </param>
    /// <returns> Scan result. </returns>
    private void ScanFiles(string[] files, ScanResult res)
    {
        res.TotalProcessedFiles += files.Length;
        Parallel.ForEach(files, file =>
        {
            try
            {
                var isJs = Path.GetExtension(file).Equals(".js");
                Parallel.ForEach(File.ReadLines(file),
                    line => { ProcessMalwareType(res, LineAnalyzer.Analyze(line, isJs)); });
            }
            catch (Exception ex)
            {
                res.TotalErrors++;
                res.ErrorMessages.Add(ex.Message);
            }
        });
    }

    /// <summary>
    ///     Processes malware type, change scan result if needed.
    /// </summary>
    /// <param name="res"> Scan result. </param>
    /// <param name="malwareType"> Malware type. </param>
    private void ProcessMalwareType(ScanResult res, Malware malwareType)
    {
        lock (_locker)
        {
            switch (malwareType)
            {
                case Malware.EvilJs:
                    res.TotalEvilJsDetects++;
                    break;
                case Malware.Remover:
                    res.TotalRemoveDetects++;
                    break;
                case Malware.DllRunner:
                    res.TotalRunDllDetects++;
                    break;
            }
        }
    }
}