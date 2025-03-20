import React, { useState } from 'react';
import axios from 'axios';
const DeliverMyOrders = () => {
  const [origin, setOrigin] = useState("ירושלים");
  const [orders, setOrders] = useState([
    { store: "ירושלים", customer: "חיפה" },
    { store: "תל אביב", customer: "ראשון לציון" },
  ]);
  const [routeSteps, setRouteSteps] = useState([]);

  const handleGetRoute = async () => {
    // איסוף כל הנקודות במסלול: חנויות ולקוחות
    const waypoints = orders.flatMap(order => [order.store, order.customer]);

    try {
      const response = await axios.post("https://localhost:7013/api/directions/optimized-route", [
        "ירושלים",
        "תל אביב",
        "חיפה",
        "צפת"
      ]);
      
      console.log("Response:", response.data);
      const route = response.data.routes[0];
      console.log(route);
      const steps = route.legs.flatMap(leg => leg.steps.map(step => step.html_instructions));
      setRouteSteps(steps);
    } catch (err) {
      console.error("שגיאה בקבלת מסלול:", err);
      alert("שגיאה בקבלת מסלול");
    }
  };

  return (
    <div>
      <h2>מסלול משלוחים</h2>

      <div>
        <label>כתובת התחלתית (משלוחן): </label>
        <input value={origin} onChange={e => setOrigin(e.target.value)} />
      </div>

      <h3>הזמנות:</h3>
      {orders.map((order, index) => (
        <div key={index}>
          <div>🛍 חנות: {order.store}</div>
          <div>📦 לקוח: {order.customer}</div>
        </div>
      ))}

      <button onClick={handleGetRoute}>חשב מסלול ממוטב</button>

      <h3>שלבי המסלול:</h3>
      <ol>
        {routeSteps.map((step, index) => (
          <li key={index} dangerouslySetInnerHTML={{ __html: step }} />
        ))}
      </ol>
    </div>
  );
};

export default DeliverMyOrders;
