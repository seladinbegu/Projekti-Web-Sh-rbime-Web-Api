import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFacebook, faTwitter, faInstagram, faYoutube } from '@fortawesome/free-brands-svg-icons';

const Footer = () => {
  return (
    <footer className="bg-gray-800 text-white py-8 mt-auto">
      <div className="container mx-auto px-4 lg:px-8">
        <div className="flex flex-wrap justify-between">
          {/* Kompania */}
          <div className="w-full lg:w-1/4 mb-6 lg:mb-0">
            <h2 className="text-lg font-bold mb-4">PRTENIUM</h2>
            <p className="text-sm font-extrabold text-center text-pink-500">
              Delivering love since 2007
            </p>
          </div>

          {/* Lidhje të shpejta */}
          <div className="w-full lg:w-1/4 mb-6 lg:mb-0">
            <h2 className="text-lg font-bold mb-4">Lidhje të Shpejta</h2>
            <ul>
              <li className="mb-2">
                <a href="/" className="text-sm hover:bg-clip-text hover:text-transparent hover:bg-gradient-to-r hover:from-purple-700 hover:via-pink-500 hover:to-red-500">
                  Kreu
                </a>
              </li>
              <li className="mb-2">
                <a href="/ushqimi" className="text-sm hover:bg-clip-text hover:text-transparent hover:bg-gradient-to-r hover:from-purple-700 hover:via-pink-500 hover:to-red-500">
                  Ushqimet
                </a>
              </li>
              <li className="mb-2">
                <a href="/kontakt" className="text-sm hover:bg-clip-text hover:text-transparent hover:bg-gradient-to-r hover:from-purple-700 hover:via-pink-500 hover:to-red-500">
                  Kontakt
                </a>
              </li>
              <li className="mb-2">
                <a href="/aboutus" className="text-sm hover:bg-clip-text hover:text-transparent hover:bg-gradient-to-r hover:from-purple-700 hover:via-pink-500 hover:to-red-500">
                  Rreth Nesh
                </a>
              </li>
            </ul>
          </div>

          {/* Informata për kontaktin */}
          <div className="w-full lg:w-1/4 mb-6 lg:mb-0">
            <h2 className="text-lg font-bold mb-4">Kontakto</h2>
            <p className="text-sm">Rruga Adem Jashari, Qyteti Vushtrri, 42000</p>
            <p className="text-sm mt-2">Email: prtenium@gmail.com</p>
            <p className="text-sm mt-2">Telefoni: +383 49 456 780</p>
          </div>

          {/* Media Sociale */}
          <div className="w-full lg:w-1/4 mb-6 lg:mb-0">
            <h2 className="text-lg font-bold mb-4">Na Ndjekni</h2>
            <div className="flex space-x-4">
              <a href="https://facebook.com" target="_blank" rel="noopener noreferrer" className="text-sm hover:text-blue-400">
                <FontAwesomeIcon icon={faFacebook} size="lg" />
              </a>
              <a href="https://twitter.com" target="_blank" rel="noopener noreferrer" className="text-sm hover:text-blue-400">
                <FontAwesomeIcon icon={faTwitter} size="lg" />
              </a>
              <a href="https://instagram.com" target="_blank" rel="noopener noreferrer" className="text-sm hover:text-blue-400">
                <FontAwesomeIcon icon={faInstagram} size="lg" />
              </a>
              <a href="https://youtube.com" target="_blank" rel="noopener noreferrer" className="text-sm hover:text-blue-400">
                <FontAwesomeIcon icon={faYoutube} size="lg" />
              </a>
            </div>
          </div>
        </div>

        <div className="mt-8 border-t border-gray-700 pt-4 text-center">
          <p className="text-sm lg:text-base">
            &copy; 2007 - {new Date().getFullYear()} PRTENIUM, Të gjitha të
            drejtat e rezervuara.
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
