<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CreateMemberActivity.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.CreateMemberActivity" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Ny medlemsaktivitet
    </h1>
    <div>
        <asp:HyperLink ID="HyperLink3" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett fel har inträffat. Korrigera felet och försök igen." />
    <asp:FormView ID="MemberActivityFormView" runat="server"
        ItemType="MemberRegistry.Model.ActivityType"
        DefaultMode="Insert"
        RenderOuterTable="false"
        InsertMethod="MemberActivityFormView_InsertItem">
        <InsertItemTemplate>
            <div>
                <label for="Members">Välj medlem</label>
            </div>
            <div>
                <asp:DropDownList ID="MemberDropDownList" runat="server"
                    ItemType="MemberRegistry.Model.Member"
                    SelectMethod="MemberDropDownList_GetData"
                    DataTextField="Namn"
                    DataValueField="MedID"
                    SelectedValue='<%# BindItem.MedAktID %>'></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="En medlem måste väljas!"
                    ControlToValidate="Members"
                    Display="None"></asp:RequiredFieldValidator>
            </div>
            <div>
                <%-- "Kommandknappar" för att lägga till en ny kontaktuppgift och rensa texfälten --%>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" />
                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Avbryt" NavigateUrl='<%# GetRouteUrl("Members", null) %>' />
            </div>
        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
