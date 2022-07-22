<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ScaleModelsExcelToLinq.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <table id="TblDdl" cellspacing="4">
            <tr>
                <td colspan="6">
                    <asp:Literal ID="LitStatus" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Collection: </td>
                <td>
                    <asp:DropDownList ID="DDLCollection" runat="server" AutoPostBack="true"/>
                </td>
                <td>Brand: </td>
                <td>
                    <asp:DropDownList ID="DDLBrand" runat="server" AutoPostBack="true" />
                </td>
                <td>Maker: </td>
                <td>
                    <asp:DropDownList ID="DDLMaker" runat="server" AutoPostBack="true" />
                </td>
                <td>
                    <asp:Button ID="Clear_Selection" runat="server" Text="Clear Selection" OnClick="Clear_Selection_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="LblResult" runat="server" Text="" Font-Bold="True" />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Panel ID="PnlProducts" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div style="clear:both"></div>
    <br />
</asp:Content>
