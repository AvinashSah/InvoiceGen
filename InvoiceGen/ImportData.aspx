<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="InvoiceGen.ImportData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog">
        <div class="container">

            <!-- Upload Excel Start-->
            <div class="row">
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Upload Product Information excel sheet</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <tr>
                                        <td>
                                            <input type="file" class="form-control" id="companyLogo" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2">
                </div>
                <div class="col-lg-5">
                </div>
            </div>
            <!-- Upload Excel End-->
            <!---->

        </div>
    </div>

</asp:Content>
