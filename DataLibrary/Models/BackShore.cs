using System;

namespace Models;

    /// <summary>
    /// Represents a BackShore.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class BackShore 
    {
        public const string TableName = "BackShore";
        public required string Backshorecontents { get; set; }

        public required string Beachname { get; set; }

        public required DateTime Date { get; set; }

        public  DateTime? Entrydate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackShore"/> class.
        /// </summary>
        public BackShore()
        {
            // Initialize properties if needed
        }
    }
