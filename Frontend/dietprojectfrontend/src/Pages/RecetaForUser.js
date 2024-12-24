import React, { useState, useEffect } from 'react';
import api from '../Components/api';

const RecetaForUser = ({ username }) => { // Accept username as a prop
  const [recetat, setRecetat] = useState([]);
 
  const [ushqime, setUshqime] = useState([]);
  const [recetaUshqimi, setRecetaUshqimi] = useState([]); // Holds RecetaUshqimi data

  useEffect(() => {
    fetchRecetat();
    fetchUshqimet();
    fetchRecetaUshqimi(); // Fetch RecetaUshqimi data
  }, []);

  const fetchRecetat = async () => {
    try {
      const response = await api.get('/Receta');
      setRecetat(response.data);
    } catch (error) {
      console.error('Error fetching recetat:', error);
    }
  };

  const fetchRecetaUshqimi = async () => {
    try {
      const response = await api.get('/RecetaUshqimi');
      setRecetaUshqimi(response.data);
      console.log("RecetaUshqimi data:", response.data); // Check the fetched RecetaUshqimi data
    } catch (error) {
      console.error('Error fetching recetaUshqimi:', error);
    }
  };

  const fetchUshqimet = async () => {
    try {
      const response = await api.get('/Ushqimi');
      setUshqime(response.data);
      console.log("Ushqime data:", response.data); // Check the fetched Ushqime data
    } catch (error) {
      console.error('Error fetching ushqime:', error);
    }
  };





  // Function to get food names for a given recipe ID
  const getFoodNames = (recetaId) => {
    const associatedFoods = recetaUshqimi
      .filter((item) => item.recetaId === recetaId)
      .map((item) => {
        const food = ushqime.find((food) => food.id === item.ushqimiId);
        return food ? food.emri : 'Unknown Food';
      });

    return associatedFoods.length > 0 ? associatedFoods.join(', ') : 'No associated foods';
  };




  const handleAddToFavorites = async (recetaId) => {
    try {
      // Fetch user ID first
      const userId = await fetchUserId();
  
      if (userId) {
        const data = {
          recetaId,
          userId, // Use the fetched user ID
        };
        
        // Send the request to add the recipe to favorites
        await api.post('/RecetaUser', data);
        alert('Receta added to favorites!');
      } else {
        alert('Failed to fetch user ID');
      }
    } catch (error) {
      console.error('Error adding to favorites:', error);
      alert('Failed to add to favorites.');
    }
  };
  
  // Fetch user ID function
  const fetchUserId = async () => {
    if (username) {
      try {
        const response = await api.get(`http://localhost:5177/users/by-username?username=${username}`);
        if (response.data && response.data.id) {
          console.log('User ID at Receta:', response.data.id);
          return response.data.id; // Return the user ID
        } else {
          console.error('User ID not found');
          return null;
        }
      } catch (error) {
        console.error('Error fetching user ID:', error);
        return null;
      }
    }
  };
  

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-4xl font-bold text-center mb-8 bg-clip-text text-transparent bg-gradient-to-r from-purple-700 via-pink-500 to-red-500">
        Recetat
      </h1>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {recetat.map((receta) => (
          <div key={receta.id} className="bg-white shadow-md p-4 rounded-md">
            <h2 className="text-xl font-bold">{receta.emri}</h2>
            <p className="text-gray-600">Udhëzimet: {receta.udhezimet}</p>
            <p className="block text-sm font-medium text-blue-600">

              Ushqimet: 
              <span className="font-bold text-blue-600">{getFoodNames(receta.id)}</span>
            </p>
            <div className="mt-4 flex space-x-4">
  
 
  <button 
    onClick={() => handleAddToFavorites(receta.id)} // Pass receta.id here
    className="bg-blue-500 text-white px-4 py-2 rounded-md"
  >
    Shto në listë
  </button>
</div>


          </div>
        ))}
      </div>

     
    </div>
  );
};


export default RecetaForUser;
