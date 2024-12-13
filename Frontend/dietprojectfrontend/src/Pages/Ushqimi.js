import React, { useState, useEffect } from 'react';
import axios from 'axios';

const BASE_URL = 'http://localhost:5177/api/Ushqimi'; // Adjust base URL if needed

const UshqimiList = () => {
  const [ushqimiList, setUshqimiList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
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
    pershkrimi: '',
    imagePath: '',
  });
  const [editingUshqimi, setEditingUshqimi] = useState(null);
  const [imagePreview, setImagePreview] = useState(null); // State for image preview

  // Fetch all Ushqimi items
  useEffect(() => {
    const fetchUshqimi = async () => {
      try {
        const response = await axios.get(BASE_URL);
        setUshqimiList(response.data);
      } catch (error) {
        setError('Error fetching Ushqimi data');
      } finally {
        setLoading(false);
      }
    };
    fetchUshqimi();
  }, []);

  // Handle delete operation
  const deleteUshqimi = async (id) => {
    try {
      await axios.delete(`${BASE_URL}/${id}`);
      setUshqimiList(ushqimiList.filter(ushqimi => ushqimi.id !== id));
    } catch (error) {
      setError('Error deleting Ushqimi');
    }
  };

  // Handle form input change
  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    if (editingUshqimi) {
      setEditingUshqimi({
        ...editingUshqimi,
        [name]: type === 'checkbox' ? checked : value,
      });
    } else {
      setNewUshqimi({
        ...newUshqimi,
        [name]: type === 'checkbox' ? checked : value,
      });
    }
  };

  // Handle image file input change
  const handleImageChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      // Set the preview image URL
      setImagePreview(URL.createObjectURL(file));
      if (editingUshqimi) {
        setEditingUshqimi({
          ...editingUshqimi,
          imagePath: file,
        });
      } else {
        setNewUshqimi({
          ...newUshqimi,
          imagePath: file,
        });
      }
    }
  };

  // Handle form submission for creating a new Ushqimi
  const handleCreateSubmit = async (e) => {
    e.preventDefault();
    try {
      const formData = new FormData();
      for (const key in newUshqimi) {
        formData.append(key, newUshqimi[key]);
      }

      const response = await axios.post(BASE_URL, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });

      setUshqimiList([...ushqimiList, response.data]);
      setNewUshqimi({
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
        pershkrimi: '',
        imagePath: '',
      });
      setImagePreview(null); // Reset image preview after submitting
    } catch (error) {
      setError('Error creating Ushqimi');
    }
  };

  // Handle form submission for updating an existing Ushqimi
  const handleUpdateSubmit = async (e) => {
    e.preventDefault();
    try {
      const formData = new FormData();
      for (const key in editingUshqimi) {
        formData.append(key, editingUshqimi[key]);
      }

      const response = await axios.put(`${BASE_URL}/${editingUshqimi.id}`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });

      setUshqimiList(ushqimiList.map(ushqimi => ushqimi.id === editingUshqimi.id ? response.data : ushqimi));
      setEditingUshqimi(null); // Reset the editing state after update
      setNewUshqimi({
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
        pershkrimi: '',
        imagePath: '',
      }); // Reset form state after submission
      setImagePreview(null); // Reset image preview after updating
    } catch (error) {
      setError('Error updating Ushqimi');
    }
  };

  // Loading state
  if (loading) return <div className="text-center text-xl">Loading...</div>;
  if (error) return <div className="text-center text-red-500">{error}</div>;

  return (
    <div className="max-w-6xl mx-auto p-6 bg-white rounded-lg shadow-md">
      <h1 className="text-3xl font-semibold text-gray-800 text-center mb-6">Ushqimi List</h1>

      {/* Create or Edit Ushqimi Form */}
      <div className="mb-6 p-4 bg-gray-100 rounded-lg shadow-sm">
        <h2 className="text-2xl font-medium text-gray-700 mb-4">
          {editingUshqimi ? 'Edit Ushqimi' : 'Create New Ushqimi'}
        </h2>
        <form onSubmit={editingUshqimi ? handleUpdateSubmit : handleCreateSubmit}>
          {/* Inputs for various fields */}
          <input
            type="text"
            name="emri"
            placeholder="Emri"
            value={editingUshqimi ? editingUshqimi.emri : newUshqimi.emri}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="number"
            name="kalori"
            placeholder="Kalori"
            value={editingUshqimi ? editingUshqimi.kalori : newUshqimi.kalori}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="proteina"
            placeholder="Proteina"
            value={editingUshqimi ? editingUshqimi.proteina : newUshqimi.proteina}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="karbohidrate"
            placeholder="Karbohidrate"
            value={editingUshqimi ? editingUshqimi.karbohidrate : newUshqimi.karbohidrate}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="yndyrna"
            placeholder="Yndyrna"
            value={editingUshqimi ? editingUshqimi.yndyrna : newUshqimi.yndyrna}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="fibrat"
            placeholder="Fibrat"
            value={editingUshqimi ? editingUshqimi.fibrat : newUshqimi.fibrat}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="vitaminC"
            placeholder="Vitamin C"
            value={editingUshqimi ? editingUshqimi.vitaminC : newUshqimi.vitaminC}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="vitaminA"
            placeholder="Vitamin A"
            value={editingUshqimi ? editingUshqimi.vitaminA : newUshqimi.vitaminA}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="kalcium"
            placeholder="Kalcium"
            value={editingUshqimi ? editingUshqimi.kalcium : newUshqimi.kalcium}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <input
            type="text"
            name="hekur"
            placeholder="Hekur"
            value={editingUshqimi ? editingUshqimi.hekur : newUshqimi.hekur}
            onChange={handleInputChange}
            className="mb-2 p-2 border rounded w-full"
          />
          <div className="flex items-center mb-2">
            <input
              type="checkbox"
              name="vegan"
              checked={editingUshqimi ? editingUshqimi.vegan : newUshqimi.vegan}
              onChange={handleInputChange}
              className="mr-2"
            />
            <label className="text-gray-700">Vegan</label>
          </div>

          {/* Image Upload */}
          <div className="mb-2">
            <input
              type="file"
              name="imagePath"
              accept="image/*"
              onChange={handleImageChange}
              className="border rounded p-2 w-full"
            />
            {imagePreview && <img src={imagePreview} alt="Preview" className="mt-2 h-40 object-contain" />}
          </div>

          <button
            type="submit"
            className="py-2 px-4 bg-blue-500 text-white rounded-lg w-full"
          >
            {editingUshqimi ? 'Update Ushqimi' : 'Create Ushqimi'}
          </button>
        </form>
      </div>

      {/* Ushqimi List Table */}
      <div className="overflow-x-auto">
        <table className="table-auto w-full border-collapse">
          <thead>
            <tr>
              <th className="py-2 px-4 border-b text-left">Emri</th>
              <th className="py-2 px-4 border-b text-left">Kalori</th>
              <th className="py-2 px-4 border-b text-left">Actions</th>
            </tr>
          </thead>
          <tbody>
            {ushqimiList.map((ushqimi) => (
              <tr key={ushqimi.id}>
                <td className="py-2 px-4 border-b">{ushqimi.emri}</td>
                <td className="py-2 px-4 border-b">{ushqimi.kalori}</td>
                <td className="py-2 px-4 border-b">
                  <button
                    onClick={() => setEditingUshqimi(ushqimi)}
                    className="mr-2 px-4 py-2 bg-yellow-500 text-white rounded-lg"
                  >
                    Edit
                  </button>
                  <button
                    onClick={() => deleteUshqimi(ushqimi.id)}
                    className="px-4 py-2 bg-red-500 text-white rounded-lg"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default UshqimiList;
