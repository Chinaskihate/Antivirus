namespace Antivirus.WebClient.Results;

public class ScanResult
{
    public int TotalProcessedFiles { get; set; }

    public int TotalEvilJsDetects { get; set; }
    
    public int TotalRemoveDetects { get; set; }
    
    public int TotalRunDllDetects { get; set; }
    
    public int TotalErrors { get; set; }
    
    public TimeSpan ExecutionTime { get; set; }
}