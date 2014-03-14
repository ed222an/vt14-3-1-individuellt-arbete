using System;
using System.Web.UI;
using MemberRegistry.Model;

namespace MemberRegistry.Pages.MemberPages
{
    public partial class Create : System.Web.UI.Page
    {
        // Lägger till en medlem.
        public void MemberFormView_InsertItem(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service service = new Service();
                    service.SaveMember(member);

                    // Sparar ett rättmeddelande i en temporär sessionsvariabel och dirigerar användaren till listan med medlemmar.
                    Page.SetTempData("SuccessMessage", "Skapandet av den nya medlemmen lyckades!");
                    Response.RedirectToRoute("MemberDetails", new { id = member.MedID });
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ett fel inträffade då medlemmen skulle läggas till.");
                }
            }
        }
    }
}