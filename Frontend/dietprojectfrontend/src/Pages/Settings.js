import React, { useState } from 'react';
import api from '../Components/api';

const Settings = ({ handleLogout }) => {
  const [usernameData, setUsernameData] = useState({ newUsername: "" });
  const [passwordData, setPasswordData] = useState({
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
  });
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  // Handle username input changes
  const handleUsernameChange = (e) => {
    setUsernameData({ ...usernameData, [e.target.name]: e.target.value });
  };

  // Handle password input changes
  const handlePasswordChange = (e) => {
    setPasswordData({ ...passwordData, [e.target.name]: e.target.value });
  };

  // Update Username
  const updateUsername = async (e) => {
    e.preventDefault();
    setMessage("");
    setError("");

    try {
      const response = await api.put("http://localhost:5177/users/update-username", usernameData);
      setMessage(response.data.Message);
      setUsernameData({ newUsername: "" });

      // Logout the user if username is updated successfully
      handleLogout();

    } catch (err) {
      setError(err.response?.data?.Message || "Failed to update username.");
    }
  };

  // Update Password
  const updatePassword = async (e) => {
    e.preventDefault();
    setMessage("");
    setError("");

    if (passwordData.newPassword !== passwordData.confirmPassword) {
      setError("New password and confirm password do not match.");
      return;
    }

    try {
      const response = await api.put("http://localhost:5177/users/update-password", passwordData);
      setMessage(response.data.Message);
      setPasswordData({
        currentPassword: "",
        newPassword: "",
        confirmPassword: "",
      });

      // Logout the user if password is updated successfully
      handleLogout();

    } catch (err) {
      setError(err.response?.data?.Message || "Failed to update password.");
    }
  };

  return (
    <div className="bg-gradient-to-b from-white via-gray-100 to-gray-200 min-h-screen">
      <header className="sticky top-0 z-50 bg-gradient-to-r from-purple-700 via-pink-500 to-red-500 text-white shadow-md p-4 text-center text-2xl font-semibold">
        Settings
      </header>
      <div className="max-w-3xl mx-auto mt-10 p-6 bg-white shadow-lg rounded-lg">
        {/* Success and Error Messages */}
        {message && (
          <div className="mb-4 p-4 text-green-700 bg-green-100 rounded-lg">
            {message}
          </div>
        )}
        {error && (
          <div className="mb-4 p-4 text-red-700 bg-red-100 rounded-lg">
            {error}
          </div>
        )}

        {/* Update Username */}
        <section className="mb-8">
          <h2 className="text-xl font-semibold text-purple-700">Update Username</h2>
          <form onSubmit={updateUsername} className="mt-4">
            <label className="block text-gray-700 font-medium mb-2" htmlFor="newUsername">
              New Username
            </label>
            <input
              type="text"
              name="newUsername"
              id="newUsername"
              value={usernameData.newUsername}
              onChange={handleUsernameChange}
              placeholder="Enter new username"
              className="w-full p-3 border rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
              required
            />
            <button
              type="submit"
              className="mt-4 w-full p-3 bg-gradient-to-r from-purple-700 via-pink-500 to-red-500 text-white font-semibold rounded-lg hover:shadow-md hover:bg-purple-800 transition duration-300"
            >
              Update Username
            </button>
          </form>
        </section>

        {/* Update Password */}
        <section>
          <h2 className="text-xl font-semibold text-purple-700">Update Password</h2>
          <form onSubmit={updatePassword} className="mt-4">
            <label className="block text-gray-700 font-medium mb-2" htmlFor="currentPassword">
              Current Password
            </label>
            <input
              type="password"
              name="currentPassword"
              id="currentPassword"
              value={passwordData.currentPassword}
              onChange={handlePasswordChange}
              placeholder="Enter current password"
              className="w-full p-3 border rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
              required
            />
            <label className="block text-gray-700 font-medium mt-4 mb-2" htmlFor="newPassword">
              New Password
            </label>
            <input
              type="password"
              name="newPassword"
              id="newPassword"
              value={passwordData.newPassword}
              onChange={handlePasswordChange}
              placeholder="Enter new password"
              className="w-full p-3 border rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
              required
            />
            <label className="block text-gray-700 font-medium mt-4 mb-2" htmlFor="confirmPassword">
              Confirm New Password
            </label>
            <input
              type="password"
              name="confirmPassword"
              id="confirmPassword"
              value={passwordData.confirmPassword}
              onChange={handlePasswordChange}
              placeholder="Confirm new password"
              className="w-full p-3 border rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
              required
            />
            <button
              type="submit"
              className="mt-4 w-full p-3 bg-gradient-to-r from-purple-700 via-pink-500 to-red-500 text-white font-semibold rounded-lg hover:shadow-md hover:bg-purple-800 transition duration-300"
            >
              Update Password
            </button>
          </form>
        </section>
      </div>
    </div>
  );
};

export default Settings;
