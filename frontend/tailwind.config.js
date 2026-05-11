export default {
  content: ['./index.html', './src/**/*.{js,jsx}'],
  theme: {
    extend: {
      colors: {
        primary: { DEFAULT: '#1d4ed8', hover: '#1e40af' },
        success: '#16a34a',
        warning: '#d97706',
        danger: '#dc2626',
        surface: '#f8fafc',
      },
      fontFamily: {
        sans: ['"DM Sans"', 'sans-serif'],
        mono: ['"JetBrains Mono"', 'monospace'],
      }
    },
  },
  plugins: [],
}