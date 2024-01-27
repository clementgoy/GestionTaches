const axios = require('axios');

module.exports = {
  devServer: {
    proxy: {
      '/api': {
        target: 'http://localhost:5138',
        changeOrigin: true,
        pathRewrite: {
          '^/api': '/api'
        }
      }
    }
  }
};

