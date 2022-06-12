namespace Antivirus.Domain.Models;

/// <summary>
///     Result of scan.
/// </summary>
public class ScanStatus
{
    private static readonly object Locker = new();
    private int _processedFiles = 0;

    public bool IsFinished { get; set; } = false;

    public int FilesToProcess { get; set; } = 0;

    /// <summary>
    ///     Total files processed.
    /// </summary>
    public int ProcessedFiles
    {
        get => _processedFiles;
        set
        {
            lock (Locker)
            {
                _processedFiles = value;
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
    ///     Scan start time.
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     Scan finish time.
    /// </summary>
    public DateTime? FinishTime { get; set; } = null;
    
    /// <summary>
    ///     Execution time.
    /// </summary>
    public TimeSpan ExecutionTime => FinishTime == null ? TimeSpan.Zero : (TimeSpan)(FinishTime - StartTime);



    /// <summary>
    ///     Concats two scan results.
    /// </summary>
    /// <param name="first"> First scan result. </param>
    /// <param name="second"> Second scan result. </param>
    /// <returns> New scan result. </returns>
    public static ScanStatus operator +(ScanStatus first, ScanStatus second)
    {
        lock (Locker)
        {
            var res = new ScanStatus
            {
                ProcessedFiles = first.ProcessedFiles + second.ProcessedFiles,
                TotalEvilJsDetects = first.TotalEvilJsDetects + second.TotalEvilJsDetects,
                TotalRemoveDetects = first.TotalRemoveDetects + second.TotalRemoveDetects,
                TotalRunDllDetects = first.TotalRunDllDetects + second.TotalRunDllDetects,
                TotalErrors = first.TotalErrors + second.TotalErrors,
                StartTime = first.StartTime > second.StartTime ? first.StartTime : second.StartTime
            };

            return res;
        }
    }
}