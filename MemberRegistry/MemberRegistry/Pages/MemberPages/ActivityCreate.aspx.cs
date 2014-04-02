using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MemberRegistry.Model;
using System.Data;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class CreateMemberActivity : System.Web.UI.Page
    {
        // Privat fält för service-klass.
        private Service _service;

        // Egenskap som initializerar ett service-objekt ifall det inte redan finns något.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        public void MemberActivityFormView_InsertItem(MemberActivity memberActivity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveMemberActivity(memberActivity);

                    // Sparar ett rättmeddelande i en temporär sessionsvariabel och dirigerar användaren till listan med medlemmar.
                    Page.SetTempData("SuccessMessage", "Medlemsaktiviteten lades till!");
                    Response.RedirectToRoute("Members", null);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ett fel inträffade då medlemmen skulle läggas till.");
                }
            }
        }

        // Hämtar medlemmarna och lägger dem i dropdownlistan.
        public IEnumerable<Member> MemberDropDownList_GetData()
        {
            return Service.GetMembers();
        }
        
        // Hämtar aktiviteter och lägger dem i dropdownlistan.
        public IEnumerable<Activity> ActivityDropDownList_GetData()
        {
            return Service.GetActivities();
        }
    }
}