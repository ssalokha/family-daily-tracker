# Frontend Structure

## Source Organization
```
src/
├── components/          # Reusable UI components
│   ├── common/         # Generic components (Button, Input, Card)
│   ├── layout/         # Layout components (Header, Sidebar, TabCarousel)
│   ├── weather/        # Weather-specific components
│   ├── appointments/   # Doctor appointment components
│   └── shopping/       # Shopping list components
├── pages/              # Page components (Login, Dashboard)
├── store/              # Redux store configuration
│   ├── slices/         # Redux slices (auth, appointments, shopping, weather)
│   └── store.ts        # Store configuration
├── services/           # API service layer
│   ├── api.ts          # Axios instance
│   ├── authService.ts
│   ├── appointmentService.ts
│   ├── shoppingService.ts
│   └── weatherService.ts
├── hooks/              # Custom React hooks
├── types/              # TypeScript type definitions
├── utils/              # Utility functions
├── test/               # Test setup and utilities
├── assets/             # Static assets
├── styles/             # Global styles
├── App.tsx             # Main App component
└── main.tsx            # Entry point
```

## Key Features
- Responsive design for 10-inch tablets and 5-7 inch mobile phones
- Auto-rotating tab carousel (Weather, Appointments, Shopping)
- Redux for state management
- Axios for API calls
- Recharts for weather visualization
- Tailwind CSS for styling
