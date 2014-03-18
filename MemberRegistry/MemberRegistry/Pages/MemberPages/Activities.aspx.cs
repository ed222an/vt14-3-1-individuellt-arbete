using System;
using System.Collections.Generic;
using System.Web.UI;
using MemberRegistry.Model;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class Activities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Visar eventuella meddelanden lagrade i de temporära sessionsvariablerna.
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        // Hämtar ut alla aktiviteter.
        public IEnumerable<MemberRegistry.Model.Activity> ActivityListView_GetData()
        {
            try
            {
                Service service = new Service();
                return service.GetActivities();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då aktiviteterna skulle hämtas från databasen.");
                return null;
            }
        }
    }
}