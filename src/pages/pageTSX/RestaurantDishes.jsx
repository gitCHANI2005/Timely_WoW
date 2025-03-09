import React, { useState, useEffect } from 'react';
import sampleRestaurants from '../../sampleRestaurants';
import '../pageCSS/RestaurantDishes.css';

const RestaurantsPage = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [category, setCategory] = useState('');
  const [location, setLocation] = useState('');
  const [filteredRestaurants, setFilteredRestaurants] = useState([]);

  useEffect(() => {
    // שליפת המיקום מ-localStorage
    const storedLocation = localStorage.getItem('userLocation');
    if (storedLocation) {
      setLocation(storedLocation);
    }

    // סינון מסעדות לפי המיקום
    const filtered = sampleRestaurants.filter(restaurant => (
      (storedLocation ? restaurant.location.includes(storedLocation) : true) &&
      (restaurant.name.toLowerCase().includes(searchTerm.toLowerCase())) &&
      (category ? restaurant.category.toLowerCase().includes(category.toLowerCase()) : true)
    ));
    setFilteredRestaurants(filtered);
  }, [searchTerm, category, location]);

  return (
    <div className="restaurants-page">
      <header className="search-header">
        <h2>מסעדות באזור {location}</h2>
        <input
          type="text"
          placeholder="חפש מסעדה לפי שם"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="search-input"
        />
        <input
          type="text"
          placeholder="קטגוריה (לדוג' פיצה, אסייתי)"
          value={category}
          onChange={(e) => setCategory(e.target.value)}
          className="category-input"
        />
      </header>
      
      <section className="restaurants-list">
        {filteredRestaurants.length > 0 ? (
          filteredRestaurants.map((restaurant, key) => (
            <div key={key} className="restaurant-card">
              <img src={restaurant.image} alt={restaurant.name} className="restaurant-image" />
              <h3>{restaurant.name}</h3>
              <p>{restaurant.category}</p>
            </div>
          ))
        ) : (
          <p className="no-results">לא נמצאו תוצאות</p>
        )}
      </section>
    </div>
  );
};

export default RestaurantsPage;
