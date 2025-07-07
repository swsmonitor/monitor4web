using System;

namespace Models;

    /// <summary>
    /// Represents a ProfileSurfaceDetails.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class ProfileSurfaceDetails 
    {
        public const string TableName = "ProfileSurfaceDetails";
        public required string BeachName { get; set; }

        public required string BeachSurface { get; set; }

        public required DateTime Date { get; set; }

        public  DateTime? Entrydate { get; set; }

        public required int EntryNo { get; set; }

        public required bool G70percent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileSurfaceDetails"/> class.
        /// </summary>
        public ProfileSurfaceDetails()
        {
            // Initialize properties if needed
        }
    }
