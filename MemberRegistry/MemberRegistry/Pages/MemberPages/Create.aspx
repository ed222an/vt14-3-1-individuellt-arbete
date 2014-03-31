<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="MemberRegistry.Pages.MemberPages.Create" %>

<asp:Content ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <h1>
        Ny medlem
    </h1>
    <div>
        <asp:HyperLink ID="HyperLink3" CssClass="alinks" runat="server" Text="Hem" NavigateUrl='<%$ RouteUrl:routename=Members %>' />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett fel har inträffat. Korrigera felet och försök igen." />
    <asp:FormView ID="MemberFormView" runat="server"
        ItemType="MemberRegistry.Model.Member"
        DefaultMode="Insert"
        RenderOuterTable="false"
        InsertMethod="MemberFormView_InsertItem">
        <InsertItemTemplate>
            <div>
                <label for="FirstName">Förnamn</label>
            </div>
            <div>
                <asp:TextBox ID="FirstName" runat="server" Text='<%# BindItem.Fnamn %>' MaxLength="20" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="Fältet får inte vara tomt!"
                    ControlToValidate="FirstName"
                    Display="None"></asp:RequiredFieldValidator>
            </div>
            <div>
                <label for="LastName">Efternamn</label>
            </div>
            <div>
                <asp:TextBox ID="LastName" runat="server" Text='<%# BindItem.Enamn %>' MaxLength="20" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ErrorMessage="Fältet får inte vara tomt!"
                    ControlToValidate="LastName"
                    Display="None"></asp:RequiredFieldValidator>
            </div>                
            <div>
                <label for="CivicRegistrationNumber">Personnummer</label>
            </div>
            <div>
                <asp:TextBox ID="CivicRegistrationNumber" runat="server" Text='<%# BindItem.PersNR %>' MaxLength="11" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ErrorMessage="Fältet får inte vara tomt!"
                    ControlToValidate="CivicRegistrationNumber"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ErrorMessage="Personnummret är inte giltig! Ange formatet ÅÅMMDD-XXXX"
                    ControlToValidate="CivicRegistrationNumber"
                    Display="None"
                    ValidationExpression="^\d{6}-\d{4}$"></asp:RegularExpressionValidator>
            </div>                
            <div>
                <label for="Address">Address</label>
            </div>
            <div>
                <asp:TextBox ID="Address" runat="server" Text='<%# BindItem.Address %>' MaxLength="30" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ErrorMessage="Fältet får inte vara tomt!"
                    ControlToValidate="Address"
                    Display="None"></asp:RequiredFieldValidator>
            </div>               
            <div>
                <label for="ZipCode">Postnummer</label>
            </div>
            <div>
                <asp:TextBox ID="ZipCode" runat="server" Text='<%# BindItem.PostNR %>' MaxLength="6" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    ErrorMessage="Fältet får inte vara tomt!"
                    ControlToValidate="ZipCode"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ErrorMessage="Postnummret måste vara i ett av följade format: XXXXX,XXX-XX, XXX XX."
                    ControlToValidate="ZipCode"
                    Display="None"
                    ValidationExpression="^\d{3}(?:[-\s]\d{2})?$"></asp:RegularExpressionValidator>
            </div>
            <div>
                <label for="Region">Ort</label>
            </div>
            <div>
                <asp:TextBox ID="Region" runat="server" Text='<%# BindItem.Ort %>' MaxLength="25" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                    ErrorMessage="Fältet får inte vara tomt!"
                    ControlToValidate="Region"
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
