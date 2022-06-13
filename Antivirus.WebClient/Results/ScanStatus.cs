namespace Antivirus.WebClient.Results;

/// <summary>
///     Scan status from WebAPI.
/// </summary>
public class ScanStatus
{
    /// <summary>
    ///     Total files processed.
    /// </summary>
    public int TotalProcessedFiles { get; set; }

    /// <summary>
    ///     Total evil javascripts detects.
    /// </summary>
    public int TotalEvilJsDetects { get; set; }

    /// <summary>
    ///     Directory to scan.
    /// </summary>
    public string Path { get; set; }

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
    ///     Scan execution time.
    /// </summary>
    public TimeSpan ExecutionTime { get; set; }

    /// <summary>
    ///     Is scan finished.
    /// </summary>
    public bool IsFinished { get; set; }

    /// <summary>
    ///     Error messages.
    /// </summary>
    public List<string> ErrorMessages { get; set; } = new();
}