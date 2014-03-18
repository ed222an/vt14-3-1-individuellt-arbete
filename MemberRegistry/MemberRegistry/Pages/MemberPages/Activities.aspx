<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Activities.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.Activities" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Aktiviteter
    </h1>
    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false">
        <asp:Literal runat="server" ID="SuccessMessageLiteral" />
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <div>
        <asp:HyperLink ID="HyperLink3" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
    </div>
    <asp:ListView ID="ActivityListView" runat="server"
        ItemType="MemberRegistry.Model.Activity"
        SelectMethod="ActivityListView_GetData"
        DataKeyNames="AktID">
        <LayoutTemplate>
            <%-- Platshållare för medlemmar --%>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <dl>
                <dt class="header">
                    <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%# GetRouteUrl("EditActivity", new { id = Item.AktID })  %>' Text='<%# Item.Akttyp %> ' />
                </dt>
            </dl>
        </ItemTemplate>
        <EmptyDataTemplate>
            <%-- Detta visas då aktiviteter saknas i databasen. --%>
            <p>
                Aktiviteter saknas.
            </p>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
