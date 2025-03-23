import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { jwtDecode } from "jwt-decode"; 
import "../pageCSS/Login.css";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    setMessage("");
  }, [email, password]);

  const handleLogin = async () => {
    if (!email || !password) {
      setMessage("יש למלא את כל השדות");
      return;
    }
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(email)) {
      setMessage("כתובת אימייל לא חוקית");
      return;
    }

    setLoading(true);
    try {
      const response = await fetch('https://localhost:7013/login', {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });

      if (!response.ok) {
        setMessage("שם משתמש או סיסמה שגויים");
        setLoading(false);
        return;
      }

      // קבלת הטוקן כמחרוזת
      const token = await response.text();
      console.log("Received Token:", token);

      // שמירה של הטוקן ב-localStorage
      localStorage.setItem("token", token);

      // פענוח הטוקן
      interface MyJwtPayload {
        role: string;
        unique_name: string;
        email: string;
      }

      const decodedToken = jwtDecode<MyJwtPayload>(token);
      console.log("Decoded Token:", decodedToken);


      // הפנייה לפי תפקיד המשתמש
      switch (decodedToken.role) {
        case "admin":
          navigate("/adminDashboard");
          break;
        case "customer":
          navigate("/RestaurantDishes");
          break;
        case "owner":
          navigate("/OwnerRestaurant");
          break;
        case "deliver":
          navigate("/DeliverMyOrders");
          break;
        default:
          navigate("/home");
      }
    } catch (error) {
      console.error("Error during login:", error);
      setMessage("שגיאה בעת התחברות");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-container">
      <h2>התחברות</h2>
      <input
        type="email"
        placeholder="אימייל"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      <input
        type="password"
        placeholder="סיסמה"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin} disabled={loading}>
        {loading ? "טוען..." : "התחבר"}
      </button>
      {message && <p className="login-message">{message}</p>}
    </div>
  );
};

export default Login;
