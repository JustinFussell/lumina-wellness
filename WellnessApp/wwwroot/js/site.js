// Lumina — minimal progressive enhancements
// HTMX + Alpine handle most interactions. This file is for small custom behaviors.

console.log('%c[Lumina] Serene wellness platform ready. Movement. Presence. Light.', 'color:#8B9A7D');

// === CRISP & SMOOTH SCROLL EXPERIENCE ===

document.addEventListener('DOMContentLoaded', () => {
  const prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  const header = document.getElementById('main-header');

  // Premium header shrink + stronger glass on scroll (buttery smooth)
  if (header && !prefersReduced) {
    let ticking = false;
    
    const onScroll = () => {
      if (!ticking) {
        requestAnimationFrame(() => {
          if (window.scrollY > 28) {
            header.classList.add('scrolled');
          } else {
            header.classList.remove('scrolled');
          }
          ticking = false;
        });
        ticking = true;
      }
    };
    
    window.addEventListener('scroll', onScroll, { passive: true });
    // Initial state
    if (window.scrollY > 28) header.classList.add('scrolled');
  }

  // Elegant scroll-triggered reveals (respects reduced motion + supports stagger)
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
      }, { threshold: 0.13, rootMargin: '0px 0px -55px 0px' });
      
      reveals.forEach(el => io.observe(el));
    } else {
      reveals.forEach(el => el.classList.add('visible'));
    }
  }

  // Very light hero parallax (only on large screens, very subtle for crisp feel)
  if (!prefersReduced && window.innerWidth > 900) {
    const heroBg = document.querySelector('.parallax-bg');
    if (heroBg) {
      let lastY = window.scrollY;
      window.addEventListener('scroll', () => {
        const y = window.scrollY;
        const delta = Math.min((y - lastY) * 0.035, 18);
        heroBg.style.transform = `translateY(${delta}px)`;
        lastY = y;
      }, { passive: true });
    }
  }
});

// Optional: PWA install prompt scaffolding for later
let deferredPrompt;
window.addEventListener('beforeinstallprompt', (e) => {
  deferredPrompt = e;
});