import React from "react";
import { useNavigate } from "react-router-dom";
import logo from "../Images/PRTENIUMLOGO.png";
import { FaUserCircle } from "react-icons/fa"; // Import Font Awesome icon


const Header = ({ isLoggedIn, handleLogout, role, username}) => {
  const navigate = useNavigate();

  // Handle login click - redirects to login page
  const handleLoginClick = () => navigate("/login");

  // Render the header UI
  return (
    <header
      className="flex justify-between items-center p-4 text-white"
      style={{
        background: "linear-gradient(180deg, rgb(76, 20, 165), rgba(210, 82, 112, 255))",
      }}
    >
      {/* Logo */}
      <div
        className="relative w-36 h-auto overflow-hidden transition-all duration-300 transform hover:scale-105"
        onClick={() => navigate("/")}
        style={{ borderRadius: "8px" }}
      >
        <img
          src={logo}
          alt="Logo"
          className="w-full h-auto"
          style={{ borderRadius: "8px", border: "none" }}
        />
      </div>

      {/* Navigation */}
      <div className="flex gap-4">
        <button
          className="px-6 py-3 bg-gradient-to-b from-purple-600 to-pink-500 text-white font-semibold rounded-md shadow-lg transform transition-all duration-300 hover:scale-110"
          onClick={() => navigate("/")}
        >
          Home
        </button>
        <button
          className="px-6 py-3 bg-gradient-to-b from-purple-600 to-pink-500 text-white font-semibold rounded-md shadow-lg transform transition-all duration-300 hover:scale-110"
          onClick={() => navigate("/ushqimi")}
        >
          Ushqimet
        </button>
        <button
          className="px-6 py-3 bg-gradient-to-b from-purple-600 to-pink-500 text-white font-semibold rounded-md shadow-lg transform transition-all duration-300 hover:scale-110"
          onClick={() => navigate("/about")}
        >
          About
        </button>
        <button
          className="px-6 py-3 bg-gradient-to-b from-purple-600 to-pink-500 text-white font-semibold rounded-md shadow-lg transform transition-all duration-300 hover:scale-110"
          onClick={() => navigate("/contact")}
        >
          Contact
        </button>

        {/* User Section */}
        {isLoggedIn ? (
          <div className="flex items-center gap-2 cursor-pointer">
           <FaUserCircle
  className="w-10 h-10 text-white cursor-pointer hover:text-gray-300 transition duration-300"
  onClick={() => navigate("/profile")}
/>
<span>{role === "Admin" ? `${username} (Admin)` : username}</span>

            {/* Conditionally render Test button for Admin role */}
           

            <button
              className="px-4 py-2 bg-gradient-to-b from-red-600 to-red-500 text-white font-semibold rounded-md shadow-lg transform transition-all duration-300 hover:scale-110"
              onClick={handleLogout} // Use the passed handleLogout function
            >
              Log Out
            </button>
          </div>
        ) : (
          <button
            className="px-6 py-3 bg-gradient-to-b from-purple-600 to-pink-500 text-white font-semibold rounded-md shadow-lg transform transition-all duration-300 hover:scale-110"
            onClick={handleLoginClick}
          >
            Log In
          </button>
        )}
      </div>
    </header>
  );
};

export default Header;
