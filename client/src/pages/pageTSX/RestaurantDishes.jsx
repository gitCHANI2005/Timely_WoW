import React, { useState, useEffect } from 'react';
import '../pageCSS/RestaurantDishes.css';

const RestaurantsPage = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [category, setCategory] = useState('');
  const [location, setLocation] = useState('');
  const [restaurants, setRestaurants] = useState([]);
  const [filteredRestaurants, setFilteredRestaurants] = useState([]);
  const [topDishes, setTopDishes] = useState([]);
  const [stores, setStores] = useState([]);
  const [selectedRestaurant, setSelectedRestaurant] = useState(null);
  const [restaurantDishes, setRestaurantDishes] = useState([]);

  useEffect(() => {
    const storedLocation = JSON.parse(localStorage.getItem('userLocation'));
    if (storedLocation) {
      setLocation(storedLocation.address);
    }

    fetch('https://localhost:7013/api/MenuDose')
      .then(response => response.json())
      .then(data => {
        console.log("מנות שהתקבלו מהשרת:", data);
        if (Array.isArray(data)) {
          setTopDishes(data.sort((a, b) => b.avgLikes - a.avgLikes).slice(0, 10));
        } else {
          console.error("מבנה הנתונים אינו כצפוי:", data);
        }
      })
      .catch(error => console.error('שגיאה בשליפת הנתונים:', error));

    fetch('https://localhost:7013/api/Store')
      .then(response => response.json())
      .then(data => {
        console.log("חנויות שהתקבלו מהשרת:", data);
        if (Array.isArray(data)) {
          setStores(data);
          setFilteredRestaurants(data);
        } else {
          console.error("מבנה הנתונים אינו כצפוי:", data);
        }
      })
      .catch(error => console.error('שגיאה בשליפת החנויות:', error));
  }, []);

  useEffect(() => {
    const filtered = stores.filter(store =>
      (searchTerm ? store.name.toLowerCase().includes(searchTerm.toLowerCase()) : true) &&
      (category ? store.category.toLowerCase().includes(category.toLowerCase()) : true)
    );
    setFilteredRestaurants(filtered);
  }, [searchTerm, category, stores]);

  const handleRestaurantClick = (restaurantId) => {
    fetch(`https://localhost:7013/api/MenuDose/getByIdRestaurant/${restaurantId}`)
      .then(response => response.json())
      .then(data => {
        setSelectedRestaurant(restaurantId);
        setRestaurantDishes(data);
      })
      .catch(error => console.error('שגיאה בשליפת המנות:', error));
  };

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
      
      <section className="top-dishes">
        <h3>המנות הפופולריות ביותר</h3>
        <div className="dishes-list">
          {topDishes.map((dish, index) => (
            <div key={index} className="dish-card">
              <img src={dish.Image} alt={dish.name} className="dish-image" />
              <h4>{dish.name}</h4>
              <p>לייקים: {dish.avgLikes}</p>
            </div>
          ))}
        </div>
      </section>

      <section className="restaurants-list">
        <h3>כל המסעדות</h3>
        <div className="restaurant-gallery">
          {filteredRestaurants.map((restaurant, index) => (
            <div key={index} className="restaurant-card" onClick={() => handleRestaurantClick(9)}>
              <img src={restaurant.Image} alt={restaurant.name} className="restaurant-image" />
              <h3>{restaurant.name}</h3>
              <p>{restaurant.category}</p>
            </div>
          ))}
        </div>
      </section>

      {selectedRestaurant && (
        <section className="restaurant-dishes">
          <h3>מנות של {filteredRestaurants.find(r => r.id === selectedRestaurant)?.name}</h3>
          <div className="dishes-list">
            {restaurantDishes.map((dish, index) => (
              <div key={index} className="dish-card">
                <img src={dish.image} alt={dish.name} className="dish-image" />
                <h4>{dish.name}</h4>
                <p>מחיר: {dish.price} ₪</p>
              </div>
            ))}
          </div>
        </section>
      )}
    </div>
  );
};

export default RestaurantsPage;
