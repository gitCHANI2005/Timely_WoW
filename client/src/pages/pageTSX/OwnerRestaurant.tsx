import React, { useState, useEffect } from "react";

// הגדרת טיפוס מותאם אישית בשם Time
interface Time {
  hours: number;
  minutes: number;
}

const OwnerRestaurant: React.FC = () => {
  const [stores, setStores] = useState<any[]>([]); // רשימת החנויות/מסעדות
  const [showAddStoreForm, setShowAddStoreForm] = useState(false); // האם להציג את טופס הוספת חנות
  const [newStore, setNewStore] = useState({
    Name: "",
    Address: "",
    city: { name: "" },
    WeekOpen: undefined as Time | undefined, // שדה טיפוס חדש
    WeekClose: undefined as Time | undefined, // שדה טיפוס חדש
    FridayOpen: undefined as Time | undefined, // שדה טיפוס חדש
    FridayClose: undefined as Time | undefined, // שדה טיפוס חדש
    ShabbatOpen: undefined as Time | undefined, // שדה טיפוס חדש
    ShabbatClose: undefined as Time | undefined, // שדה טיפוס חדש
  });

  // פונקציה להמיר Time לפורמט של שעה:דקה (HH:mm)
  const timeToString = (time: Time | undefined): string => {
    if (!time) return ''; // אם אין ערך אז נחזיר ריק
    const hours = time.hours.toString().padStart(2, '0'); // שמים אפס אם השעה קטנה מ-10
    const minutes = time.minutes.toString().padStart(2, '0'); // שמים אפס אם הדקות קטנות מ-10
    return `${hours}:${minutes}`;
  };

  // דמוי חיבור ל-API לקבלת רשימת החנויות
  useEffect(() => {
    const fetchStores = async () => {
      try {
        const response = await fetch("/api/stores"); // כתובת ה-API שלך
        const data = await response.json();
        setStores(data); // עדכון הסטייט עם הנתונים שהתקבלו
      } catch (error) {
        console.error("שגיאה בטעינת החנויות:", error);
      }
    };

    fetchStores();
  }, []);

  // פונקציה להוספת חנות חדשה
  const addStore = async () => {
    try {
      const response = await fetch("/api/stores", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newStore), // שולחים את המידע של החנות
      });

      if (response.ok) {
        const addedStore = await response.json();
        setStores([...stores, addedStore]); // הוספת החנות החדשה לרשימה
        setShowAddStoreForm(false); // סגירת הטופס לאחר הוספת החנות
      } else {
        console.error("שגיאה בהוספת החנות");
      }
    } catch (error) {
      console.error("שגיאה בהוספת החנות:", error);
    }
  };

  return (
    <div className="owner-dashboard">
      <h1>ברוך הבא לעסק שלך!</h1>

      <button
        className="view-stores-button"
        onClick={() => setShowAddStoreForm(!showAddStoreForm)}
      >
        {showAddStoreForm ? "הסתר את טופס הוספת חנות" : "הוסף חנות חדשה"}
      </button>

      {showAddStoreForm && (
        <div className="add-store-form">
          <h2>הוסף חנות חדשה</h2>
          <label>שם החנות:</label>
          <input
            type="text"
            value={newStore.Name}
            onChange={(e) => setNewStore({ ...newStore, Name: e.target.value })}
          />
          <label>כתובת:</label>
          <input
            type="text"
            value={newStore.Address}
            onChange={(e) => setNewStore({ ...newStore, Address: e.target.value })}
          />
          <label>עיר:</label>
          <input
            type="text"
            value={newStore.city?.name}
            onChange={(e) =>
              setNewStore({ ...newStore, city: { name: e.target.value } })
            }
          />
          <label>שעות פתיחה במהלך השבוע:</label>
          <input
            type="time"
            value={newStore.WeekOpen ? timeToString(newStore.WeekOpen) : ""}
            onChange={(e) =>
              setNewStore({
                ...newStore,
                WeekOpen: {
                  hours: parseInt(e.target.value.split(":")[0], 10),
                  minutes: parseInt(e.target.value.split(":")[1], 10),
                },
              })
            }
          />
          <label>שעות סגירה במהלך השבוע:</label>
          <input
            type="time"
            value={newStore.WeekClose ? timeToString(newStore.WeekClose) : ""}
            onChange={(e) =>
              setNewStore({
                ...newStore,
                WeekClose: {
                  hours: parseInt(e.target.value.split(":")[0], 10),
                  minutes: parseInt(e.target.value.split(":")[1], 10),
                },
              })
            }
          />
          {/* הוספת שדות נוספים לפי הצורך */}

          <button onClick={addStore}>הוסף חנות</button>
        </div>
      )}

      <div className="store-list">
        <h2>החנויות שלך:</h2>
        {stores.length > 0 ? (
          <ul>
            {stores.map((store, index) => (
              <li key={index}>
                <h3>{store.Name}</h3>
                <p>כתובת: {store.Address}</p>
                <p>עיר: {store.city?.name}</p>
              </li>
            ))}
          </ul>
        ) : (
          <p>אין חנויות בבעלותך.</p>
        )}
      </div>
    </div>
  );
};

export default OwnerRestaurant;
