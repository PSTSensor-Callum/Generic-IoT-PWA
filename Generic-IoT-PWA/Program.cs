using Blazor.Bluetooth;
using BlazorStrap;
using Generic_IoT_PWA;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Configuration;
using System.Data.SqlClient;
using Generic_IoT_PWA.Data;
using Microsoft.EntityFrameworkCore;
using Generic_IoT_PWA.Settings;
using Generic_IoT_PWA.Services.Database;
using Generic_IoT_PWA.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

IConfiguration configuration = builder.Configuration;
builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddBlazorStrap();

builder.Services.AddBluetoothNavigator();


// Adding Pagination Services
builder.Services.Configure<PaginationSettings>(configuration.GetSection(nameof(PaginationSettings)));
builder.Services.AddSingleton<IPaginationSettings>(sp => sp.GetRequiredService<IOptions<PaginationSettings>>().Value);

//Adding MongoDb Database
builder.Services.AddSingleton<IDataServiceSettings>(x => x.GetRequiredService<IOptions<DataServiceSettings>>().Value);
builder.Services.AddSingleton<IDataService, DataService>();

await builder.Build().RunAsync();
