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
    public partial class EditActivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Visar eventuella meddelanden lagrade i de temporära sessionsvariablerna.
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        // Privat fält för service-klass.
        private Service _service;

        // Egenskap som initializerar ett service-objekt ifall det inte redan finns något.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        // Hämtar alla medlemmar som utövar aktiviteten.
        public IEnumerable<MemberRegistry.Model.ActivityType> ActivityTypeListView_GetData([RouteData] int id)
        {
            try
            {
                return Service.GetActivityById(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmarna skulle hämtas.");
                return null;
            }
        }

        // Tar bort vald kontakt.
        public void ActivityTypeListView_DeleteItem(object sender, CommandEventArgs e)
        {
            try
            {
                var id = int.Parse(e.CommandArgument.ToString());
                Service.DeleteMemberActivityById(id);

                // Sparar ett rättmeddelande i en temporär sessionsvariabel och dirigerar användaren till listan med medlemmar.
                Page.SetTempData("SuccessMessage", "Medlemsaktiviteten togs bort.");
                Response.RedirectToRoute("Activities", null);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmen skulle tas bort.");
            }
        }
    }
}