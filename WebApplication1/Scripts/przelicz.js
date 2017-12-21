function przelicz() {

    var x = $("#usd").val();
    var code = $("#kod1").val();

    $.ajax({
        url: "/Waluty/Przelicz?code="+code+"&kwota="+x,
        success: function (data) {
            var kwota = data.kwota;
            $("#pln").val(kwota);
        }
    });

}