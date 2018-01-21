<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddInvoice.aspx.cs" Inherits="InvoiceGen.AddInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog">
        <div class="container">
            <!--  -->
            <div class="row">
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Your Details</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Company name" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="PAN" />
                                        </th>
                                        <th>
                                            <span>or</span>
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="GSTIN" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Name" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="City or State Zip" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Company's Address" />
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2"></div>
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Upload Company Logo</strong></div>
                        <div class="panel-body">
                            <input type="file" class="form-control" id="companyLogo" />
                        </div>
                    </div>
                </div>
            </div>
            <!--  -->
            <!--  -->
            <div class="row">
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Bill To:</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Clien't name" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="PAN" />
                                        </th>
                                        <th>
                                            <span>or</span>
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="GSTIN" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Name" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="City or State Zip" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Company's Address" />
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2"></div>
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Ship To:</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <tr>
                                        <th>
                                            <input type="checkbox" class="form-control" placeholder="Same as Bill Address" />
                                        </th>
                                        <td>
                                            <input type="text" class="form-control" placeholder="Your Clien't name" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="PAN" />
                                        </th>
                                        <th>
                                            <span>or</span>
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="GSTIN" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Name" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="City or State Zip" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Company's Address" />
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--  -->

            <!--  -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Item Details</strong></div>
                        <div class="panel-body">
                        </div>
                    </div>
                </div>
            </div>
            <!--  -->

            <!--  -->
            <div class="row">
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Notes For Customer</strong></div>
                        <div class="panel-body">
                            <asp:TextBox ID="TextArea1" class="form-control" TextMode="multiline" Columns="50" Rows="5" runat="server" placeholder="Enter Notes For Customer" />
                        </div>
                    </div>
                </div>
            </div>
            <!--  -->

            <!--  -->
            <div class="row">
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Terms & Conditions</strong></div>
                        <div class="panel-body">
                            <asp:TextBox ID="TextBox1" class="form-control" TextMode="multiline" Columns="50" Rows="5" runat="server" placeholder="Enter Terms & Conditions for Customer If any" />
                        </div>
                    </div>
                </div>
            </div>
            <!--  -->
        </div>
    </div>
</asp:Content>
