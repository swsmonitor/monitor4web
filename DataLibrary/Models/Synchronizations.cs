using System;

namespace Models;

    /// <summary>
    /// Represents a Synchronizations.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class Synchronizations 
    {
        public const string TableName = "Synchronizations";
        public required long Id { get; set; }

        public required string Source { get; set; }

        public required string Destination { get; set; }

        public  string? DestinationId { get; set; }

        public required DateTime MinDate { get; set; }

        public required DateTime MaxDate { get; set; }

        public required DateTime SyncDate { get; set; }

        public  ulong? RecordCount { get; set; }

        public required string Status { get; set; }

        public  string? Uploader { get; set; }

        public  string? Organization { get; set; }

        public  string? Subbasin { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Synchronizations"/> class.
        /// </summary>
        public Synchronizations()
        {
            // Initialize properties if needed
        }
    }
