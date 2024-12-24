// src/RecetaDetail.js
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom'; // To get the route parameters
import api from '../Components/api'; // Assuming you have an axios instance

const RecetaDetail = () => {
  const { id } = useParams(); // Extract 'id' from the URL
  const [newReceta, setNewReceta] = useState({
    emri: '',
    udhezimet: '',
  });
  const [loading, setLoading] = useState(true); // Loading state
  const [error, setError] = useState(null); // Error state

  // Fetch the recipe details when the component mounts or the 'id' changes
  useEffect(() => {
    const fetchReceta = async () => {
      try {
        const response = await api.get(`http://localhost:5177/api/Receta/${id}`);
        setNewReceta(response.data); // Update state with the fetched recipe
        setLoading(false); // Set loading to false after data is fetched
      } catch (error) {
        console.error('Error fetching receta:', error);
        setError('An error occurred while fetching the recipe.'); // Set error message
        setLoading(false); // Set loading to false even on error
      }
    };

    fetchReceta();
  }, [id]); // Only re-fetch when 'id' changes

  // Display loading, error, or the recipe details
  if (loading) {
    return <div className="text-center text-white">Loading...</div>;
  }

  if (error) {
    return <div className="text-center text-white">{error}</div>;
  }

  return (
    <div className="min-h-screen bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white flex flex-col items-center justify-center">
      <div className="bg-white text-gray-800 p-6 rounded-lg shadow-lg w-80">
        {/* Display recipe name */}
        <h1 className="text-2xl font-bold text-center text-indigo-600">{newReceta.emri}</h1>

        {/* Display instructions */}
        <p className="text-center mt-4">{newReceta.udhezimet}</p>
      </div>
    </div>
  );
};

export default RecetaDetail;
