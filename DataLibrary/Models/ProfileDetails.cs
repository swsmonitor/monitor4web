using System;

namespace Models;

    /// <summary>
    /// Represents a ProfileDetails.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class ProfileDetails 
    {
        public const string TableName = "ProfileDetails";
        public  string? BeachName { get; set; }

        public  DateTime? Date { get; set; }

        public  DateTime? Entrydate { get; set; }

        public  int? EntryNo { get; set; }

        public  string? Notes { get; set; }

        public  string? Species { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileDetails"/> class.
        /// </summary>
        public ProfileDetails()
        {
            // Initialize properties if needed
        }
    }
