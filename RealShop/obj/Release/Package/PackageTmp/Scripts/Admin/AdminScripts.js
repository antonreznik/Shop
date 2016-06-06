//Function of change availability of product
function ChangeInCheckBox(obj) {
    var isAvailable;
    if ($(obj).is(":checked")) {
        isAvailable = false;
    }

    else {
        isAvailable = true;
    }
    var productId = $(obj).data("productid");
    var categoryId = $(obj).data("categoryid");

    $.ajax({
        url: '/Admin/ChangeAvailabilityOfProduct',
        data: { productId: productId, categoryId: categoryId, isAvailable: isAvailable }
    })
}

//Function of change availability of color
function ChangeColorAvailabilityInCheckBox(obj) {
    var isAvailable;
    if ($(obj).is(":checked")) {
        isAvailable = false;
    }

    else {
        isAvailable = true;
    }

    var colorId = $(obj).data("productid");
    $.ajax({
        url: '/Admin/ChangeAvailabilityOfColor',
        data: { colorId: colorId, isAvailable: isAvailable }
    })
}