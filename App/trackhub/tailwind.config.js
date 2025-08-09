/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}"
  ],
  theme: {
    extend: {
      colors: {
        'c-bg-accent': '#0d1117',
        'c-bg-normal': '#241824',
        'c-bg-contrast': '#010409',

        'c-border': '#31374a',
        'c-border-grid': '#37486E',

        'c-text-normal': '#fffff0',        
        'c-text-grey': '#c2c6c9',
        'c-text-data': '#90A1D3',
        'c-text-grid': '#9fa6bc',
        'c-text-shadow': '#6773B4',

        'c-grid-icon': '#90A1D3',
        'c-remove-icon': '#F66E6E',

        'c-logo': '#ffae00'
      }
    },
  },
  plugins: [],
}

