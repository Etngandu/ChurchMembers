using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ENB.Church.Members.Entities
{
    public enum Skill_Level
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        [Display(Name = "1er Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        pre_primaire_A = 1,

        [Display(Name = "1er Primaire B")]
        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        pre_primaire_B = 2,

        [Display(Name = "2eme Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        deu_primaire_A = 3
    }
}
