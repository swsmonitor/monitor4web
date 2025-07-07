using System;

namespace Models;

    /// <summary>
    /// Represents a TaxonCommonNames.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class TaxonCommonNames 
    {
        public const string TableName = "TaxonCommonNames";
        public  string? Comments { get; set; }

        public  string? Simplified { get; set; }

        public  string? TaxonCommonName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonCommonNames"/> class.
        /// </summary>
        public TaxonCommonNames()
        {
            // Initialize properties if needed
        }
    }
