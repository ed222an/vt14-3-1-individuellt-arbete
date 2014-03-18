<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="DeleteActivity.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.DeleteActivity" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Ta bort medlem
    </h1>
    <div>
        <asp:HyperLink ID="HyperLink3" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
        <asp:HyperLink ID="HyperLink4" CssClass="alinks" runat="server" Text="Aktiviteter" NavigateUrl='<%$ RouteUrl:routename=Activities %>' />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:PlaceHolder runat="server" ID="ConfirmationPlaceHolder">
        <p>
            Är du säker på att du vill ta bort medlemsaktiviteten?
        </p>
    </asp:PlaceHolder>
    <div>
        <asp:LinkButton runat="server" ID="DeleteLinkButton" Text="Ja, ta bort medlemmen"
            OnCommand="DeleteLinkButton_Command" CommandArgument='<%$ RouteValue:id %>' />
        <asp:HyperLink runat="server" ID="CancelHyperLink" Text="Avbryt" />
    </div>
</asp:Content>
