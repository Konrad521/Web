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

var LastPrice = 0;

function GetPrice() {

    $.ajax({
        url: "/Waluty/BitBay",
        success: function (data) {
            var price = data.price;          
            $("#pricespan").html(price);

            if (price == LastPrice || LastPrice == 0) {
                $("#pricespan").css("color", "black");
            }
            else if (price > LastPrice) {
                $("#pricespan").css("color", "green");
            }
            else {
                $("#pricespan").css("color", "red")
            }


            LastPrice = price;
        }
    });

}

