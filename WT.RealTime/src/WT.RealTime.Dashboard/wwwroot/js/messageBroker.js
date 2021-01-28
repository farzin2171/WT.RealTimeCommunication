"use strict"

const signalrConnection = new signalR.HubConnectionBuilder()
    .withUrl("/messagebroker")
    .build();

signalrConnection.start().then(function () {
    console.log("SignalR Hub Connected");
}).catch(function (err) {
    return console.error(err.toString());
});

let messageCount = 0;
let map;
let marker;

function initMap(coords) {

    if (!coords) {

        let options = {
            center: new google.maps.LatLng(45.5162589, -73.5939865),
            zoom: 16
        };
        map = new google.maps.Map(document.getElementById("googleMap"), options);

      
        marker = new google.maps.Marker({
            position: { lat: 45.5162589, lng: -73.5939865 },
            map: map
        });
    }
   
    if (coords) {
        console.log(coords.lat);
        console.log(typeof (coords.lat));
        let newposition = new google.maps.LatLng(coords.lat, coords.lng);
        console.log(newposition);
        marker.setPosition(newposition);
        map.panTo(newposition);
    }

   
}


signalrConnection.on("onMessageRecived", function (eventMessage) {
   

    var point = JSON.parse(eventMessage.title);
    
    initMap({ lat: point.Latitude, lng: point.Longitude });
    //messageCount++;
    //const msgCountH4 = document.getElementById("messageCount");
    //msgCountH4.innerText = "Messages: " + messageCount.toString();
    //const ul = document.getElementById("messages");
    //const li = document.createElement("li");
    //li.innerText = messageCount.toString();

    //for (const property in eventMessage) {
    //    const newDiv = document.createElement("div");
    //    const classAttrib = document.createAttribute("style");
    //    classAttrib.value = "font-size: 80%;";
    //    newDiv.setAttributeNode(classAttrib);
    //    const newContent = document.createTextNode(`${property}: ${eventMessage[property]}`);
    //    newDiv.appendChild(newContent);
    //    li.appendChild(newDiv);
    //}
    //ul.appendChild(li);
    //window.scrollTo(0, document.body.scrollHeight);
});