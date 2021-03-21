using BlazorApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Program
    {
        private const string API_BASE_URL = "http://localhost:38275/";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            //builder.HostEnvironment.BaseAddress
            //builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://localhost:38275") });
            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri(API_BASE_URL));
            builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = new Uri(API_BASE_URL));
            builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = new Uri(API_BASE_URL));
            await builder.Build().RunAsync();
        }
    }
}
