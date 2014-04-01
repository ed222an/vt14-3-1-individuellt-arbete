using MemberRegistry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class ActivityEdit : System.Web.UI.Page
    {
        // Privat fält för service-klass.
        private Service _service;

        // Egenskap som initializerar ett service-objekt ifall det inte redan finns något.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        // Hämtar den valda medlemsaktiviteten.
        public MemberRegistry.Model.MemberActivity MemberActivityFormView_GetItem([RouteData] int id)
        {
            try
            {
                return Service.GetMemberActivityById(id); // Ändra
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemsaktiviteten skulle hämtas vid uppdatering.");
                return null;
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void MemberActivityFormView_UpdateItem(int medAktId)
        {
            try
            {
                var memberActivity = Service.GetMemberActivityById(medAktId);
                if (memberActivity == null)
                {
                    // Hittade inte medlemsaktiviteten.
                    ModelState.AddModelError(String.Empty,
                        String.Format("Medlemsaktivitet nummer {0} hittades inte.", medAktId));
                    return;
                }

                if (TryUpdateModel(memberActivity))
                {
                    // Sparar ett rättmeddelande i en temporär sessionsvariabel och dirigerar användaren till listan med medlemsaktiviteter.
                    Service.SaveMemberActivity(memberActivity);
                    Response.RedirectToRoute("ActivityDetails", new { id = memberActivity.MedaktID });
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då medlemmen skulle uppdateras.");
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<MemberRegistry.Model.ActivityType> ActivityTypeListView_GetData()
        {
            return null;
        }
    }
}