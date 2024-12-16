import React from 'react';

const About = () => {
  return (
    <div>
      {/* Main Content Section */}
      <section className="bg-gradient-to-r from-indigo-500 via-purple-500 to-pink-500 text-white py-24">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h1 className="text-3xl sm:text-4xl md:text-5xl font-extrabold mb-6">
            Rreth Platformës Tonë
          </h1>
          <p className="text-base sm:text-lg mb-8 md:mb-12">
            Ne jemi këtu për t'ju ndihmuar të përmirësoni jetën tuaj përmes ushqimeve të shëndetshme dhe dietave të personalizuara. Misioni ynë është të krijojmë një komunitet që kujdeset për shëndetin dhe mirëqenien.
          </p>
        </div>
      </section>

      {/* Mission Section */}
      <section className="bg-indigo-100 py-12 sm:py-16">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-2xl sm:text-3xl font-bold text-indigo-600 mb-4 sm:mb-6">Misioni Ynë</h2>
          <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">
            Të ndihmojmë njerëzit të arrijnë qëllimet e tyre shëndetësore dhe të jetojnë një jetë më të shëndetshme.
          </p>
          <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">
            Të ndërtojmë një platformë të besueshme që frymëzon dhe edukon për rëndësinë e ushqimeve të freskëta dhe natyrale.
          </p>
        </div>
      </section>

      {/* Team Section */}
      <section className="py-16 sm:py-20 bg-white">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-3xl sm:text-4xl font-bold text-gray-800 mb-8 sm:mb-12">Kush Jemi Ne?</h2>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6 sm:gap-8">
            {/* Team Member 1 */}
            <div className="bg-gray-100 p-6 sm:p-8 rounded-lg shadow-lg hover:shadow-2xl transition-shadow duration-300">
              <h3 className="text-xl sm:text-2xl font-semibold text-indigo-600 mb-4">Ekspertë në Ushqim</h3>
              <p className="text-sm sm:text-base text-gray-700">
                Ne jemi një ekip i përkushtuar me përvojë në ushqime dhe dieta. Qëllimi ynë është të ofrojmë mbështetje cilësore dhe të personalizuar.
              </p>
            </div>

            {/* Team Member 2 */}
            <div className="bg-gray-100 p-6 sm:p-8 rounded-lg shadow-lg hover:shadow-2xl transition-shadow duration-300">
              <h3 className="text-xl sm:text-2xl font-semibold text-indigo-600 mb-4">Mësues të Përkushtuar</h3>
              <p className="text-sm sm:text-base text-gray-700">
                Ne edukojmë për rëndësinë e të ushqyerit shëndetshëm dhe mënyrat për të përmirësuar cilësinë e jetës përmes ushqimeve të freskëta.
              </p>
            </div>

            {/* Team Member 3 */}
            <div className="bg-gray-100 p-6 sm:p-8 rounded-lg shadow-lg hover:shadow-2xl transition-shadow duration-300">
              <h3 className="text-xl sm:text-2xl font-semibold text-indigo-600 mb-4">Partnerë të Komunitetit</h3>
              <p className="text-sm sm:text-base text-gray-700">
                Ne bashkëpunojmë me komunitete lokale për të siguruar që të gjithë të kenë akses në ushqime të freskëta dhe shëndetësore.
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* Values Section */}
      <section className="bg-yellow-100 py-12 sm:py-16">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-2xl sm:text-3xl font-bold text-yellow-600 mb-4 sm:mb-6">Vlerat Tonë Kryesore</h2>
          <div className="text-left inline-block">
            <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">1. Kujdes për cilësinë e ushqimeve dhe shëndetin.</p>
            <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">2. Transparencë dhe integritet në gjithçka që bëjmë.</p>
            <p className="text-sm sm:text-base text-gray-700 mb-2 sm:mb-4">3. Inovacion dhe përmirësim i vazhdueshëm.</p>
          </div>
        </div>
      </section>

      {/* Call to Action */}
      <section className="bg-indigo-600 text-white py-16 sm:py-20">
        <div className="max-w-7xl mx-auto text-center px-4 sm:px-6 lg:px-8">
          <h2 className="text-2xl sm:text-4xl font-extrabold mb-4 sm:mb-6">
            Bashkohuni me Ne Sot!
          </h2>
          <p className="text-sm sm:text-lg mb-6 sm:mb-8">
            Na ndihmoni të ndërtojmë një të ardhme më të shëndetshme dhe më të lumtur së bashku.
          </p>
        </div>
      </section>
    </div>
  );
};

export default About;
