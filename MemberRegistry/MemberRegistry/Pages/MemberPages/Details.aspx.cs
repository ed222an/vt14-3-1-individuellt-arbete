using System;
using System.Web.ModelBinding;
using System.Web.UI;
using MemberRegistry.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Visar eventuella meddelanden lagrade i de temporära sessionsvariablerna.
            SuccessMessageLiteral.Text = Page.GetTempData("SuccessMessage") as string;
            SuccessMessagePanel.Visible = !String.IsNullOrWhiteSpace(SuccessMessageLiteral.Text);
        }

        // Hämtar den valda medlemmen ur databasen.
        public MemberRegistry.Model.Member MemberFormView_GetItem([RouteData] int id)
        {
            try
            {
                Service service = new Service();
                return service.GetMember(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmen hämtades.");
                return null;
            }
        }

        // Hämtar ut medlemsaktiviteterna
        public IEnumerable<MemberRegistry.Model.ActivityType> MemberActivityListView_GetData([RouteData] int id)
        {
            try
            {
                Service service = new Service();
                return service.GetMemberActivities(id);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade då medlemmarna skulle hämtas från databasen.");
                return null;
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void MemberActivityListView_UpdateItem([RouteData] int id, int activityId)
        {
            Service service = new Service();

            service.AddMemberActivityById(id ,activityId);
        }
    }
}