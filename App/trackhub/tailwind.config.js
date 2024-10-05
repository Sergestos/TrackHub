/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}"
  ],
  theme: {
    extend: {
      colors: {
        'c-bg-accent': '#141824',
        'c-bg-normal': '#241824',
        'c-border': '#31374a',
        'c-text-low': '#9fa6bc'
      },
      fontSize: {
        '28': '28px'
      }
    },
  },
  plugins: [],
}

