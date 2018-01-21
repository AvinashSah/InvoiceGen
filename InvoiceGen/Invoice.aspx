<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="InvoiceGen.Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog">
        <div class="container">
            <!-- Search Table -->
            <div class="row">
                <div class="form-group form-inline">
                    <label for="SearchInvoiceButton">Search Invoices</label>
                    <table class="table" id="SearchInvoiceButton">
                        <tbody>
                            <tr>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <select class="form-control" id="clientName">
                                            <option>--Select Client--</option>
                                            <option>Anand Dresses</option>
                                            <option>Transformers Ltd.</option>
                                            <option>A & P Group Ltd.</option>
                                        </select>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <select class="form-control" id="clientCity">
                                            <option>--Select City--</option>
                                            <option>Karnataka</option>
                                            <option>Maharastra</option>
                                            <option>Kolkata</option>
                                        </select>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <input type="text" class="form-control" id="invoiceNo" placeholder="#invoiceNumber" />
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <button type="submit" id="searchInvoice" class="btn btn-primary mb-2"><i class="fa fa-search" style="padding-right: 10px"></i>Search</button>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <button type="reset" id="resetInvoice" class="btn btn-primary mb-2"><i class="fa fa-recycle" style="padding-right: 10px"></i>Reset</button>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <button type="button" id="AddInvoice" class="btn btn-primary mb-2"><i class="fa fa-plus-square" style="padding-right: 10px"></i>Add Invoice</button>
                                    </div>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <!-- Search Table End-->
            <!--Invoice Lists-->
            <div class="row">
                <label for="AddInvoiceTable">Result</label>
                <table id="InvoiceList" class="table table-striped table-bordered" cellspacing="0" width="100%">
                    <thead>
                        <tr>
                            <th scope="col">#No</th>
                            <th scope="col">Client Name</th>
                            <th scope="col">Invoice No</th>
                            <th scope="col">Due Date</th>
                            <th scope="col">Amount</th>
                            <th scope="col">Tax</th>
                            <th scope="col">Total</th>
                            <th scope="col">Status</th>
                            <th scope="col">Date of Payment</th>
                        </tr>
                    </thead>
                </table>
            </div>

            <!--Invoice Lists End-->
        </div>
    </div>
</asp:Content>

