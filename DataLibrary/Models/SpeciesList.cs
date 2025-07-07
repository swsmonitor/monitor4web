using System;

namespace Models;

    /// <summary>
    /// Represents a SpeciesList.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class SpeciesList 
    {
        public const string TableName = "SpeciesList";
        public  int? AphiaID { get; set; }

        public  DateTime? ChangeDate { get; set; }

        public  string? ChangeReason { get; set; }

        public  string? Class { get; set; }

        public  string? CommonNameOrDescription { get; set; }

        public  int? ComplexityRank { get; set; }

        public  string? Family { get; set; }

        public  string? FormerScientificName { get; set; }

        public  string? Genus { get; set; }

        public required int ID { get; set; }

        public  bool? Invasive { get; set; }

        public  string? Kingdom { get; set; }

        public  bool? NonNative { get; set; }

        public  string? Order { get; set; }

        public  string? Phylum { get; set; }

        public  bool? ProfileData { get; set; }

        public  string? ScientificName { get; set; }

        public  string? Subphylum { get; set; }

        public  string? TaxonCommonName { get; set; }

        public  int? TSN { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeciesList"/> class.
        /// </summary>
        public SpeciesList()
        {
            // Initialize properties if needed
        }
    }
