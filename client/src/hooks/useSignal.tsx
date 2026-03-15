import { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr"; // Import SignalR library

// Define type for Order
interface Order {
  address: string;   
  customerName: string; 
  orderId: string;
  deliveryDate: string; 
  items: string[];
  city: string;
}

// Define type for Delivery Driver
interface Deliver {
  id: number;
  name: string;
  city: string;  // The city where the delivery driver operates
}

// This hook uses SignalR and returns the list of orders
const useSignal = (area: string, deliveryDrivers: Deliver[]): { orders: Order[] } => {
  const [orders, setOrders] = useState<Order[]>([]); // Create state for orders
  const [isConnected, setIsConnected] = useState<boolean>(false); // SignalR connection state

  useEffect(() => {
    // If the area is invalid (empty or null), do not connect to SignalR
    if (!area) {
      console.error("Invalid area value.");
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
        console.log("Successfully connected to SignalR");
        setIsConnected(true);  // Update connection state
          return newConnection.invoke("JoinGroup", area);  // Join group only if area is valid
      })
      .catch((err) => {
        console.error("Error connecting to SignalR: ", err);
        setIsConnected(false);  // If there is an error, keep connection state as false
      });

    newConnection.on("NewOrder", (order: Order) => {
      setOrders((prevOrders) => [...prevOrders, order]);

      // Send notification to relevant delivery drivers
      sendNotificationsForOrder(order);
    });

    return () => {
      newConnection.stop().then(() => console.log("Disconnected from SignalR"));
    };

  }, [area, deliveryDrivers]); // Reconnect whenever area or delivery drivers change

  // Function to send notifications to delivery drivers
  const sendNotificationsForOrder = (order: Order) => {
    // Filter delivery drivers by the city specified in the order
    const relevantDrivers = deliveryDrivers.filter(driver => driver.city === order.city);
  
    // Send notification to all delivery drivers matching the city
    relevantDrivers.forEach(driver => {
      sendNotification(driver.id, `New order from ${order.customerName} in ${order.city}, items: ${order.items.join(", ")}`);
    });
  };

  // Function to send notification to a delivery driver
  const sendNotification = (driverId: number, message: string) => {
    console.log(`Sent notification to driver ${driverId}: ${message}`);
  };

  return { orders }; // Return the list of orders
};

export default useSignal;
