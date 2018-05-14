<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="S101._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>员工管理</h1>
    <asp:DropDownList ID="DropDownListDepartments" runat="server" />
    <asp:Button ID="ButtonSearch" runat="server" Text="查询" OnClick="ButtonSearch_Click" />
<asp:GridView ID="GridViewEmployees" runat="server" AutoGenerateColumns="false" Width="100%">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="姓名" />
        <asp:BoundField DataField="Gender" HeaderText="性别" />
        <asp:BoundField DataField="BirthDate" HeaderText="生日" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="Department" HeaderText="部门" />
    </Columns>
</asp:GridView>
</asp:Content>
