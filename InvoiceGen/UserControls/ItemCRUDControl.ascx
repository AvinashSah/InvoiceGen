<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemCRUDControl.ascx.cs" Inherits="InvoiceGen.UserControls.ItemCRUDControl" %>
<div id="modal modal-content">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">
            ×
        </button>
        <h4 class="modal-title">Register Form</h4>
    </div>
    <div class="modal-body">
        <asp:Label runat="server" Text="Enter  UserName"></asp:Label>
        <asp:TextBox runat="server" class="form-control" ID="txtUserReg">
        </asp:TextBox>
        <br />
        <br />

        <asp:TextBox runat="server" TextMode="Password" ID="txtPassReg">
        </asp:TextBox>
    </div>
    <div class="modal-footer">
        <asp:Button ID="btnSave" Text="Save" OnClick="Register_Click"
            runat="server" />
    </div>
</div>
