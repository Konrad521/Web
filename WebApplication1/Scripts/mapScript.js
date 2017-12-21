//mapka
function initMap() {

    //pobierz dane lokalizacji z localStorage
    var lat = parseFloat(localStorage.getItem("lat"));
    var lng = parseFloat(localStorage.getItem("lng"));

    //stwórz obiekt lokalizacji potrzebny dla google
    var loc = { lat: lat, lng: lng };

    //pobierz mapę z odpowiednim zoomem
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 12,
        center: loc
    });

    var marker = new google.maps.Marker({
        position: loc,
        map: map
    });
}