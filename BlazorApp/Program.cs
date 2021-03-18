using BlazorApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            //builder.HostEnvironment.BaseAddress
            //builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://localhost:38275") });
            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("http://localhost:38275/"));
            await builder.Build().RunAsync();
        }
    }
}
