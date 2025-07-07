using AirtableApiClient;
using DataLibrary.DataSources;
using Models;
using System.Text;
using System.Text.Json;

namespace DataLibrary.Crud;
public static class QuadratDataCrud
{
    // This class is responsible for reading and writing Quadrat records to and from data sources like Airtable.
    // It uses the AirtableHelper to interact with the Airtable API.
    /// <summary>
    /// Reads all quadrat data from Airtable.
    /// </summary>
    /// <param name="config">The configuration for connecting to Airtable.</param>
    /// <returns>A list of QuadratData objects.</returns>
    public static async Task<IEnumerable<QuadratData>> ReadQuadratData(AirTableConfig config)
    {

        List<QuadratData> allQuadrats = new List<QuadratData>();
        List<AirtableRecord> results = await AirtableHelper.ReadTable(config, QuadratData.TableName);
        if (results == null)
        {
            Console.WriteLine("No records retrieved");
            return GetTestQuadratData(); // Return test data if no records found
        }
        try
        {
            // Get records from Airtable asynchronously
            foreach (var record in results)
            {
                StringBuilder recordInfo = new StringBuilder();
                recordInfo.AppendLine($"Record ID: {record.Id} created: {record.CreatedTime}");

                // Deserialize the record manually into my QuadratData object
                var json = JsonSerializer.Serialize(record.Fields);
                var myRecord = JsonSerializer.Deserialize<QuadratData>(json);
                if (myRecord != null)
                {
                    allQuadrats.Add(myRecord);
                    recordInfo.AppendLine($"Name: {myRecord.BeachName}");
                }
                string recordDetails = recordInfo.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading Airtable records: {ex.Message}");
        }

        return allQuadrats.Count > 0 ? allQuadrats : GetTestQuadratData(); // Return test data if no records found
    }
    internal static List<QuadratData> GetTestQuadratData()
    {
        return new List<QuadratData>()
        {
            new QuadratData
            {
                ID = 1,
                BeachName = "Test Beach",
                Date = new DateTime(2023, 10, 1),
                Quadrat = "Q1",
                Species = "Test Species",
                SpeciesLinkId = 1234567890,
                Tide = "High",
            }
        };
    }
}
