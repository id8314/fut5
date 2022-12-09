// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
// Ver 1.0.7
const fut5Version = "1.0.7";
self.addEventListener('fetch', () => { });
