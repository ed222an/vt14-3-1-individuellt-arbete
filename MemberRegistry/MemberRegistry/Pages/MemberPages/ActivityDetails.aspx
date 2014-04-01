<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ActivityDetails.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.EditActivity" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Redigera aktivitet
    </h1>
    <asp:Panel runat="server" ID="SuccessMessagePanel" Visible="false">
        <asp:Literal runat="server" ID="SuccessMessageLiteral" />
    </asp:Panel>
    <div>
        <asp:HyperLink ID="HyperLink1" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
        <asp:HyperLink ID="HyperLink4" CssClass="alinks" runat="server" Text="Aktiviteter" NavigateUrl='<%$ RouteUrl:routename=Activities %>' />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:ListView ID="ActivityTypeListView" runat="server"
        ItemType="MemberRegistry.Model.ActivityType"
        SelectMethod="ActivityTypeListView_GetData"
        DataKeyNames="MedAktID">
        <LayoutTemplate>
            <%-- Platshållare för medlemmar --%>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <dl>
                <dt class="content">
                    <asp:Label ID="Label1" runat="server" Text='<%# Item.Fnamn + " " + Item.Enamn %> '></asp:Label>
                </dt>
                <dt class="content">
                    - Avgiftstatus: <%#: Item.Avgiftstatus %>
                </dt>
                <dt class="command">
                    <%-- "Kommandknapp" för att redigera medlemsaktivitet --%>
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("ActivityEdit", new { id = Item.MedAktID }) %>' />
                    <%-- "Kommandknapp" för att ta bort medlem från aktiviteten --%>
                    <asp:HyperLink ID="HyperLink2" runat="server" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("ActivityDelete", new { id = Item.MedAktID }) %>' />
                </dt>
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
