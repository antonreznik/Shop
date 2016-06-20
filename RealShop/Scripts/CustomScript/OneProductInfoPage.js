
function getImageOfColor(ColorId) {
    $.ajax({
        url: "/Colors/GetPartialViewOfColor",
        data: { ColorId: ColorId },
        success: function (data) {
            $(".one_product_image").html(data);
        }
    });
}

function oneBuyButton(ProductId, CategoryId) {
    var ColorId = $(".one_big_color").children("img").attr("id");
        
    $.ajax({
        url: "/Cart/AddProductToCart",
        data:{ProductId:ProductId, CategoryId:CategoryId, ColorId:ColorId},
        success: function (data) {
            if (@Model.Colors.Count > 0 && ColorId == undefined) {
                    
                $(".one_product_image").html(data);
            }
            else {
                $("#@one_message").css("display", "block").fadeOut(1500);
                $("#infocart").html(data);
            }
        }
    });
}