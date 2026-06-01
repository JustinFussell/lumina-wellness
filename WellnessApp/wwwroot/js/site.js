// Lumina — minimal progressive enhancements
// HTMX + Alpine handle most interactions. This file is for small custom behaviors.

console.log('%c[Lumina] Serene wellness platform ready. Movement. Presence. Light.', 'color:#8B9A7D');

// Elegant scroll-triggered reveals (respects reduced motion)
document.addEventListener('DOMContentLoaded', () => {
  const prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  
  if (!prefersReduced) {
    const reveals = document.querySelectorAll('.reveal');
    if ('IntersectionObserver' in window && reveals.length) {
      const io = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
          if (entry.isIntersecting) {
            entry.target.classList.add('visible');
            io.unobserve(entry.target);
          }
        });
      }, { threshold: 0.12, rootMargin: '0px 0px -50px 0px' });
      reveals.forEach(el => io.observe(el));
    } else {
      reveals.forEach(el => el.classList.add('visible'));
    }
  }
});

// Optional: PWA install prompt scaffolding for later
let deferredPrompt;
window.addEventListener('beforeinstallprompt', (e) => {
  deferredPrompt = e;
});