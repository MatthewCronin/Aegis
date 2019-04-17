<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToDo.aspx.cs" Inherits="Aegis.ToDo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMsg" ForeColor="Red" Visible="false" runat="server" />
        <asp:Label ID="lblSelection" Visible="false" runat="server" />
        <asp:Label ID="lblSelectedID" Visible="false" runat="server" />
        <asp:Label ID="lblUserID" Visible="false" Text="1" runat="server" />
        <asp:panel id="pnlOptions" visible="true" runat="server">
            <table style="width:80%; margin-right:auto; margin-left:auto;">
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkAdd" Text="Add ToDo" OnClick="lnkAdd_click" runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkEdit" Text="Edit ToDo" OnClick="lnkEdit_click" runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkComplete" Text="Complete ToDo" OnClick="lnkComplete_click" runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkDisable" Text="Disable ToDo" OnClick="lnkDisable_click" runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkLogOut" Text="Log Out" OnClick="lnkLogOut_click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:panel>
        <asp:Panel ID="pnlAdd" Visible="false" runat="server">
            <table>
                <tr>
                    <td colspan="4">
                        <h2>
                            Add ToDo Item
                        </h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        Title: <asp:TextBox ID="txtTitleAdd" runat="server" />
                    </td>
                    <td style="vertical-align:middle;">
                        Description: <asp:TextBox ID="txtDescriptionAdd" TextMode="MultiLine" Columns="35" Rows="3" runat="server" />
                    </td>
                    <td>
                        StartDate: <asp:TextBox TextMode="Date" ID="txtStartAdd" runat="server" />
                    </td>
                    <td>
                        EndDate: <asp:TextBox TextMode="Date" ID="txtEndAdd" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSubmitAdd" Text="Submit" OnClick="btnSubmitAdd_click" runat="server" />
                        <asp:Button ID="btnCancelAdd" Text="Cancel" OnClick="btnCancelAdd_click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlGrid" Visible="false" runat="server">
            <table>
            <%-- selection grid for all other functions here --%>
                <tr>
                    <td>
                        <asp:DataGrid id="grdTODo" runat="server" DataKeyField="ToDoID" AutoGenerateColumns="false" >
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkGrid" CommandArgument='<%#Eval("ToDoID") %>' OnCommand="lnkGrid_click" Text="Select" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="Title" DataField="Title" />
                                <asp:BoundColumn HeaderText="Description" DataField="Description" />
                                <asp:BoundColumn HeaderText="Start Date" DataField="StartDate" />
                                <asp:BoundColumn HeaderText="End Date" DataField="EndDate" />
                                <asp:BoundColumn HeaderText="Is Completed" DataField="Completed" />
                                <asp:BoundColumn HeaderText="Is Disabled" DataField="Disabled" />
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnGridCancel" Text="Cancel" OnClick="btnCancelGrid_click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEdit" Visible="false" runat="server">
            <table>
                <%-- All editable fields--%>
                <tr>
                    <td colspan="4">
                        <h2>
                            Edit ToDo Item
                        </h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        Title: <asp:TextBox ID="txtTitleEdit" runat="server" />
                    </td>
                    <td>
                        Description: <asp:TextBox ID="txtDescriptionEdit" TextMode="MultiLine" Columns="10" Rows="15" runat="server" />
                    </td>
                    <td>
                        StartDate: <asp:TextBox TextMode="Date" ID="txtStartEdit" runat="server" />
                    </td>
                    <td>
                        EndDate: <asp:TextBox TextMode="Date" ID="txtEndEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Completed:
                        <asp:RadioButtonList ID="rblCompletedEdit" runat="server">
                            <asp:ListItem Text="Yes" Value="True" />
                            <asp:ListItem Text="No" Value="False" Selected="True" />
                        </asp:RadioButtonList>
                    </td>
                    <td colspan="2">
                        Disabled: <asp:Label ID ="lblDisableEdit" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSubmitEdit" Text="Submit" OnClick="btnSubmitEdit_click" runat="server" />
                        <asp:Button ID="btnCancelEdit" Text="Cancel" OnClick="btnCancelEdit_click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlComplete" Visible="false" runat="server">
            <table>
                <%-- confirmation button --%>
                <tr>
                    <td colspan="4">
                        <h2>
                            Confirm Complete
                        </h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        Title: <asp:Label ID="lblTitleComplete" runat="server" /><br />
                        Description: <asp:Label ID="lblDescriptionComplete" runat="server" /><br />
                        StartDate: <asp:Label ID="lblStartDateComplete" runat="server" /><br />
                        EndDate: <asp:Label ID="lblEndDateComplete" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        Complete:
                        <asp:RadioButtonList ID="rblCompleteConfirm" runat="server">
                            <asp:ListItem Text="Yes" Value="True" Selected="True"  />
                            <asp:ListItem Text="No" Value="False"/>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSubmitComplete" Text="Confirm" OnClick="btnSubmitComplete_click" runat="server" />
                        <asp:Button ID="btnCancelComplete" Text="Cancel" OnClick="btnCancelComplete_click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlDisable" Visible="false" runat="server">
            <table>
                <%-- confirmation button --%>
                <tr>
                    <td colspan="4">
                        <h2>
                            Confirm Disable
                        </h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        Title: <asp:Label ID="lblTitleDisable" runat="server" /><br />
                        Description: <asp:Label ID="lblDescriptionDisable" runat="server" /><br />
                        StartDate: <asp:Label ID="lblStartDateDisable" runat="server" /><br />
                        EndDate: <asp:Label ID="lblEndDateDisable" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        Disable:<br />
                        <asp:RadioButtonList ID="rblConfirmDisable" runat="server">
                            <asp:ListItem Text="Yes" Value="True" Selected="True"  />
                            <asp:ListItem Text="No" Value="False"/>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnSubmitDisable" Text="Confirm" OnClick="btnSubmitDisable_click" runat="server" />
                        <asp:Button ID="btnCancelDisable" Text="Cancel" OnClick="btnCancelDisable_click" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
