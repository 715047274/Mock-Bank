import { defineConfig } from 'vite';
import reactRefresh from '@vitejs/plugin-react-refresh';
const path = require('path');

// https://vitejs.dev/config/
export default defineConfig({
  build:{
    outDir: '../MockBank.WebApi/wwwroot',
      rollupOptions: {
          output: {
              chunkFileNames: 'assets/js/[name]-[hash].js',
              entryFileNames: 'assets/js/[name]-[hash].js',
              assetFileNames: ({name}) => {
                  if (/\.(gif|jpe?g|png|svg)$/.test(name ?? '')) {
                      return 'assets/images/[name]-[hash][extname]';
                  }
                  if (/\.css$/.test(name ?? '')) {
                      return 'assets/css/[name]-[hash][extname]';
                  }
                  // default value
                  // ref: https://rollupjs.org/guide/en/#outputassetfilenames
                  return 'assets/[name]-[hash][extname]';
              },
          },
      }
  },
  resolve: {
    alias: [
        { find: '@', replacement: path.resolve(__dirname, '/src') }, 
        { find: '@can', replacement: path.resolve(__dirname, '/src/pages/BekeleyPayment') },
        { find: '@us', replacement: path.resolve(__dirname, '/src/pages/CentralPayment') }
    ],
  },
  plugins: [reactRefresh()],
});
