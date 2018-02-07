<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddInvoice.aspx.cs" Inherits="InvoiceGen.AddInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="addInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog" runat="server">
        <div class="container">
            <!--  -->
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Your Details</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="companyName" runat="server" CssClass="form-control" placeholder="Company Name" AutoPostBack="true" TabIndex="0"></asp:TextBox>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="compannyGstin" runat="server" CssClass="form-control" placeholder="GSTIN" AutoPostBack="true" OnTextChanged="compannyGstin_TextChanged" TabIndex="1"></asp:TextBox>
                                        </th>
                                        <th>
                                            <span>or</span>
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="PAN" id="companyPan" runat="server" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Name" id="companyContactName" tabindex="2" runat="server" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Address Line 1" id="companyAddrLine1" tabindex="3" runat="server" />
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Address Line 2" id="companyAddrLine2" tabindex="4" runat="server" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:DropDownList ID="companyAddrState" runat="server" CssClass="form-control" TabIndex="5" OnSelectedIndexChanged="companyAddrState_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </th>
                                        <th>
                                            <asp:DropDownList ID="companyAddrCity" runat="server" CssClass="form-control" TabIndex="6" OnSelectedIndexChanged="companyAddrCity_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-default height" id="panelLogo" runat="server">
                        <div class="panel-heading"><strong>Upload Company Logo</strong></div>
                        <div class="panel-body">
                            <asp:FileUpload ID="comapnyLogoUploadFile" runat="server" CssClass="form-group" TabIndex="7" />
                            <asp:Button ID="btnCompanyLogoUpload" runat="server" Text="Upload" OnClick="btnCompanyLogoUpload_Click" CssClass="form-group" TabIndex="8" />
                        </div>
                    </div>
                    <input type="text" class="form-control" runat="server" id="companyLogoID" style="display: none" />

                    <asp:Image ID="companyLogo" runat="server" Style="float: right" />
                </div>
            </div>
            <!--  -->
            <!--  -->
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Bill To:</strong></div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" runat="server" placeholder="Your Clien't name" id="billToClientName" tabindex="9" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:TextBox ID="billToClientGSTIN" runat="server" TabIndex="10" CssClass="form-control" placeholder="GSTIN" AutoPostBack="true" OnTextChanged="billToClientGSTIN_TextChanged"></asp:TextBox>
                                        </th>
                                        <th>
                                            <span>or</span>
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="PAN" runat="server" id="billToClientPAN" tabindex="11" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Name" runat="server" id="billToClientContactName" tabindex="12" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Address Line 1" id="billToClientAddline1" runat="server" tabindex="13" />
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Address Line 2" id="billToClientAddline2" runat="server" tabindex="14" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:DropDownList ID="billToClientStateList" runat="server" CssClass="form-control" TabIndex="15" OnSelectedIndexChanged="billToClientStateList_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </th>
                                        <th>
                                            <asp:DropDownList ID="billToClientCityList" runat="server" CssClass="form-control" TabIndex="16" OnSelectedIndexChanged="billToClientCityList_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="panel panel-default height">
                        <div class="panel-heading">
                            <strong>Ship To: </strong>
                            <asp:CheckBox ID="chkSameAsBillAddress" OnCheckedChanged="chkSameAsBillAddress_CheckedChanged" Text="Same as Bill Address" Style="float: right" TabIndex="17" TextAlign="Right" runat="server" AutoPostBack="true" />
                        </div>
                        <div class="panel-body">
                            <table class="table-condensed">
                                <tbody>
                                    <%--<tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Clien't name" tabindex="18" runat="server" id="shipToClientName" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Your Name" tabindex="21" runat="server" id="shipToClientContactName" />
                                        </th>
                                    </tr>--%>
                                    <tr>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Address Line 1" tabindex="22" id="shipToClientAddLine1" runat="server" />
                                        </th>
                                        <th>
                                            <input type="text" class="form-control" placeholder="Address Line 2" tabindex="23" id="shipToClientAddLine2" runat="server" />
                                        </th>
                                    </tr>
                                    <tr>
                                        <th>
                                            <asp:DropDownList ID="shipToClientStateList" runat="server" CssClass="form-control" TabIndex="24" OnSelectedIndexChanged="shipToClientStateList_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </th>
                                        <th>
                                            <asp:DropDownList ID="shipToClientCityList" runat="server" CssClass="form-control" TabIndex="25" OnSelectedIndexChanged="shipToClientCityList_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
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
                        <div class="panel-heading">
                            <strong>Item Details</strong>
                            <button type="button" id="AddItemToInvoice" tabindex="26" class="btn btn-primary mb-2" style="float: right; padding: 3px"><i class="fa fa-plus-circle"></i></button>
                        </div>
                        <div class="panel-body">
                            <table id="itemList" class="table table-striped" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <%--<th scope="col" style="display: none;">#No</th>--%>
                                        <th scope="col">Product</th>
                                        <th scope="col">Description</th>
                                        <th scope="col">HSN/SAC</th>
                                        <th scope="col">GST%</th>
                                        <th scope="col">QTY</th>
                                        <th scope="col">Rate</th>
                                        <th scope="col">Total Amount</th>
                                        <th scope="col">Operation</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--  -->


            <!--  -->
            <div class="row">
                <div class="col-lg-5" style="float: right">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Total Amount</strong></div>
                        <div class="panel-body">
                            <table id="amoutCalculationInterState" style="display: none" class="table table-striped" cellspacing="0" width="100%" runat="server">
                                <thead>
                                    <tr>
                                        <th scope="col">Total Cost</th>
                                        <th scope="col">IGST</th>
                                        <th scope="col">Final Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <table id="amoutCalculationIntraState" style="display: none" class="table table-striped" cellspacing="0" width="100%" runat="server">
                                <thead>
                                    <tr>
                                        <th scope="col">Total Cost</th>
                                        <th scope="col">CGST</th>
                                        <th scope="col">SGST</th>
                                        <th scope="col">Final Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
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
                            <asp:TextBox ID="termsAndCondition" TabIndex="27" class="form-control" TextMode="multiline" Columns="50" Rows="5" runat="server" placeholder="Enter Terms & Conditions for Customer If any" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-2">
                </div>
                <div class="col-lg-5">
                    <div class="panel panel-default height">
                        <div class="panel-heading"><strong>Notes For Customer</strong></div>
                        <div class="panel-body">
                            <asp:TextBox ID="notesForCustomer" TabIndex="28" class="form-control" TextMode="multiline" Columns="50" Rows="5" runat="server" placeholder="Enter Notes For Customer" />
                        </div>
                    </div>
                </div>
            </div>
            <!--  -->
            <div class="row">
                <%--<div class="form-group col-lg-2">
                    <button type="submit" id="generatePDF" tabindex="30" class="btn btn-primary mb-2" runat="server" onserverclick="generateInvoicePDF_ServerClick"><i class="fa fa-pagelines" style="padding-right: 10px"></i>Generate PDF</button>
                </div>--%>

                <div class="form-group col-lg-12">
                    <button id="CreateBill" tabindex="29" class="btn btn-primary mb-2"><i class="fa fa-pagelines" style="padding-right: 10px"></i>Generate Invoice</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
