using System.ComponentModel.DataAnnotations;

namespace MemberRegistry.Model
{
    // En publik klass som innehåller samma parametrar som medlemmarna i databasen.
    public class Member
    {
        public int MedID { get; set; }

        [Required(ErrorMessage = "Ett förnamn måste anges.")]
        [StringLength(20, ErrorMessage = "Förnamnet kan bestå av som mest 20 tecken.")]
        public string Fnamn { get; set; }

        [Required(ErrorMessage = "Ett efternamn måste anges.")]
        [StringLength(20, ErrorMessage = "Efternamnet kan bestå av som mest 20 tecken.")]
        public string Enamn { get; set; }

        [Required(ErrorMessage = "Ett personnummer måste anges.")]
        [StringLength(11, ErrorMessage = "Förnamnet måste innehålla 11 tecken enligt formatet ÅÅMMDD-XXXX.")]
        [RegularExpression(@"^\d{6}-\d{4}$", ErrorMessage = "Personnummret måste vara i formatet ÅÅMMDD-XXXX.")]
        public string PersNR { get; set; }

        public int BefID { get; set; }

        [Required(ErrorMessage = "En address måste anges.")]
        [StringLength(30, ErrorMessage = "Addressen kan bestå av som mest 30 tecken.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ett postnummer måste anges.")]
        [StringLength(6, ErrorMessage = "Postnummret kan bestå av som mest 6 tecken.")]
        public string PostNR { get; set; }

        [Required(ErrorMessage = "En ort måste anges.")]
        [StringLength(25, ErrorMessage = "Orten kan bestå av som mest 25 tecken.")]
        public string Ort { get; set; }
    }
}