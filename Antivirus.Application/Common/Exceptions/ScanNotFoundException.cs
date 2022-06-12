namespace Antivirus.Application.Common.Exceptions;

/// <summary>
///     Thrown if scan was not found.
/// </summary>
public class ScanNotFoundException : Exception
{
    public ScanNotFoundException(int id)
    {
        Id = id;
    }

    public ScanNotFoundException(int id, string message) : base(message)
    {
        Id = id;
    }

    public ScanNotFoundException(int id, string message, Exception innerException) : base(message, innerException)
    {
        Id = id;
    }

    /// <summary>
    ///     Id of scan.
    /// </summary>
    public int Id { get; set; }
}