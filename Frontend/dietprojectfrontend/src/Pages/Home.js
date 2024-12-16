import React from 'react';
import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div>
      {/* Main Content Section */}
      <section className="bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white py-24">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h1 className="text-3xl sm:text-4xl md:text-5xl font-extrabold mb-6">
            Mirësevini në Platformën Tuaj të Preferuar për Ushqimin dhe Dietat!
          </h1>
          <p className="text-base sm:text-lg mb-8 md:mb-12">
            Zbuloni funksione të fuqishme që do ta bëjnë përvojën tuaj më të lehtë dhe të shijshme. Ushqimi nuk ka qenë kurrë kaq i lehtë dhe i shijshëm!
          </p>
          <Link 
            to="/ushqimi" 
            className="inline-block bg-yellow-500 text-black py-3 px-6 sm:px-8 rounded-full text-sm sm:text-lg font-semibold hover:bg-yellow-600 transition-all duration-300"
          >
            Filloni Tani
          </Link>
        </div>
      </section>

      {/* Fun Fact Section */}
      <section className="bg-indigo-100 py-12 sm:py-16">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-2xl sm:text-3xl font-bold text-indigo-600 mb-4 sm:mb-6">Fun Fact për Ushqimin!</h2>
          <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">
            A e dini që një porcion i vogël borziloku mund të rrisë energjinë tuaj dhe të ndihmojë sistemin tuaj të tretjes?
          </p>
          <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">
            Ushqimi është gjithçka! E keni ditur që ngrënia e ushqimeve të freskëta mund të përmirësojë humorin dhe koncentrimin tuaj?
          </p>
          <p className="text-sm sm:text-base text-gray-700">
            Ne besojmë në fuqinë e ushqimeve natyrale dhe shëndetshme për të krijuar një jetë të lumtur dhe energjike.
          </p>
        </div>
      </section>

      {/* Features Section */}
      <section className="py-16 sm:py-20 bg-white">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-3xl sm:text-4xl font-bold text-gray-800 mb-8 sm:mb-12">Karakteristikat e Ushqimit dhe Dietave</h2>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 sm:gap-8">
            {/* Feature 1 */}
            <div className="bg-gray-100 p-6 sm:p-8 rounded-lg shadow-lg hover:shadow-2xl transition-shadow duration-300">
              <h3 className="text-xl sm:text-2xl font-semibold text-indigo-600 mb-4">Dieta të Personalizuara</h3>
              <p className="text-sm sm:text-base text-gray-700">
                Krijoni dieta që i përshtaten nevojave tuaja individuale dhe qëllimeve shëndetësore. U bëni dieta fun dhe efektive!
              </p>
            </div>

            {/* Feature 2 */}
            <div className="bg-gray-100 p-6 sm:p-8 rounded-lg shadow-lg hover:shadow-2xl transition-shadow duration-300">
              <h3 className="text-xl sm:text-2xl font-semibold text-indigo-600 mb-4">Receta të Shëndetshme</h3>
              <p className="text-sm sm:text-base text-gray-700">
                Zbuloni receta të shijshme dhe të shëndetshme që do të përmirësojnë mirëqenien tuaj. Ushqimi mund të jetë i shijshëm dhe i shëndetshëm!
              </p>
            </div>

            {/* Feature 3 */}
            <div className="bg-gray-100 p-6 sm:p-8 rounded-lg shadow-lg hover:shadow-2xl transition-shadow duration-300">
              <h3 className="text-xl sm:text-2xl font-semibold text-indigo-600 mb-4">Monitorimi i Përparimit</h3>
              <p className="text-sm sm:text-base text-gray-700">
                Monitoroni progresin tuaj në dietë dhe aktivitete fizike për të arritur qëllimet e shëndetshme. Shikoni çdo hap dhe ndihmuar veten të arrini suksesin.
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* Healthy Tips Section */}
      <section className="bg-yellow-100 py-12 sm:py-16">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-2xl sm:text-3xl font-bold text-yellow-600 mb-4 sm:mb-6">Këshilla Shëndetësore për një Jetë më të Shëndetshme</h2>
          <div className="text-left inline-block">
            <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">1. Konsumoni më shumë fruta dhe perime të freskëta për të pasur energji gjatë gjithë ditës.</p>
            <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">2. Pini shumë ujë për të mbajtur trupin tuaj të hidratuar dhe për të ndihmuar në tretje.</p>
            <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">3. Hani ushqime me lëndë ushqyese të pasura që ndihmojnë trupin tuaj të qëndrojë i fortë dhe i shëndetshëm.</p>
            <p className="text-sm sm:text-base text-gray-700">4. Mos harroni të flini mjaftueshëm për të rinovuar energjinë tuaj dhe për të mbajtur trupin në formë.</p>
          </div>
        </div>
      </section>

      {/* Call to Action */}
      <section className="bg-indigo-600 text-white py-16 sm:py-20">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-2xl sm:text-4xl font-extrabold mb-4 sm:mb-6">
            Gati për të Filluar?
          </h2>
          <p className="text-sm sm:text-lg mb-6 sm:mb-8">
            Bashkohuni me ne dhe filloni të shijoni përfitimet e dietave dhe ushqimeve të shëndetshme!
          </p>
          <Link 
            to="/register" 
            className="inline-block bg-yellow-500 text-black py-2 px-6 sm:py-3 sm:px-8 rounded-full text-sm sm:text-lg font-semibold hover:bg-yellow-600 transition-all duration-300"
          >
            Regjistrohu
          </Link>
        </div>
      </section>
    </div>
  );
};

export default Home;
