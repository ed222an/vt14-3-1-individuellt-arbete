using System;
using System.ComponentModel.DataAnnotations;

namespace MemberRegistry.Model
{
    public class MemberActivity
    {
        public int MedAktID { get; set; }
        public int AktID { get; set; }
        public int MedID { get; set; }

        [Required(ErrorMessage = "En avgiftsstatus måste anges.")]
        [StringLength(7, ErrorMessage = "Avgiftsstatusen kan bestå av som mest 7 tecken.")]
        public string Avgiftstatus { get; set; }
    }
}