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

builder.Services.AddDbContext<PWADbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
//Uncomment when live/making changes
//InitialiseDb(builder.Services);

static void InitialiseDb(IServiceCollection services)
{
    PWADbContext? context = services.BuildServiceProvider().GetService<PWADbContext>();
    context?.Database.Migrate();
    context?.SaveChanges();
}

await builder.Build().RunAsync();
