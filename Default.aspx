<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MemberRegistry.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kalmar Äventyrare</title>
</head>
<body>
    <form id="form" runat="server">
    <div>
        <asp:Panel ID="SuccessPanel" runat="server" Visible="false" CssClass="fluff">
                <asp:Label ID="SuccessLabel" runat="server" Text="Skapandet av kontakten lyckades!"></asp:Label>
        </asp:Panel>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett fel har inträffat. Korrigera felet och försök igen."
            CssClass="validation-summary-errors" 
                ValidationGroup="Validations"/>

            <%-- Visar alla kontakter --%>
            <asp:ListView ID="MemberListView" runat="server"
                ItemType="MemberRegistry.Model.Member"
                SelectMethod="MemberListView_GetData"
                InsertMethod="MemberListView_InsertItem"
                UpdateMethod="MemberListView_UpdateItem"
                DeleteMethod="MemberListView_DeleteItem"
                DataKeyNames="MedID"
                InsertItemPosition="FirstItem">
                <LayoutTemplate>
                    <table class="grid">
                        <tr>
                            <th>
                                Förnamn
                            </th>
                            <th>
                                Efternamn
                            </th>
                            <th>
                                Personnummer
                            </th>
                            <th>
                                Address
                            </th>
                            <th>
                                Postnummer
                            </th>
                            <th>
                                Ort
                            </th>
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- Mall för nya rader. --%>
                    <tr>
                        <td>
                            <asp:Label ID="FirstNameLabel" runat="server" Text='<%#: Item.Fnamn %> ' />
                        </td>
                        <td>
                            <asp:Label ID="LastNameLabel" runat="server" Text='<%#: Item.Enamn %> ' />
                        </td>
                        <td>
                            <asp:Label ID="CivicRegistrationNumberLabel" runat="server" Text='<%#: Item.PersNR %>' />
                        </td>
                        <td>
                            <asp:Label ID="AddressLabel" runat="server" Text='<%#: Item.Address %>' />
                        </td>
                        <td>
                            <asp:Label ID="ZipCodeLabel" runat="server" Text='<%#: Item.PostNR %>' />
                        </td>
                        <td>
                            <asp:Label ID="RegionLabel" runat="server" Text='<%#: Item.Ort %>' />
                        </td>
                        <td class="command">
                            <%-- "Kommandknappar" för att ta bort och redigera kontaktuppgifter --%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" OnClientClick="Confirm();" />
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%-- Detta visas då kontaktuppgifter saknas i databasen --%>
                    <table class="grid">
                        <tr>
                            <td>
                                Kontaktuppgifter saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <%-- Mall för rad i tabellen för att lägga till nya kontaktuppgifter --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" Text='<%# BindItem.Fnamn %>' MaxLength="20" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="FirstName"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server" Text='<%# BindItem.Enamn %>' MaxLength="20" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="LastName"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="CivicRegistrationNumber" runat="server" Text='<%# BindItem.PersNR %>' MaxLength="11" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="CivicRegistrationNumber"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ErrorMessage="Personnummret är inte giltig! Ange formatet ÅÅMMDD-XXXX"
                                ControlToValidate="CivicRegistrationNumber"
                                Display="None"
                                ValidationExpression="^\d{6}-\d{4}$"
                                ValidationGroup="Validations"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="Address" runat="server" Text='<%# BindItem.Address %>' MaxLength="30" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="Address"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="ZipCode" runat="server" Text='<%# BindItem.PostNR %>' MaxLength="6" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="ZipCode"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="Region" runat="server" Text='<%# BindItem.Ort %>' MaxLength="25" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="Region"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <%-- "Kommandknappar" för att lägga till en ny kontaktuppgift och rensa texfälten --%>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <%-- Mall för rad i tabellen för att redigera kontaktuppgifter. --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName1" runat="server" Text='<%# BindItem.Fnamn %>' MaxLength="20" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="FirstName1"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="LastName1" runat="server" Text='<%# BindItem.Enamn %>' MaxLength="20" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="LastName1"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="CivicRegistrationNumber1" runat="server" Text='<%# BindItem.PersNR %>' MaxLength="11" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="CivicRegistrationNumber1"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ErrorMessage="Personnummret är inte giltig! Ange formatet ÅÅMMDD-XXXX"
                                ControlToValidate="CivicRegistrationNumber1"
                                Display="None"
                                ValidationExpression="^\d{6}-\d{4}$"
                                ValidationGroup="Validations"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="Address1" runat="server" Text='<%# BindItem.Address %>' MaxLength="30" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="Address1"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="ZipCode1" runat="server" Text='<%# BindItem.PostNR %>' MaxLength="6" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="ZipCode1"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="Region1" runat="server" Text='<%# BindItem.Ort %>' MaxLength="25" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                ErrorMessage="Fältet får inte vara tomt!"
                                ControlToValidate="Region1"
                                Display="None"
                                ValidationGroup="Validations"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta --%>
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
    </div>
    </form>
    <script src="Scripts/confirm.js"></script>
</body>
</html>
