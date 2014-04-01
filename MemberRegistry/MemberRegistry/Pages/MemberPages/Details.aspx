<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.Details" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Medlem
    </h1>
    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false" CssClass="icon-ok">
        <asp:Literal runat="server" ID="SuccessMessageLiteral" />
    </asp:Panel>
    <div>
        <asp:HyperLink ID="HyperLink3" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:FormView ID="MemberFormView" runat="server"
        ItemType="MemberRegistry.Model.Member"
        SelectMethod="MemberFormView_GetItem"
        RenderOuterTable="false">
        <ItemTemplate>
            <div class="header">
                <label for="Name">Namn</label>
            </div>
            <div class="content">
                <%#: Item.Fnamn + " " + Item.Enamn %>
            </div>
            <div class="header">
                <label for="CivicRegistrationNumber">Personnummer</label>
            </div>
            <div class="content">
                <%#: Item.PersNR %>
            </div>
            <div class="header">
                <label for="Address">Adress</label>
            </div>
            <div class="content">
                <%#: Item.Address %>
            </div>
            <div class="header">
                <label for="ZipCode">Postnummer</label>
            </div>
            <div class="content">
                <%#: Item.PostNR %>
            </div>
            <div class="header">
                <label for="Region">Ort</label>
            </div>
            <div class="content">
                <%#: Item.Ort %>
            </div>
            <div>
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("MemberEdit", new { id = Item.MedID }) %>' />
                <asp:HyperLink ID="HyperLink2" runat="server" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("MemberDelete", new { id = Item.MedID }) %>' />
                <asp:HyperLink ID="HyperLink3" runat="server" Text="Hem" NavigateUrl='<%# GetRouteUrl("Members", null)%>' />
            </div>
        </ItemTemplate>
    </asp:FormView>
    <asp:ListView ID="MemberActivityListView" runat="server"
        ItemType="MemberRegistry.Model.ActivityType"
        SelectMethod="MemberActivityListView_GetData"
        DataKeyNames="MedID">
        <LayoutTemplate>
            <%-- Platshållare för medlemsaktiviteter --%>
            <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <dl>
                <dt class="header">
                    <label for="Aktivitet">Aktivitet</label>
                </dt>
                <dd class="content">
                    <%#: Item.Akttyp %>
                </dd>
                <dd class="content">
                    - Avgiftstatus: <%#: Item.Avgiftstatus %>
                </dd>
            </dl>
        </ItemTemplate>
        <EmptyDataTemplate>
            <%-- Detta visas då aktiviteter saknas i databasen. --%>
            <p>
                Medlemsaktivitet saknas.
            </p>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
