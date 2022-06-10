using System.Text;

namespace Antivirus.Domain.Models;

/// <summary>
///     Result of scan.
/// </summary>
public struct ScanResult
{
    private static readonly object Locker = new();
    private int _totalProcessedFiles = 0;

    public ScanResult()
    {
        
    }

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
    public int TotalEvilJsDetects { get; set; } = 0;

    /// <summary>
    ///     Total rm -rf detects.
    /// </summary>
    public int TotalRemoveDetects { get; set; } = 0;

    /// <summary>
    ///     Total Rundll32 sus.dll SusEntry detects.
    /// </summary>
    public int TotalRunDllDetects { get; set; } = 0;

    /// <summary>
    ///     Total errors.
    /// </summary>
    public int TotalErrors { get; set; } = 0;

    /// <summary>
    ///     Scan execution time.
    /// </summary>
    public TimeSpan ExecutionTime { get; set; } = TimeSpan.Zero;

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
                ExecutionTime = first.ExecutionTime + second.ExecutionTime
            };

            return res;
        }
    }
}