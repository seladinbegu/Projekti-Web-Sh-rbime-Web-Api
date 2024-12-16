import React, { useEffect, useState } from 'react';
import axios from 'axios';


const Ushqimi = () => {
  const [ushqimet, setUshqimet] = useState([]);
  const [newUshqimi, setNewUshqimi] = useState({
    emri: '',
    kalori: '',
    proteina: '',
    karbohidrate: '',
    yndyrna: '',
    fibrat: '',
    vitaminC: '',
    vitaminA: '',
    kalcium: '',
    hekur: '',
    vegan: false,
    kaGluten: false,
    kaBulmet: false,
    kategoria: '',
    origjina: '',
    imagePath: '',
  });
  const [isEditing, setIsEditing] = useState(false);

  
  const apiUrl = 'http://localhost:5177/api/Ushqimi'; // Replace with your actual API URL
  
  useEffect(() => {
    fetchUshqimet();
  }, []);
  
  // Fetch all Ushqimet
  const fetchUshqimet = async () => {
    try {
      const response = await axios.get(apiUrl, {
        withCredentials: true, // Ensure cookies are sent with the request
      });
      console.log("Fetched data:", response.data);  // Check if it's an array
      setUshqimet(Array.isArray(response.data) ? response.data : []); // Ensure it's an array
    } catch (error) {
      console.error("Error fetching ushqimet:", error);
    }
  };
  
  const handleEdit = (ushqimi) => {
    setNewUshqimi({ ...ushqimi });  // Set the form fields to the selected 'ushqimi'
    setIsEditing(true); // Set editing mode to true
  };
  
  const handleSubmit = async (e) => {
    e.preventDefault();
  
    // Ensure that all required fields are filled before proceeding
    if (!newUshqimi.emri || !newUshqimi.kategoria) {
      console.error('Please fill out the required fields.');
      return; // Prevent submission if required fields are missing
    }
  
    try {
      // Determine the HTTP method based on whether you're editing or creating
      const method = isEditing ? 'PUT' : 'POST';
      const requestData = { ...newUshqimi }; // Copy state data for submission
  
      // Ensure `id` is included for editing
      if (isEditing) {
        requestData.id = newUshqimi.id;
      }
  
      // Set the correct API URL for creating or updating
      const url = isEditing ? `${apiUrl}/${newUshqimi.id}` : apiUrl;
  
      // Perform the API request (POST for creating, PUT for editing)
      const response = await axios({
        method: method,
        url: url,
        data: requestData,
        withCredentials: true, // Ensure cookies are sent with the request
      });
  
      // If the request was successful (status 200 or 201 for creation)
      if (response.status === 200 || response.status === 201) {
        // Fetch updated data
        fetchUshqimet();
  
        // Reset form state to clear inputs
        setNewUshqimi({
          emri: '',
          kalori: '',
          proteina: '',
          karbohidratet: '',
          yndyrna: '',
          fibrat: '',
          vitaminC: '',
          vitaminA: '',
          kalcium: '',
          hekur: '',
          vegan: false,
          kaGluten: false,
          kaBulmet: false,
          kategoria: '',
          origjina: '',
          imagePath: '',
        });
  
        // Reset the editing state if in edit mode
        setIsEditing(false);
      }
    } catch (error) {
      console.error('Error creating/updating Ushqimi:', error);
      // You can also display an error message to the user here
    }
  };
  
  // Handle delete
  const deleteUshqimi = async (id) => {
    const confirmDelete = window.confirm("Are you sure you want to delete this Ushqimi?");
    if (confirmDelete) {
      try {
        await axios.delete(`${apiUrl}/${id}`, {
          withCredentials: true, // Ensure cookies are sent with the request
        });
        fetchUshqimet(); // Refresh the list
      } catch (error) {
        console.error("Error deleting ushqimi:", error);
      }
    }
  };
  
 
  

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-4xl font-bold text-center mb-8">Menaxhmenti i Ushqimeve</h1>
  
      <div className="mb-8">
        <h2 className="text-2xl font-semibold mb-6 text-center">Ushqimet Ekzistuse</h2>
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
