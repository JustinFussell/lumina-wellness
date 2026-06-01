// Lumina — minimal progressive enhancements
// HTMX + Alpine handle most interactions. This file is for small custom behaviors.

console.log('%c[Lumina] Serene wellness platform ready. Movement. Presence. Light.', 'color:#8B9A7D');

// === LUMINA 2026 — REALLY MODERN INTERACTIONS ===

document.addEventListener('DOMContentLoaded', () => {
  const prefersReduced = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  const header = document.getElementById('main-header');
  const nav = document.getElementById('desktop-nav');

  // Modern header scroll behavior (shrink + glass intensification)
  if (header && !prefersReduced) {
    let ticking = false;
    const onScroll = () => {
      if (!ticking) {
        requestAnimationFrame(() => {
          header.classList.toggle('scrolled', window.scrollY > 32);
          ticking = false;
        });
        ticking = true;
      }
    };
    window.addEventListener('scroll', onScroll, { passive: true });
    if (window.scrollY > 32) header.classList.add('scrolled');
  }

  // Modern active nav highlighting (client-side, crisp, no server dependency)
  if (nav) {
    const path = window.location.pathname.toLowerCase();
    const links = nav.querySelectorAll('a[data-page]');
    links.forEach(link => {
      const page = link.dataset.page;
      if ((path === '/' || path.includes('index')) && page === 'index') {
        link.classList.add('active', 'text-lumina-sage');
      } else if (path.includes(page)) {
        link.classList.add('active', 'text-lumina-sage');
      }
    });
  }

  // Advanced modern reveals (scale + blur + stagger) — very 2026 premium motion
  if (!prefersReduced) {
    const reveals = document.querySelectorAll('.reveal');
    if ('IntersectionObserver' in window && reveals.length) {
      const io = new IntersectionObserver((entries) => {
        entries.forEach((entry, i) => {
          if (entry.isIntersecting) {
            setTimeout(() => {
              entry.target.classList.add('visible');
            }, i * 45); // gentle natural stagger
            io.unobserve(entry.target);
          }
        });
      }, { threshold: 0.12, rootMargin: '0px 0px -60px 0px' });
      reveals.forEach(el => io.observe(el));
    } else {
      reveals.forEach(el => el.classList.add('visible'));
    }
  }

  // Subtle modern hero parallax (performant, only when meaningful)
  if (!prefersReduced && window.innerWidth > 920) {
    const heroBg = document.querySelector('.parallax-bg');
    if (heroBg) {
      let lastY = window.scrollY;
      window.addEventListener('scroll', () => {
        const y = window.scrollY;
        const delta = Math.min((y - lastY) * 0.028, 22);
        heroBg.style.transform = `translate3d(0, ${delta}px, 0)`;
        lastY = y;
      }, { passive: true });
    }
  }

  // Bonus modern touch: subtle pointer glow on premium cards (very light)
  if (!prefersReduced) {
    document.querySelectorAll('.premium-card').forEach(card => {
      card.addEventListener('mousemove', (e) => {
        const rect = card.getBoundingClientRect();
        const x = ((e.clientX - rect.left) / rect.width) * 100;
        const y = ((e.clientY - rect.top) / rect.height) * 100;
        card.style.setProperty('--mouse-x', `${x}%`);
        card.style.setProperty('--mouse-y', `${y}%`);
      });
    });
  }
});

// Optional: PWA install prompt scaffolding for later
let deferredPrompt;
window.addEventListener('beforeinstallprompt', (e) => {
  deferredPrompt = e;
});