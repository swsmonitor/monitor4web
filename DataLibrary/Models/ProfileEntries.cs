using System;

namespace Models;

    /// <summary>
    /// Represents a ProfileEntries.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class ProfileEntries 
    {
        public const string TableName = "ProfileEntries";
        public  string? BeachName { get; set; }

        public  DateTime? Date { get; set; }

        public  DateTime? Entrydate { get; set; }

        public  int? EntryNo { get; set; }

        public  int? Length { get; set; }

        public  double? SurveyReading { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileEntries"/> class.
        /// </summary>
        public ProfileEntries()
        {
            // Initialize properties if needed
        }
    }
