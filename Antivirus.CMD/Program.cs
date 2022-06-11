using System.Net.Http.Headers;
using Antivirus.WebClient;
using Antivirus.WebClient.Interfaces;
using Antivirus.WebClient.Services;

IManagerService service = new ManagerService(new ManagerHttpClientFactory("https://localhost:7000/"));
var path = Console.ReadLine();
var result = await service.ScanAsync(path);

Console.WriteLine(result.ExecutionTime);