using System.ComponentModel.DataAnnotations;

namespace MemberRegistry.Model
{
    public class MemberActivity
    {
        public int MedAktID { get; set; }
        public int AktID { get; set; }
        public int MedID { get; set; }
    }
}