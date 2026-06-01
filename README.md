# Lumina

**A serene, accessible, full-featured wellness studio booking platform.**

Lumina is a beautifully crafted web application for a boutique Pilates, Yoga, and mindful movement studio in Rondebosch, Cape Town. It delivers a premium member experience while giving studio owners powerful yet simple tools to run their business.

> Built as a portfolio project to demonstrate modern .NET development, exceptional attention to accessibility (WCAG 2.2), thoughtful UX, and clean, maintainable architecture.

## ✨ Vision & Philosophy

Lumina is inspired by tools like Octiv but reimagined for a single, high-end boutique studio. The focus is on:

- **Radical accessibility** — Every feature is designed for users of all abilities
- **Clinical + wellness synergy** — Built in the same building as a physiotherapy practice
- **Calm, premium experience** — The interface should feel like an extension of the studio itself
- **Simplicity with power** — Owners can run daily operations in minutes without training

**Tagline:** *Movement. Presence. Light.*

## 🛠 Tech Stack

| Layer              | Technology                                      |
|--------------------|-------------------------------------------------|
| Backend            | ASP.NET Core 8, Razor Pages                     |
| Styling            | Tailwind CSS v4 + custom design system          |
| Interactivity      | HTMX + Alpine.js (progressive enhancement)      |
| Real-time          | SignalR                                         |
| Database           | EF Core + SQLite (easy migration to Postgres)   |
| Authentication     | ASP.NET Core Identity + role-based policies     |
| Payments           | Stripe (ZAR support)                            |
| PWA                | Web App Manifest + Service Worker               |
| Tooling            | .NET 8, npm (for Tailwind CLI)                  |

## 🌟 Key Features (Current & Planned)

### Public Experience
- Stunning, calm marketing site
- Interactive class schedule with powerful filters
- "Find Your Flow" intelligent class recommender
- Rich class details with accessibility information

### Member Experience
- Seamless registration with wellness profile
- Beautiful booking flow with capacity awareness
- Personal dashboard, bookings, practice insights
- Credit packages & digital waivers
- Full PWA support (installable + offline schedule)

### Owner / Admin Experience
- Clean dashboard with key KPIs
- Visual schedule management (recurring + exceptions)
- One-tap attendance
- Member CRM with private notes + accessibility flags
- Simple reporting & analytics

### Accessibility & Inclusion (Core Principle)
- WCAG 2.2 AA target
- Full keyboard + screen reader support
- `prefers-reduced-motion` and high-contrast support
- Rich class metadata (chair options, low-sensory, prenatal, adaptive, etc.)
- Private member accessibility profiles surfaced respectfully to instructors

## 📸 Screenshots

> *Screenshots will be added as the UI matures*

- [ ] Public homepage
- [ ] Class schedule with filters
- [ ] Booking flow
- [ ] Member dashboard
- [ ] Owner admin views

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Node.js 18+ (for Tailwind CSS build)
- Git

### Local Development

```bash
# Clone the repository
git clone https://github.com/JustinFussell23/lumina-wellness.git
cd lumina-wellness

# Restore .NET packages
dotnet restore

# Install Tailwind CLI (first time only)
npm install

# Build Tailwind CSS
npm run css:build

# Run the application
dotnet run
```

The app will be available at `https://localhost:7052`

### First Run Notes
- A SQLite database is created automatically on first run
- Seeded demo data will be added in early development phases

## 🗺 Current Status & Roadmap

This project is under active development. See the detailed implementation plan for the full scope.

**Completed (Early Foundations)**
- Project initialization & clean architecture setup
- Beautiful Lumina design system (Tailwind)
- Transformed public layout & stunning homepage
- Core package setup (EF Core, Identity, SignalR)
- PWA manifest

**Next Priorities**
- Database schema + rich seeding (realistic Rondebosch classes & members)
- Authentication with extended wellness profiles
- Interactive schedule page with booking engine
- Owner admin dashboard

For the complete phased plan, feature details, and data model, see the internal implementation plan.

## 📁 Project Structure

```
WellnessApp/
├── WellnessApp/                 # Main ASP.NET project
│   ├── Pages/                   # Razor Pages (public + member + admin)
│   ├── Data/                    # EF Core context & entities
│   ├── Services/                # Business logic (BookingService, etc.)
│   └── wwwroot/                 # Static assets + Tailwind
├── WellnessApp.slnx
├── README.md
└── .gitignore
```

## 🤝 Why This Project Matters (For Portfolio)

This project demonstrates:

- **Strong product thinking** — Designed from real studio owner & member needs
- **Accessibility as a first-class concern** (not an afterthought)
- **Modern full-stack .NET practices** with progressive enhancement
- **Clean separation of concerns** and maintainable code
- **Attention to detail** in micro-interactions, copy, and visual design
- **Real-world constraints** (working around Windows npm execution policies, choosing pragmatic trade-offs)

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details (to be added).

---

Built with care in Cape Town 🌿

*For studio owners who believe movement should feel like coming home.*
