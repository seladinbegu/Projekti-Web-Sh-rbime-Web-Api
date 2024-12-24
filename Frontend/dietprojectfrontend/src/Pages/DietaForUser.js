import React, { useState, useEffect } from 'react';
import api from '../Components/api';

const DietaForUser = () => {
  const [dietat, setDietat] = useState([]);


  useEffect(() => {
    fetchDietat();
  }, []);

  const fetchDietat = async () => {
    try {
      const response = await api.get('/Dieta'); // Use `api` here
      setDietat(response.data);
    } catch (error) {
      console.error('Error fetching dietat:', error);
    }
  };


 
  return (
    <div className="container mx-auto p-6">
<h1 className="text-4xl font-bold text-center mb-8 bg-clip-text text-transparent bg-gradient-to-r from-purple-700 via-pink-500 to-red-500">
  Dietat
</h1>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {dietat.map((dieta) => (
          <div key={dieta.id} className="bg-white shadow-md p-4 rounded-md">
            <h2 className="text-xl font-bold">{dieta.emri}</h2>
            <p className="text-gray-600"><b>Lloji:</b> {dieta.lloji}</p>
            <p className="text-gray-600"><b>Përshkrimi</b>: {dieta.pershkrimi}</p>
            <div className="mt-4">
             
            </div>
          </div>
        ))}
      </div>

     
    </div>
  );
};

export default DietaForUser;
