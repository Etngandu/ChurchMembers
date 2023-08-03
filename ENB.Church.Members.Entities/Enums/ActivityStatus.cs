using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Church.Members.Entities
{
    /// <summary>
    /// Determines the Status of an LawyerEvent record.
    /// </summary>

    public enum ActivityStatus
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates Activity Planned.
        /// </summary>
        [Display(Name = "Planned")]
        orange = 1,

        /// <summary>
        /// Indicates Activity Canceld.
        /// </summary>
        [Display(Name = "Cancelled")]
        yellow = 2,

        /// <summary>
        /// Indicates Activity Completed.
        /// </summary>
        [Display(Name = "Completed")]
        green = 3,
    }
}
