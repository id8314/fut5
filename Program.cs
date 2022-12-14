using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using Microsoft.Extensions.Configuration;

using fut5;
using fut5.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Load api url (static Fut5Config.API_URL) from configured value in wwwroot/appsettings.json
// See appsettings.json https://stackoverflow.com/a/70408216/251674
var settings = new Fut5Config();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);
builder.Services.AddSingleton<Fut5ApiService>();

await builder.Build().RunAsync();
