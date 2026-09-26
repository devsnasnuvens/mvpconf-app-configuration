using Azure.Identity;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

var appConfigEndpoint = builder.Configuration["AppConfig:Endpoint"];

if (!string.IsNullOrWhiteSpace(appConfigEndpoint))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(new Uri(appConfigEndpoint), new DefaultAzureCredential())
            .Select("MVPConf:*")
            .ConfigureRefresh(refreshOptions => refreshOptions
                .RegisterAll()
                .SetRefreshInterval(TimeSpan.FromSeconds(60))
            );
            // Descomentar para habilitar o refresh automático das configurações do Azure App Configuration
            
            // Descomentar para habilitar a gestão de feature flags
            //.UseFeatureFlags();
    });
}

builder.Services.AddControllersWithViews();

// Descomentar para habilitar a gestão de feature flags
// builder.Services.AddFeatureManagement();


// Descomentar para habilitar o refresh automático das configurações do Azure App Configuration
// // // Required to resolve IConfigurationRefresherProvider used by the refresh middleware below.
if (!string.IsNullOrWhiteSpace(appConfigEndpoint))
{
    builder.Services.AddAzureAppConfiguration();
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Descomentar para habilitar o refresh automático das configurações do Azure App Configuration
if (!string.IsNullOrWhiteSpace(appConfigEndpoint))
{
    app.UseAzureAppConfiguration();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();