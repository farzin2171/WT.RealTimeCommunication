function myMap() {
    var options = {
        center: new google.maps.LatLng(45.5162589, -73.5939865),
        zoom: 16
    };
    var map = new google.maps.Map(document.getElementById("googleMap"), options);

    //Add marker
    //var marker = new google.maps.Marker({
    //    position: { lat: 45.5162589, lng: -73.5939865 },
    //    map:map
    //});

    addMarker({ lat: 45.5162589, lng: -73.5939865 });

    var infoWindow = new google.maps.InfoWindow({
        content: '<h1>hellow</h1>',
    });

    //marker.addListener('click', function () {
    //    infoWindow.open(map, marker);
    //});

    function addMarker(coords) {
        var marker = new google.maps.Marker({
            position: coords,
            map: map
        });
    }
}