import React from "react";
import { Link } from "react-router-dom"; // Import Link for routing

const Header = () => {
  return (
    <header className="bg-blue-600 text-white p-4">
      <div className="container mx-auto flex justify-between items-center">
        <h1 className="text-2xl font-bold">
          <Link to="/" className="hover:text-gray-300">
            Your Logo
          </Link>
        </h1>
        <nav>
          <ul className="flex space-x-6">
            <li>
              <Link to="/ushqimi" className="hover:text-gray-300">
                Ushqimi
              </Link>
            </li>
            <li>
              <Link to="/another-page" className="hover:text-gray-300">
                Another Page
              </Link>
            </li>
          </ul>
        </nav>
      </div>
    </header>
  );
};

export default Header;
