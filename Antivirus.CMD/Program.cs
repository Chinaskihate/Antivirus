using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Antivirus.Application.Interfaces.ScanManagers;
using Antivirus.Application.Services.ScanManagers;
using Antivirus.WebClient;
using Antivirus.WebClient.Interfaces;
using Antivirus.WebClient.Services;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

//IManagerService service = new ManagerService(new ManagerHttpClientFactory("https://localhost:7000/"));
//var path = Console.ReadLine();
//var result = await service.ScanAsync(path);

//Console.WriteLine(result.ExecutionTime);
BenchmarkRunner.Run<ScanWithFeatureBenchmark>();

public class ScanWithFeatureBenchmark
{
    [Params("D:\\Info\\programming\\ZZZRemoveItThen\\tests",
        @"D:\Info\programming\ZZZRemoveItThen",
        "D:\\Info\\programming\\Interviews\\Kasp2")]
    public string Path { get; set; }

    [Benchmark]
    public async Task Scan()
    {
        IScanManager manager = new ScanManager();
        var id = manager.CreateScan(Path);
        
        while (!IsFinishedScan(id, manager))
        {
            await Task.Delay(25);
        }
    }

    private bool IsFinishedScan(Guid id, IScanManager manager)
    {
        return manager.GetStatus(id).IsCompleted;
    }
}