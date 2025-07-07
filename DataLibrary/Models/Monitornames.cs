using System;

namespace Models;

    /// <summary>
    /// Represents a Monitornames.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class Monitornames 
    {
        public const string TableName = "Monitornames";
        public  string? BeachName { get; set; }

        public  DateTime? Date { get; set; }

        public  DateTime? Entrydate { get; set; }

        public  string? FirstLast { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Monitornames"/> class.
        /// </summary>
        public Monitornames()
        {
            // Initialize properties if needed
        }
    }
