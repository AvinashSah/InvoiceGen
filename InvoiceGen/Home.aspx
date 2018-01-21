<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="InvoiceGen.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog">
        <div class="container">
            <!-- Page Heading -->
            <div class="row">
                <div class="col-lg-12">
                    <h2 class="background double animated wow fadeInUp" data-wow-delay="0.2s">
                        <span>Search By <strong>HSN Code</strong>  Or <strong>Description</strong></span>
                        <input type="text" placeholder="Type Here" />
                    </h2>
                </div>
            </div>
            <div class="row">
                <table id="example" class="table table-striped table-bordered" cellspacing="0" width="100%">
                    <thead>
                        <tr>
                            <th scope="col">#HSN/SAC Code</th>
                            <th scope="col">Rate(%)</th>
                            <th scope="col">Cess(%)</th>
                            <th scope="col">Cess fixed</th>
                            <th scope="col">Quantity</th>
                            <th scope="col">Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">99812</th>
                            <td>Nil</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>Fish Processing Services</td>
                        </tr>
                        <tr>
                            <th scope="row">998739</th>
                            <td>18</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>Installation services of other goods n.e.c.</td>
                        </tr>
                        <tr>
                            <th scope="row">9988</th>
                            <td>5</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>Services by way of job work in relation to- (a) Printing of newspapers; (b) Textile yarns (other than of man-made fibres) and textile fabrics; (c) Cut and polished diamonds; precious and semi-precious stones; or plain and studded jewellery of gold and other precious metals, falling under Chapter of HSN; (d) Printing of books (including Braille books), journals and periodicals; (e) Processing of hides, skins and leather falling under Chapter 41 of HSN.</td>
                        </tr>
                        <tr>
                            <th scope="row">998736</th>
                            <td>18</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>Installation services of electrical machinery and apparatus n.e.c.</td>
                        </tr>
                        <tr>
                            <th scope="row">99812</th>
                            <td>Nil</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>Fish Processing Services</td>
                        </tr>
                        <tr>
                            <th scope="row">998739</th>
                            <td>18</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>Installation services of other goods n.e.c.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
