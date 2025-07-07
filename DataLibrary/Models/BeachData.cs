using System;

namespace Models;

    /// <summary>
    /// Represents a BeachData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class BeachData 
    {
        public const string TableName = "BeachData";
        public  string? AdditionalNotes { get; set; }

        public  string? BeachDirections { get; set; }

        public required string BeachName { get; set; }

        public bool? Bulkhead { get; set; }

        public  string? BulkHeadConstruction { get; set; }

        public  int? County { get; set; }

        public  bool? CurrentlyMonitored { get; set; }

        public  int? DnrClass { get; set; }

        public  DateTime? Entrydate { get; set; }

        public required int ID { get; set; }

        public  string? Island { get; set; }

        public  string? Latitude { get; set; }

        public  string? Longitude { get; set; }

        public  string? ProfileDirections { get; set; }

        public  decimal? ProfileLineStart { get; set; }

        public  int? SurveyWidth { get; set; }

        public  string? TideChart { get; set; }

        public  string? VertRef { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeachData"/> class.
        /// </summary>
        public BeachData()
        {
            // Initialize properties if needed
        }
    }
