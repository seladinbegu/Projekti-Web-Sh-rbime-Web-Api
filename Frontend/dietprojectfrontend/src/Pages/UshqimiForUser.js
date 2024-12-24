import React, { useEffect, useState } from 'react';
import api from '../Components/api';


const UshqimiForUser = () => {
  const [ushqimet, setUshqimet] = useState([]);

  const [receta, setReceta] = useState([]);
  const [recetaUshqimi, setRecetaUshqimi] = useState([]); // Holds RecetaUshqimi data

  useEffect(() => {
    fetchRecetat();
    fetchUshqimet();
    fetchRecetaUshqimi(); // Fetch RecetaUshqimi data
  }, []);

  const fetchUshqimet = async () => {
    try {
      const response = await api.get('/Ushqimi');
      setUshqimet(response.data);
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

  const fetchRecetat = async () => {
    try {
      const response = await api.get('/Receta');
      setReceta(response.data);
    } catch (error) {
      console.error('Error fetching recetat:', error);
    }
  };

 
  
  
  
  



  // Function to get food names for a given recipe ID
  const getRecetaNames = (ushqimiId) => {
    if (!receta || !recetaUshqimi) {
      return 'Loading...'; // Return a loading message while data is being fetched
    }
  
    // Filter RecetaUshqimi to get associations for the given UshqimiId
    const associatedReceta = recetaUshqimi
      .filter((item) => item.ushqimiId === ushqimiId) // Check for matching UshqimiId
      .map((item) => {
        const recetaFound = receta.find((receta) => receta.id === item.recetaId); // Find the corresponding Receta
        return recetaFound ? recetaFound.emri : 'Unknown Receta'; // If found, return name, else 'Unknown Receta'
      });
  
    // Return all associated Receta names or a message if none are found
    return associatedReceta.length > 0 ? associatedReceta.join(', ') : 'No associated receta';
  };
  


  
  
  

  return (
    <div className="container mx-auto p-6">
<h1 className="text-4xl font-bold text-center mb-8 bg-clip-text text-transparent bg-gradient-to-r from-purple-700 via-pink-500 to-red-500">
  Ushqimet
</h1>
  
      <div className="mb-8">
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          {ushqimet.map((ushqimi) => (
            <div key={ushqimi.id} className="bg-white shadow-md rounded-lg overflow-hidden">
              <div className="p-4">
                {/* Image */}
                {ushqimi.imagePath && (
                  <img
                    src={ushqimi.imagePath}  // Displaying the image from ImagePath
                    alt={ushqimi.emri}
                    className="w-full h-48 object-cover rounded-md mb-4"
                  />
                )}
  
                {/* Title and Category */}
                <h3 className="text-2xl font-bold text-gray-900 mb-3">{ushqimi.emri}</h3>
  <p className="text-md font-medium text-blue-700 mb-2 uppercase tracking-wide">
    {ushqimi.kategoria}
  </p>
  <p className="text-sm leading-relaxed" style={{ fontFamily: "'Poppins', sans-serif", fontWeight: '600', color: '#4A5568' }}>
  Origjina: {ushqimi.origjina}
</p>

  
                {/* Nutritional Info */}
                <div className="text-sm text-gray-600 mb-4">
                  <p><strong>Kalori:</strong> {ushqimi.kalori} kcal</p>
                  <p><strong>Proteina:</strong> {ushqimi.proteina} g</p>
                  <p><strong>Karbohidrate:</strong> {ushqimi.karbohidrate} g</p>
                  <p><strong>Yndyrna:</strong> {ushqimi.yndyrna} g</p>
                  <p><strong>Fibrat:</strong> {ushqimi.fibrat} g</p>
                  <p><strong>Vitamin C:</strong> {ushqimi.vitaminC} mg</p>
                  <p><strong>Vitamin A:</strong> {ushqimi.vitaminA} IU</p>
                  <p><strong>Kalcium:</strong> {ushqimi.kalcium} mg</p>
                  <p><strong>Hekur:</strong> {ushqimi.hekur} mg</p>
                </div>
  
                {/* Vegan, Gluten, Dairy info */}
                <div className="text-sm text-gray-600 mb-4">
                  <p className={`${ushqimi.vegan ? "text-green-500" : "text-red-500"}`}><strong>Vegan:</strong>   {ushqimi.vegan ? "✅" : "❌"}
                  </p>
                  <p className={`${ushqimi.kaGluten ? "text-green-500" : "text-red-500"}`}><strong>Ka Gluten:</strong> {ushqimi.kaGluten ? "✅" : "❌"}</p>
                  <p className={`${ushqimi.kaBulmet ? "text-green-500" : "text-red-500"}`}><strong>Ka Bulmet:</strong> {ushqimi.kaBulmet ? "✅" : "❌"}</p>
                </div>

                <p className="block text-sm font-medium text-blue-600">

Recetat: 
<span className="font-bold text-blue-600">{getRecetaNames(ushqimi.id)}</span>
</p>
              </div>
  
              {/* Edit and Delete Buttons */}
            
            </div>
          ))}
        </div>
      </div>
  
          
    </div>
  );
  
};

export default UshqimiForUser;
