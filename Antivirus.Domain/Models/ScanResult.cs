using System.Text;

namespace Antivirus.Domain.Models;

/// <summary>
///     Result of scan.
/// </summary>
public class ScanResult
{
    private static readonly object Locker = new();
    private int _totalProcessedFiles;

    /// <summary>
    ///     Total files processed.
    /// </summary>
    public int TotalProcessedFiles
    {
        get => _totalProcessedFiles;
        set
        {
            lock (Locker)
            {
                _totalProcessedFiles = value;
            }
        }
    }

    /// <summary>
    ///     Total evil javascripts detects.
    /// </summary>
    public int TotalEvilJSDetects { get; set; }

    /// <summary>
    ///     Total rm -rf detects.
    /// </summary>
    public int TotalRMDetects { get; set; }

    /// <summary>
    ///     Total Rundll32 sus.dll SusEntry detects.
    /// </summary>
    public int TotalRunDLLDetects { get; set; }

    /// <summary>
    ///     Total errors.
    /// </summary>
    public int TotalErrors { get; set; }

    /// <summary>
    ///     Error messages.
    /// </summary>
    public List<string> ErrorMessages { get; set; } = new();

    /// <summary>
    ///     Scan execution time.
    /// </summary>
    public TimeSpan ExecutionTime { get; set; }

    /// <summary>
    ///     Concats two scan results.
    /// </summary>
    /// <param name="first"> First scan result. </param>
    /// <param name="second"> Second scan result. </param>
    /// <returns> New scan result. </returns>
    public static ScanResult operator +(ScanResult first, ScanResult second)
    {
        lock (Locker)
        {
            var res = new ScanResult
            {
                TotalProcessedFiles = first.TotalProcessedFiles + second.TotalProcessedFiles,
                TotalEvilJSDetects = first.TotalEvilJSDetects + second.TotalEvilJSDetects,
                TotalRMDetects = first.TotalRMDetects + second.TotalRMDetects,
                TotalRunDLLDetects = first.TotalRunDLLDetects + second.TotalRunDLLDetects,
                TotalErrors = first.TotalErrors + second.TotalErrors,
                ErrorMessages = first.ErrorMessages.Concat(second.ErrorMessages).ToList(),
                ExecutionTime = first.ExecutionTime + second.ExecutionTime
            };

            return res;
        }
    }
}