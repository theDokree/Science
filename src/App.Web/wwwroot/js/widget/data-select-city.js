$(document).ready(function () {

    html = "";
    obj = {
        "1": "Name",
        "2": "Age",
        "3": "Gender"
    }
    for (var key in obj) {
        //html += "<option value=" + key + ">" + obj[key] + "</option>"

        $('#data-select-city').append(new Option(obj[key], key));
        //$('#data-select-city').trigger('liszt:updated');
        $("#data-select-city").data("placeholder", "Select").trigger('chosen:updated');
    }

});




