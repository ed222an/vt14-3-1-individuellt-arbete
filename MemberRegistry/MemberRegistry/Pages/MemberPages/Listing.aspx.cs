using System;
using System.Collections.Generic;
using System.Web.UI;
using MemberRegistry.Model;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class Listing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Visar eventuella meddelanden lagrade i de temporära sessionsvariablerna.
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        // Hämtar alla kunde i databasen.
        public IEnumerable<MemberRegistry.Model.Member> MemberListView_GetData()
        {
            try
            {
                Service service = new Service();
                return service.GetMembers();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmarna skulle hämtas från databasen.");
                return null;
            }
        }
    }
}