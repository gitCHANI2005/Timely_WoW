import React, { useState } from "react";
import "../pageCSS/Login.css"; // סגנון העיצוב לדף

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");

  const handleLogin = async () => {
    // קריאה לצד שרת מדומה
    const response = await mockLogin(email, password);
    if (response.success) {
      setMessage(`התחברת בהצלחה כ-${response.userType}`);
    } else {
      setMessage("שם משתמש או סיסמה שגויים");
    }
  };

  return (
    <div className="login-container">
      <h2>התחברות</h2>
      <input type="email" placeholder="אימייל" value={email} onChange={(e) => setEmail(e.target.value)} />
      <input type="password" placeholder="סיסמה" value={password} onChange={(e) => setPassword(e.target.value)} />
      <button onClick={handleLogin}>התחבר</button>
      {message && <p className="login-message">{message}</p>}
    </div>
  );
};

// חיקוי צד שרת
const mockLogin = async (email: string, password: string) => {
  return new Promise<{ success: boolean; userType?: string }>((resolve) => {
    setTimeout(() => {
      if (email === "customer@example.com" && password === "123456") {
        resolve({ success: true, userType: "לקוח" });
      } else if (email === "business@example.com" && password === "123456") {
        resolve({ success: true, userType: "בעל עסק" });
      } else if (email === "delivery@example.com" && password === "123456") {
        resolve({ success: true, userType: "משלוחן" });
      } else {
        resolve({ success: false });
      }
    }, 1000);
  });
};

export default Login;
