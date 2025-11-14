import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path'

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'),
    },
  },
  server: {
    port: 3000,
    proxy: {
      '/api': {
        target: 'http://localhost:3001',
        changeOrigin: true,
      },
    },
  },
  build: {
    // Target modern browsers for better optimization
    target: 'es2015',

    // Optimize chunk size
    chunkSizeWarningLimit: 600,

    rollupOptions: {
      output: {
        // Manual chunk splitting for better caching
        manualChunks: {
          // React core
          'react-vendor': ['react', 'react-dom', 'react-router-dom'],

          // Monaco Editor (large dependency)
          'monaco': ['@monaco-editor/react', 'monaco-editor'],

          // Markdown rendering
          'markdown': ['react-markdown', 'remark-gfm', 'rehype-highlight'],

          // UI components
          'ui': ['lucide-react', 'clsx'],

          // State management
          'state': ['zustand'],
        },

        // Better chunk naming for debugging
        chunkFileNames: (chunkInfo) => {
          const facadeModuleId = chunkInfo.facadeModuleId
          if (facadeModuleId) {
            const name = facadeModuleId.split('/').slice(-1)[0].replace('.tsx', '').replace('.ts', '')
            return `chunks/${name}-[hash].js`
          }
          return 'chunks/[name]-[hash].js'
        },

        // Cleaner asset names
        assetFileNames: 'assets/[name]-[hash][extname]',
        entryFileNames: 'entries/[name]-[hash].js',
      },
    },

    // Enable minification
    minify: 'terser',
    terserOptions: {
      compress: {
        drop_console: true, // Remove console.logs in production
        drop_debugger: true,
      },
    },

    // Source maps for production debugging (can be disabled for smaller builds)
    sourcemap: false,

    // Report compressed size
    reportCompressedSize: true,
  },

  // Optimize dependency pre-bundling
  optimizeDeps: {
    include: [
      'react',
      'react-dom',
      'react-router-dom',
      'zustand',
      'lucide-react',
    ],
    exclude: ['@monaco-editor/react'], // Monaco is already optimized
  },
})
