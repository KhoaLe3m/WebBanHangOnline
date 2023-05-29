$(document).ready(function () {
    ShowCount();
    $('body').on('click', ".btnAddToCart", function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }
        $.ajax({
            url: "/shoppingcart/addtocart",
            type: "POST",
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $("#checkout_items").html(rs.count);
                    alert(rs.msg);
                }
            }
        })
    });
    $('body').on('click', ".btnUpdate", function (f) {
        f.preventDefault();
        var id = $(this).data("id");
        var quantity = $("#Quantity_" + id).val();
        Update(id,quantity);
    });
    $('body').on('click', ".btnDelete", function (f) {
        f.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Bạn có muốn xóa sản phẩm này không?");
        if (conf == true) {
            $.ajax({
                url: "/shoppingcart/Delete",
                type: "POST",
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $("#checkout_items").html(rs.count);
                        $("#trow_" + id).remove();
                        LoadCart();
                    }
                }
            })
        }
    });
    $('body').on('click', ".btnDeleteAll", function (f) {
        f.preventDefault();
        var conf = confirm("Bạn có muốn xóa giỏ hàng không?");
        if (conf == true) {
            $.ajax({
                url: "/shoppingcart/DeleteAll",
                type: "POST",
                data: {},
                success: function (rs) {
                    if (rs.Success) {
                        $("#checkout_items").html(rs.count);
                        LoadCart();
                    }
                }
            })
        }
    });
});
function ShowCount() {
    $.ajax({
        url: "/shoppingcart/ShowCount",
        type: "GET",
        success: function (rs) {
            $("#checkout_items").html(rs.count);
        }
    })
}
function LoadCart() {
    $.ajax({
        url: "/shoppingcart/PartialCartView",
        type: "GET",
        success: function (rs) {
            $("#loadData").html(rs);
        }
    })
}
function Update(id,quantity) {
    $.ajax({
        url: "/shoppingcart/UpdateQuantity",
        type: "POST",
        data: {id:id,quantity:quantity},
        success: function (rs) {
            LoadCart();
        }
    })
}