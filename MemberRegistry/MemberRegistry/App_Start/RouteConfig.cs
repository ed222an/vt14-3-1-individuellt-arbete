using System.Web.Routing;

namespace MemberRegistry
{
    public class RouteConfig
    {
        // Bestämmer alla router för de olika sidorna.
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Members",          "medlemmar",                "~/Pages/MemberPages/Listing.aspx");
            routes.MapPageRoute("MemberCreate",     "medlemmar/ny",             "~/Pages/MemberPages/Create.aspx");
            routes.MapPageRoute("MemberDetails",    "medlemmar/{id}",           "~/Pages/MemberPages/Details.aspx");
            routes.MapPageRoute("MemberEdit",       "medlemmar/{id}/redigera",  "~/Pages/MemberPages/Edit.aspx");
            routes.MapPageRoute("MemberDelete",     "medlemmar/{id}/tabort",    "~/Pages/MemberPages/Delete.aspx");

            routes.MapPageRoute("Activities",       "aktiviteter",              "~/Pages/MemberPages/Activities.aspx");
            routes.MapPageRoute("ActivityCreate",   "aktiviteter/ny",           "~/Pages/MemberPages/ActivityCreate.aspx");
            routes.MapPageRoute("ActivityDetails",  "aktiviteter/{id}",         "~/Pages/MemberPages/ActivityDetails.aspx");
            routes.MapPageRoute("ActivityEdit",     "aktiviteter/{id}/redigera","~/Pages/MemberPages/ActivityEdit.aspx");
            routes.MapPageRoute("ActivityDelete",   "aktiviteter/{id}/tabort",  "~/Pages/MemberPages/ActivityDelete.aspx");

            routes.MapPageRoute("Error",            "serverfel",                "~/Pages/Shared/ErrorPage.html");

            routes.MapPageRoute("Default",          "",                         "~/Pages/MemberPages/Listing.aspx");
        }
    }
}