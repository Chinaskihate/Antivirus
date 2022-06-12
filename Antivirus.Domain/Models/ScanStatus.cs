namespace Antivirus.Domain.Models;

/// <summary>
///     Result of scan.
/// </summary>
public class ScanStatus
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
    ///     Scan start time.
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     Scan finish time.
    /// </summary>
    public DateTime? FinishTime { get; set; }

    /// <summary>
    ///     Is scan finished.
    /// </summary>
    public bool IsFinished => FinishTime != null;

    /// <summary>
    ///     Error messages.
    /// </summary>
    public List<string> ErrorMessages { get; set; } = new();
}