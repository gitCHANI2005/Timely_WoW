import { useEffect, useState, useCallback } from "react";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import useSignalR from "../../hooks/useSignal";
import "../pageCSS/DeliveryOrders.css";

const DeliverMyOrders = () => {
  const [origin, setOrigin] = useState("");
  const [routeSteps, setRouteSteps] = useState([]);
  const [area, setArea] = useState("");
  const [showAreaInput, setShowAreaInput] = useState(false);
  const [orders, setOrders] = useState([]);

  const signalROrders = useSignalR(area);

  const getDeliveryIdFromToken = useCallback(() => {
    const token = localStorage.getItem("token");
    if (token) {
      const decodedToken = jwtDecode(token);
      return decodedToken["user_id"];
    }
    return null;
  }, []);

  const fetchOrders = useCallback(async () => {
    const deliverId = getDeliveryIdFromToken();
    if (!deliverId) {
      alert("לא נמצא מזהה למשלוחן.");
      return;
    }

    try {
      const response = await axios.get(`https://localhost:7013/api/order/by-deliver/${deliverId}`);
      if (Array.isArray(response.data)) {
        setOrders(response.data);
      } else {
        setOrders([]);
        alert("לא נמצאו הזמנות.");
      }
    } catch (err) {
      console.error("שגיאה בהבאת ההזמנות:", err);
      alert("שגיאה בהבאת ההזמנות");
    }
  }, [getDeliveryIdFromToken]);

  useEffect(() => {
    fetchOrders();
  }, [fetchOrders]);

  useEffect(() => {
    if (signalROrders.orders) {
      setOrders(signalROrders.orders);
    }
  }, [signalROrders.orders]);

  const handleGetRoute = async () => {
    const deliverId = getDeliveryIdFromToken();
    if (!deliverId) {
      alert("לא נמצא מזהה למשלוחן.");
      return;
    }

    const waypoints = orders.flatMap(order => [order.store, order.customer]);
    try {
      const response = await axios.post(`https://localhost:7013/api/orders/by-deliver/${deliverId}`, waypoints);
      const route = response.data.routes[0];
      const steps = route.legs.flatMap(leg => leg.steps.map(step => step.html_instructions));
      setRouteSteps(steps);
    } catch (err) {
      console.error("שגיאה בקבלת מסלול:", err);
      alert("שגיאה בקבלת מסלול");
    }
  };

  const handleJoinGroup = () => {
    if (area) {
      console.log(`הצטרפת לקבוצה לאזור: ${area}`);
    }
  };

  return (
    <div className="deliver-my-orders-page">
      <div className="location-section">
        <h2>מסלול משלוחים</h2>
        <input
          className="location-input"
          value={origin}
          onChange={e => setOrigin(e.target.value)}
          placeholder="הכנס כתובת התחלתית"
        />
      </div>
      <div className="cta-section">
        {!showAreaInput && (
          <button className="cta-btn" onClick={() => setShowAreaInput(true)}>
            עוד לא התחברת לצ'אט ההזמנות?
          </button>
        )}
      </div>
      {showAreaInput && (
        <div className="area-section">
          <input
            type="text"
            value={area}
            onChange={(e) => setArea(e.target.value)}
            placeholder="הכנס את שם האזור"
          />
          <button className="cta-btn" onClick={handleJoinGroup}>
            הצטרף לקבוצה
          </button>
        </div>
      )}
      <div className="orders-section">
        {orders.length === 0 ? (
          <p>אין הזמנות להציג כרגע.</p>
        ) : (
          orders.map((order, index) => (
            <div key={index} className="order-card">
              <div>חנות איסוף: {order.store || "לא צוין"}</div>
              <div>כתובת לקוח: {order.customer || "לא צוין"}</div>
              <div>סטטוס: {order.status === 0 ? "לא הושלם" : "הושלם"}</div>
            </div>
          ))
        )}
      </div>
      <div className="cta-section">
        <button className="delivery-btn" onClick={handleGetRoute}>
          חשב את המסלול הממוטב
        </button>
      </div>
      <div className="cta-section">
        <h3>שלבי המסלול:</h3>
        <ol>
          {routeSteps.map((step, index) => (
            <li key={index} dangerouslySetInnerHTML={{ __html: step }} />
          ))}
        </ol>
      </div>
    </div>
  );
};

export default DeliverMyOrders;
