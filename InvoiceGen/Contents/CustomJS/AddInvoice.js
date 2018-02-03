$(document).ready(function () {

    $("#AddItemToInvoice").click(function () {
        var rowCount = $('#itemList').rowCount();
        AddNewItemInputRow(rowCount);
    });

    $.fn.rowCount = function () {
        return $('tr', $(this).find('tbody')).length;
    };
    //Add new Row on add item click button
    function AddNewItemInputRow(rowCount) {

        var htmlContent = "<tr id=\"tr" + (rowCount + 1) + "\" style=\"width:100%\">";
        htmlContent += "<th>" + "<input type=\"text\" class=\"form-control\" disabled=\"disabled\" value=" + (rowCount + 1) + " id=\"itemNumber" + (rowCount) + "\" \>  " + "</th>";
        htmlContent += "<td>" + "<input type=\"text\" class=\"productName form-control\" placeholder=\"Item Name\" id=\"itemName" + (rowCount) + "\" \>  " + "</td>";
        htmlContent += "<td>" + "<input type=\"text\" class=\"form-control\" placeholder=\"Item Description\" id=\"itemDescription" + (rowCount + 1) + "\" \>  " + "</td>";
        htmlContent += "<td>" + "<input type=\"text\" autocomplete=\"on\" class=\"HSNCode form-control\" placeholder=\"Item HSN/SAC\" id=\"itemHSNSAC" + (rowCount + 1) + "\" \>  " + " <ul class=\"suggesstion-boxHSN\"></ul> </td>";
        htmlContent += "<td>" + "<input type=\"text\" class=\"form-control\" placeholder=\"Item Qty\" id=\"itemQty" + (rowCount + 1) + "\" \>  " + "</td>";
        htmlContent += "<td>" + "<input type=\"text\" class=\"itemRate form-control\" placeholder=\"Item Rate\" id=\"itemRate" + (rowCount + 1) + "\" \>  " + "</td>";
        htmlContent += "<td>" + "<input type=\"text\" class=\"form-control\" placeholder=\"Item Total Amount\" id=\"itemAmount" + (rowCount + 1) + "\" \>  " + "</td>";
        htmlContent += "<td>" + "<button type=\"button\" id=\"itemDelete" + (rowCount + 1) + "\" class=\"itemDeleteAddInvoice btn btn-primary\" style=\"float: right\"><i class=\"fa fa-cut\" style=\"padding-right: 10px\"></i></button>" + "</td>";
        htmlContent += "</tr>";

        $("#itemList tbody").append(htmlContent);
    }

    $("#itemList").on('keypress', '.itemRate', function () {
        var id = this.id;
        var identifier = id.replace('itemRate', '');
        var itemAmountTxtBox = document.getElementById("itemAmount" + identifier);
        var qtyTxtBoxRef = document.getElementById("itemQty" + identifier);
        var rateTxtBoxRef = document.getElementById("itemRate" + identifier);
        //calculate amount
        itemAmountTxtBox.innerText = qtyTxtBoxRef.innerText * rateTxtBoxRef.innerText;
    });

    $("#itemList").on('click', '.itemDeleteAddInvoice', function () {
            $(this).parents('tr').first().remove();
        });

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
