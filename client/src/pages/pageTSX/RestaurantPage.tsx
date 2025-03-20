import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Card, CardContent, Button, Checkbox, FormControlLabel } from "@mui/material"; 
import { Heart } from "lucide-react";
import axios from "axios";
import '../pageCSS/RestaurantPage.css'; 
import Header from "./Header";

export default function RestaurantPage() {
  const { RestaurantId } = useParams(); 
  const [restaurant, setRestaurant] = useState<any>(null);
  const [dishes, setDishes] = useState<any[]>([]);
  const [liked, setLiked] = useState(false);
  const [selectedDish, setSelectedDish] = useState<any>(null);
  const [selectedAddOns, setSelectedAddOns] = useState<any[]>([]);
  const [quantity, setQuantity] = useState(1);
  const [totalPrice, setTotalPrice] = useState(0);
  const [likedDishes, setLikedDishes] = useState<string[]>([]);

  useEffect(() => {
    const fetchRestaurantData = async () => {
      try {
        const res = await axios.get(`https://localhost:7013/api/Store/${RestaurantId}`);
        setRestaurant(res.data); // שמירת נתוני המסעדה
      } catch (error) {
        console.error("שגיאה בטעינת המסעדה:", error);
      }
    };

    const fetchDishesData = async () => {
      try {
        const res = await axios.get(`https://localhost:7013/api/MenuDose/getByIdRestaurant/${RestaurantId}`);
        setDishes(res.data); // שמירת נתוני המנות
      } catch (error) {
        console.error("שגיאה בטעינת המנות:", error);
      }
    };

    fetchRestaurantData();
    fetchDishesData();
  }, [RestaurantId]);

const toggleLike = (dishId: string) => {
  setLikedDishes((prevLikedDishes) => 
    prevLikedDishes.includes(dishId)
      ? prevLikedDishes.filter(id => id !== dishId) // הסרת מנה מהרשימה
      : [...prevLikedDishes, dishId] // הוספת מנה לרשימה
  );
};


  const handleAddOnChange = (event: React.ChangeEvent<HTMLInputElement>, addOn: any) => {
    if (event.target.checked) {
      setSelectedAddOns([...selectedAddOns, addOn]);
    } else {
      setSelectedAddOns(selectedAddOns.filter((item) => item.name !== addOn.name));
    }
  };

  const handleQuantityChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setQuantity(Number(event.target.value));
  };

  const calculateTotalPrice = () => {
    if (!selectedDish) return 0;
    const addOnsPrice = selectedAddOns.reduce((acc: number, addOn: any) => acc + addOn.price, 0);
    return (selectedDish.price + addOnsPrice) * quantity;
  };

  useEffect(() => {
    setTotalPrice(calculateTotalPrice());
  }, [selectedAddOns, quantity, selectedDish]);

  if (!restaurant) return <div>טוען נתוני מסעדה...</div>;
  if (!dishes.length) return(
    <div className="restaurant-container">
      <Header /> 
      <h1 className="restaurant-title">{restaurant.name}</h1>
      <img src={ `data:image/png;base64,${restaurant.image}`} alt={restaurant.name} className="restaurant-image" />
      <p className="no-dishes-message">אין מנות להצגה</p>
    </div>
  );

  return (
    <div className="restaurant-container">
      <Header /> 
      <h1 className="restaurant-title">{restaurant.name}</h1>
      <img src={ `data:image/png;base64,${restaurant.image}`} alt={restaurant.name} className="restaurant-image" />
      <div className="dishes-grid">
        {dishes.map((dish: any) => (
          <Card 
            key={dish.id} 
            className="dish-card" 
            onClick={() => {
              setSelectedDish(dish);
              setSelectedAddOns([]);
              setQuantity(1);
            }}
          >
            <img src={`data:image/png;base64,${dish.image}`} alt={dish.name} className="dish-image" />
            <CardContent>
              <h3 className="dish-title">{dish.name}</h3>
              <p className="dish-description">{dish.description}</p>
              <p className="dish-price">₪{dish.price}</p>
               {/* כפתור אהבתי לכל מנה */}
              <button 
                onClick={(e) => {
                  e.stopPropagation(); // מונע סגירה של המודאל בלחיצה
                  toggleLike(dish.id);
                }} 
                className={`like-button ${likedDishes.includes(dish.id) ? "liked" : ""}`}
              >
                <Heart className="heart-icon" />
                {likedDishes.includes(dish.id) ? "אהבתי" : "אהבתי?"}
              </button>
            </CardContent>
          </Card>
        ))}
      </div>
      {selectedDish && (
        <div className="modal-overlay">
          <div className="modal-content">
                  {/* כפתור סגירה */}
            <button className="close-modal" onClick={() => setSelectedDish(null)}>
              ✖
            </button>

            <img src={`data:image/png;base64,${selectedDish.image}`} alt={selectedDish.name} className="modal-image" />
            <h2 className="modal-title">{selectedDish.name}</h2>
            <p className="modal-description">{selectedDish.description}</p>
            <p className="modal-price">₪{selectedDish.price}</p>

            {/* תוספות */}
            <h3>תוספות</h3>
            {selectedDish.addOns?.map((addOn: any) => (
              <FormControlLabel
                key={addOn.name}
                control={
                  <Checkbox
                    onChange={(e) => handleAddOnChange(e, addOn)}
                    name={addOn.name}
                  />
                }
                label={`${addOn.name} - ₪${addOn.price}`}
              />
            ))}

            {/* בחירת כמות */}
            <div className="quantity-container">
              <label>כמות:</label>
              <input
                type="number"
                value={quantity}
                onChange={handleQuantityChange}
                min={1}
                className="quantity-input"
              />
            </div>

            {/* מחירים */}
            <h3>מחיר סופי: ₪{totalPrice.toFixed(2)}</h3>

            {/* כפתור הוספה להזמנה */}
            <Button
              onClick={() => {
                alert(`הוזמנה מנה ${selectedDish.name} עם ${selectedAddOns.length}`)
                setSelectedDish(null);
                setSelectedAddOns([]);
                setQuantity(1);
                setTotalPrice(0);
              }}
              variant="contained"
              color="primary"
            >
              הוסף להזמנה
            </Button>
          </div>
        </div>
      )}
    </div>
  );
}
