// TODO: remove ref to appl.

using Antivirus.Application.Interfaces;
using Antivirus.Application.Services;

IScanService service = new ScanService();
var path = Console.ReadLine();
var result = await service.ScanAsync(path);
Console.WriteLine("errors: " + result.TotalErrors);
Console.WriteLine("js: " + result.TotalEvilJsDetects);
Console.WriteLine("files: " + result.TotalProcessedFiles);
Console.WriteLine("remove: " + result.TotalRemoveDetects);
Console.WriteLine("run dll: " + result.TotalRunDllDetects);
Console.WriteLine(result.ExecutionTime);