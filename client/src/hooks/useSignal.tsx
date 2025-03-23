import { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr"; // יבוא של ספריית SignalR

// הגדרת טיפוס להזמנה
interface Order {
  address: string;   
  customerName: string; 
  orderId: string;
  deliveryDate: string; 
  items: string[];
  city: string;
}

// הגדרת טיפוס למשלוחן
interface Deliver {
  id: number;
  name: string;
  city: string;  // העיר שבה המשלוחן פועל
}

// ההוק משתמש ב-SignalR ומחזיר את רשימת ההזמנות
const useSignal = (area: string, deliveryDrivers: Deliver[]): { orders: Order[] } => {
  const [orders, setOrders] = useState<Order[]>([]); // יצירת מצב להזמנות
  const [isConnected, setIsConnected] = useState<boolean>(false); // מצב חיבור ל-SignalR

  useEffect(() => {
    // אם האזור לא תקין (ריק או null), אל נתחבר ל-SignalR
    if (!area) {
      console.error("הערך של האזור לא תקין.");
      return;
    }

    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7013/chatHub", {
        transport: signalR.HttpTransportType.WebSockets | 
                   signalR.HttpTransportType.ServerSentEvents | 
                   signalR.HttpTransportType.LongPolling
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    newConnection
      .start()
      .then(() => {
        console.log("התחבר ל-SignalR בהצלחה");
        setIsConnected(true);  // עדכון מצב החיבור
        return newConnection.invoke("JoinGroup", area);  // הצטרפות לקבוצה רק אם יש ערך תקין
      })
      .catch((err) => {
        console.error("שגיאה בחיבור ל-SignalR: ", err);
        setIsConnected(false);  // אם יש שגיאה, מצב החיבור נשאר false
      });

    newConnection.on("NewOrder", (order: Order) => {
      setOrders((prevOrders) => [...prevOrders, order]);

      // שליחת התראה למשלוחנים הרלוונטיים
      sendNotificationsForOrder(order);
    });

    return () => {
      newConnection.stop().then(() => console.log("התנתק מ-SignalR"));
    };

  }, [area, deliveryDrivers]); // בכל פעם שהאזור או המשלוחנים משתנים, החיבור מתחדש

  // פונקציה לשליחת התראות למשלוחנים
  const sendNotificationsForOrder = (order: Order) => {
    // פילטרים את המשלוחנים לפי העיר שהוזנה בהזמנה
    const relevantDrivers = deliveryDrivers.filter(driver => driver.city === order.city);
  
    // שולחים התראה לכל המשלוחנים שתואמים לעיר
    relevantDrivers.forEach(driver => {
      sendNotification(driver.id, `הזמנה חדשה מ-${order.customerName} בעיר ${order.city}, פריטים: ${order.items.join(", ")}`);
    });
  };

  // פונקציה לשליחת התראה למשלוחן
  const sendNotification = (driverId: number, message: string) => {
    console.log(`שלח התראה למשלוחן ${driverId}: ${message}`);
  };

  return { orders }; // מחזיר את רשימת ההזמנות
};

export default useSignal;
