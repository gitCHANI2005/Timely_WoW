import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../pageCSS/Header.css";

const Header = () => {
  const navigate = useNavigate();
  const [menuOpen, setMenuOpen] = useState(false); // מצב לפתיחה/סגירה של התפריט

  const userName = localStorage.getItem("userName");
  const firstLetter = userName ? userName.charAt(0).toUpperCase() : "G";

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userName");
    navigate("/login");
  };

  return (
    <header className="header">
      <div 
        className="user-info" 
        onClick={() => setMenuOpen(!menuOpen)}
        onMouseLeave={() => setMenuOpen(false)} // התפריט ייסגר ביציאה עם העכבר
      >
        {/* אייקון משתמש */}
        <div className="user-circle">
          {firstLetter}
        </div>
        
        {/* חץ קטן שמצביע על תפריט */}
        <span className="dropdown-arrow">▼</span>

        {/* תפריט נפתח */}
        {menuOpen && (
          <div className="dropdown-menu">
            <button className="logout-button" onClick={handleLogout}>
              Log out
            </button>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;
