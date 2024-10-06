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
        'c-border-grid': '#37486E',

        'c-text-normal': '#9fa6bc',
        'c-text-data': '#90A1D3',
        'c-text-grid': '#85A9FF',
        'c-text-shadow': '#6773B4',

        'c-grid-icon': '#85A9FF',
        'c-remove-icon': '#F66E6E',

        'c-logo': '#ffae00'
      }
    },
  },
  plugins: [],
}

