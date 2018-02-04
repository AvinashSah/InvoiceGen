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
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" id=\"itemGST" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Qty\" id=\"itemQty" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"itemRate form-control\" placeholder=\"Item Rate\" id=\"itemRate" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Total Amount\" id=\"itemAmount" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<button onclick=\"return false\" id=\"itemDelete" + (rowCount + 1) + "\" class=\"btn btn-danger nopadding\" style=\"padding:2px\"><i class=\"fa fa-remove\"></i></button>" + "</td>";
            htmlContent += "</tr>";
            $("#itemList tbody").append(htmlContent);
        }
        else {
            htmlContent = "<tr id=\"tr" + (rowCount + 1) + "\" style=\"width:100%\">";
            //htmlContent += "<td>" + "<input type=\"text\" style=\"display: none;\" runat=\"server\" class=\"form-control\" disabled=\"disabled\" value=" + (rowCount + 1) + " id=\"itemNumber" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"productName form-control\" placeholder=\"Item Name\" id=\"itemName" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Description\" id=\"itemDescription" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" autocomplete=\"on\" class=\"HSNCode form-control\" placeholder=\"Item HSN/SAC\" id=\"itemHSNSAC" + (rowCount + 1) + "\" \>  " + " <ul class=\"suggesstion-boxHSN\"></ul> </td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" id=\"itemGST" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Qty\" id=\"itemQty" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"itemRate form-control\" placeholder=\"Item Rate\" id=\"itemRate" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<input type=\"text\" runat=\"server\" class=\"form-control\" placeholder=\"Item Total Amount\" id=\"itemAmount" + (rowCount + 1) + "\" \>  " + "</td>";
            htmlContent += "<td>" + "<button id=\"itemDelete" + (rowCount + 1) + "\" class=\"itemDeleteAddInvoice btn btn-danger\" style=\"padding:2px\"><i class=\"fa fa-remove\"></i></button>" + "</td>";
            htmlContent += "</tr>";
            $("#itemList tbody").append(htmlContent);
        }


    }

    //Event on Item Rate Change
    $("#itemList").on('change', '.itemRate', function () {
        var id = this.id;
        var identifier = id.replace('itemRate', '');
        var itemRate = $('#itemRate' + identifier).val();
        var qty = $('#itemQty' + identifier).val();
        //calculate amount
        $('#itemAmount' + identifier).val(itemRate * qty);
        ReEvaluateItemCost();
    });

    //function to evaluate item cost
    function ReEvaluateItemCost() {

        var rowCount = $('#itemList').rowCount();

        var compannyGstin = $("[id$=compannyGstin]").val().substring(1, 2);
        var billToClientGSTIN = $("[id$=billToClientGSTIN]").val().substring(1, 2);

        if (compannyGstin !== "" && billToClientGSTIN !== "") {
            if (compannyGstin === billToClientGSTIN) {
                var cumulativeintraStateCGST = 0;
                var cumulativeintraStateSGST = 0;
                var cumulativeintraStateCost = 0;
                var finalintraStateCost = 0;
                for (var i = 1; i <= rowCount; i++) {
                    var intraStategst = $('#itemGST' + i).val();
                    var intraStatesgst = intraStategst / 2;
                    var intraStatecgst = intraStategst / 2;

                    var intraStateamount = $('#itemAmount' + i).val();
                    var intraStatesgstperc = (intraStatesgst / 100) * intraStateamount;
                    var intraStatecgstperc = (intraStatecgst / 100) * intraStateamount;

                    cumulativeintraStateCGST += +intraStatecgstperc;
                    cumulativeintraStateSGST += +intraStatesgstperc;
                    cumulativeintraStateCost += +intraStateamount;
                    finalintraStateCost += +(cumulativeintraStateCGST + cumulativeintraStateSGST + cumulativeintraStateCost);
                }

                var totalCostHtmlContent = "<tr>";
                totalCostHtmlContent += "<td>" + cumulativeintraStateCost + "</td>";
                totalCostHtmlContent += "<td>" + cumulativeintraStateCGST + "</td>";
                totalCostHtmlContent += "<td>" + cumulativeintraStateSGST + "</td>";
                totalCostHtmlContent += "<td>" + finalintraStateCost + "</td>";
                totalCostHtmlContent += "</tr>";

                $("[id$=amoutCalculationInterState]").hide();
                $("[id$=amoutCalculationIntraState]").show();
                $("[id$=amoutCalculationIntraState] tbody").remove();
                $("[id$=amoutCalculationIntraState] tbody").append(totalCostHtmlContent);
            }
            else {
                var cumulativeinterStateGST = 0;
                var cumulativeinterStateCost = 0;
                var finalinterStateCost = 0;
                for (var j = 1; j <= rowCount; j++) {
                    var interStategst = $('#itemGST' + j).val();
                    var interStateamount = $('#itemAmount' + j).val();
                    var interStategstperc = (interStategst / 100) * interStateamount;

                    cumulativeinterStateGST += +interStategstperc;
                    cumulativeinterStateCost += +interStateamount;
                    finalinterStateCost += +(cumulativeinterStateGST + cumulativeinterStateCost);
                }
                var totalCostHtmlContent2 = "<tr>";
                totalCostHtmlContent2 += "<td>" + cumulativeinterStateCost + "</td>";
                totalCostHtmlContent2 += "<td>" + cumulativeinterStateGST + "</td>";
                totalCostHtmlContent2 += "<td>" + finalinterStateCost + "</td>";
                totalCostHtmlContent2 += "</tr>";

                $("[id$=amoutCalculationIntraState]").hide();
                $("[id$=amoutCalculationInterState]").show();
                $("[id$=amoutCalculationInterState] tbody").remove();
                $("[id$=amoutCalculationInterState] tbody").append(totalCostHtmlContent2);
            }
        }
        else {
            var companyAddrState = $("[id$=companyAddrState] option:selected").val();
            var companyAddrCity = $("[id$=companyAddrCity] option:selected").val();
            var billToClientStateList = $("[id$=billToClientStateList] option:selected").val();
            var billToClientCityList = $("[id$=billToClientCityList] option:selected").val();
            if (companyAddrState !== "" && companyAddrCity !== "") {
                //If State selected is same for two comapny and bill
                if (companyAddrState === billToClientStateList) {
                    var cumulativeintraStateCGST_StateCode = 0;
                    var cumulativeintraStateSGST_StateCode = 0;
                    var cumulativeintraStateCost_StateCode = 0;
                    var finalintraStateCost_StateCode = 0;
                    for (var i_StateCode = 1; i_StateCode <= rowCount; i_StateCode++) {
                        var intraStategst_StateCode = $('#itemGST' + i_StateCode).val();
                        var intraStatesgst_StateCode = intraStategst_StateCode / 2;
                        var intraStatecgst_StateCode = intraStategst_StateCode / 2;

                        var intraStateamount_StateCode = $('#itemAmount' + i_StateCode).val();
                        var intraStatesgstperc_StateCode = (intraStatesgst_StateCode / 100) * intraStateamount_StateCode;
                        var intraStatecgstperc_StateCode = (intraStatecgst_StateCode / 100) * intraStateamount_StateCode;

                        cumulativeintraStateCGST_StateCode += +intraStatecgstperc_StateCode;
                        cumulativeintraStateSGST_StateCode += +intraStatesgstperc_StateCode;
                        cumulativeintraStateCost_StateCode += +intraStateamount_StateCode;
                        finalintraStateCost_StateCode += +(cumulativeintraStateCGST_StateCode + cumulativeintraStateSGST_StateCode + cumulativeintraStateCost_StateCode);
                    }

                    var totalCostHtmlContent_StateCode = "<tr>";
                    totalCostHtmlContent_StateCode += "<td>" + cumulativeintraStateCost_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "<td>" + cumulativeintraStateCGST_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "<td>" + cumulativeintraStateSGST_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "<td>" + finalintraStateCost_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "</tr>";

                    $("[id$=amoutCalculationInterState]").hide();
                    $("[id$=amoutCalculationIntraState]").show();
                    $("[id$=amoutCalculationIntraState] tbody").remove();
                    $("[id$=amoutCalculationIntraState] tbody").append(totalCostHtmlContent_StateCode);
                }
                else {
                    var cumulativeinterStateGST_StateCode = 0;
                    var cumulativeinterStateCost_StateCode = 0;
                    var finalinterStateCost_StateCode = 0;
                    for (var j_StateCode = 1; j_StateCode <= rowCount; j_StateCode++) {
                        var interStategst_StateCode = $('#itemGST' + j_StateCode).val();
                        var interStateamount_StateCode = $('#itemAmount' + j_StateCode).val();
                        var interStategstperc_StateCode = (interStategst_StateCode / 100) * interStateamount_StateCode;

                        cumulativeinterStateGST_StateCode += +interStategstperc_StateCode;
                        cumulativeinterStateCost_StateCode += +interStateamount_StateCode;
                        finalinterStateCost_StateCode += +(cumulativeinterStateGST_StateCode + cumulativeinterStateCost_StateCode);
                    }
                    var totalCostHtmlContent2_StateCode = "<tr>";
                    totalCostHtmlContent2_StateCode += "<td>" + cumulativeinterStateCost_StateCode + "</td>";
                    totalCostHtmlContent2_StateCode += "<td>" + cumulativeinterStateGST + "</td>";
                    totalCostHtmlContent2_StateCode += "<td>" + finalinterStateCost_StateCode + "</td>";
                    totalCostHtmlContent2_StateCode += "</tr>";

                    $("[id$=amoutCalculationIntraState]").hide();
                    $("[id$=amoutCalculationInterState]").show();
                    $("[id$=amoutCalculationInterState] tbody").remove();
                    $("[id$=amoutCalculationInterState] tbody").append(totalCostHtmlContent2_StateCode);
                }
            }
            else {
                alert("Please eiether provide GSTIN/PAN !")
            }
        }
    }


    $("#itemList").on('click', '.itemDeleteAddInvoice', function () {
        var confirmation = false;
        confirm("DO you really want to remove item ?", confirmation);
        if (confirmation) {
            $(this).parents('tr').first().remove();
            ReEvaluateItemCost();
        }
    });



    //Function to make parameters and send to asmx service
    function createBill_Click() {
        var companyName = $("[id$=companyName]").val();
        var companyContactName = $("[id$=companyContactName]").val();
        var compannyGstin = $("[id$=compannyGstin]").val();
        var companyPan = $("[id$=companyPan]").val();
        var companyAddrLine1 = $("[id$=companyAddrLine1]").val();
        var companyAddrLine2 = $("[id$=companyAddrLine2]").val();
        var companyAddrCity = $("[id$=companyAddrCity] option:selected").val();
        var companyAddrState = $("[id$=companyAddrState] option:selected").val();
        var companyLogoID = $("[id$=companyLogoID]").val();

        var billToClientName = $("[id$=billToClientName]").val();
        var billToClientContactName = $("[id$=billToClientContactName]").val();
        var billToClientGSTIN = $("[id$=billToClientGSTIN]").val();
        var billToClientPAN = $("[id$=billToClientPAN]").val();
        var billToClientAddline1 = $("[id$=billToClientAddline1]").val();
        var billToClientAddline2 = $("[id$=billToClientAddline2]").val();
        var billToClientCityList = $("[id$=billToClientCityList] option:selected").val();
        var billToClientStateList = $("[id$=billToClientStateList] option:selected").val();

        //var shipToClientName = $("[id$=shipToClientName]").val();
        //var shipToClientContactName = $("[id$=shipToClientContactName]").val();
        var shipToClientAddLine1 = $("[id$=shipToClientAddLine1]").val();
        var shipToClientAddLine2 = $("[id$=shipToClientAddLine2]").val();
        var shipToClientCityList = $("[id$=shipToClientCityList] option:selected").val();
        var shipToClientStateList = $("[id$=shipToClientStateList] option:selected").val();

        var Customer = {
            Name: companyName,
            ContactName: companyContactName,
            GSTIN: compannyGstin,
            PAN: companyPan,
            BillAddL1: companyAddrLine1,
            BillAddL2: companyAddrLine2,
            BillAddCityID: companyAddrCity,
            BillStateID: companyAddrState,
            CustomerLogoPath: companyLogoID
        }

        var Client = {
            Name: billToClientName,
            ContactName: billToClientContactName,
            GSTIN: billToClientGSTIN,
            PAN: billToClientPAN,
            BillAddL1: billToClientAddline1,
            BillAddL2: billToClientAddline2,
            BillAddCityID: billToClientCityList,
            BillStateID: billToClientStateList,
            ShipAddL1: shipToClientAddLine1,
            ShipAddL2: shipToClientAddLine2,
            ShipAddCityID: shipToClientCityList,
            ShipStateID: shipToClientStateList
        }

        var productList = [];

        var rowCount = $('#itemList').rowCount();
        var compannyGstin = $("[id$=compannyGstin]").val().substring(1, 2);
        var billToClientGSTIN = $("[id$=billToClientGSTIN]").val().substring(1, 2);

        if (compannyGstin !== "" && billToClientGSTIN !== "") {

        }


        PushDataToService(Customer, Client)
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
