// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#add-new-attr").click(function () {
        $.ajax({
            url: "/Product/ProductAttributePartialView",
            type: 'GET',
            success: function (res) {
                $("#product-attributes").append(res);
            },
            error: function (err) {
                console.log(err);
                alert('failed');
            }
        });
    });
});