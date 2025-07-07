using AirtableApiClient;

namespace DataLibrary.DataSources;


public class AirTableConfig
{
    public string ApiKey { get; set; } = string.Empty;
    public string BaseId { get; set; } = string.Empty;
}

// Interface with Airtable via their API using the AirtableApiClient library
// This cannot be used with Blazor apps as it uses async methods and the library is not compatible with Blazor WebAssembly
// It is designed to be used in console, MAUI or server-side applications
public static class AirtableHelper
{
    public static DateTime LastApiRequest { get; set; } = DateTime.MinValue;
    public static int ApiRequestCount { get; set; } = 0;

    public static async Task<List<AirtableRecord>> ReadTable(object connectionInfo, string tableName, string? recordId = null)
    {
        if (connectionInfo is null || connectionInfo is not AirTableConfig config)
        {
            throw new ArgumentException("Invalid connection info provided. Expected AirTableConfig.", nameof(connectionInfo));
        }

        if (LastApiRequest == DateTime.MinValue)
        {
            LastApiRequest = DateTime.Now.AddSeconds(-1.0); // Initialize the last request time
        }
        try
        {
            AirTableConfig airtableConfig = connectionInfo as AirTableConfig;

            // Airtable only returns a maximum of 100 records per request, so you may need to handle pagination
            // Also throttling is required for large datasets
            string offset = string.Empty;
            var allRecords = new List<AirtableRecord>();

            using (AirtableBase airtableBase = new AirtableBase(airtableConfig?.ApiKey, airtableConfig?.BaseId))
            {
                do
                {
                    try
                    {
                        Task<AirtableListRecordsResponse> records = airtableBase.ListRecords(tableName, offset);
                        AirtableListRecordsResponse response = await records;
                        if (response.Success)
                        {
                            allRecords.AddRange(response.Records);
                            offset = response.Offset;
                            // Test for throttling
                            ApiRequestCount++;

                            /*
                            To detect if your Airtable API usage is being throttled, look for a 429 error code in your API responses.
                            This indicates that you've exceeded the rate limit, which is 5 requests per second per base and 50 requests per second for personal access tokens.
                            If you receive this error, you'll need to implement a retry mechanism with exponential backoff to avoid further throttling.
                            From Airtable documentation: If you exceed this rate, you will receive a 429 status code and must wait 30 seconds before subsequent requests will succeed. 
                            API integrations should pause and wait before retrying the API request
                            */

                            if (DateTime.Now.Subtract(LastApiRequest).TotalSeconds < 1 && ApiRequestCount > 5)
                            {
                                // If the last request was less than a second ago, and we have made too many requests then wait for a second
                                await Task.Delay(1000);
                                ApiRequestCount = 0; // Reset the request count
                                LastApiRequest = DateTime.Now.AddSeconds(-1.0); // Update the last request time
                            }

                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is AirtableTooManyRequestsException)
                        {
                            // Handle throttling error
                            Console.WriteLine("Throttling detected. Waiting for 30 seconds before retrying...");
                            await Task.Delay(30500); // wait 30.5 seconds
                            continue; // Retry the request
                        }
                        else
                        {
                            Console.WriteLine($"Error retrieving records: {ex.Message}");
                            return new List<AirtableRecord>(); // Return empty list on error
                        }
                    }
                } while (!string.IsNullOrEmpty(offset));
                return allRecords;
            }
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }
        return new List<AirtableRecord>();
    }
    //var query = new AirtableListRecordsOptions
    //{
    //    FilterByFormula = "AND({Status}='Active', {Age}>30)",
    //    Sort = new List<Sort> { new Sort("Name", SortDirection.Desc) },
    //    MaxRecords = 100
    //};

    // Task<AirtableListRecordsResponse> task = airtableBase.ListRecords("YOUR_TABLE_NAME", query);

    //// Example: Create a new record
    //public async Task<AirtableCreateUpdateReplaceRecordResponse> CreateRecordAsync(Dictionary<string, object> newData)
    //{
    //    var fields = new Fields();
    //    foreach (var key in newData.Keys)
    //    {
    //        fields.AddField(key, newData[key]);
    //    }

    //    var airtableClient = new AirtableBase(apiKey, baseId);
    //    var createTask = airtableClient.CreateRecord("YOUR_TABLE_NAME", fields);
    //    AirtableCreateUpdateReplaceRecordResponse createResponse = await createTask;
    //    return createResponse;
    //}

}

