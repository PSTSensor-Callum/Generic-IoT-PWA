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

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorStrap();

builder.Services.AddBluetoothNavigator();

builder.Services.AddDbContext<PWADbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

await builder.Build().RunAsync();
