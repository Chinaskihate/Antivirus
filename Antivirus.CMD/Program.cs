using Antivirus.WebClient;
using Antivirus.WebClient.Interfaces;
using Antivirus.WebClient.Results;
using Antivirus.WebClient.Services;

IManagerService service = new ManagerService(new ManagerHttpClientFactory("https://localhost:5001/api/ScannerManager"));

if (args.Length == 2)
{
    switch (ParseCommandType(args[0]))
    {
        case CommandType.CreateScan:
            await ProcessCreateScanCommand(args[1]);
            break;
        case CommandType.GetStatus:
            await ProcessGetStatusCommand(args[1]);
            break;
        case CommandType.Invalid:
            PrintIncorrectCommandsMessage();
            break;
    }
}
else
{
    PrintIncorrectCommandsMessage();
}

/// <summary>
///     Process create scan command.
/// </summary>
async Task ProcessCreateScanCommand(string path)
{
    try
    {
        var id = await CreateScan(path);
        Console.WriteLine($"Scan was created with ID: {id}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

/// <summary>
///     Process get status command.
/// </summary>
async Task ProcessGetStatusCommand(string str)
{
    int id;
    if (int.TryParse(str, out id) && id >= 0)
    {
        try
        {
            var status = await GetStatus(id);
            PrintStatus(status);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    else
    {
        Console.WriteLine("Scan id have to be integer at least 0.");
    }
}

/// <summary>
///     Create new scan.
/// </summary>
async Task<int> CreateScan(string path)
{
    return await service.CreateScanAsync(path);
}

/// <summary>
///     Get status of scan.
/// </summary>
async Task<ScanStatus> GetStatus(int id)
{
    return await service.GetStatusAsync(id);
}

/// <summary>
///     Prints message if commands were incorrect.
/// </summary>
void PrintIncorrectCommandsMessage()
{
    Console.WriteLine($"Incorrect command!{Environment.NewLine}" +
                      $"Use: scan <path to directory>{Environment.NewLine}" +
                      "Or: status <scan id>");
}

/// <summary>
///     Prints status of scan.
/// </summary>
void PrintStatus(ScanStatus status)
{
    Console.WriteLine("====== SCAN STATUS ======");
    Console.WriteLine($"DIRECTORY TO SCAN: {status.Path}");
    Console.WriteLine($"FINISHED: {(status.IsFinished ? "YES" : "NO")}");
    Console.WriteLine($"PROCESSED FILES: {status.TotalProcessedFiles}");
    Console.WriteLine($"JS DETECTS: {status.TotalEvilJsDetects}");
    Console.WriteLine($"RM -RF DETECTS: {status.TotalRemoveDetects}");
    Console.WriteLine($"RUNDLL32 DETECTS: {status.TotalRunDllDetects}");
    Console.WriteLine($"NUMBER OF ERRORS: {status.TotalErrors}");
    Console.WriteLine($"EXECUTION TIME: {status.ExecutionTime}");
    Console.WriteLine("ERRORS:");
    foreach (var message in status.ErrorMessages)
    {
        Console.WriteLine($"\t{message}");
    }

    Console.WriteLine();
}

/// <summary>
///     Parse command type.
/// </summary>
CommandType ParseCommandType(string command)
{
    if (command.ToLower() == "scan")
    {
        return CommandType.CreateScan;
    }

    if (command.ToLower() == "status")
    {
        return CommandType.GetStatus;
    }

    return CommandType.Invalid;
}

/// <summary>
///     Type of command.
/// </summary>
internal enum CommandType
{
    CreateScan,
    GetStatus,
    Invalid
}