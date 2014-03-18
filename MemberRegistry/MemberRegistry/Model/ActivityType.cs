using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberRegistry.Model
{
    public class ActivityType : MemberActivity
    {
        public string Fnamn { get; set; }
        public string Enamn { get; set; }
        public string Akttyp { get; set; }
    }
}