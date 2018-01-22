<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="InvoiceGen.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog">
        <div class="container">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s">
                        <span>Search By <strong>HSN Code</strong>  Or <strong>Description</strong></span>
                        <%--<input type="text" placeholder="Type Here" />--%>
                    </h2>
                </div>
            </div>
            <div class="row">
                <table id="productList" class="table table-striped table-bordered" cellspacing="0" width="100%">
                    <thead>
                        <tr>
                            <th scope="col">#No</th>
                            <th scope="col">Product/Services</th>
                            <th scope="col">HSN/SAC code</th>
                            <th scope="col">Description</th>
                            <th scope="col">Purchase Rate</th>
                            <th scope="col">Sales Rate</th>
                            <th scope="col">Cess %</th>
                            <th scope="col">GST %</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:PlaceHolder ID="allProductsListByFilter" runat="server"></asp:PlaceHolder>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
