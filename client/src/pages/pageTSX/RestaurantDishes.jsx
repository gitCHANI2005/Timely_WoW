import React, { useState, useEffect } from 'react';
import '../pageCSS/RestaurantDishes.css';

const RestaurantsPage = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [category, setCategory] = useState('');
  const [location, setLocation] = useState('');
  const [restaurants, setRestaurants] = useState([]);
  const [filteredRestaurants, setFilteredRestaurants] = useState([]);

  useEffect(() => {
    // שליפת המיקום מ-localStorage
    const storedLocation = JSON.parse(localStorage.getItem('userLocation'));
    if (storedLocation) {
      setLocation(storedLocation.address);
    }

    // קריאה ל-API לשליפת מנות מהתפריט
    fetch('https://localhost:7013/api/MenuDose')
      .then(response => response.json())
      .then(data => {
        console.log("נתונים שהתקבלו מהשרת:", data);
        setRestaurants(data);
        setFilteredRestaurants(data);
      })
      .catch(error => console.error('שגיאה בשליפת הנתונים:', error));
  }, []);

  useEffect(() => {
    // סינון מסעדות על פי שם, קטגוריה ומיקום
    const filtered = restaurants.filter(restaurant =>
      (searchTerm ? restaurant.name.toLowerCase().includes(searchTerm.toLowerCase()) : true) &&
      (category ? restaurant.category.toLowerCase().includes(category.toLowerCase()) : true)
    );

    setFilteredRestaurants(filtered);
  }, [searchTerm, category, restaurants]);

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
          filteredRestaurants.map((restaurant, index) => (
            <div key={index} className="restaurant-card">
              {restaurant.image && <img src={restaurant.image} alt={restaurant.name} className="restaurant-image" />}
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
