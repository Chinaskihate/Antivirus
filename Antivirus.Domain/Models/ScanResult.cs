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
    public int TotalEvilJsDetects { get; set; }

    /// <summary>
    ///     Total rm -rf detects.
    /// </summary>
    public int TotalRemoveDetects { get; set; }

    /// <summary>
    ///     Total Rundll32 sus.dll SusEntry detects.
    /// </summary>
    public int TotalRunDllDetects { get; set; }

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
                TotalEvilJsDetects = first.TotalEvilJsDetects + second.TotalEvilJsDetects,
                TotalRemoveDetects = first.TotalRemoveDetects + second.TotalRemoveDetects,
                TotalRunDllDetects = first.TotalRunDllDetects + second.TotalRunDllDetects,
                TotalErrors = first.TotalErrors + second.TotalErrors,
                ErrorMessages = first.ErrorMessages.Concat(second.ErrorMessages).ToList(),
                ExecutionTime = first.ExecutionTime + second.ExecutionTime
            };

            return res;
        }
    }
}