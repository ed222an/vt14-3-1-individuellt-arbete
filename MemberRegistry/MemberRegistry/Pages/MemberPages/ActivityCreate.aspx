<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ActivityCreate.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.CreateMemberActivity" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Ny medlemsaktivitet
    </h1>
    <div>
        <asp:HyperLink ID="HyperLink3" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett fel har inträffat. Korrigera felet och försök igen." />
    <asp:FormView ID="MemberActivityFormView" runat="server"
        ItemType="MemberRegistry.Model.MemberActivity"
        DefaultMode="Insert"
        RenderOuterTable="false"
        DataKeyNames="MedAktID, MedID, AktID"
        InsertMethod="MemberActivityFormView_InsertItem">
        <InsertItemTemplate>
            <div>
                <label for="Members">Välj medlem & aktivitet</label>
            </div>
            <div>
                <%-- Lista med medlemmar --%>
                <asp:DropDownList ID="MemberDropDownList" runat="server"
                    ItemType="MemberRegistry.Model.Member"
                    SelectMethod="MemberDropDownList_GetData"
                    DataTextField="Fnamn"
                    DataValueField="MedID"
                    SelectedValue='<%# BindItem.MedID %>'></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="En medlem måste väljas!"
                    ControlToValidate="MemberDropDownList"
                    Display="None"></asp:RequiredFieldValidator>

                <%-- Lista med aktiviteter --%>
                <asp:DropDownList ID="ActivityDropDownList" runat="server"
                    ItemType="MemberRegistry.Model.Activity"
                    SelectMethod="ActivityDropDownList_GetData"
                    DataTextField="AktTyp"
                    DataValueField="AktID"
                    SelectedValue='<%# BindItem.AktID %>'></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="En aktivitet måste väljas!"
                    ControlToValidate="ActivityDropDownList"
                    Display="None"></asp:RequiredFieldValidator>

                <%-- Textfält för avgiftstatus --%>
                <div>
                    <label for="FeeStatus">Avgiftstatus</label>
                </div>
                <div>
                    <asp:TextBox ID="FeeStatus" runat="server" Text='<%# BindItem.Avgiftstatus %>' MaxLength="7" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ErrorMessage="Fältet får inte vara tomt!"
                        ControlToValidate="FeeStatus"
                        Display="None"></asp:RequiredFieldValidator>
                </div>
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
