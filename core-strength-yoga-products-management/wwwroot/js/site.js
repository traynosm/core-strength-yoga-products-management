// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#add-new-attr").click(function () {
        $.ajax({
            url: "/Product/ProductAttributePartialView",
            type: 'GET',
            success: function (res) {
                let html = '<div class="bg-success">' + res + '</div>'
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

        //const uploadFile = file => {
        //    console.log("Uploading file...");
        //    const API_ENDPOINT = "/Product/UploadImage";
        //    const request = new XMLHttpRequest();
        //    const formData = new FormData();

        //    request.open("POST", API_ENDPOINT, true);
        //    request.onreadystatechange = () => {
        //        if (request.readyState === 4 && request.status === 200) {
        //            console.log(request.responseText);
        //        }
        //    };
        //    formData.append('image', file);
        //    let formObj = new URLSearchParams(formData).toString();
        //    console.log(formObj);
        //    request.send(formObj);
        //};

        //uploadFile(fileInput.files[0]);

        //fileInput.addEventListener("change", event => {
        //    const files = event.target.files;
        //    uploadFile(files[0]);
        //});

        $.ajax({
            url: "/Product/UploadImage",
            type: 'POST',
            data: imageData,
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function () {
                alert('success')
            },
            //error: function (err) {
            //    console.log(err);
            //    alert('failed');
            //}
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
    //validate product Name
    //$("namecheck").hide();
    //let nameError = true;
    //$("#c").keyup(function () {
    //    validateName();
    //});

    //function validateName() {
    //    let nameValue = $("#productnames").val();
    //    if (nameValue.length == "") {
    //        $("#namecheck").show();
    //        nameError = false;
    //        return false;
    //    } else if (nameValue.length < 3 || nameValue.length > 50) {
    //        $("#namecheck").show();
    //        $("#namecheck").html("**length of productname must be between 3 and 50");
    //        nameError = false;
    //        return false;
    //    } else {
    //        $("#namecheck").hide();
    //    }
    //}
    //$("#submitbtn").click(function (event) {
    //    event.preventDefault()
    //    validateName();
    //    //validateEquipment();
    //    //validateType();
    //    //validateDescription();
    //    //validatePrice();
    //    //validateStockLevel();
    //    //validatePriceAdjustment();
    //    if (
    //        nameError == true //&&
    //        //passwordError == true &&
    //        //confirmPasswordError == true &&
    //        //emailError == true
    //    ) {
    //        return true;
    //    } else {
    //        return false;
    //    }
    //});
});