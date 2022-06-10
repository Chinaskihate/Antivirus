using System.Diagnostics;
using System.Text.RegularExpressions;
using Antivirus.Application.Interfaces;
using Antivirus.Domain.Models;

namespace Antivirus.Application.Services;

public class ScanService : IScanService
{
    private readonly object _locker = new();

    /// <summary>
    ///     Current scanning file.
    /// </summary>
    public string CurrentFile { get; private set; } = string.Empty;

    /// <summary>
    ///     Scans directory asynchronously.
    /// </summary>
    /// <param name="path"> Path to directory. </param>
    /// <returns> Scan result(task). </returns>
    public async Task<ScanResult> ScanAsync(string path)
    {
        var watch = Stopwatch.StartNew();
        var res = new ScanResult();
        CurrentFile = path;
        await Task.Run(() => res = ScanDirectory(path));
        watch.Stop();
        CurrentFile = string.Empty;
        res.ExecutionTime = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);

        return res;
    }

    /// <summary>
    ///     Scans directory.
    /// </summary>
    /// <param name="path"> Path to directory. </param>
    /// <returns> Scan result. </returns>
    private ScanResult ScanDirectory(string path)
    {
        var res = new ScanResult();
        CurrentFile = path;
        try
        {
            var files = Directory.GetFiles(path);
            res += ScanFiles(files);
            var subdirectories = Directory.GetDirectories(path);
            Parallel.ForEach(subdirectories, subdirectory =>
            {
                var directoryResult = ScanDirectory(subdirectory);
                lock (_locker)
                {
                    res += directoryResult;
                }
            });
        }
        catch (Exception ex)
        {
            res.TotalErrors++;
            res.ErrorMessages.Add(ex.Message);
        }

        return res;
    }

    /// <summary>
    ///     Scans files.
    /// </summary>
    /// <param name="files"> Paths to files. </param>
    /// <returns> Scan result. </returns>
    private ScanResult ScanFiles(string[] files)
    {
        var res = new ScanResult();
        res.TotalProcessedFiles += files.Length;
        Parallel.ForEach(files, file =>
        {
            try
            {
                CurrentFile = file;
                var isJs = Path.GetExtension(file).Equals(".js");
                Parallel.ForEach(File.ReadLines(file), line =>
                {
                    ProcessMalwareType(res, LineAnalyzer.Analyze(line, isJs));
                });
            }
            catch (Exception ex)
            {
                res.TotalErrors++;
                res.ErrorMessages.Add(ex.Message);
            }
        });

        return res;
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