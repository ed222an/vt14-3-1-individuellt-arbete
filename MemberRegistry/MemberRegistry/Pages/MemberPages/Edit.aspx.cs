using System;
using System.Web.ModelBinding;
using System.Web.UI;
using MemberRegistry.Model;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class Edit : System.Web.UI.Page
    {
        // Privat fält för service-klass.
        private Service _service;

        // Egenskap som initializerar ett service-objekt ifall det inte redan finns något.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        // Hämtar den valda medlemmen.
        public MemberRegistry.Model.Member MemberFormView_GetItem([RouteData] int id)
        {
            try
            {
                return Service.GetMember(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmen skulle hämtas vid uppdatering.");
                return null;
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void MemberFormView_UpdateItem(int medId)
        {
            try
            {
                var member = Service.GetMember(medId);
                if (member == null)
                {
                    // Hittade inte kunden.
                    ModelState.AddModelError(String.Empty,
                        String.Format("Medlemmen nummer {0} hittades inte.", medId));
                    return;
                }

                if (TryUpdateModel(member))
                {
                    // Sparar ett rättmeddelande i en temporär sessionsvariabel och dirigerar användaren till listan med medlemmar.
                    Service.SaveMember(member);
                    Response.RedirectToRoute("MemberDetails", new { id = member.MedID });
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då medlemmen skulle uppdateras.");
            }
        }
    }
}