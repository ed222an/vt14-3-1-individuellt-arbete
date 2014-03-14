using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MemberRegistry.Model;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class Delete : System.Web.UI.Page
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
            CancelHyperLink.NavigateUrl = GetRouteUrl("MemberDetails", new { id = Id });

            // Är det inte en postback hämtas medlemmens för- & efternamn som sedan presenteras i en sträng på sidan.
            if (!IsPostBack)
            {
                try
                {
                    var member = Service.GetMember(Id);
                    if (member != null)
                    {
                        MemberName.Text = String.Format("{0} {1}", member.Fnamn, member.Enamn);
                        return;
                    }

                    // Hittade inte medlemmen.
                    ModelState.AddModelError(String.Empty,
                        String.Format("Medlem nummer {0} hittades inte.", Id));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmen skulle tas bort.");
                }

                // Döljer bekräftelsen samt deleteknappen.
                ConfirmationPlaceHolder.Visible = false;
                DeleteLinkButton.Visible = false;
            }
        }

        // Tar bort den valda medlemmen.
        protected void DeleteLinkButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var id = int.Parse(e.CommandArgument.ToString());
                Service.DeleteMember(id);

                // Sparar ett rättmeddelande i en temporär sessionsvariabel och dirigerar användaren till listan med medlemmar.
                Page.SetTempData("SuccessMessage", "Medlemmen togs bort.");
                Response.RedirectToRoute("Members", null);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmen skulle tas bort.");
            }
        }
    }
}