<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="OnlineJobPortalUsingAspose._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style>
        em
        {
            color: Red;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<asp:Literal ID="msg" runat="server"></asp:Literal>
    <h2>
        Please fill the following form:
    </h2>
    <p>
        <h3>
            <strong>Personal Details:</strong>
        </h3>
        <table width="100%" class="table table-bordered">
            <tr>
                <td width="30%">
                    <label>
                        Applied for Position:<em>*</em>
                    </label>
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Name:<em>*</em></label>
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        DOB:<em>*</em></label>
                </td>
                <td>
                    <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Email:<em>*</em></label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Phone:<em>*</em></label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Address:<em>*</em></label>
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
         <h3>
            <strong>Skills: </strong> 
        </h3>
        <asp:TextBox TextMode="MultiLine" Width="100%"  Rows="5" ID="txtSkills" runat="server"></asp:TextBox>
        <h3>
            <strong>Educational Details: </strong>
        </h3>
        <asp:GridView ID="gvEducationalDetails" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false"
            Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Degree">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDegree" runat="server" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Marks/GPA">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTotalMarks" runat="server" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Obtained Marks/CGPA">
                    <ItemTemplate>
                        <asp:TextBox ID="txtObtainedMarks" runat="server" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Institute">
                    <ItemTemplate>
                        <asp:TextBox ID="txtInstitute" runat="server" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <h3>
            <strong>Professional Experience: </strong>
        </h3>
        <asp:GridView ID="gvExperience" CssClass="table table-bordered"  runat="server" AutoGenerateColumns="false" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Designation">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDesignation" runat="server" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Duration">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDuration" runat="server" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Organization">
                    <ItemTemplate>
                        <asp:TextBox ID="txtOrganization" runat="server" Width="99%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
        <h3>
            <strong>Cover Letter: </strong> 
        </h3>
        <asp:TextBox TextMode="MultiLine" Width="100%" Rows="5" ID="txtCoverLetter" runat="server"></asp:TextBox>
        <br />
        <center>
            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Submit Application" OnClick="btnSave_Click" />
        </center>
        <br />
        <div style="float:right">
        <asp:Button ID="btnMergeAllFiles" runat="server" CssClass="btn btn-warning" Text="Create Catalog of Applications" 
        onclick="btnMergeAllFiles_Click" /></div>
        <br />
    </p>
</asp:Content>
