import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Card, CardContent, Button, Checkbox, FormControlLabel } from "@mui/material"; // שימוש ב-MUI
import { Heart } from "lucide-react";
import '../pageCSS/RestaurantPage.css'; 
import zisalek from '../../picture/zisalek.png'
import ice2 from '../../picture/iceCream2.jpg'
import iceCafe from '../../picture/iceCoffe.webp'

export default function RestaurantPage() {
  const { name } = useParams();
  const [restaurant, setRestaurant] = useState<any>(null);
  const [liked, setLiked] = useState(false);
  const [selectedDish, setSelectedDish] = useState<any>(null);
  const [selectedAddOns, setSelectedAddOns] = useState<any[]>([]);
  const [quantity, setQuantity] = useState(1); // כמות המנות שנבחרה
  const [totalPrice, setTotalPrice] = useState(0); // המחיר הסופי

  // Mock Data של המסעדה
  const mockRestaurantData = {
    name: "זיסלק",
    imgUrl: zisalek,
    dishes: [
      {
        id: 1,
        name: "גלידת 3 צבעים",
        description: "גלידת 3 צבעים אירופאית",
        price: 42,
        image: ice2,
        addOns: [
          { name: "קצפת", price: 5 },
          { name: "שוקולד חם", price: 8 },
          { name: "סוכריות צבעוניות", price: 4 },
        ]
      },
      {
        id: 2,
        name: "אייס קפה",
        description: "אייס קפה עם קצפת",
        price: 25,
        image: iceCafe,
        addOns: [
          { name: "קצפת", price: 5 },
          { name: "חלב שוקולד", price: 6 },
        ]
      }
    ],
    fridayOpen: 1080,  // 18:00
    fridayClose: 1320, // 22:00
    shabbatOpen: 1080, // 18:00
    shabbatClose: 1320, // 22:00
    weekOpen: 720,  // 12:00
    weekClose: 1320 // 22:00
  };

  useEffect(() => {
    // במקום קריאה אמיתית לשרת, נשתמש בנתונים מדומים
    setTimeout(() => {
      setRestaurant(mockRestaurantData);  // נדמה שהנתונים נטענים
    }, 1000);  // השהייה של שנייה לדימוי של טעינה
  }, [name]);

  const toggleLike = async () => {
    setLiked(!liked);
  };

  const isOpen = () => {
    const now = new Date();
    const day = now.getDay();
    const time = now.getHours() * 60 + now.getMinutes();
    let open, close;

    if (day === 5) {
      open = restaurant.fridayOpen;
      close = restaurant.fridayClose;
    } else if (day === 6) {
      open = restaurant.shabbatOpen;
      close = restaurant.shabbatClose;
    } else {
      open = restaurant.weekOpen;
      close = restaurant.weekClose;
    }

    return time >= open && time <= close
      ? `פתוח עד ${Math.floor(close / 60)}:${close % 60}`
      : `סגור - ייפתח ב-${Math.floor(open / 60)}:${open % 60}`;
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
  
  

  if (!restaurant) return <div>טוען...</div>;

  return (
    <div className="restaurant-container">
      <h1 className="restaurant-title">{restaurant.name}</h1>
      <img src={restaurant.imgUrl} alt={restaurant.name} className="restaurant-image" />
      <p className="restaurant-status">{isOpen()}</p>
      <div className="dishes-grid">
        {restaurant.dishes.map((dish: any) => (
         <Card 
         key={dish.id} 
         className="dish-card" 
         onClick={() => {
           setSelectedDish(dish);
           setSelectedAddOns([]); // איפוס תוספות
           setQuantity(1); // איפוס כמות
         }}
       >
       
            <img src={dish.image} alt={dish.name} className="dish-image" />
            <CardContent>
              <h3 className="dish-title">{dish.name}</h3>
              <p className="dish-description">{dish.description}</p>
              <p className="dish-price">₪{dish.price}</p>
            </CardContent>
          </Card>
        ))}
      </div>

      <button onClick={toggleLike} className={`like-button ${liked ? "liked" : ""}`}>
        <Heart className="heart-icon" />
        {liked ? "אהבתי" : "אהבתי?"}
      </button>

      {selectedDish && (
        <div className="modal-overlay">
          <div className="modal-content">
            <img src={selectedDish.image} alt={selectedDish.name} className="modal-image" />
            <h2 className="modal-title">{selectedDish.name}</h2>
            <p className="modal-description">{selectedDish.description}</p>
            <p className="modal-price">₪{selectedDish.price}</p>

        {/* תוספות */}
        <h3>תוספות</h3>
          {selectedDish.addOns && selectedDish.addOns.map((addOn: any) => (
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
              alert(`הוזמנה מנה ${selectedDish.name} עם ${selectedAddOns.length} תוספות!`);
              setSelectedDish(null);
              setSelectedAddOns([]);
              setQuantity(1);
              setTotalPrice(0); // איפוס המחיר הסופי
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
