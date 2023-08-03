using System.ComponentModel.DataAnnotations;

namespace ENB.Church.Members.Entities
{
    /// <summary>
    /// Determines the day of a the week.
    /// </summary>
    public enum Activity_Type
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a monday.
        /// </summary>
        Seminaire = 1,

       
        /// <summary>
        /// Indicates tuesday.
        /// </summary>
        Retraite = 2,

        
        /// <summary>
        /// Indicates wednesday.
        /// </summary>
        culte_mercredi = 3,

        /// <summary>
        /// Indicates thursday.
        /// </summary>
        culte_dominical = 4,

        /// <summary>
        /// Indicates friday.
        /// </summary>
        Veille_priere = 5,

            /// <summary>
            /// Indicates friday.
            /// </summary>
        Campagne_evangelisation = 5

    }
}
