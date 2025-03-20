import { useEffect, useState, useRef } from "react";
import { GoogleMap, LoadScript, DirectionsService, DirectionsRenderer } from "@react-google-maps/api";
import Header from './Header.tsx';

const libraries = ["places"];

const mockOrders = [
    { id: 1, customer: "לקוח 1", status: "בהמתנה", pickup: "תל אביב", dropoff: "ירושלים" },
    { id: 2, customer: "לקוח 2", status: "בדרך", pickup: "חיפה", dropoff: "תל אביב" },
    { id: 3, customer: "לקוח 3", status: "נמסר", pickup: "באר שבע", dropoff: "חיפה" }
];

const MapComponent = () => {
    const [directions, setDirections] = useState(null);
    const [map, setMap] = useState(null);

    const calculateRoute = () => {
        if (!map) return;

        const directionsService = new window.google.maps.DirectionsService();

        const waypoints = mockOrders
            .filter(order => order.status !== "נמסר")
            .map(order => ({ location: order.pickup, stopover: true }));

        if (waypoints.length < 2) return;

        const request = {
            origin: waypoints[0].location,
            destination: waypoints[waypoints.length - 1].location,
            waypoints: waypoints.slice(1, -1),
            travelMode: window.google.maps.TravelMode.DRIVING,
        };

        directionsService.route(request, (result, status) => {
            if (status === window.google.maps.DirectionsStatus.OK) {
                setDirections(result);
            } else {
                console.error("Error fetching directions: ", status);
            }
        });
    };
    console.log("Google Maps API Key:", process.env.REACT_APP_GOOGLE_MAPS_API_KEY2);
    return (
        <LoadScript googleMapsApiKey={process.env.REACT_APP_GOOGLE_MAPS_API_KEY2} libraries={libraries} language="he">
            <h2>הזמנות הלקוח</h2>
            <ul>
                {mockOrders.map(order => (
                    <li key={order.id}>
                        {order.customer} - {order.status} (Pickup: {order.pickup}, Dropoff: {order.dropoff})
                    </li>
                ))}
            </ul>
            <GoogleMap
                center={{ lat: 32.0853, lng: 34.7818 }}
                zoom={10}
                mapContainerStyle={{ width: "100%", height: "500px" }}
                onLoad={(map) => setMap(map)}
            >
                {directions && <DirectionsRenderer directions={directions} />}
            </GoogleMap>
            <button onClick={calculateRoute}>חשב מסלול</button>
        </LoadScript>
    );
};

export default MapComponent;
