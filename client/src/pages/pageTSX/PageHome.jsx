import React, { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { Autocomplete, useJsApiLoader } from '@react-google-maps/api';
import '../pageCSS/PageHome.css';

const libraries = ['places'];
const HomePage = () => {
  const navigate = useNavigate();
  const [address, setAddress] = useState('');
  const [location, setLocation] = useState(null);
  const [error, setError] = useState('');
  const [restaurants, setRestaurants] = useState([]);
  const autocompleteRef = useRef(null);

  const { isLoaded } = useJsApiLoader({
    googleMapsApiKey: "AIzaSyD2npkJc4VTXAojiWWCAxBEjSC4R40HkSo",
    libraries,
    language: "he"
  });

  useEffect(() => {
    fetch(`https://localhost:7013/api/Store`)
      .then((response) => response.json())
      .then((data) => {
        console.log("Data received:", data); // הדפסת הנתונים שהתקבלו
  
        const formattedData = data.map((restaurant) => ({
          ...restaurant,
          image: restaurant.image ? `data:image/png;base64,${restaurant.image}` : null,
        }));
  
        console.log("Formatted Data:", formattedData); // הדפסת הנתונים לאחר עיבוד
  
        setRestaurants(formattedData);
      })
      .catch((error) => console.error('Error fetching restaurants:', error));
  }, []);
  

  const arrayBufferToBase64 = (buffer) => {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    bytes.forEach((byte) => {
      binary += String.fromCharCode(byte);
    });
    return btoa(binary);
  };

  const onLoad = (autocomplete) => {
    autocomplete.setComponentRestrictions({ country: 'IL' });
    autocompleteRef.current = autocomplete;
  };

  const onPlaceChanged = () => {
    if (autocompleteRef.current) {
      const place = autocompleteRef.current.getPlace();
      if (place.geometry) {
        const selectedLocation = {
          address: place.formatted_address,
          lat: place.geometry.location.lat(),
          lon: place.geometry.location.lng(),
        };
  
        localStorage.setItem('userLocation', JSON.stringify(selectedLocation)); // שמירה ב-localStorage
        navigate('/RestaurantDishes'); // מעבר לדף החדש
      } else {
        setError('כתובת לא תקפה, נסו שוב');
      }
    }
  };
  

  return (
    <div className="home-page">
      <header className="home-header">
        <div className="auth-buttons">
          <button className="login-btn" onClick={() => navigate('/login')}>התחברות</button>
          <button className="signup-btn" onClick={() => navigate('/signup/customer')}>הרשמה</button>
        </div>
      </header>

      <section className="location-section">
        <h2>איפה אתה?</h2>
        {isLoaded ? (
          <Autocomplete onLoad={onLoad} onPlaceChanged={onPlaceChanged}>
            <input
              type="text"
              placeholder="הכנס את המיקום שלך"
              className="location-input"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
            />
          </Autocomplete>
        ) : (
          <p>טוען את הכתובת...</p>
        )}
        {error && <p className="error-message">{error}</p>}
        {location && (
          <p className="success-message">
            המיקום נמצא: רוחב: {location.lat}, אורך: {location.lon}
          </p>
        )}
      </section>

      <section className="restaurants-section">
        <h3>מסעדות שותפות</h3>
        <div className="restaurants-grid">
          {restaurants.length > 0 ? (
            restaurants.map((restaurant) => (
              <div key={restaurant.id} className="restaurant-card">
                {restaurant.image ? (
                  <img src={restaurant.image} alt={restaurant.name} />
                ) : null} 
                <p>{restaurant.name}</p>
              </div>
            ))
          ) : (
            <p>טוען מסעדות...</p>
          )}
        </div>
      </section>

      <section className="cta-section">
        <button className="cta-btn delivery-btn" onClick={() => navigate('/signup/delivery')}>
          משלוחנים? הצטרפו עכשיו!
        </button>
        <button className="cta-btn restaurant-btn" onClick={() => navigate('/signup/business')}>
          מסעדה חדשה? הצטרפו אלינו!
        </button>
      </section>
    </div>
  );
};

export default HomePage;
