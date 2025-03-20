import React, { useState } from 'react';
import "../pageCSS/Signup.css"; // סגנון העיצוב לדף

const BusinessSignUp = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Phone: '',
    Identity: '',
    Email: '',
    Password: '',
    Role: 2,
  });

  const [message, setMessage] = useState<string | null>(null); // סטייט להודעות

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage(null); // מנקה את ההודעה לפני שליחה חדשה
    
    try {
      const response = await fetch(`https://localhost:7013/api/Owner`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        const errorText = await response.text();
        console.error('שגיאה בעת שליחת הנתונים:', errorText);
        setMessage("שגיאה בהרשמה. נסה שוב.");
        return;
      }

      const result = await response.json(); 
      console.log('הנתונים נשלחו בהצלחה:', result);
      
      setMessage("✅ ההרשמה בוצעה בהצלחה! ברוך הבא!");

      // ניתן לנקות את השדות לאחר ההצלחה
      setFormData({
        Name: '',
        Phone: '',
        Identity: '',
        Email: '',
        Password: '',
        Role: 2,
      });

    } catch (error) {
      console.error('שגיאה בקשר עם השרת:', error);
      setMessage("⚠️ שגיאה בחיבור לשרת. נסה מאוחר יותר.");
    }
  };

  return (
    <div className="signup-form">
      <h2>הירשם כבעל מסעדה</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" name="Name" value={formData.Name} onChange={handleChange} placeholder="שם מלא" required />
        <input type="tel" name="Phone" value={formData.Phone} onChange={handleChange} placeholder="טלפון" required />
        <input type="text" name="Identity" value={formData.Identity} onChange={handleChange} placeholder="ת.ז" required />
        <input type="email" name="Email" value={formData.Email} onChange={handleChange} placeholder="אימייל" required />
        <input type="password" name="Password" value={formData.Password} onChange={handleChange} placeholder="סיסמא" required />
        <button type="submit">הירשם</button>
      </form>

      {/* הצגת הודעה בהתאם לתוצאה */}
      {message && <p className={`message ${message.includes("שגיאה") ? "error" : "success"}`}>{message}</p>}
    </div>
  );
};

export default BusinessSignUp;
