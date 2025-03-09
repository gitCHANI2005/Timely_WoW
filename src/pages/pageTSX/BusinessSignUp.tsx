import React, { useState } from 'react';
import "../pageCSS/Signup.css"; // סגנון העיצוב לדף

const BusinessSignUp = () => {
  const [formData, setFormData] = useState({
    fullName: '',
    phone: '',
    idNumber: '',
    email: ''
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // שליחת בקשה לשרת
    console.log(formData);
  };

  return (
    <div className="signup-form">
      <h2>הירשם כבעל מסעדה</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" name="fullName" value={formData.fullName} onChange={handleChange} placeholder="שם מלא" required />
        <input type="tel" name="phone" value={formData.phone} onChange={handleChange} placeholder="טלפון" required />
        <input type="text" name="idNumber" value={formData.idNumber} onChange={handleChange} placeholder="ת.ז" required />
        <input type="email" name="email" value={formData.email} onChange={handleChange} placeholder="אימייל" required />
        <button type="submit">הירשם</button>
      </form>
    </div>
  );
};

export default BusinessSignUp;

