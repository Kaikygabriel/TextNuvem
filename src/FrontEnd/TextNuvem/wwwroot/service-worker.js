self.importScripts('./service-worker-assets.js');

const cacheNamePrefix = 'offline-cache-';
const cacheName = `${cacheNamePrefix}${self.assetsManifest.version}`;

const assets = self.assetsManifest.assets
    .filter(asset =>
        asset.url.match(/\.(dll|pdb|wasm|html|js|json|css)$/));

self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(cacheName).then(cache =>
            cache.addAll(assets.map(asset => asset.url))
        )
    );
});

self.addEventListener('fetch', event => {
    event.respondWith(
        caches.match(event.request)
            .then(response => response || fetch(event.request))
    );
});