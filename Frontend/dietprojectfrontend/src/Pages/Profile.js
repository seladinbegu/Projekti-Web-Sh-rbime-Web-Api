import React, { useEffect, useState, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { FaUserAlt, FaShieldAlt, FaCog, FaSignOutAlt } from "react-icons/fa";
import api from "../Components/api";

const Profile = ({ username, role, handleLogout }) => {
  const navigate = useNavigate();
  const [favoriteRecipes, setFavoriteRecipes] = useState([]); // State to store favorite recipes

  // Fetch user ID based on username
  const fetchUserId = useCallback(async () => {
    if (username) {
      try {
        const response = await api.get(`http://localhost:5177/users/by-username?username=${username}`);
        if (response.data && response.data.id) {
          return response.data.id; // Return the user ID
        } else {
          console.error("User ID not found");
          return null;
        }
      } catch (error) {
        console.error("Error fetching user ID:", error);
        return null;
      }
    }
  }, [username]); // Add username as dependency

  // Fetch favorite recipes based on user ID
  const fetchFavoriteRecipes = useCallback(async () => {
    try {
      const userId = await fetchUserId(); // Fetch user ID
      if (userId) {
        const response = await api.get(`http://localhost:5177/api/RecetaUser/user/${userId}`);
        const recipes = response.data;
    
        // If no recipes found, set an empty array
        if (recipes && recipes.length > 0) {
          setFavoriteRecipes(recipes); // Store the favorite recipes
          fetchRecipeNames(recipes); // Fetch and update recipe names
        } else {
          setFavoriteRecipes([]); // No recipes found, set empty array
          console.log("No favorite recipes found.");
        }
      }
    } catch (error) {
      // Handle errors without crashing
      console.error("Error fetching data:", error);
      setFavoriteRecipes([]); // Set empty array on error
    }
    
  }, [fetchUserId]); // Add fetchUserId as a dependency

  // Fetch recipe names using the recipe IDs
  const fetchRecipeNames = async (recipes) => {
    try {
        // Extract recipe IDs from the favorite recipes
        const recipeIds = recipes.map((receta) => receta.recetaId); // Using 'recetaId'

        // Construct the query string with all the IDs
        const idsParam = recipeIds.map(id => `ids=${id}`).join('&');

        // Log the final URL to ensure it's correct
        const url = `http://localhost:5177/api/Receta/emri?${idsParam}`;
        console.log(`Requesting URL: ${url}`);

        // Send the GET request with the correct query string
        const response = await api.get(url);

        // Map over the recipes and update them with their names
        const updatedRecipes = recipes.map((receta) => {
            const foundRecipe = response.data.find((item) => item.id === receta.recetaId);
            return { ...receta, emri: foundRecipe ? foundRecipe.emri : 'Unknown' };
        });

        setFavoriteRecipes(updatedRecipes); // Update state with recipe names
    } catch (error) {
        console.error("Error fetching recipe names:", error);
    }
};



const handleRecipeClick = (id) => {
  navigate(`/receta/${id}`);  // Navigate to RecetaDetail using the recipe id
};

  

  // Fetch recipes on component mount or when username changes
  useEffect(() => {
    fetchFavoriteRecipes();
  }, [fetchFavoriteRecipes]); // Include fetchFavoriteRecipes as a dependency

  return (
    <div className="min-h-screen bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white flex flex-col items-center justify-center">
      {/* Profile Card */}
      <div className="bg-white text-gray-800 p-6 rounded-lg shadow-lg w-80 hover:shadow-2xl transition duration-300 ease-in-out transform hover:scale-105">
        {/* Profile Header */}
        <div className="flex items-center justify-center mb-6">
          <div className="w-16 h-16 bg-gray-300 rounded-full flex items-center justify-center text-white text-3xl font-bold">
            {username ? username[0].toUpperCase() : "U"}
          </div>
        </div>

        {/* Welcome Text */}
        <h1 className="text-2xl font-bold mb-4 text-center text-indigo-600">
          Welcome to Your Profile
        </h1>

        {/* Username and Role */}
        <div className="text-center">
          <p className="text-lg mb-2">
            <FaUserAlt className="inline-block text-indigo-600 mr-2" />
            <strong>Username:</strong> {username}
          </p>
          <p className="text-lg mb-4">
            <FaShieldAlt className="inline-block text-indigo-600 mr-2" />
            <strong>Role:</strong> {role === "Admin" ? "Administrator" : "User"}
          </p>
        </div>

        {/* Favorite Recipes */}
        <div className="mt-6">
          <h2 className="text-xl font-bold text-center text-indigo-600 mb-4">Your Favorite Recipes</h2>
          <ul className="list-disc pl-6">
          {favoriteRecipes.length > 0 ? (
  favoriteRecipes.map((receta) => (
    <li
      key={receta.id}
      className="text-lg cursor-pointer text-indigo-500 hover:underline"
      onClick={() => handleRecipeClick(receta.recetaId)} // Use recetaId here
    >
      {receta.emri} {/* Displaying recipe name */}
    </li>
  ))
) : (
  <p className="text-center text-gray-600">No favorite recipes found.</p>
)}

          </ul>
        </div>

        {/* Settings and Logout Buttons */}
        <div className="mt-6">
          <button
            onClick={() => navigate("/settings")}
            className="w-full bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white font-semibold py-2 rounded-md mb-3 transition duration-300 ease-in-out hover:opacity-90 flex items-center justify-center"
          >
            <FaCog className="mr-2" /> Settings
          </button>

          <button
            onClick={handleLogout}
            className="w-full bg-red-500 text-white font-semibold py-2 rounded-md transition duration-300 ease-in-out hover:opacity-90 flex items-center justify-center"
          >
            <FaSignOutAlt className="mr-2" /> Logout
          </button>
        </div>
      </div>
    </div>
  );
};

export default Profile;
