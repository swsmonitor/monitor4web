using System;

namespace Models;

    /// <summary>
    /// Represents a Volunteers.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class Volunteers 
    {
        public const string TableName = "Volunteers";
        public required bool Active { get; set; }

        public  string? Address { get; set; }

        public  string? BWGroup { get; set; }

        public  string? CellPhone { get; set; }

        public  string? City { get; set; }

        public  string? Email { get; set; }

        public  DateTime? Entrydate { get; set; }

        public required string FirstLast { get; set; }

        public  string? FirstName { get; set; }

        public required int ID { get; set; }

        public  string? LastName { get; set; }

        public required bool Lead { get; set; }

        public  string? Phone { get; set; }

        public required bool SpeciesExpert { get; set; }

        public  DateTime? StartDate { get; set; }

        public  string? State { get; set; }

        public  string? VolunteerNotes { get; set; }

        public  double? Zip { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Volunteers"/> class.
        /// </summary>
        public Volunteers()
        {
            // Initialize properties if needed
        }
    }
