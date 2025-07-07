using AirtableApiClient;
using DataLibrary.DataSources;
using Models;
using System.Text.Json;

namespace DataLibrary.Crud;
public static class BeachDataCrud
{
    // This class is responsible for reading and writing BeachData records to and from data sources like Airtable.
    // It uses the AirtableHelper to interact with the Airtable API.
    // Or the MySqlHelper class if accessing MySql
    /// <summary>
    /// Reads all beach data from Airtable.
    /// </summary>
    /// <param name="config">The configuration for connecting to Airtable.</param>
    /// <returns>A list of BeachData objects.</returns>
    public static async Task<IEnumerable<BeachData>> ReadBeachData(object config)
    {
        if (config is AirTableConfig)
        {
            List<AirtableRecord> results = await AirtableHelper.ReadTable((object)config, BeachData.TableName);
            return DeserializeRecords(results);
        }
        else if (config is MySqlConfig mySqlConfig)
        {
            List<SqlRecord> results = await MySqlHelper.ReadTable(config, BeachData.TableName);
            return DeserializeRecords(results);
        }
        else throw new ArgumentException("Invalid configuration type provided. Expected AirTableConfig or MySqlConfig.", nameof(config));
        return GetTestBeachData(); // Return test data if no records found
    }

    private static List<BeachData> DeserializeRecords(List<AirtableRecord> results)
    {
        return DeserializeRecords(results.Select(r => new SqlRecord { Fields = r.Fields }));
    }

    public static void LoadClass<T>(SqlRecord? record, out object resultClass)
    {
        var json = JsonSerializer.Serialize(record.Fields);
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        resultClass = (object)(JsonSerializer.Deserialize<T>(json, jsonOptions) ?? throw new ArgumentException($"Failed to deserialize JSON into {typeof(T).Name}"));
    }

    private static List<BeachData> DeserializeRecords(IEnumerable<SqlRecord> results)
    {
        List<BeachData> allBeaches = new List<BeachData>();
        if (results == null)
        {
            Console.WriteLine("No records retrieved");
            return GetTestBeachData(); // Return test data if no records found
        }
        try
        {
            // Get records from Airtable asynchronously
            foreach (var record in results)
            {
                // Deserialize the record manually into my BeachData object
                LoadClass<BeachData>(record, out object? myRecord);
                if (myRecord != null)
                {
                    allBeaches.Add((BeachData)myRecord);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading records: {ex.Message}");
        }
        return allBeaches.Count > 0 ? allBeaches : GetTestBeachData(); // Return test data if no records found
    }

    public static List<BeachData> GetTestBeachData()
    {
        return new List<BeachData>()
        {
            new BeachData
            {
                ID = 1,
                BeachName = "Test Beach",
                Bulkhead = true,
                Latitude = "47",
                Longitude = "-122.3321",
                AdditionalNotes = "Sample note",
                County = 1,
                Island = "Test Island",
                CurrentlyMonitored = true,
                DnrClass = 2,
                SurveyWidth = 100
            }
        };
    }
}
