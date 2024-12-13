import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Header from "./Components/Header"; // Import your new header
import Ushqimi from "./Pages/Ushqimi"; // Your page components

function App() {
  return (
    <Router>
      <Header /> {/* The new header */}
      <div className="main-content">
        <Routes>
          <Route path="/ushqimi" element={<Ushqimi />} />
          {/* Add other routes here */}
        </Routes>
      </div>
    </Router>
  );
}

export default App;
