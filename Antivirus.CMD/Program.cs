using System.Net.Http.Headers;
using Antivirus.WebClient;
using Antivirus.WebClient.Interfaces;
using Antivirus.WebClient.Services;

IManagerService service = new ManagerService(new ManagerHttpClientFactory("https://localhost:7000/api/ScannerManager"));
var path = Console.ReadLine();
var result = await service.CreateScanAsync(path);
Console.WriteLine(result);

Thread.Sleep(3000);
var status = await service.GetStatusAsync(result);
Console.WriteLine(status.ExecutionTime);