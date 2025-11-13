/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  darkMode: 'class',
  theme: {
    extend: {
      colors: {
        java: '#f89820',
        python: '#3776ab',
        kotlin: '#7f52ff',
        rust: '#ce422b',
        csharp: '#239120',
        flutter: '#02569b',
        javascript: '#f7df1e',
      },
    },
  },
  plugins: [],
}
