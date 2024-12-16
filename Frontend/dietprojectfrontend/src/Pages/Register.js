import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import api from '../Components/Api';  // Make sure the case matches exactly (Api.js)

const Register = () => {
  const [formData, setFormData] = useState({
    UserName: '',
    Email: '',
    Password: '',
  });
  const [error, setError] = useState('');
  const navigate = useNavigate(); // For navigation

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await api.post('/Auth/register', formData); // Use api instance
      if (response.status === 200) {
        alert('Regjistrimi u krye me sukses! Mirë se vini.');
        setError('');
        setFormData({
          UserName: '',
          Email: '',
          Password: '',
        });
        navigate('/'); // Redirect to home page after successful registration
      } else {
        throw new Error('Regjistrimi dështoi');
      }
    } catch (error) {
      console.error('Regjistrimi dështoi:', error.response?.data?.message || error.message);
      setError('Regjistrimi dështoi. Ju lutem provoni përsëri më vonë.');
    }
  };

  return (
    <>
    <div className="flex justify-center items-center min-h-screen bg-gray-100">
      <div className="max-w-md w-full bg-white p-8 rounded-lg shadow-lg">
        <h2 className="text-2xl font-semibold mb-4 text-center">Regjistrohu</h2>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label htmlFor="UserName" className="block">Emri i Përdoruesit:</label>
            <input
              type="text"
              id="UserName"
              name="UserName"
              value={formData.UserName}
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:border-blue-500"
            />
          </div>
          <div>
            <label htmlFor="Email" className="block">Email:</label>
            <input
              type="email"
              id="Email"
              name="Email"
              value={formData.Email}
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:border-blue-500"
            />
          </div>
          <div>
            <label htmlFor="Password" className="block">Fjalëkalimi:</label>
            <input
              type="password"
              id="Password"
              name="Password"
              value={formData.Password}
              onChange={handleChange}
              required
              className="w-full border border-gray-300 rounded-md px-3 py-2 focus:outline-none focus:border-blue-500"
            />
          </div>
          <div className="flex justify-center">
            <button type="submit" 
  className="w-full bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white font-semibold py-2 rounded-md transition duration-300 ease-in-out hover:opacity-90"
            >
              Regjistrohu
            </button>
          </div>
        </form>
        {error && <div className="text-red-500 text-center">{error}</div>}
        <div className="mt-4 text-center">
          <span className="text-gray-600">Posedoni llogari?</span>
          <Link to="/login" className="text-blue-500 hover:underline ml-1">Kyqu</Link>
        </div>
      </div>
    </div>
    </>
  );
};

export default Register;
