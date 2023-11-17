using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using AzureFunction.ChatBot;

public class Program
{
    public static void Main()
    {
        IHost host = new HostBuilder()
             .ConfigureFunctionsWorkerDefaults()
             .ConfigureServices(services =>
                                {
                                    services.AddSingleton<IConfigProvider, ConfigProvider>();
                                })
             .Build();
        host.Run();
    }
}