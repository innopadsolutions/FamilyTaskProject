using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Services;

namespace WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services
                .AddBlazorise(options =>
               {
                   options.ChangeTextOnKeyPress = true;
               })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("FamilyTaskAPI", client => client.BaseAddress = new Uri("http://localhost:5000/api/"));
            //builder.Services.AddHttpClient("FamilyTaskAPI", client => client.BaseAddress = new Uri("http://103.106.20.178:5000/api/"));
            builder.Services.AddSingleton<IMemberDataService, MemberDataService>();
            builder.Services.AddSingleton<ITaskDataService, TaskDataService>();
            builder.Services.AddSingleton<IDragAndDropService, DragAndDropService>();

            var host = builder.Build();

            host.Services
            .UseBootstrapProviders()
            .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}