using System;

namespace Models;

    /// <summary>
    /// Represents a BeachEventInfo.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class BeachEventInfo 
    {
        public const string TableName = "BeachEventInfo";
        public  Single? AirTemp { get; set; }

        public  string? BackshoreEnvironment { get; set; }

        public  string? BackshoreVegetation { get; set; }

        public  int? BackshoreVegetationOBSO { get; set; }

        public  Single? BarometricPressure { get; set; }

        public required string BeachName { get; set; }

        public  string? BeachProfileNotes { get; set; }

        public required bool BivalveDig { get; set; }

        public  string? Bluff { get; set; }

        public  string? Bulkhead { get; set; }

        public  string? BulkheadCondition { get; set; }

        public required bool BullKelpBeds { get; set; }

        public  string? CloudCover { get; set; }

        public  double? CorrectedTideHeight { get; set; }

        public required DateTime Date { get; set; }

        public  DateTime? EndTime { get; set; }

        public  DateTime? Entrydate { get; set; }

        public  string? ErosionSinceLast { get; set; }

        public required bool HandHeldAvailable { get; set; }

        public  string? HandHeldDataInput { get; set; }

        public  int? LeadId { get; set; }

        public required bool PhotosTaken { get; set; }

        public  string? Pictures { get; set; }

        public  string? Precipitation { get; set; }

        public required bool RedTide { get; set; }

        public  int? Salinity { get; set; }

        public  string? Seagrasspercent { get; set; }

        public  int? SeagrassBedsAtLowerTideLevels { get; set; }

        public  string? Seaweedpercent { get; set; }

        public  int? SeaweedsALlowerTideLevels { get; set; }

        public  int? Spartina { get; set; }

        public  string? SpeciesExpert { get; set; }

        public required bool SpeciesListGenerated { get; set; }

        public  Single? TideHeightAtEnd { get; set; }

        public  string? Ulvapercent { get; set; }

        public  int? UlvaALowerTideLevels { get; set; }

        public  double? VerticalHeight { get; set; }

        public  double? VerticalHeightOld { get; set; }

        public  Single? WaterTemp { get; set; }

        public  int? Weather { get; set; }

        public  string? Wind { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeachEventInfo"/> class.
        /// </summary>
        public BeachEventInfo()
        {
            // Initialize properties if needed
        }
    }
