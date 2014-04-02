using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MemberRegistry.Model
{
    public class ActivityType : MemberActivity
    {
        [Required(ErrorMessage = "Ett förnamn måste anges.")]
        [StringLength(20, ErrorMessage = "Förnamnet kan bestå av som mest 20 tecken.")]
        public string Fnamn { get; set; }

        [Required(ErrorMessage = "Ett efternamn måste anges.")]
        [StringLength(20, ErrorMessage = "Efternamnet kan bestå av som mest 20 tecken.")]
        public string Enamn { get; set; }

        [StringLength(25, ErrorMessage = "Aktivitetstypen kan bestå av som mest 25 tecken.")]
        public string Akttyp { get; set; }
    }
}