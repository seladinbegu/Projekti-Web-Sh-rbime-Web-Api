import React, { useState, useEffect } from 'react';
import api from '../Components/api';

const Receta = ({ username }) => { // Accept username as a prop
  const [recetat, setRecetat] = useState([]);
  const [newReceta, setNewReceta] = useState({
    emri: '',
    udhezimet: '',
    ushqime: [], // Holds selected Ushqimi IDs
  });
  const [ushqime, setUshqime] = useState([]);
  const [recetaUshqimi, setRecetaUshqimi] = useState([]); // Holds RecetaUshqimi data
  const [isEditing, setIsEditing] = useState(false);
  const [selectedFoods, setSelectedFoods] = useState([]); // Added selectedFoods state

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

  const handleEdit = (receta) => {
    const ushqimeArray = Array.isArray(receta.ushqime) ? receta.ushqime : [];
    setNewReceta({
      ...receta,
      ushqime: ushqimeArray.map((ushqim) => ushqim.id),
    });
    setIsEditing(true);
  };

 const handleSubmit = async (e) => {
  e.preventDefault();

  try {
    const method = isEditing ? 'put' : 'post';
    const url = isEditing ? `/Receta/${newReceta.id}` : '/Receta';

    // Save or update the Receta
    const recetaResponse = await api({
      method,
      url,
      data: {
        emri: newReceta.emri,
        udhezimet: newReceta.udhezimet,
      },
    });

    // If editing, delete old RecetaUshqimi entries
    if (isEditing) {
      // Find the existing RecetaUshqimi relationships that are not selected
      const existingUshqimiIds = recetaUshqimi
        .filter((item) => item.recetaId === newReceta.id)
        .map((item) => item.ushqimiId);

      // Delete the associations that are no longer selected
      for (let ushqimiId of existingUshqimiIds) {
        if (!selectedFoods.includes(ushqimiId)) {
          await api.delete(`/RecetaUshqimi/${newReceta.id}/${ushqimiId}`);
        }
      }
    }

    // Add new RecetaUshqimi entries for selected foods
    for (let ushqimiId of selectedFoods) {
      // Check if the association already exists
      const existingAssociation = recetaUshqimi.some(
        (item) => item.recetaId === recetaResponse.data.id && item.ushqimiId === ushqimiId
      );
      
      if (!existingAssociation) {
        const recetaUshqimiData = {
          recetaId: recetaResponse.data.id || newReceta.id, // Use the correct ID
          ushqimiId,
        };
        await api.post('/RecetaUshqimi', recetaUshqimiData);
      }
    }

    // Refresh the data and reset the form
    fetchRecetat();
    fetchRecetaUshqimi();
    setNewReceta({ emri: '', udhezimet: '', ushqime: [] });
    setSelectedFoods([]); // Clear selected foods
    setIsEditing(false);
  } catch (error) {
    console.error('Error submitting receta:', error);
  }
};


  const deleteReceta = async (id) => {
    if (window.confirm('Are you sure you want to delete this receta?')) {
      try {
        await api.delete(`/Receta/${id}`);
        fetchRecetat();
      } catch (error) {
        console.error('Error deleting receta:', error);
      }
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

  const handleCheckboxChange = (e) => {
    const foodId = parseInt(e.target.value); // Get the food ID from the checkbox
    if (e.target.checked) {
      setSelectedFoods([...selectedFoods, foodId]); // Add the selected food ID
    } else {
      setSelectedFoods(selectedFoods.filter(id => id !== foodId)); // Remove the deselected food ID
    }
  };







  
  // Fetch user ID function


  return (
    <div className="container mx-auto p-6">
      <h1 className="text-4xl font-bold text-center mb-8 bg-clip-text text-transparent bg-gradient-to-r from-purple-700 via-pink-500 to-red-500">
        Menaxhmenti i Recetave
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
    onClick={() => handleEdit(receta)}
    className="bg-yellow-500 text-white px-4 py-2 rounded-md"
  >
    Përmirëso
  </button>
  <button
    onClick={() => deleteReceta(receta.id)}
    className="bg-red-500 text-white px-4 py-2 rounded-md"
  >
    Fshijë
  </button>
 
</div>


          </div>
        ))}
      </div>

      <form onSubmit={handleSubmit} className="bg-gray-100 p-6 mt-6 rounded-md">
        <h2 className="text-2xl font-bold mb-4">{isEditing ? 'Përmirëso Recetën' : 'Krijo Recetën'}</h2>
        <input
          type="text"
          placeholder="Emri"
          value={newReceta.emri}
          onChange={(e) => setNewReceta({ ...newReceta, emri: e.target.value })}
          className="block w-full mb-4 p-2 border rounded-md"
        />
        <textarea
          placeholder="Udhëzimet"
          value={newReceta.udhezimet}
          onChange={(e) => setNewReceta({ ...newReceta, udhezimet: e.target.value })}
          className="block w-full mb-4 p-2 border rounded-md"
        />
        
        <div className="mb-4">
          <label className="block text-sm font-medium text-gray-700">Ushqimet</label>
          <div>
            {ushqime.map((food) => (
              <div key={food.id} className="flex items-center mb-2">
                <input
                  type="checkbox"
                  id={`food-${food.id}`} // Make sure each checkbox has a unique ID
                  value={food.id}
                  className="mr-2"
                  onChange={handleCheckboxChange} // Handle checkbox state change
                />
                <label htmlFor={`food-${food.id}`} className="text-gray-700">{food.emri}</label>

              </div>
            ))}
          </div>
        </div>
        <button
          type="submit"
          className="w-full bg-blue-500 text-white py-2 rounded-md"
        >
          {isEditing ? 'Përmirëso' : 'Krijo'}
        </button>
      </form>
    </div>
  );
};


export default Receta;
