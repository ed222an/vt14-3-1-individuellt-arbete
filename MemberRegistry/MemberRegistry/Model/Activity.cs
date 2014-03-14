using System.ComponentModel.DataAnnotations;

namespace MemberRegistry.Model
{
    public class Activity
    {
        public int AktID { get; set; }

        [Required(ErrorMessage = "En aktivitetstyp måste anges.")]
        [StringLength(25, ErrorMessage = "Aktivitetstypen kan bestå av som mest 25 tecken.")]
        public string Akttyp { get; set; }
    }
}