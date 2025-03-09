import React, { useState } from 'react';
import "../pageCSS/Signup.css"; // סגנון העיצוב לדף

const CustomerSignUp = () => {
  const [formData, setFormData] = useState({
    fullName: '',
    phone: '',
    idNumber: '',
    email: '',
    cityHome: '',
    addressHome: '',
    cityWork: '',
    addressWork: '',
    creditCardNumber: '',
    expirationDate: '',
    cvv: ''
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // לשלוח את הנתונים לשרת
    console.log(formData);
  };

  return (
    <div className="signup-form">
      <h2>הירשם כלקוח</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" name="fullName" value={formData.fullName} onChange={handleChange} placeholder="שם מלא" required />
        <input type="tel" name="phone" value={formData.phone} onChange={handleChange} placeholder="טלפון" required />
        <input type="text" name="idNumber" value={formData.idNumber} onChange={handleChange} placeholder="ת.ז" required />
        <input type="email" name="email" value={formData.email} onChange={handleChange} placeholder="אימייל" required />
        <input 
        type="password" 
        placeholder="סיסמא" 
        required 
    />
        <h3>כתובת בבית</h3>
        <input type="text" name="cityHome" value={formData.cityHome} onChange={handleChange} placeholder="עיר בבית" />
        <input type="text" name="addressHome" value={formData.addressHome} onChange={handleChange} placeholder="כתובת הבית" />

        <h3>כתובת עבודה (אופציונלית)</h3>
        <input type="text" name="cityWork" value={formData.cityWork} onChange={handleChange} placeholder="עיר בעבודה" />
        <input type="text" name="addressWork" value={formData.addressWork} onChange={handleChange} placeholder="כתובת העבודה" />
        
        <h3>פרטי אשראי</h3>
        <input type="text" name="creditCardNumber" value={formData.creditCardNumber} onChange={handleChange} placeholder="מספר כרטיס אשראי" required />
        <input type="text" name="expirationDate" value={formData.expirationDate} onChange={handleChange} placeholder="תוקף כרטיס" required />
        <input type="text" name="cvv" value={formData.cvv} onChange={handleChange} placeholder="CVV" required />
        
        <button type="submit">הירשם</button>
      </form>
    </div>
  );
};

export default CustomerSignUp;
