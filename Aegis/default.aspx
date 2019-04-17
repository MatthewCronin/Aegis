<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Aegis._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlDefault" Visible="true" runat="server">
                <table>
                    <tr>
                        <td colspan="2">
                            <h2>
                                Welcome, please log in or create a user
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:LinkBUtton ID="lnkNewUser" Text="Create New User" OnClick="lnkNewUser_click" runat="server" />
                            <asp:Label ID="lblMsg" Visible="false" ForeColor="Red" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            User Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPword" TextMode="Password" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnValidate" Text="Login" OnClick="btnValidate_click" runat="server" /><br />
                        </td>
                    </tr>                    
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlCreate" Visible="false" runat="server">
                <table>
                    <tr>
                        <td colspan="2">
                            <h2>
                                Create User
                            </h2>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            User Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUNameCreate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPwordCreate" TextMode="Password" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Security Question 1: <br />
                            <asp:DropDownList ID="ddlSecurityQuestion1" runat="server" />
                        </td>
                        <td>
                            Security Answer 1: <br />
                            <asp:TextBox ID="txtSecAnswer1" TextMode="Password" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Security Question 2: <br />
                            <asp:DropDownList ID="ddlSecurityQuestion2" runat="server" />
                        </td>
                        <td>
                            Security Answer 2: <br />
                            <asp:TextBox ID="txtSecAnswer2" TextMode="Password" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnCreate" Text="Create User" OnClick="btnCreate_click" runat="server" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
