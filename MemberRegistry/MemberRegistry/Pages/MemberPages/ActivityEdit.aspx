<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ActivityEdit.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.ActivityEdit" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Redigera aktivitet
    </h1>
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
                    <%# Item.Fnamn + " " + Item.Enamn %>
                </dt>
                <dt class="content">
                    - Avgiftstatus: <%#: Item.Avgiftstatus %>
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
    <asp:FormView ID="MemberActivityFormView" runat="server"
        ItemType="MemberRegistry.Model.ActivityType"
        DataKeyNames="MedAktID"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="MemberActivityFormView_GetItem"
        UpdateMethod="MemberActivityFormView_UpdateItem">
        <EditItemTemplate>
            <div>
                <label for="FeeStatus">Redigera avgiftstatus</label>
            </div>
            <div>
                <asp:TextBox ID="FeeStatus" runat="server" Text='<%# BindItem.Avgiftstatus %>' MaxLength="7" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="Fältet får inte vara tomt!"
                    ControlToValidate="FeeStatus"
                    Display="None"></asp:RequiredFieldValidator>
            </div>
            <div>
                <%-- "Kommandknappar" för att lägga till en ny kontaktuppgift och rensa texfälten --%>
                <asp:LinkButton ID="LinkButton" runat="server" CommandName="Update" Text="Spara" />
                <asp:HyperLink ID="HyperLink" runat="server" Text="Avbryt" NavigateUrl='<%# GetRouteUrl("ActivityDetails", new { id = Item.AktID }) %>' />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
