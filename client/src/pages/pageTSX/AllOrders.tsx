import React, { useState } from "react";
import "../pageCSS/AllOrders.css"; // קובץ העיצוב

interface Order {
  id: number;
  customerName: string;
  address: string;
  city: string;
  status: string;
}

// יצירת נתונים מדומים (Mock Data)
const mockOrders: Order[] = [
  { id: 1, customerName: "דוד לוי", address: "רחוב הרצל 10", city: "תל אביב", status: "pending" },
  { id: 2, customerName: "רונית כהן", address: "רחוב בן גוריון 5", city: "חיפה", status: "pending" },
  { id: 3, customerName: "משה אברג'יל", address: "שדרות רוטשילד 22", city: "ירושלים", status: "pending" },
];

const AllOrders: React.FC = () => {
  const [orders, setOrders] = useState<Order[]>(mockOrders);

  const handleTakeOrder = (orderId: number) => {
    alert(`הזמנה ${orderId} נלקחה!`);
    setOrders((prevOrders) => prevOrders.filter((order) => order.id !== orderId));
  };

  return (
    <div className="orders-container">
      {orders.length === 0 ? (
        <p className="no-orders">🎉 אין הזמנות פתוחות כרגע!</p>
      ) : (
        orders.map((order) => (
          <div key={order.id} className="order-card">
            <h3>🛒 הזמנה #{order.id}</h3>
            <p>👤 {order.customerName}</p>
            <p>📍 {order.address}, {order.city}</p>
            <button onClick={() => handleTakeOrder(order.id)}>🚀 לקחתי</button>
          </div>
        ))
      )}
    </div>
  );
};

export default AllOrders;
