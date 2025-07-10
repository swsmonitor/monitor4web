
export function load_map(dotNetHelper, midlng, midlat, zoom, geojsonString) {
    if (window._leafletMap) {
        window._leafletMap.remove();
    }
    // Example: Store the map instance globally when initializing
    const map = L.map('map').setView({ lon: midlng, lat: midlat }, zoom);
    window._leafletMap = map;
    window._markers = {}; // Object to store markers by id

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var legend = L.control({ position: 'topright' });
    legend.onAdd = function (map) {

        var div = L.DomUtil.create('div', 'info legend');
        var labels = ['<span style="font-size:20px"><strong>Survey Sites</strong></span>'];
        var categories = ['Selected', 'Monitored', 'Inactive'];

        for (var i = 0; i < categories.length; i++) {
            var color = "black";
            switch (categories[i]) {
                case "Selected":
                    color = 'blue';
                    break;
                case "Monitored":
                    color = 'green';
                    break;
                case "Inactive":
                    color = 'darkred';
                    break;
            }
            labels.push(
                '<i class="bi bi-circle-fill" style="font-size:20px;color:' + color + '"/><span style="margin: 10px">' +
                 (categories[i] ? categories[i] : '+') + '</span>');

        }
        div.innerHTML = labels.join('<br>');
        return div;
    };
    legend.addTo(map);


    if (geojsonString) {
        const geojson = JSON.parse(geojsonString);
        L.geoJSON(geojson).addTo(map);
    }
    // We don't need a click handler for the map surface- just the markers
    //map.on('click', function (e) {
    //    dotNetHelper.invokeMethodAsync('OnMapClick', e.latlng.lat, e.latlng.lng);
    //});
    return "Map loaded"
}
export async function show_marker(id) {
    var marker = window._markers[id];
    marker.setOpacity(1);
}
export async function hide_marker(id) {
    var marker = window._markers[id];
    marker.setOpacity(.1);
}
export async function change_marker_to_original(id) {
    var marker = window._markers[id];
    if (marker != null) {
        marker.setIcon( marker.markerIcon);
    }
}
export async function change_marker_to_selected(id) {
    var marker = window._markers[id];
    marker.setIcon(blueIcon);

    //if (marker != null) {
    //    marker.remove;
    //}

    //await add_marker(dotNetHelper, marker.lat, marker.lng, marker.isActive, marker.popupText, marker.id);
    window._leafletMap.flyTo([marker.lat, marker.lng], window._leafletMap.getZoom(), { animate: true, });
}
var redIcon = L.AwesomeMarkers.icon({
    icon: 'cone-striped',
    prefix: 'bi',
    iconColor: 'yellow',
    markerColor: 'red'
});
var greenIcon = L.AwesomeMarkers.icon({
    icon: 'star-fill',
    prefix: 'bi',
    iconColor: 'black',
    markerColor: 'green'
});
var blueIcon = L.AwesomeMarkers.icon({
    icon: 'bookmark-fill',
    prefix: 'bi',
    iconColor: 'yellow',
    markerColor: 'blue'
});

export async function add_marker(dotNetHelper, lat, lng, isActive, popupText, id) {
    if (!window._leafletMap) {
        // Map not initialized
        return;
    }

    var colorIcon = isActive ? greenIcon : redIcon;

    const marker = L.marker([lat, lng], { icon: colorIcon, title: popupText }).addTo(window._leafletMap);
    marker.Id = id;
    marker.markerIcon = colorIcon;
    marker.lat = lat;
    marker.lng = lng;

    if (popupText) {
        marker.bindPopup(popupText);
    }
    window._markers[id] = marker;

    marker.on('click', function () {
        // Call the .NET method when marker is clicked
        dotNetHelper.invokeMethodAsync('OnMarkerClick', lat, lng, id);
    });
}
