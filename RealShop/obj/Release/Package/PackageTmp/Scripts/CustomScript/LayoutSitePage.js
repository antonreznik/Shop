$(document).ready(function () {
    //Открываем корзину
    $("#cart").click(function () {
        $.ajax({
            url: "/Cart/ShowFullCart",
            success: function (data) { $("#in_cart").html(data) }
        })
        $("#in_cart_modal").modal();
    });

    //Открываем информацию "О продукции"
    $(".about_production").click(function () {
        $("#about_production").modal();
    });

    //Открываем информацию "О доставке"
    $(".delivery_info").click(function () {
        $("#delivery_info").modal();
    });

    //Открываем информацию "Оформление заказа"
    $(".make_order_info").click(function () {
        $("#make_order_info").modal();
    });

    //Открываем информацию "Контакты"
    $(".contacts").click(function () {
        $("#contacts").modal();
    });
});

//функция открывает информацию о товаре
function ShowProductInfo(productId, categoryId) {
    $.ajax({
        url: "/Home/GetOneProductInfo",
        data: { ProductId: productId, CategoryId: categoryId },
        success: function (data) {
            $("#single_product_page").modal();
            $("#page_product_info1").html(data)
        }
    });
};

//функция отображает сообщение "товар добавлен в корзину" при нажатии на кнопку "В корзину"
function ShowMessageAddToCart() {
    $(this).parent().parent().children(".product_image").children(".message_add_to_cart").css("display", "block").fadeOut(2500);
};

//Функция закрытия ajax-loader по завершении запроса
function CloseModalAjaxLoader(obj) {
    $("#wait").dialog("destroy");
    $(obj).parent().hide();
};

//Функция запуска ajax-loader при старте запроса
function StartAjaxLoader() {
    $("#wait").dialog({ minWidth: 50, width: 50, minHeight: 60, height: 100, modal: true });
    $("#wait").dialog("widget").find(".ui-dialog-titlebar").hide();
};

//-----------Cosmetic filter-----------------------
//Отображение блоков с подтипами при выборе чекбокса
//Для глаз
$("#for_eyes").change(function () {
    if ($("#for_eyes").is(":checked")) {
        $("#eyes_subtypes").slideDown();
    }

    else {
        $("#eyes_subtypes").slideUp();
        $("#mascara").prop("checked", false);
        $("#liner").prop("checked", false);
        $("#eyeliner").prop("checked", false);
        $("#eye_shadow").prop("checked", false);
    }
});
//для губ
$("#for_lips").change(function () {
    if ($("#for_lips").is(":checked")) {
        $("#lips_subtypes").slideDown();
    }

    else {
        $("#lips_subtypes").slideUp();
        $("#lipstick").prop("checked", false);
        $("#lip_gloss").prop("checked", false);
        $("#lip_liner").prop("checked", false);
    }
});
//Тональные средства
$("#tonic").change(function () {
    if ($("#tonic").is(":checked")) {
        $("#tonic_subtypes").slideDown();
    }

    else {
        $("#tonic_subtypes").slideUp();
        $("#foundation").prop("checked", false);
        $("#powder").prop("checked", false);
        $("#correctors").prop("checked", false);
        $("#makeup_base").prop("checked", false);
        $("#blusher").prop("checked", false);
    }
});
//Для ногтей
$("#nail").change(function () {
    if ($("#nail").is(":checked")) {
        $("#nail_subtypes").slideDown();
    }

    else {
        $("#nail_subtypes").slideUp();
        $("#nail_polish").prop("checked", false);
        $("#nail_care").prop("checked", false);
        $("#polish_remover").prop("checked", false);
    }
});

