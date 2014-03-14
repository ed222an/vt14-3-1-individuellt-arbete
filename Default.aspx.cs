using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MemberRegistry.Model;

namespace MemberRegistry
{
    public partial class Default : System.Web.UI.Page
    {
        // Sessionsvariabel för meddelandesträng
        private string SuccessMessage
        {
            get
            {
                string successMessage = Session["SuccessMessage"] as string;
                Session.Remove("SuccessMessage");
                return successMessage;
            }
            set { Session["SuccessMessage"] = value; }
        }

        // Kontrollerar ifall sessionsvariabeln är null.
        public bool ExistingMessage
        {
            get
            {
                return Session["SuccessMessage"] != null;
            }
        }

        // Privat fält för service-klass.
        private Service _service;

        // Egenskap som initializerar ett service-objekt ifall det inte redan finns något.
        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Returnerar ExistingMessage true sätts texten i Succestill variabelns sträng och meddelandet visas.
            if (ExistingMessage)
            {
                SuccessPanel.Visible = true;
                SuccessLabel.Text = SuccessMessage;
            }
        }

        // Visar listan med medlemmar
        public IEnumerable<MemberRegistry.Model.Member> MemberListView_GetData()
        {
            return Service.GetMembers();
        }

        // Lägger till en medlem.
        public void MemberListView_InsertItem(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service.SaveMember(member);
                    SuccessMessage = String.Format("Skapandet av den nya medlemmen lyckades!");
                    Response.Redirect(Request.Path);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då medlemmen skulle läggas till.");
                }
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void MemberListView_UpdateItem(int medId)
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
                    Service.SaveMember(member);
                    SuccessMessage = String.Format("Uppdateringen av medlemmen lyckades!");
                    Response.Redirect(Request.Path);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då medlemmen skulle uppdateras.");
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void MemberListView_DeleteItem(int medId)
        {
            try
            {
                // Kallar på javascript för att visa en confirm-box.
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    Service.DeleteMember(medId);
                    SuccessMessage = String.Format("Borttagandet av medlemmen lyckades!");
                    Response.Redirect(Request.Path);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då medlemmen skulle tas bort.");
            }
        }
    }
}