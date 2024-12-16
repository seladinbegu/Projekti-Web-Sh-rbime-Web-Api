import React from "react";
import { useNavigate } from "react-router-dom";
import { FaUserAlt, FaShieldAlt, FaCog, FaSignOutAlt } from "react-icons/fa";

const Profile = ({ username, role, handleLogout}) => {
  const navigate = useNavigate();

  return (
    <div
      className="min-h-screen bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white flex flex-col items-center justify-center"
    >
      {/* Profile Card */}
      <div className="bg-white text-gray-800 p-6 rounded-lg shadow-lg w-80 hover:shadow-2xl transition duration-300 ease-in-out transform hover:scale-105">
        {/* Profile Header */}
        <div className="flex items-center justify-center mb-6">
          <div className="w-16 h-16 bg-gray-300 rounded-full flex items-center justify-center text-white text-3xl font-bold">
            {username ? username[0].toUpperCase() : "U"}
          </div>
        </div>
        
        {/* Welcome Text */}
        <h1 className="text-2xl font-bold mb-4 text-center text-indigo-600">
          Welcome to Your Profile
        </h1>
        
        {/* Username and Role */}
        <div className="text-center">
          <p className="text-lg mb-2">
            <FaUserAlt className="inline-block text-indigo-600 mr-2" />
            <strong>Username:</strong> {username}
          </p>
          <p className="text-lg mb-4">
            <FaShieldAlt className="inline-block text-indigo-600 mr-2" />
            <strong>Role:</strong> {role === "Admin" ? "Administrator" : "User"}
          </p>
        </div>

        {/* Settings and Logout Buttons */}
        <div className="mt-6">
          <button
            onClick={() => navigate("/settings")}
            className="w-full bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white font-semibold py-2 rounded-md mb-3 transition duration-300 ease-in-out hover:opacity-90 flex items-center justify-center"
          >
            <FaCog className="mr-2" /> Settings
          </button>

          <button
  onClick={handleLogout} // Use the handleLogout function
  className="w-full bg-red-500 text-white font-semibold py-2 rounded-md transition duration-300 ease-in-out hover:opacity-90 flex items-center justify-center"
>
  <FaSignOutAlt className="mr-2" /> Logout
</button>

        </div>

        {/* Additional Information (Optional) */}
        <div className="mt-6 text-center text-sm text-gray-600">
          <p>
            <span className="font-semibold">Tip:</span> Keep your profile updated to receive notifications and improve your experience.
          </p>
        </div>
      </div>
    </div>
  );
};

export default Profile;
