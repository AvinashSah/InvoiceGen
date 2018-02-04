$(document).ready(function () {

    AddNewItemInputRow(0);

    $("#AddItemToInvoice").click(function () {
        var rowCount = $('#itemList').rowCount();
        AddNewItemInputRow(rowCount);
    });

    $("#CreateBill").click(function () {
        createBill_Click();
    });

    $.fn.rowCount = function () {
        return $('tr', $(this).find('tbody')).length;
    };
    //Add new Row on add item click button
    function AddNewItemInputRow(rowCount) {
        var htmlContent = "";
        if (rowCount === 0) {
            htmlContent = "<tr id=\"tr" + (rowCount + 1) + "\" style=\"width:100%\">";
            //htmlContent += "<td>" + "<input type=\"text\" style=\"display: none;\" runat=\"server\" class=\"form-control\" disabled=\"disabled\" value=" + (rowCount + 1) + " id=\"itemNumber" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"productName form-control\" placeholder=\"Item Name\" id=\"itemName" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Description\" id=\"itemDescription" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" autocomplete=\"on\" class=\"HSNCode form-control\" placeholder=\"Item HSN/SAC\" id=\"itemHSNSAC" + (rowCount + 1) + "\" \>  " + " <ul class=\"suggesstion-boxHSN\"></ul> </td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Qty\" id=\"itemQty" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"itemRate form-control\" placeholder=\"Item Rate\" id=\"itemRate" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Total Amount\" id=\"itemAmount" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<button onclick=\"return false\" id=\"itemDelete" + (rowCount + 1) + "\" class=\"btn btn-danger nopadding\" style=\"padding:2px\"><i class=\"fa fa-remove\"></i></button>" + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" style=\"display: none;\" id=\"itemGST" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "</tr>";
            $("#itemList tbody").append(htmlContent);
        }
        else {
            htmlContent = "<tr id=\"tr" + (rowCount + 1) + "\" style=\"width:100%\">";
            //htmlContent += "<td>" + "<input type=\"text\" style=\"display: none;\" runat=\"server\" class=\"form-control\" disabled=\"disabled\" value=" + (rowCount + 1) + " id=\"itemNumber" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"productName form-control\" placeholder=\"Item Name\" id=\"itemName" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Description\" id=\"itemDescription" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" autocomplete=\"on\" class=\"HSNCode form-control\" placeholder=\"Item HSN/SAC\" id=\"itemHSNSAC" + (rowCount + 1) + "\" \>  " + " <ul class=\"suggesstion-boxHSN\"></ul> </td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Qty\" id=\"itemQty" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"itemRate form-control\" placeholder=\"Item Rate\" id=\"itemRate" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Total Amount\" id=\"itemAmount" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<button id=\"itemDelete" + (rowCount + 1) + "\" class=\"itemDeleteAddInvoice btn btn-danger\" style=\"padding:2px\"><i class=\"fa fa-remove\"></i></button>" + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" style=\"display: none;\" id=\"itemGST" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "</tr>";
            $("#itemList tbody").append(htmlContent);
        }


    }

    $("#itemList").on('change', '.itemRate', function () {
        var id = this.id;
        var identifier = id.replace('itemRate', '');
        var itemRate = $('#itemRate' + identifier).val();
        var qty = $('#itemQty' + identifier).val();
        //calculate amount
        $('#itemAmount' + identifier).val(itemRate * qty);
    });

    $("#itemList").on('click', '.itemDeleteAddInvoice', function () {
        $(this).parents('tr').first().remove();
    });



    //Function to make parameters and send to asmx service
    function createBill_Click() {
        var companyName = $("[id$=companyName]").val();
        var compannyGstin = $("[id$=compannyGstin]").val();
        var companyPan = $("[id$=companyPan]").val();
        var companyAddrLine1 = $("[id$=companyAddrLine1]").val();
        var companyAddrLine2 = $("[id$=companyAddrLine2]").val();
        var companyAddrCity = $("[id$=companyAddrCity] option:selected").val();
        var companyAddrState = $("[id$=companyAddrState] option:selected").val();
        var companyLogoID = $("[id$=companyLogoID]").val();

        var billToClientName = $("[id$=billToClientName]").val();
        var billToClientGSTIN = $("[id$=billToClientGSTIN]").val();
        var billToClientPAN = $("[id$=billToClientPAN]").val();
        var billToClientAddline1 = $("[id$=billToClientAddline1]").val();
        var billToClientAddline2 = $("[id$=billToClientAddline2]").val();
        var billToClientCityList = $("[id$=billToClientCityList] option:selected").val();
        var billToClientStateList = $("[id$=billToClientStateList] option:selected").val();

    }

    //$("#itemList").on('keypress', '.HSNCode', function () {
    //    var id = this.id;
    //    var prefixText = document.getElementById(id).innerText;
    //    var byClass = document.getElementById(id).getElementsByClassName(".suggesstion-boxHSN");
    //    var count = prefixText.length;

    //    $.ajax({
    //        type: "POST",
    //        url: "AutoFillService.asmx/GetProductListByHSNSACCode",
    //        data: "{ prefixText: '" + prefixText + "', count: " + count + "}",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (r) {
    //            var data = [];
    //            for (var key in r.d) {
    //                if (r.d.hasOwnProperty(key)) {
    //                    var product = r.d[key];
    //                    for (var propertyName in product) {
    //                        if (propertyName = "Name") {
    //                            data.push = { "productName": product.Name };
    //                        }
    //                    }
    //                }
    //            }
    //            $(byClass).show();
    //            $(byClass).html(data);
    //            $(id).css("background", "#FFF");
    //        },
    //        error: function (r) {
    //            alert(r.responseText);
    //        },
    //        failure: function (r) {
    //            alert(r.responseText);
    //        }
    //    });
    //});

    //$("#itemList").on('productName', '.HSNCode', function () {
    //    alert(this.id)
    //});

});
