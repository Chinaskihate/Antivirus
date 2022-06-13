using System.Diagnostics;
using Antivirus.Application.Interfaces.ScanServices;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services.ScanServices;

/// <summary>
///     Service for scanning directory.
/// </summary>
public class ScanService : IScanService
{
    private readonly object _locker = new();

    /// <summary>
    ///     Scans directory.
    /// </summary>
    /// <param name="path"> Directory to scan. </param>
    /// <returns> Status of scan. </returns>
    public ScanStatus Scan(string path)
    {
        var watch = Stopwatch.StartNew();
        var res = new ScanStatus()
        {
            Path = path
        };
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
    /// <param name="path"> Directory to scan. </param>
    /// <param name="res"> Scan status. </param>
    private void ScanDirectory(string path, ScanStatus res)
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
            res.ErrorMessages.Add(ex.Message);
        }
    }

    /// <summary>
    ///     Scans files in directory.
    /// </summary>
    /// <param name="files"> Files to scan. </param>
    /// <param name="res"> Status of scan. </param>
    private void ScanFiles(string[] files, ScanStatus res)
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
                res.ErrorMessages.Add(ex.Message);
            }
        });
    }

    /// <summary>
    ///     Processes malware type, changes scan result if needed.
    /// </summary>
    /// <param name="res"> Scan result. </param>
    /// <param name="malwareType"> Malware type. </param>
    private void ProcessMalwareType(ScanStatus res, Malware malwareType)
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