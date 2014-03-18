using MemberRegistry.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class DeleteActivity : System.Web.UI.Page
    {
        // Privat fält för service-klass.
        private Service _service;

        // Egenskap som initializerar ett service-objekt ifall det inte redan finns något.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        // Hämtar ut den valda medlemmens id.
        protected int Id
        {
            get { return int.Parse(RouteData.Values["id"].ToString()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Ger avbryt-knappen sin URL så användaren kommer tillbaks till den valda medlemmens detaljerade sida.
            CancelHyperLink.NavigateUrl = GetRouteUrl("Activities", null);
        }

        protected void DeleteLinkButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var id = int.Parse(e.CommandArgument.ToString());
                Service.DeleteMemberActivityById(id);

                // Sparar ett rättmeddelande i en temporär sessionsvariabel och dirigerar användaren till listan med medlemmar.
                Page.SetTempData("SuccessMessage", "Medlemsaktiviteten togs bort.");
                Response.RedirectToRoute("Activities", id);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemsaktiviteten skulle tas bort.");
            }
        }
    }
}