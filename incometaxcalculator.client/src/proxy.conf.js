const { env } = require('process');
const PROXY_CONFIG = [
  {
    context: [
      "/api",
      "/swagger"
    ],
    target: "https://localhost:7216",
    secure: false,
    changeOrigin: true,
    logLevel: "debug",
    pathRewrite: {
      "^/api": "/api"
    }
  }
];

console.log('Proxy configuration loaded');
module.exports = PROXY_CONFIG;
const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7216';

// const PROXY_CONFIG = [
//   {
//     context: [
//       "/weatherforecast",
//     ],
//     target,
//     secure: false
//   }
// ]
//
// module.exports = PROXY_CONFIG;
// const PROXY_CONFIG = [
//   {
//     context: [
//       "/api",
//       "/swagger"
//     ],
//     target: "https://localhost:7128",
//     secure: false,
//     changeOrigin: true
//   }
// ];
//
// module.exports = PROXY_CONFIG;
