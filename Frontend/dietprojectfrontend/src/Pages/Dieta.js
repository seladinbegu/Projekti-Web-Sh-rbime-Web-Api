import React, { useState, useEffect } from 'react';
import { api } from '../Components/AxiosConfig';

const Dieta = () => {
  const [dietat, setDietat] = useState([]);
  const [newDieta, setNewDieta] = useState({
    emri: '',
    lloji: '',
    pershkrimi: '',
    dataKrijimit: new Date().toISOString(),
  });
  const [isEditing, setIsEditing] = useState(false);

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

  const handleEdit = (dieta) => {
    setNewDieta(dieta);
    setIsEditing(true);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const method = isEditing ? 'put' : 'post';
      const url = isEditing ? `/Dieta/${newDieta.id}` : '/Dieta';

      await api({
        method,
        url,
        data: newDieta,
      });

      fetchDietat();
      setNewDieta({ emri: '', lloji: '', pershkrimi: '', dataKrijimit: new Date().toISOString() });
      setIsEditing(false);
    } catch (error) {
      console.error('Error submitting dieta:', error);
    }
  };

  const deleteDieta = async (id) => {
    if (window.confirm('Are you sure you want to delete this dieta?')) {
      try {
        await api.delete(`/Dieta/${id}`); // Use `api` here
        fetchDietat();
      } catch (error) {
        console.error('Error deleting dieta:', error);
      }
    }
  };

  return (
    <div className="container mx-auto p-6">
      <h1 className="text-4xl font-bold text-center mb-8">Menaxhimi i Dietave</h1>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
        {dietat.map((dieta) => (
          <div key={dieta.id} className="bg-white shadow-md p-4 rounded-md">
            <h2 className="text-xl font-bold">{dieta.emri}</h2>
            <p className="text-gray-600">Lloji: {dieta.lloji}</p>
            <p className="text-gray-600">Përshkrimi: {dieta.pershkrimi}</p>
            <p className="text-gray-600">Data Krijimit: {new Date(dieta.dataKrijimit).toLocaleDateString()}</p>
            <div className="mt-4">
              <button
                onClick={() => handleEdit(dieta)}
                className="mr-2 bg-yellow-500 text-white px-4 py-2 rounded-md"
              >
                Përmirëso
              </button>
              <button
                onClick={() => deleteDieta(dieta.id)}
                className="bg-red-500 text-white px-4 py-2 rounded-md"
              >
                Fshijë
              </button>
            </div>
          </div>
        ))}
      </div>

      <form onSubmit={handleSubmit} className="bg-gray-100 p-6 mt-6 rounded-md">
        <h2 className="text-2xl font-bold mb-4">{isEditing ? 'Përmirëso Dietën' : 'Krijo Dietën'}</h2>
        <input
          type="text"
          placeholder="Emri"
          value={newDieta.emri}
          onChange={(e) => setNewDieta({ ...newDieta, emri: e.target.value })}
          className="block w-full mb-4 p-2 border rounded-md"
        />
        <input
          type="text"
          placeholder="Lloji"
          value={newDieta.lloji}
          onChange={(e) => setNewDieta({ ...newDieta, lloji: e.target.value })}
          className="block w-full mb-4 p-2 border rounded-md"
        />
        <textarea
          placeholder="Përshkrimi"
          value={newDieta.pershkrimi}
          onChange={(e) => setNewDieta({ ...newDieta, pershkrimi: e.target.value })}
          className="block w-full mb-4 p-2 border rounded-md"
        />
        <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded-md">
          {isEditing ? 'Përmirëso' : 'Krijo'}
        </button>
      </form>
    </div>
  );
};

export default Dieta;
