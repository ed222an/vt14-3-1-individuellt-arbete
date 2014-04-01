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

        [Required(ErrorMessage = "Ett startdatum måste anges.")]
        [StringLength(10, ErrorMessage = "Startdatumet kan bestå av som mest 10 tecken.")]
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Startdatumet måste vara i formatet ÅÅÅÅ-MM-DD.")]
        public DateTime Startdatum { get; set; }

        [Required(ErrorMessage = "Ett slutdatum måste anges.")]
        [StringLength(10, ErrorMessage = "Slutdatumet kan bestå av som mest 10 tecken.")]
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Slutdatumet måste vara i formatet ÅÅÅÅ-MM-DD.")]
        public DateTime Slutdatum { get; set; }
    }
}