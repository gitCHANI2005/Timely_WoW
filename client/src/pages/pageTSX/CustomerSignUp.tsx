import React, { useState, useEffect } from 'react';
import "../pageCSS/Signup.css"; // סגנון העיצוב לדף

// טיפוס עיר
interface City {
  id: string;
  name: string;
}

const CustomerSignUp = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Phone: '',
    Identity: '',
    Password: '',
    Email: '',
    Role: 0,
    CityHome: '',
    CityIdHome: '',
    AdressHome: '',
    CityWork: '',
    CityIdWork: '',
    AdressWork: '',
    CardNumber: '',
    CardValidity: '',
    CardCvv: ''
  });

  const [cities, setCities] = useState<City[]>([]); // רשימת הערים מהשרת
  const [message, setMessage] = useState('');

  // טעינת רשימת הערים מהשרת
  useEffect(() => {
    const fetchCities = async () => {
      try {
        const response = await fetch(`https://localhost:7013/api/City`);
        if (!response.ok) throw new Error("שגיאה בטעינת ערים");
        const data: City[] = await response.json();
        setCities(data);
      } catch (error) {
        console.error("❌ שגיאה בטעינת הערים:", error);
      }
    };

    fetchCities();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;

    if (name === "CityHome") {
      const selectedCity = cities.find(city => city.name === value);
      setFormData(prevData => ({
        ...prevData,
        CityHome: value,
        CityIdHome: selectedCity ? selectedCity.id : '',
      }));
    } else if (name === "CityWork") {
      const selectedCity = cities.find(city => city.name === value);
      setFormData(prevData => ({
        ...prevData,
        CityWork: value,
        CityIdWork: selectedCity ? selectedCity.id : '',
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
      const response = await fetch(`https://localhost:7013/api/Customer`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        setMessage('❌ הייתה בעיה בהרשמה, נסה שנית.');
        console.error("❌ שגיאה בשרת:", await response.text());
        return;
      }

      setMessage('✅ נרשמת בהצלחה!');
    } catch (error) {
      console.error("❌ שגיאת תקשורת:", error);
      setMessage('❌ שגיאה, לא ניתן להתחבר לשרת.');
    }
  };

  return (
    <div className="signup-form">
      <h2>הירשם כלקוח</h2>
      <form onSubmit={handleSubmit}>
        <input type="text" name="Name" value={formData.Name} onChange={handleChange} placeholder="שם מלא" required />
        <input type="tel" name="Phone" value={formData.Phone} onChange={handleChange} placeholder="טלפון" required />
        <input type="text" name="Identity" value={formData.Identity} onChange={handleChange} placeholder="ת.ז" required />
        <input type="password" name="Password" value={formData.Password} onChange={handleChange} placeholder="סיסמא" required />
        <input type="email" name="Email" value={formData.Email} onChange={handleChange} placeholder="אימייל" required />

        <h3>🏠 כתובת בית</h3>
        <select name="CityHome" value={formData.CityHome} onChange={handleChange} required>
          <option value="">בחר עיר</option>
          {cities.map(city => (
            <option key={city.id} value={city.name}>{city.name}</option>
          ))}
        </select>
        <input type="hidden" name="CityIdHome" value={formData.CityIdHome} />
        <input type="text" name="AdressHome" value={formData.AdressHome} onChange={handleChange} placeholder="כתובת הבית" />

        <h3>🏢 כתובת עבודה (אופציונלית)</h3>
        <select name="CityWork" value={formData.CityWork} onChange={handleChange}>
          <option value="">בחר עיר</option>
          {cities.map(city => (
            <option key={city.id} value={city.name}>{city.name}</option>
          ))}
        </select>
        <input type="hidden" name="CityIdWork" value={formData.CityIdWork} />
        <input type="text" name="AdressWork" value={formData.AdressWork} onChange={handleChange} placeholder="כתובת העבודה" />

        <h3>פרטי אשראי</h3>
        <input type="text" name="CardNumber" value={formData.CardNumber} onChange={handleChange} placeholder="מספר כרטיס אשראי" required />
        <input type="date" name="CardValidity" value={formData.CardValidity} onChange={handleChange} placeholder="תוקף כרטיס" required />
        <input type="text" name="CardCvv" value={formData.CardCvv} onChange={handleChange} placeholder="CVV" required />
        
        <button type="submit">הירשם</button>
      </form>

      {message && <p className={`message ${message.includes("❌") ? "error" : "success"}`}>{message}</p>}
    </div>
  );
};

export default CustomerSignUp;
