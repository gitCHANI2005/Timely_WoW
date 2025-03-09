import React, { useState } from 'react';
import "../pageCSS/Signup.css"; // סגנון העיצוב לדף
const DeliverySignUp = () => {
  const [formData, setFormData] = useState({
    fullName: '',
    phone: '',
    idNumber: '',
    email: '',
    birthDate: '',
    city: '',
    vehicleNumber: '',
    bankNumber: '',
    bankAccount: '',
    bankBranch: ''
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
      <h2>הירשם למשלוחן</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" name="fullName" value={formData.fullName} onChange={handleChange} placeholder="שם מלא" required />
        <input type="tel" name="phone" value={formData.phone} onChange={handleChange} placeholder="טלפון" required />
        <input type="text" name="idNumber" value={formData.idNumber} onChange={handleChange} placeholder="ת.ז" required />
        <input type="email" name="email" value={formData.email} onChange={handleChange} placeholder="אימייל" required />
        <input type="date" name="birthDate" value={formData.birthDate} onChange={handleChange} placeholder="תאריך לידה" />
        <input type="text" name="city" value={formData.city} onChange={handleChange} placeholder="עיר לעבוד בה" />
        <input type="text" name="vehicleNumber" value={formData.vehicleNumber} onChange={handleChange} placeholder="מספר לוחית רכב" />
        
        <h3>פרטי בנק</h3>
        <input type="text" name="bankNumber" value={formData.bankNumber} onChange={handleChange} placeholder="מספר בנק" />
        <input type="text" name="bankAccount" value={formData.bankAccount} onChange={handleChange} placeholder="מספר חשבון בנק" />
        <input type="text" name="bankBranch" value={formData.bankBranch} onChange={handleChange} placeholder="מס סניף בנק" />
        
        <button type="submit">הירשם</button>
      </form>
    </div>
  );
};

export default DeliverySignUp;
