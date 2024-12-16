/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,js,jsx,ts,tsx}", // Add all relevant file extensions
  ],
  theme: {
    extend: {
      colors: {
        logoColor1: "#2596be",
        logoColor2: "rgb(37, 150, 190)",
      }
    },
  },
  plugins: [],
}
