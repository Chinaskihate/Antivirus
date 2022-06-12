namespace Antivirus.Application.Common.Exceptions;

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

    public int Id { get; set; }
}