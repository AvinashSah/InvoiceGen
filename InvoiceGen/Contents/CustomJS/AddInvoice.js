$(document).ready(function () {

    BindStateListToControls();

    AddNewItemInputRow(0);

    $("#AddItemToInvoice").click(function () {
        ValidateForm();
        var rowCount = $('#itemList').rowCount();
        AddNewItemInputRow(rowCount);
    });

    $("#CreateBill").click(function () {
        createBill_Click();
    });

    $.fn.rowCount = function () {
        return $('tr', $(this).find('tbody')).length;
    };

    $("#companyAddrState").change(function () {
        var optionSelected = $(this).find("option:selected");
        var valueSelected = optionSelected.val();
        var textSelected = optionSelected.text();
        BindCityListForCompanyAddress(valueSelected);
    });

    $("#billToClientStateList").change(function () {
        var optionSelected = $(this).find("option:selected");
        var valueSelected = optionSelected.val();
        var textSelected = optionSelected.text();
        BindCityListForClientStateList(valueSelected);
    });

    $("#shipToClientStateList").change(function () {
        var optionSelected = $(this).find("option:selected");
        var valueSelected = optionSelected.val();
        var textSelected = optionSelected.text();
        BindCityListForShipToClientStateList(valueSelected);
    });

    function BindCityListForShipToClientStateList(valueSelected) {
        var cityList = [];
        $.ajax({
            type: "POST",
            url: "AutoFillService.asmx/GetListOfCitiesByStates",
            data: JSON.stringify({ valueSelected: valueSelected }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                for (var key in r.d) {
                    if (r.d.hasOwnProperty(key)) {
                        var DropDownCity = r.d[key];
                        var cityOption = { value: DropDownCity.Value, text: DropDownCity.text }
                        cityList.push(cityOption);
                    }
                }
                BindCityListForClientShip(cityList);
            },
            error: function (r) {
                alert(r.responseText);
            },
            failure: function (r) {
                alert(r.responseText);
            }
        });
    }

    function BindCityListForClientShip(cityList) {
        $("#shipToClientCityList").html('');

        var selectStatement = "<option Value=\"0\" selected>--Select City--</option>";
        $("#shipToClientCityList").append(selectStatement);

        for (var i = 0; i < cityList.length; i++) {
            var cityListHtml = "<option Value=\"" + cityList[i].value + "\">" + cityList[i].text + "</option>";
            $("#shipToClientCityList").append(cityListHtml);
        }
        return false;
    }

    function BindCityListForClientStateList(valueSelected) {
        var cityList = [];
        $.ajax({
            type: "POST",
            url: "AutoFillService.asmx/GetListOfCitiesByStates",
            data: JSON.stringify({ valueSelected: valueSelected }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                for (var key in r.d) {
                    if (r.d.hasOwnProperty(key)) {
                        var DropDownCity = r.d[key];
                        var cityOption = { value: DropDownCity.Value, text: DropDownCity.text }
                        cityList.push(cityOption);
                    }
                }
                BindCityListForClient(cityList);
            },
            error: function (r) {
                alert(r.responseText);
            },
            failure: function (r) {
                alert(r.responseText);
            }
        });
    }

    function BindCityListForClient(cityList) {
        $("#billToClientCityList").html('');

        var selectStatement = "<option Value=\"0\" selected>--Select City--</option>";
        $("#billToClientCityList").append(selectStatement);

        for (var i = 0; i < cityList.length; i++) {
            var cityListHtml = "<option Value=\"" + cityList[i].value + "\">" + cityList[i].text + "</option>";
            $("#billToClientCityList").append(cityListHtml);
        }
        return false;
    }

    function BindCityListForCompanyAddress(valueSelected) {
        var cityList = [];
        $.ajax({
            type: "POST",
            url: "AutoFillService.asmx/GetListOfCitiesByStates",
            data: JSON.stringify({ valueSelected: valueSelected }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                for (var key in r.d) {
                    if (r.d.hasOwnProperty(key)) {
                        var DropDownCity = r.d[key];
                        var cityOption = { value: DropDownCity.Value, text: DropDownCity.text }
                        cityList.push(cityOption);
                    }
                }
                BindCityListForComanyAddress(cityList);
            },
            error: function (r) {
                alert(r.responseText);
            },
            failure: function (r) {
                alert(r.responseText);
            }
        });
    }

    function BindCityListForComanyAddress(cityList) {
        $("#companyAddrCity").html('');

        var selectStatement = "<option Value=\"0\" selected>--Select City--</option>";
        $("#companyAddrCity").append(selectStatement);

        for (var i = 0; i < cityList.length; i++) {
            var cityListHtml = "<option Value=\"" + cityList[i].value + "\">" + cityList[i].text + "</option>";
            $("#companyAddrCity").append(cityListHtml);
        }
        return false;
    }

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
        return false;
    }

    //Gets state list from DB through ajax to bind on state controls
    function BindStateListToControls() {
        var stateList = [];
        $.ajax({
            type: "POST",
            url: "AutoFillService.asmx/GetListOfStates",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                for (var key in r.d) {
                    if (r.d.hasOwnProperty(key)) {
                        var DropDownState = r.d[key];
                        var stateOption = { value: DropDownState.Value, text: DropDownState.text }
                        stateList.push(stateOption);
                    }
                }
                BindStateList(stateList);
            },
            error: function (r) {
                alert(r.responseText);
            },
            failure: function (r) {
                alert(r.responseText);
            }
        });
    }

    //Binds State list to state controls on page load
    function BindStateList(stateList) {
        $("#companyAddrState").empty();
        $("#billToClientStateList").empty();
        $("#shipToClientStateList").empty();

        var selectStatement = "<option Value=\"0\" selected>--Select State--</option>";
        $("#companyAddrState").append(selectStatement);
        $("#billToClientStateList").append(selectStatement);
        $("#shipToClientStateList").append(selectStatement);
        for (var i = 0; i < stateList.length; i++) {
            var stateListHtml = "<option Value=\"" + stateList[i].value + "\">" + stateList[i].text + "</option>";
            $("#companyAddrState").append(stateListHtml);
            $("#billToClientStateList").append(stateListHtml);
            $("#shipToClientStateList").append(stateListHtml);
        }
        return false;
    }

    //Company GSTIN Change Event
    $("[id$=compannyGstin]").on('change', function () {
        if ($("[id$=compannyGstin]").val() !== "") {
            var compannyGstinlength = $("[id$=compannyGstin]").val().length;
            if (compannyGstinlength > 0 && compannyGstinlength !== 15) {
                alert("Company GSTIN should of 15 characters !");
                event.preventDefault();
                return false;
            }
            else {
                var gstinStateCode = $("[id$=compannyGstin]").val().substring(0, 2);
                BindStateForCompanyBasedOnGstinSelected(gstinStateCode);
            }
        }
    });
    //Company GSTIN Change Event helper
    function BindStateForCompanyBasedOnGstinSelected(gstinStateCode) {
        var gstinstateID = "";
        $.ajax({
            type: "POST",
            url: "AutoFillService.asmx/GetStateIDByGSTIN",
            data: JSON.stringify({ gstin: gstinStateCode }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                for (var key in r.d) {
                    if (r.d.hasOwnProperty(key)) {
                        var DropDownState = r.d;
                        gstinstateID = DropDownState.Value;
                    }
                }
                $("#companyAddrState").val(gstinstateID);
                BindCityListForCompanyAddress(gstinstateID);
            },
            error: function (r) {
                alert(r.responseText);
            },
            failure: function (r) {
                alert(r.responseText);
            }
        });
    }

    //Client GSTIN Change Event
    $("[id$=billToClientGSTIN]").on('change', function () {
        if ($("[id$=billToClientGSTIN]").val() !== "") {
            var compannyGstinlength = $("[id$=billToClientGSTIN]").val().length;
            if (compannyGstinlength > 0 && compannyGstinlength !== 15) {
                alert("Client's GSTIN should of 15 characters !");
                event.preventDefault();
                return false;
            }
            else {
                var gstinStateCode = $("[id$=billToClientGSTIN]").val().substring(0, 2);
                BindStateForClientBasedOnGstinSelected(gstinStateCode);
            }
        }
    });

    function BindStateForClientBasedOnGstinSelected(gstinStateCode) {
        var gstinstateID = "";
        $.ajax({
            type: "POST",
            url: "AutoFillService.asmx/GetStateIDByGSTIN",
            data: JSON.stringify({ gstin: gstinStateCode }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                for (var key in r.d) {
                    if (r.d.hasOwnProperty(key)) {
                        var DropDownState = r.d;
                        gstinstateID = DropDownState.Value;
                    }
                }
                $("#billToClientStateList").val(gstinstateID);
                BindCityListForClientStateList(gstinstateID);
                if ($('#chkSameAsBillAddress').is(':checked')) {
                    $("#shipToClientStateList").val(gstinstateID);
                    BindCityListForShipToClientStateList(gstinstateID);
                }
            },
            error: function (r) {
                alert(r.responseText);
            },
            failure: function (r) {
                alert(r.responseText);
            }
        });
    }

    $("[id$=chkSameAsBillAddress]").on('change', function () {
        if (!this.checked) {
            var sure = confirm("Are you sure?");
            event.preventDefault();
            $('#shipToClientCityList option[value!="0"]').remove();
            $("#shipToClientStateList").val(0);
            $("#shipToClientCityList").val(0);
        }

        if ($("[id$=chkSameAsBillAddress]").is(':checked')) {
            var selectedAddStateClientBill = $("#billToClientStateList").find("option:selected").val();
            var selectedAddCityClientBill = $("#billToClientCityList").find("option:selected").val();
            $("#shipToClientStateList").val(selectedAddStateClientBill);
            BindCityListForShipToClientStateList(selectedAddStateClientBill);
            $("#shipToClientCityList").val(selectedAddCityClientBill);
            $("[id$=shipToClientAddLine1]").val($("[id$=billToClientAddline1]").val());
            $("[id$=shipToClientAddLine2]").val($("[id$=billToClientAddline2]").val());
        }

    });


    //Event on Item Rate Change
    $("#itemList").on('change', '.itemRate', function () {
        var id = this.id;
        var identifier = id.replace('itemRate', '');
        var itemRate = $('#itemRate' + identifier).val();
        var qty = $('#itemQty' + identifier).val();
        //calculate amount
        $('#itemAmount' + identifier).val(itemRate * qty);
        ReEvaluateItemCost();
        return false;
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
                totalCostHtmlContent += "<td id=\"intraStateTotalAmount\">" + cumulativeintraStateCost + "</td>";
                totalCostHtmlContent += "<td id=\"intraStateTotalCGST\">" + cumulativeintraStateCGST + "</td>";
                totalCostHtmlContent += "<tdid=\"intraStateTotalSGST\">" + cumulativeintraStateSGST + "</td>";
                totalCostHtmlContent += "<td id=\"intraStateFinalAmount\">" + finalintraStateCost + "</td>";
                totalCostHtmlContent += "</tr>";

                $("[id$=amoutCalculationInterState]").hide();
                $("[id$=amoutCalculationIntraState]").show();
                $("[id$=amoutCalculationIntraState]").closest('tr').remove();
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
                totalCostHtmlContent2 += "<td id=\"interStateTotalAmount\">" + cumulativeinterStateCost + "</td>";
                totalCostHtmlContent2 += "<td id=\"intraStateTotalGST\">" + cumulativeinterStateGST + "</td>";
                totalCostHtmlContent2 += "<td id=\"intraStateFinalCost\">" + finalinterStateCost + "</td>";
                totalCostHtmlContent2 += "</tr>";

                $("[id$=amoutCalculationIntraState]").hide();
                $("[id$=amoutCalculationInterState]").show();
                $("[id$=amoutCalculationInterState]").closest('tr').remove();
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
                    totalCostHtmlContent_StateCode += "<td id=\"intraStateTotalAmount\">" + cumulativeintraStateCost_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "<td id=\"intraStateTotalCGST\">" + cumulativeintraStateCGST_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "<td id=\"intraStateTotalSGST\">" + cumulativeintraStateSGST_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "<td id=\"intraStateFinalAmount\">" + finalintraStateCost_StateCode + "</td>";
                    totalCostHtmlContent_StateCode += "</tr>";

                    $("[id$=amoutCalculationInterState]").hide();
                    $("[id$=amoutCalculationIntraState]").show();
                    $("[id$=amoutCalculationIntraState]").closest('tr').remove();
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
                    totalCostHtmlContent2_StateCode += "<td id=\"interStateTotalAmount\">" + cumulativeinterStateCost_StateCode + "</td>";
                    totalCostHtmlContent2_StateCode += "<td id=\"intraStateTotalGST\">" + cumulativeinterStateGST + "</td>";
                    totalCostHtmlContent2_StateCode += "<td id=\"intraStateFinalCost\">" + finalinterStateCost_StateCode + "</td>";
                    totalCostHtmlContent2_StateCode += "</tr>";

                    $("[id$=amoutCalculationIntraState]").hide();
                    $("[id$=amoutCalculationInterState]").show();
                    $("[id$=amoutCalculationInterState]").closest('tr').remove();
                    $("[id$=amoutCalculationInterState] tbody").append(totalCostHtmlContent2_StateCode);
                }
            }
            else {
                alert("Please eiether provide GSTIN/PAN !")
            }
        }
    }
    //Deletes item from item list
    $("#itemList").on('click', '.itemDeleteAddInvoice', function () {
        var confirmation = false;
        confirm("Do you really want to remove item ?", confirmation);
        if (confirmation) {
            $(this).parents('tr').remove();
            ReEvaluateItemCost();
        }
        return false;
    });
    //Function to make parameters and send to asmx service
    function createBill_Click() {
        ValidateForm();
        var companyName = $("[id$=companyName]").val();
        var companyContactName = $("[id$=companyContactName]").val();
        var compannyGstin = $("[id$=compannyGstin]").val();
        var companyPan = $("[id$=companyPan]").val();
        var companyAddrLine1 = $("[id$=companyAddrLine1]").val();
        var companyAddrLine2 = $("[id$=companyAddrLine2]").val();
        var companyAddrCity = $("[id$=companyAddrCity] option:selected").val();
        var companyAddrState = $("[id$=companyAddrState] option:selected").val();
        var companyLogoID = $("[id$=companyLogoID]").val();
        //console.log($('#companyLogo').attr('src'));

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

        var notesForCustomer = $("[id$=notesForCustomer]").val();
        var termsAndCondition = $("[id$=termsAndCondition]").val();

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
        var rowCount = $('#itemList').rowCount();
        var productList = [];
        var productBillMapping = [];
        for (var iProd = 1; iProd <= rowCount; iProd++) {
            var prod_ID = iProd;
            var prod_name = $('#itemName' + iProd).val();
            var prod_HSNCode = $('#itemHSNSAC' + iProd).val();
            var prod_description = $('#itemDescription' + iProd).val();
            var prod_Gst = $('#itemGST' + iProd).val();
            var prod_Rate = $('#itemRate' + iProd).val();
            var prod_Qty = $('#itemQty' + iProd).val();
            var prod_TotalAmount = $('#itemAmount' + iProd).val();
            var product = { Name: prod_name, HSNCode: prod_HSNCode, Description: prod_description, GSTPercentage: prod_Gst, ID: prod_ID }
            productList.push(product);

            var billProdMapp = {};
            var totalAmount = +prod_TotalAmount + +((prod_Gst / 100) * prod_TotalAmount);
            var cgsttmp = (((prod_Gst / 2) / 100) * prod_TotalAmount);
            var sgsttmp = (((prod_Gst / 2) / 100) * prod_TotalAmount);
            var gsttmp = ((prod_Gst / 100) * prod_TotalAmount);

            if ($("[id$=compannyGstin]").val().substring(1, 2) !== "" && $("[id$=billToClientGSTIN]").val().substring(1, 2) !== "") {
                if ($("[id$=compannyGstin]").val().substring(1, 2) === $("[id$=billToClientGSTIN]").val().substring(1, 2)) {
                    billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: totalAmount, CGST: cgsttmp, SGST: sgsttmp }
                }
                else {
                    billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: totalAmount, IGST: gsttmp }
                }
            }
            else {
                if ($("[id$=companyAddrState] option:selected").val() !== "" && $("[id$=billToClientStateList] option:selected").val() !== "") {

                    if ($("[id$=companyAddrState] option:selected").val() === $("[id$=billToClientStateList] option:selected").val()) {
                        billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: totalAmount, CGST: cgsttmp, SGST: sgsttmp }
                    }
                    else {
                        billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: totalAmount, IGST: gsttmp }
                    }
                }
            }
            productBillMapping.push(billProdMapp);
        }

        PushInvoiceDataToService(Customer, Client, productList, productBillMapping, notesForCustomer, termsAndCondition);

        return false;
    }
    //Validate Form Function
    function ValidateForm() {
        if ($("[id$=compannyGstin]").val() !== "") {
            var compannyGstinlength = $("[id$=compannyGstin]").val().length;
            if (compannyGstinlength !== 15) {
                alert("Company GSTIN should of 15 characters !");
                event.preventDefault();
            }
        }

        if ($("[id$=billToClientGSTIN]").val() !== "") {
            var billToClientGSTINLength = $("[id$=billToClientGSTIN]").val().length;
            if (billToClientGSTINLength !== 16) {
                alert("Client's GSTIN should of 16 characters !");
                event.preventDefault();;
            }
        }

        if ($("[id$=billToClientGSTIN]").val() === "" && $("[id$=compannyGstin]").val() === "") {
            alert("You need to provide eiether PAN/GSTIN !")
            event.preventDefault();
        }

    }
    //Get Product List from UI and prepare aJSon object
    function GetProducListFromUI() {
        var returnProductList = [];


        var rowCount = $('#itemList').rowCount();
        for (var iProd = 1; iProd <= rowCount; iProd++) {
            var prod_ID = iProd;
            var prod_name = $('#itemName' + iProd).val();
            var prod_HSNCode = $('#itemHSNSAC' + iProd).val();
            var prod_description = $('#itemDescription' + iProd).val();
            var prod_Gst = $('#itemGST' + iProd).val();
            var prod_Rate = $('#itemRate' + iProd).val();
            var prod_Qty = $('#itemQty' + iProd).val();
            var prod_TotalAmount = $('#itemAmount' + iProd).val();
            var product = { Name: prod_name, HSNCode: prod_HSNCode, Description: prod_description, GSTPercentage: prod_Gst, ID: prod_ID }
            returnProductList.push(product);
        }
    }
    //Get product bill mapping
    function GetProductBillMapping() {
        var returnproductBillMapping = [];

        var rowCount = $('#itemList').rowCount();
        for (var iProd = 1; iProd <= rowCount; iProd++) {
            var prod_ID = iProd;
            var billProdMapp = {};
            if ($("[id$=compannyGstin]").val().substring(1, 2) !== "" && $("[id$=billToClientGSTIN]").val().substring(1, 2) !== "") {
                if ($("[id$=compannyGstin]").val().substring(1, 2) === $("[id$=billToClientGSTIN]").val().substring(1, 2)) {
                    billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: $('#intraStateFinalAmount').val(), CGST: $('#intraStateTotalCGST').val(), SGST: $('#intraStateTotalSGST').val() }
                }
                else {
                    billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: $('#intraStateFinalCost').val(), IGST: $('#intraStateTotalGST').val() }
                }
            }
            else {
                if ($("[id$=companyAddrState] option:selected").val() !== "" && $("[id$=billToClientStateList] option:selected").val() !== "") {

                    if ($("[id$=companyAddrState] option:selected").val() === $("[id$=billToClientStateList] option:selected").val()) {
                        billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: $('#intraStateFinalAmount').val(), CGST: $('#intraStateTotalCGST').val(), SGST: $('#intraStateTotalSGST').val() }
                    }
                    else {
                        //billProdMapp = { ProductID: prod_ID, SalesRate: prod_Rate, Qyantity: prod_Qty, TotalAmount: $('#intraStateFinalCost').val(), IGST: $('#intraStateTotalGST').val() }
                    }
                }
            }
            productBill.push(billProdMapp);
        }
    }

    //pushes create invoice data to server
    function PushInvoiceDataToService(Customer, Client, productList, productBillMapping, notesForCustomer, termsAndCondition) {

        var postData = {
            Customer: Customer,
            Client: Client,
            productList: productList,
            productBillMapping: productBillMapping,
            notesForCustomer: notesForCustomer,
            termsAndCondition: termsAndCondition
        };

        $.ajax({
            type: "POST",
            url: "AutoFillService.asmx/SubmitAddInvoiceData",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(postData),
            success: function (data) {
                var json = JSON.parse(data);
                if (json.submited) {
                    alert(json.message);
                }
                else {
                    alert(json.message);
                }
            }
        });
    }
});
