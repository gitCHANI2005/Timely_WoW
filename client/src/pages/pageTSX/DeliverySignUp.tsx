import React, { useState, useEffect } from 'react';
import "../pageCSS/Signup.css"; // Styling for the page

// Define a type for a city
interface City {
  id: string;
  name: string;
}

const DeliverySignUp = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Phone: '',
    Identity: '',
    Password: '',
    Email: '',
    Role: 1,
    DateOfBirth: '',
    IsActive: true,
    cityName: '',
    cityId: '',  // Hidden field to store the city ID
    NumOfCar: '',
    BankNumber: '',
    BankAccount: '',
    BankBranch: '',
  });

  const [cities, setCities] = useState<City[]>([]); // List of cities from the server
  const [message, setMessage] = useState('');

  // Load the list of cities from the server
  useEffect(() => {
    const fetchCities = async () => {
      try {
        const response = await fetch(`https://localhost:7013/api/City`);
        if (!response.ok) throw new Error("שגיאה בטעינת ערים");
        const data: City[] = await response.json(); // Explicit typing for the city list
        setCities(data);
      } catch (error) {
        console.error("❌ שגיאה בטעינת הערים:", error);
      }
    };

    fetchCities();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    
    if (name === "cityName") {
      // Directly update the city ID according to the selected city name
      const selectedCity = cities.find(city => city.name === value);
      setFormData(prevData => ({
        ...prevData,
        cityName: value,
        cityId: selectedCity ? selectedCity.id : '', // If no city is found, store an empty string
      }));
    } else {
      setFormData(prevData => ({
        ...prevData,
        [name]: value,
      }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    console.log("📤 שולח נתונים:", formData); 

    try {
      const response = await fetch(`https://localhost:7013/api/Deliver`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        setMessage('הייתה בעיה בהרשמה, נסה שנית.');
        console.error("❌ שגיאה בשרת:", await response.text());
        return;
      }

      const token = await response.text(); 
      if (token) {
        localStorage.setItem('token', token);
        setMessage('✅ נרשמת בהצלחה!');
        // console.log("✅ Token saved:", token);
      } else {
        setMessage('🚨 שגיאה: השרת לא החזיר טוקן.');
      }

    } catch (error) {
      console.error("❌ שגיאת תקשורת:", error);
      setMessage('שגיאה, לא ניתן להתחבר לשרת.');
    }
  };

  return (
<div className="signup-form">
  <h2>הירשם למשלוחן</h2>
  <form onSubmit={handleSubmit}>
    <input type="text" name="Name" value={formData.Name} onChange={handleChange} placeholder="שם מלא" required />
    <input type="tel" name="Phone" value={formData.Phone} onChange={handleChange} placeholder="טלפון" required />
    <input type="text" name="Identity" value={formData.Identity} onChange={handleChange} placeholder="ת.ז" required />
    <input type="password" name="Password" value={formData.Password} onChange={handleChange} placeholder="סיסמא" required />
    <input type="email" name="Email" value={formData.Email} onChange={handleChange} placeholder="אימייל" required />
    <input type="date" name="DateOfBirth" value={formData.DateOfBirth} onChange={handleChange} placeholder="תאריך לידה" />
    
    {/* Select a city from the list */}
    <select name="cityName" value={formData.cityName} onChange={handleChange} required>
      <option value="">בחר עיר</option>
      {cities.map(city => (
        <option key={city.id} value={city.name}>{city.name}</option>
      ))}
    </select>
    
    <input type="hidden" name="cityId" value={formData.cityId} />
    <input type="text" name="NumOfCar" value={formData.NumOfCar} onChange={handleChange} placeholder="מספר לוחית רכב" />
    
    <h3>פרטי בנק</h3>
    <input type="text" name="BankNumber" value={formData.BankNumber} onChange={handleChange} placeholder="מספר בנק" />
    <input type="text" name="BankAccount" value={formData.BankAccount} onChange={handleChange} placeholder="מספר חשבון בנק" />
    <input type="text" name="BankBranch" value={formData.BankBranch} onChange={handleChange} placeholder="מס סניף בנק" />
    
    <button type="submit">הירשם</button>
  </form>

  {message && <p>{message}</p>}
</div>
  );
};

export default DeliverySignUp;