// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function doAnAlert(alertType) {

    var showAlertDuration = 2000;
    var fadeoutSpeed = "slow";

    switch (alertType) {
        // Generic
        case "failure":
            $("#alertDivRed").text("Something went wrong. Please refresh and try again.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;

        // Order
        case "orderCreateSuccess":
            $("#alertDivGreen").text("Order created.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
        case "orderEditSuccess":
            $("#alertDivGreen").text("Order edited.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
        case "orderReceiveSuccess":
            $("#alertDivGreen").text("Order received. Assets Created.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
        case "orderDeleteSuccess":
            $("#alertDivGreen").text("Order deleted.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;

        // Person        
        case "personCreateSuccess":
            $("#alertDivGreen").text("Person created.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
        case "personEditSuccess":
            $("#alertDivGreen").text("Person edited.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
        case "personDeleteSuccess":
            $("#alertDivGreen").text("Person deleted.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;

        //Asset
        case "assetCreateSuccess":
            $("#alertDivGreen").text("Asset created.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
        case "assetEditSuccess":
            $("#alertDivGreen").text("Asset edited.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
        case "assetDeleteSuccess":
            $("#alertDivGreen").text("Asset deleted.").toggle().delay(showAlertDuration).fadeOut(fadeoutSpeed);
            break;
    }
}

function getGridPageFromNumberButtons(el) {
    var pageNumber = $(el).attr('data-page');
    var isActive = $(el).hasClass("active");

    if (isActive == false) {
        getGridPageAjax(pageNumber);
    }
}

function getGridPageFromNextPreviousButtons(el) {
    var pageNumber = $(el).attr('data-page');
    var isDisabled = $(el).hasClass("disabled");

    if (isDisabled == false) {
        getGridPageAjax(pageNumber);
    }
}
