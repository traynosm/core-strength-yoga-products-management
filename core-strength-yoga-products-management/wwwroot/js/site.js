// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#add-new-attr").click(function (event) {
        event.preventDefault();
        $.ajax({
            url: "/Product/ProductAttributePartialView",
            type: 'GET',
            success: function (res) {
                let html = '<div>' + res + '</div>'
                $("#product-attributes").append(html);
            },
            error: function (err) {
                console.log(err);
                alert('failed');
            }
        });
        $(this).hide();
    });

    $("#upload-img").click(function () {

        let fileInput = document.getElementById('myFile');
        var imageData = new FormData();
        imageData.append('image', fileInput.files[0]);

        let i = { 'image': fileInput.files[0] }

        console.log(JSON.stringify(i))

        $.ajax({
            url: "/Product/UploadImage",
            type: 'POST',
            data: imageData,
            dataType: 'json',
            processData: false,
            contentType: false,
            beforeSend: function () {
                $('#upload-image-success').hide();
                $('#upload-image-failed').hide();
            },
            success: function () {
                $('#upload-image-success').show();
            },
            error: function (err) {
                $('#upload-image-failed').show();
            }
        });
    });

    function serialize(data) {
        let obj = {};
        for (let [key, value] of data) {
            if (obj[key] !== undefined) {
                if (!Array.isArray(obj[key])) {
                    obj[key] = [obj[key]];
                }
                obj[key].push(value);
            } else {
                obj[key] = value;
            }
        }
        return obj;
    }
});