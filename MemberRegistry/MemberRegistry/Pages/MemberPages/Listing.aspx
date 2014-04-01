<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.Listing" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Medlemmar
    </h1>
    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false">
        <asp:Literal runat="server" ID="SuccessMessageLiteral" />
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div>
        <asp:HyperLink ID="HyperLink3" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
        <asp:HyperLink ID="HyperLink4" CssClass="alinks" runat="server" Text="Aktiviteter" NavigateUrl='<%$ RouteUrl:routename=Activities %>' />
        <asp:HyperLink ID="HyperLink1" CssClass="alinks" runat="server" NavigateUrl='<%$ RouteUrl:routename=MemberCreate %>' Text="Lägg till ny medlem" />
        <asp:HyperLink ID="HyperLink5" CssClass="alinks" runat="server" NavigateUrl='<%$ RouteUrl:routename=ActivityCreate %>' Text="Lägg till medlem i aktivitetsgrupp" />
    </div>
    <asp:ListView ID="MemberListView" runat="server"
        ItemType="MemberRegistry.Model.Member"
        SelectMethod="MemberListView_GetData"
        DataKeyNames="MedID">
        <LayoutTemplate>
            <%-- Platshållare för medlemmar --%>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <dl>
                <dt class="header">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# GetRouteUrl("MemberDetails", new { id = Item.MedID })  %>' Text='<%# Item.Fnamn + " " + Item.Enamn %> ' /></dt>
                <dd class="content">
                    <%#: Item.Address %>
                </dd>
                <dd class="content">
                    <%#: Item.PostNR %> <%#: Item.Ort %>
                </dd>
            </dl>
        </ItemTemplate>
        <EmptyDataTemplate>
            <%-- Detta visas då medlemmar saknas i databasen. --%>
            <p>
                Medlemmar saknas.
            </p>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
