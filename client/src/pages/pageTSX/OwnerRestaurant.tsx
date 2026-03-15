import React, { useState, useEffect } from "react";

// Define a custom type named Time
interface Time {
  hours: number;
  minutes: number;
}

const OwnerRestaurant: React.FC = () => {
  const [stores, setStores] = useState<any[]>([]); // List of stores/restaurants
  const [showAddStoreForm, setShowAddStoreForm] = useState(false); // Whether to show the add store form
  const [newStore, setNewStore] = useState({
    Name: "",
    Address: "",
    city: { name: "" },
    WeekOpen: undefined as Time | undefined, // New typed field
    WeekClose: undefined as Time | undefined, // New typed field
    FridayOpen: undefined as Time | undefined, // New typed field
    FridayClose: undefined as Time | undefined, // New typed field
    ShabbatOpen: undefined as Time | undefined, // New typed field
    ShabbatClose: undefined as Time | undefined, // New typed field
  });

  // Function to convert Time to HH:mm string format
  const timeToString = (time: Time | undefined): string => {
    if (!time) return ''; // Return empty string if no value exists
    const hours = time.hours.toString().padStart(2, '0'); // Add leading zero if hour is less than 10
    const minutes = time.minutes.toString().padStart(2, '0'); // Add leading zero if minutes are less than 10
    return `${hours}:${minutes}`;
  };

  // Simulated API connection to fetch the list of stores
  useEffect(() => {
    const fetchStores = async () => {
      try {
        const response = await fetch("/api/stores"); // Your API endpoint
        const data = await response.json();
        setStores(data); // Update state with the received data
      } catch (error) {
        console.error("שגיאה בטעינת החנויות:", error);
      }
    };

    fetchStores();
  }, []);

  // Function to add a new store
  const addStore = async () => {
    try {
      const response = await fetch("/api/stores", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(newStore), // Send the store data
      });

      if (response.ok) {
        const addedStore = await response.json();
        setStores([...stores, addedStore]); // Add the new store to the list
        setShowAddStoreForm(false); // Close the form after adding the store
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
          {/* Add more fields as needed */}

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