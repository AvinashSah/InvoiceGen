<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="InvoiceGen.ImportData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog">
        <div class="container">

            <!-- Upload Excel Start-->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Upload Product Information excel sheet</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="productsDataFile" runat="server" class="form-control" />
                                        </td>
                                        <td>
                                            <asp:Button ID="productsDataFileButton" runat="server" Text="Upload"
                                                OnClick="productsDataFileButton_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="productsDataFileLabel" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Upload Excel End-->
            <!--Display Uploaded datas start-->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Uploaded product Information</strong></div>
                        <div class="panel-body">
                            <table id="UploadedProductList" class="table table-striped table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th scope="col">#No</th>
                                        <th scope="col">Product/Services</th>
                                        <th scope="col">HSN/SAC code</th>
                                        <th scope="col">Description</th>
                                        <th scope="col">UoM</th>
                                        <th scope="col">Purchase Rate</th>
                                        <th scope="col">Sales Rate</th>
                                        <th scope="col">Cess %</th>
                                        <th scope="col">GST %</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="uploadedProductsTbody" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--Display Uploaded data end-->

        </div>
    </div>

</asp:Content>
