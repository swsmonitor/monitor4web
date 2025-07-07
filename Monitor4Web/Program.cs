using DataLibrary.DataSources;
using DataLibrary.ModelExtensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Models;
using SharedComponents.SharedData;
using Syncfusion.Blazor;
using System.Reflection;
using System.Text.Json;
using Monitor4Web;

internal class Program
{
    public static AppSettings AppSettings { get; set; } = new AppSettings(); // Holds application settings
    public static AirTableConfig CurrentAirtableConfig = null;
    public static bool UseAirtable = true; // Default to true, can be overridden by appsettings.json
    public static bool UseJson = false; // Default to false, can be overridden by appsettings.json
    private static async Task Main(string[] args)
    {
        // string syncfusionLicenseKey = Environment.GetEnvironmentVariable("SyncfusionLicenseKey") ?? string.Empty;
        string syncfusionLicense = string.Empty;

        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        try
        {
            string jsonSettings = GetJsonResource("appsettings.json");
            AppSettings = JsonSerializer.Deserialize<AppSettings>(jsonSettings) ?? new AppSettings();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading appsettings.json: {ex.Message}");
            throw;
        }

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        if ((string.IsNullOrEmpty(AppSettings.ApiKey) || string.IsNullOrEmpty(AppSettings.BaseId)) &&
            AppSettings.isTrue(AppSettings.UseAirtable))
        {
            throw new InvalidOperationException("ApiKey and BaseId must be configured in appsettings.json for Airtable access.");
        }
        else
        {
            CurrentAirtableConfig = new AirTableConfig
            {
                ApiKey = AppSettings.ApiKey ?? string.Empty,
                BaseId = AppSettings.BaseId ?? string.Empty
            };
        }

        StaticData.DataSourceConfig = (object)CurrentAirtableConfig;
        StaticData.UseAirtable = AppSettings.isTrue(AppSettings.UseAirtable); // Set the static data config
        StaticData.UseJson = AppSettings.isTrue(AppSettings.UseJson); // Only use Json if not using Airtable; desktop uses mysql
        if (StaticData.UseJson)
        {
            string jsonResource = "beachdata.json";
            var jsonContent = GetJsonResource(jsonResource);
            try
            {
                var beaches = JsonSerializer.Deserialize<IEnumerable<BeachData>>(jsonContent);
                StaticData.Beaches = beaches;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON data: {ex.Message}");
                throw;
            }
        }
        else
        {
            StaticData.DataSourceConfig = CurrentAirtableConfig;
            await StaticData.PreLoadBeachesAsync(); // Initialize Beaches
        }

        builder.Services.AddSyncfusionBlazor();

        var host = builder.Build();
        var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();

        var module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/config.js");
        AppSettings.SyncfusionLicense = await module.InvokeAsync<string>("get_key", "syncFusionLicense");
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppSettings.SyncfusionLicense);

        await host.RunAsync();
    }

    private static string GetJsonResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        if (assembly == null)
        {
            throw new InvalidOperationException("Assembly not found.");
        }
        IEnumerable<string> resourceNames = assembly.GetManifestResourceNames();
        if (resourceNames is null || !resourceNames.Any())
        {
            throw new InvalidOperationException("No resources found in assembly.");
        }
        string fullResourceName = resourceNames.FirstOrDefault(name => name.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));
        if (fullResourceName == null)
        {
            throw new InvalidOperationException($"Resource '{resourceName}' not found in assembly.");
        }
        using var stream = assembly.GetManifestResourceStream(fullResourceName);
        if (stream == null)
        {
            throw new InvalidOperationException($"Stream for resource '{resourceName}' is null.");
        }
        using var reader = new StreamReader(stream);
        if (reader == null)
        {
            throw new InvalidOperationException("StreamReader is null.");
        }
        return reader.ReadToEnd();
    }
}
