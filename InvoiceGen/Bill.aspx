<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Bill.aspx.cs" Inherits="InvoiceGen.Bill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="padding100" id="blog">
        <div class="container">
            <!-- Search Table -->
            <div class="row">
                <div class="form-group form-inline">
                    <label for="SearchBillButton">Search Bills</label>
                    <table class="table" id="SearchBillButton">
                        <tbody>
                            <tr>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <select class="form-control" id="vendorName">
                                            <option>--Select Vendor--</option>
                                            <option>Fish Processing Services</option>
                                            <option>Transformers Ltd.</option>
                                            <option>A & P Group Ltd.</option>
                                        </select>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <select class="form-control" id="vendorCity">
                                            <option>--Select City--</option>
                                            <option>Karnataka</option>
                                            <option>Maharastra</option>
                                            <option>Kolkata</option>
                                        </select>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <input type="text" class="form-control" id="billNo" placeholder="#billNumber" />
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <button type="submit" id="searchBill" class="btn btn-primary mb-2"><i class="fa fa-search" style="padding-right: 10px"></i>Search</button>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <button type="reset" id="resetBill" class="btn btn-primary mb-2"><i class="fa fa-recycle" style="padding-right: 10px"></i>Reset</button>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-group col-lg-2">
                                        <button type="button" id="AddBill" class="btn btn-primary mb-2"><i class="fa fa-plus-square" style="padding-right: 10px"></i>Add Bill</button>
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
                <label for="BillList">Result</label>
                <table id="BillList" class="table table-striped table-bordered" cellspacing="0" width="100%">
                    <thead>
                        <tr>
                            <th scope="col">#No</th>
                            <th scope="col">Product/Services</th>
                            <th scope="col">HSN/SAC</th>
                            <th scope="col">Description</th>
                            <th scope="col">UoM</th>
                            <th scope="col">QTY</th>
                            <th scope="col">PurchaseRate</th>
                            <th scope="col">Value</th>
                            <th scope="col">Discount</th>
                            <th scope="col">CGST</th>
                            <th scope="col">SGST</th>
                            <th scope="col">IGST</th>
                            <th scope="col">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">1</th>
                            <td>Fish Processing Services</td>
                            <td>99812</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                            <td>NA</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <!--Invoice Lists End-->
        </div>
    </div>
</asp:Content>
