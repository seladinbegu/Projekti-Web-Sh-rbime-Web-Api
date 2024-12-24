import React, { useEffect, useState } from 'react';
import api from '../Components/api';


const Ushqimi = () => {
  const [ushqimet, setUshqimet] = useState([]);
  const [newUshqimi, setNewUshqimi] = useState({
    emri: '',
    kalori: 0,
    proteina: 0,
    karbohidrate: 0,
    yndyrna: 0,
    fibrat: 0,
    vitaminC: 0,
    vitaminA: 0,
    kalcium: 0,
    hekur: 0,
    vegan: false,
    kaGluten: false,
    kaBulmet: false,
    kategoria: '',
    origjina: '',
    imagePath: '',
    dataKrijimit: '',
    receta: [],  // Holds selected Ushqimi IDs
  });
  const [receta, setReceta] = useState([]);
  const [recetaUshqimi, setRecetaUshqimi] = useState([]); // Holds RecetaUshqimi data
  const [isEditing, setIsEditing] = useState(false);
  const [selectedRecetat, setSelectedRecetat] = useState([]); // Added selectedFoods state

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

  const handleEdit = (ushqimi) => {
    const recetaArray = Array.isArray(ushqimi.receta) ? ushqimi.receta : [];
    setNewUshqimi({
      ...ushqimi,
      receta: recetaArray.map((receta) => receta.id),
      
    });
    setIsEditing(true);

  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    try {
      const method = isEditing ? 'put' : 'post';
      const url = isEditing ? `/Ushqimi/${newUshqimi.id}` : '/Ushqimi';
  
      // Save or update the Ushqimi
      const ushqimiResponse = await api({
        method,
        url,
        data: {
          emri: newUshqimi.emri,
          kalori: newUshqimi.kalori,
          proteina: newUshqimi.proteina,
          karbohidrate: newUshqimi.karbohidrate,
          yndyrna: newUshqimi.yndyrna,
          fibrat: newUshqimi.fibrat,
          vitaminC: newUshqimi.vitaminC,
          vitaminA: newUshqimi.vitaminA,
          kalcium: newUshqimi.kalcium,
          hekur: newUshqimi.hekur,
          vegan: newUshqimi.vegan,
          kaGluten: newUshqimi.kaGluten,
          kaBulmet: newUshqimi.kaBulmet,
          kategoria: newUshqimi.kategoria,
          origjina: newUshqimi.origjina,
          imagePath: newUshqimi.imagePath,
        },
      });
  
      const ushqimiId = ushqimiResponse.data.id || newUshqimi.id;
  
      if (isEditing) {
        // Get the current RecetaUshqimi associations for the current Ushqimi
        const currentRecetaUshqimi = recetaUshqimi.filter(
          (item) => item.ushqimiId === ushqimiId
        );
  
        // Get the recetaIds that are currently associated with the Ushqimi
        const currentRecetaIds = currentRecetaUshqimi.map((item) => item.recetaId);
  
        // Step 1: Remove associations for unselected Receta (this is where we fix the issue)
        for (let recetaId of currentRecetaIds) {
          if (!selectedRecetat.includes(recetaId)) {
            try {
              await api.delete(`/RecetaUshqimi/${recetaId}/${ushqimiId}`);
            } catch (error) {
              console.error(`Failed to delete RecetaUshqimi with recetaId=${recetaId} and ushqimiId=${ushqimiId}:`, error);
            }
          }
        }
  
        // Step 2: Add associations for newly selected Receta (if not already associated)
        for (let recetaId of selectedRecetat) {
          if (!currentRecetaIds.includes(recetaId)) {
            try {
              await api.post('/RecetaUshqimi', { ushqimiId, recetaId });
            } catch (error) {
              console.error(`Failed to add RecetaUshqimi with recetaId=${recetaId} and ushqimiId=${ushqimiId}:`, error);
            }
          }
        }
      } else {
        // For new Ushqimi, add all selected RecetaUshqimi associations
        for (let recetaId of selectedRecetat) {
          try {
            await api.post('/RecetaUshqimi', {
              ushqimiId: ushqimiResponse.data.id,
              recetaId,
            });
          } catch (error) {
            console.error(`Failed to add RecetaUshqimi with recetaId=${recetaId}:`, error);
          }
        }
      }
  
      // Refresh data and reset the form
      await fetchRecetat();
      await fetchRecetaUshqimi();
      setNewUshqimi({
        emri: '',
        kalori: 0,
        proteina: 0,
        karbohidrate: 0,
        yndyrna: 0,
        fibrat: 0,
        vitaminC: 0,
        vitaminA: 0,
        kalcium: 0,
        hekur: 0,
        vegan: false,
        kaGluten: false,
        kaBulmet: false,
        kategoria: '',
        origjina: '',
        imagePath: '',
        receta: [],
      });
      setSelectedRecetat([]);
      setIsEditing(false);
      fetchUshqimet();

    } catch (error) {
      console.error('Error submitting receta:', error);
    }
  };
  
  
  

  const deleteUshqimi = async (id) => {
    if (window.confirm('Are you sure you want to delete this ushqimi?')) {
      try {
        await api.delete(`/Ushqimi/${id}`);
        fetchUshqimet();
      } catch (error) {
        console.error('Error deleting ushqimet:', error);
      }
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
  

  const handleCheckboxChange = (e) => {
    const recetaId = parseInt(e.target.value, 10); // Ensure the value is treated as a number
  
    if (e.target.checked) {
      // Add the recetaId to the selectedRecetat array if it is checked
      setSelectedRecetat((prevSelected) => [...prevSelected, recetaId]);
    } else {
      // Remove the recetaId from the selectedRecetat array if it is unchecked
      setSelectedRecetat((prevSelected) =>
        prevSelected.filter((id) => id !== recetaId)
      );
    }
  };
  
  
  

  return (
    <div className="container mx-auto p-6">
<h1 className="text-4xl font-bold text-center mb-8 bg-clip-text text-transparent bg-gradient-to-r from-purple-700 via-pink-500 to-red-500">
  Menaxhmenti i Ushqimeve
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
                  <p><strong>Data e Krijimit:</strong> {
    new Date(ushqimi.dataKrijimit).toLocaleString('sq-KS', {
        weekday: 'long', // Full day name
        year: 'numeric', // Full year
        month: 'long', // Full month name
        day: 'numeric', // Day of the month
        hour: '2-digit', // Two-digit hour
        minute: '2-digit', // Two-digit minute
        second: '2-digit', // Two-digit second
        hour12: false, // 24-hour format
    })
}</p>

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
              <div className="px-4 py-2 bg-gray-100 text-right">
                <button
                  onClick={() => handleEdit(ushqimi)}
                  className="bg-yellow-500 text-white px-4 py-2 rounded-md hover:bg-yellow-600 focus:outline-none mr-2"
                >
                  Përmirëso
                </button>
                <button
                  onClick={() => deleteUshqimi(ushqimi.id)}
                  className="bg-red-500 text-white px-4 py-2 rounded-md hover:bg-red-600 focus:outline-none"
                >
                  Fshijë
                </button>
              </div>
            </div>
          ))}
        </div>
      </div>
  
      {/* Form to create or update a Ushqimi */}
      <div className="bg-white p-6 shadow-md rounded-lg">
        <h2 className="text-2xl font-semibold mb-6">{isEditing ? "Përmirëso Ushqimin" : "Krijo Ushqimin"}</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {/* Form Fields */}
          <div>
            <label className="block text-sm font-medium text-gray-700">Emri</label>
            <input
              type="text"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Emri"
              value={newUshqimi.emri}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, emri: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Kalori</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Kalori"
              value={newUshqimi.kalori}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, kalori: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Proteina</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Proteina"
              value={newUshqimi.proteina}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, proteina: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Karbohidrate</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Karbohidrate"
              value={newUshqimi.karbohidrate}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, karbohidrate: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Yndyrna</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Yndyrna"
              value={newUshqimi.yndyrna}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, yndyrna: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Fibrat</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Fibrat"
              value={newUshqimi.fibrat}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, fibrat: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Vitamin C</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Vitamin C"
              value={newUshqimi.vitaminC}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, vitaminC: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Vitamin A</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Vitamin A"
              value={newUshqimi.vitaminA}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, vitaminA: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Kalcium</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Kalcium"
              value={newUshqimi.kalcium}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, kalcium: e.target.value })}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">Hekur</label>
            <input
              type="number"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Hekur"
              value={newUshqimi.hekur}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, hekur: e.target.value })}
            />
          </div>
          <div>
  <label className="block text-sm font-medium text-gray-700">Origjina</label>
  <select
    className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
    value={newUshqimi.origjina}
    onChange={(e) => setNewUshqimi({ ...newUshqimi, origjina: e.target.value })}
  >
    <option value="" disabled hidden>
      Zgjidh një origjinë
    </option>
    <option value="Kosova">Kosova</option>
    <option value="Internacionale">Internacionale</option>
    <option value="Evropa">Evropa</option>
    <option value="Azia">Azia</option>
    <option value="Amerika Veriore">Amerika Veriore</option>
    <option value="Amerika Jugore">Amerika Jugore</option>
    <option value="Afrika">Afrika</option>
    
  </select>
</div>

          <div>
  <label className="block text-sm font-medium text-gray-700">Kategoria</label>
  <select
    className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
    value={newUshqimi.kategoria}
    onChange={(e) => setNewUshqimi({ ...newUshqimi, kategoria: e.target.value })}
  >
    <option value="" disabled hidden>
      Zgjidh një kategori
    </option>
    <option value="Ushqime Tradicionale">Ushqime Tradicionale</option>
    <option value="Fast Food">Fast Food</option>
    <option value="Ushqime Deti">Ushqime Deti</option>
    <option value="Drithëra dhe Produkte të Tyre">Drithëra dhe Produkte të Tyre</option>
    <option value="Ushqime Vegane dhe Vegetariane">Ushqime Vegane dhe Vegetariane</option>
    <option value="Produktet e Bulmetit">Produktet e Bulmetit</option>
    <option value="Ushqime të Përpunuara">Ushqime të Përpunuara</option>
    <option value="Ushqime të Gatuara në Shtëpi">Ushqime të Gatuara në Shtëpi</option>
    <option value="Ëmbëlsira dhe Pastiçeri">Ëmbëlsira dhe Pastiçeri</option>
    <option value="Pije">Pije</option>
    <option value="Milkshakes">Milkshakes</option>

  </select>
</div>

          <div>
  <label className="block text-sm font-medium text-gray-700">Vegan</label>
  <input
    type="checkbox"
    className="mt-1 block px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
    checked={newUshqimi.vegan}
    onChange={(e) => setNewUshqimi({ ...newUshqimi, vegan: e.target.checked })}
  />

<label className="block text-sm font-medium text-gray-700">kaGluten</label>
  <input
    type="checkbox"
    className="mt-1 block px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
    checked={newUshqimi.kaGluten}
    onChange={(e) => setNewUshqimi({ ...newUshqimi, kaGluten: e.target.checked })}
  />
  <label className="block text-sm font-medium text-gray-700">kaBulmet</label>
  <input
    type="checkbox"
    className="mt-1 block px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
    checked={newUshqimi.kaBulmet}
    onChange={(e) => setNewUshqimi({ ...newUshqimi, kaBulmet: e.target.checked })}
  />
</div>

          <div>
            <label className="block text-sm font-medium text-gray-700">Image Path</label>
            <input
              type="text"
              className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-blue-500 focus:border-blue-500"
              placeholder="Image Path"
              value={newUshqimi.imagePath}
              onChange={(e) => setNewUshqimi({ ...newUshqimi, imagePath: e.target.value })}
            />
          </div>
        </div>
        <div className="mb-4">
          <label className="block text-sm font-medium text-gray-700">Recetat</label>
         <div>
  {receta.map((receta) => (
    <div key={receta.id} className="flex items-center mb-2">
      <input
        type="checkbox"
        id={`receta-${receta.id}`} // Make sure each checkbox has a unique ID
        value={receta.id}
        className="mr-2"
        onChange={handleCheckboxChange} // Handle checkbox state change
        checked={selectedRecetat.includes(receta.id)} // Check if the recetaId is in the selectedRecetat array
      />
      <label htmlFor={`receta-${receta.id}`} className="text-gray-700">
        {receta.emri}
      </label>
    </div>
  ))}
</div>

        </div>
  
        {/* Update or Create Button */}
        <div className="mt-6 text-right">
          <button
            onClick={handleSubmit}
            className="bg-blue-500 text-white px-6 py-2 rounded-md hover:bg-blue-600 focus:outline-none"
          >
            {isEditing ? "Përmirëso Ushqimin" : "Krijo Ushqimin"}
          </button>
        </div>
      </div>
    </div>
  );
  
};

export default Ushqimi;
