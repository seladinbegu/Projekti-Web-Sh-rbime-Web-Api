import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Contact = () => {

  // Food options and their names with new URLs
  const foods = [
    { name: 'Pizza', image: 'https://thumbs.dreamstime.com/z/cute-d-icon-pizza-slice-isolated-white-background-d-design-pizza-slice-over-white-background-generative-ai-274501353.jpg' },
    { name: 'Burger', image: 'https://img.freepik.com/free-psd/3d-rendering-delicious-cheese-burger_23-2149108546.jpg' },
    { name: 'Sushi', image: 'https://pics.craiyon.com/2023-11-01/2c0ab473eadb4f22b953d9b6e9c9f108.webp' },
    { name: 'Pasta', image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcThtfeBq_1VfKqvnmXNfZK2eezjV87YRqBkug&s' },
    { name: 'Akullore', image: 'https://png.pngtree.com/png-vector/20240513/ourmid/pngtree-ice-cream-3d-illustration-rendering-png-image_12436747.png' },
  ];

  // State variables
  const [currentFood, setCurrentFood] = useState(getRandomFood());
  const [userGuess, setUserGuess] = useState('');
  const [result, setResult] = useState('');

  // Function to get a random food from the list
  function getRandomFood() {
    return foods[Math.floor(Math.random() * foods.length)];
  }

  // Handle the user's guess
  const handleGuess = () => {
    if (userGuess.toLowerCase() === currentFood.name.toLowerCase()) {
      setResult('Saktë! Mirë bërë!');
    } else {
      setResult(`Ups! Përgjigja e saktë është ${currentFood.name}.`);
    }
  };

  // Function to reset the game
  const resetGame = () => {
    setUserGuess('');
    setResult('');
    setCurrentFood(getRandomFood());
  };

  // Use navigate hook for redirection
  const navigate = useNavigate();

  const handleRedirect = () => {
    navigate('/register');
  };

  return (
    <div>
      {/* Contact Header Section */}
      <section className="bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white py-16 sm:py-20">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h1 className="text-3xl sm:text-4xl md:text-5xl font-extrabold mb-6">
            Kontaktoni Me Ne
          </h1>
          <p className="text-base sm:text-lg md:text-xl mb-6">
            Jemi këtu për të ndihmuar. Nëse keni pyetje, sugjerime, apo komente, ju lutemi mos hezitoni të na kontaktoni!
          </p>

          {/* Contact Information */}
          <div className="mt-8 space-y-4">
            <p className="text-lg sm:text-xl">
              <strong>Email:</strong> <a href="mailto:prtenium@gmail.com" className="text-yellow-300">prtenium@gmail.com</a>
            </p>
            <p className="text-lg sm:text-xl ">
            <strong>Adresa:</strong> <span className="text-yellow-300">Rruga Adem Jashari, Qyteti Vushtrri, 42000</span>
            </p>
            <p className="text-lg sm:text-xl">
              <strong>Telefoni:</strong> <a href="tel:+38349456780" className="text-yellow-300">+383 49 456 780</a>
            </p>
          </div>
        </div>
      </section>

      {/* Guess the Food Game Section */}
      <section className="py-16 bg-white text-center">
        <h2 className="text-2xl sm:text-3xl font-bold text-gray-800 mb-8">
          Gjeje Ushqimin!
        </h2>
        <div className="mb-4">
          <img
            src={currentFood.image}
            alt="Food"
            className="mx-auto mb-4 w-64 h-64 object-cover rounded-lg shadow-lg"
          />
          <p className="text-lg text-gray-700 mb-4">Cili është ky ushqim?</p>
          <input
            type="text"
            value={userGuess}
            onChange={(e) => setUserGuess(e.target.value)}
            className="p-2 border border-gray-300 rounded-lg mb-4"
            placeholder="Shkruani përgjigjen tuaj"
          />
          <button
            onClick={handleGuess}
            className="bg-blue-500 text-white py-2 px-6 rounded-full mb-4 hover:bg-blue-600"
          >
            Gjeje
          </button>
          {result && <p className="text-xl font-bold text-gray-800">{result}</p>}
        </div>
        <button
          onClick={resetGame}
          className="bg-green-500 text-white py-2 px-6 rounded-full hover:bg-green-600"
        >
          Provo Përsëri
        </button>
      </section>

      {/* Call to Action Section */}
      <section className="bg-indigo-600 text-white py-12 sm:py-16">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-2xl sm:text-4xl font-extrabold mb-4">
            Gati për të filluar?
          </h2>
          <p className="text-sm sm:text-lg mb-6">
            Mos hezitoni të na kontaktoni për çdo pyetje apo sugjerim!
          </p>
          <button
            className="inline-block bg-yellow-500 text-black py-2 px-6 sm:py-3 sm:px-8 rounded-full font-semibold hover:bg-yellow-600 transition-all duration-300"
            onClick={handleRedirect}
          >
            Regjistrohu
          </button>
        </div>
      </section>
    </div>
  );
};

export default Contact;
