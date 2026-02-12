# Family Daily Tracker - Implementation Plan & Prompts

## Project Overview
A full-stack family management application with weather tracking, doctor appointments, and shopping list features. Built with Clean Architecture (Backend) and modern React (Frontend), containerized with Docker.

---

## Phase 1: Backend Implementation (Week 1-2)

### 1.1 Domain Layer Setup

**Prompt for AI Assistant:**
```
Create the Domain layer for a Family Tracker application following Clean Architecture principles. 

1. Create User entity with:
   - Id (Guid), UserName (string), Birthday (DateTime), UserAge (calculated property), Email (nullable), Password (hashed string)
   - UserRole enum: User, TabletUser, AdminUser
   
2. Create DoctorAppointment entity with:
   - Id, CreatedByUserId, AppointmentForUserId, DateTime, Location (street, building number), IsCompleted, CreatedAt, UpdatedAt
   
3. Create ShoppingItem entity with:
   - Id, Name, Quantity, CreatedBy, CreatedAt, UpdatedAt
   
4. Add domain exceptions for validation errors
5. Use value objects where appropriate (e.g., Location, Email)
```

**Files to create:**
- `backend/src/FamilyTracker.Domain/Entities/User.cs`
- `backend/src/FamilyTracker.Domain/Entities/DoctorAppointment.cs`
- `backend/src/FamilyTracker.Domain/Entities/ShoppingItem.cs`
- `backend/src/FamilyTracker.Domain/Enums/UserRole.cs`
- `backend/src/FamilyTracker.Domain/ValueObjects/Location.cs`
- `backend/src/FamilyTracker.Domain/Common/BaseEntity.cs`
- `backend/src/FamilyTracker.Domain/Exceptions/DomainException.cs`

---

### 1.2 Application Layer - CQRS Setup

**Prompt for AI Assistant:**
```
Implement the Application layer using MediatR for CQRS pattern and FluentValidation.

1. Create Commands:
   - CreateUserCommand, UpdateUserCommand, DeleteUserCommand
   - CreateAppointmentCommand, UpdateAppointmentCommand, DeleteAppointmentCommand, CompleteAppointmentCommand
   - CreateShoppingItemCommand, UpdateShoppingItemCommand, DeleteShoppingItemCommand, ClearShoppingListCommand
   
2. Create Queries:
   - GetUserByIdQuery, GetAllUsersQuery, LoginQuery (username/password)
   - GetAppointmentsQuery (with date range filter), GetAppointmentHistoryQuery
   - GetShoppingListQuery
   
3. Add FluentValidation validators for all commands
4. Create DTOs for all entities
5. Define repository interfaces (IUserRepository, IAppointmentRepository, IShoppingRepository)
6. Create IEmailService interface for sending shopping lists
7. Create IWeatherService interface for fetching weather data
```

**Files to create:**
- `backend/src/FamilyTracker.Application/Commands/Users/CreateUserCommand.cs`
- `backend/src/FamilyTracker.Application/Commands/Appointments/CreateAppointmentCommand.cs`
- `backend/src/FamilyTracker.Application/Commands/Shopping/CreateShoppingItemCommand.cs`
- `backend/src/FamilyTracker.Application/Queries/Users/LoginQuery.cs`
- `backend/src/FamilyTracker.Application/Validators/CreateUserCommandValidator.cs`
- `backend/src/FamilyTracker.Application/DTOs/UserDto.cs`
- `backend/src/FamilyTracker.Application/Interfaces/IUserRepository.cs`
- `backend/src/FamilyTracker.Application/Interfaces/IEmailService.cs`
- `backend/src/FamilyTracker.Application/Interfaces/IWeatherService.cs`

---

### 1.3 Infrastructure Layer

**Prompt for AI Assistant:**
```
Implement the Infrastructure layer with Entity Framework Core and PostgreSQL.

1. Create ApplicationDbContext with DbSets for all entities
2. Configure entity relationships and constraints using Fluent API
3. Implement repositories (UserRepository, AppointmentRepository, ShoppingRepository)
4. Create database migrations
5. Implement DataSeeder class to seed initial users as specified in requirements:
   - Sergey (Admin, password: hashed with BCrypt)
   - Natallia (User, password: hashed with BCrypt)
   - Dasha (User, password: hashed with BCrypt)
   - Alex (User, password: hashed with BCrypt)
   - Home (TabletUser, password: hashed with BCrypt 111111)
6. Calculate UserAge from Birthday in the seeder
7. Implement EmailService using SMTP
8. Implement WeatherService to fetch weather for Poznan, Poland (use OpenWeatherMap API or similar)
9. Add password hashing service using BCrypt.Net
```

**Files to create:**
- `backend/src/FamilyTracker.Infrastructure/Persistence/ApplicationDbContext.cs`
- `backend/src/FamilyTracker.Infrastructure/Persistence/Configurations/UserConfiguration.cs`
- `backend/src/FamilyTracker.Infrastructure/Repositories/UserRepository.cs`
- `backend/src/FamilyTracker.Infrastructure/Services/EmailService.cs`
- `backend/src/FamilyTracker.Infrastructure/Services/WeatherService.cs`
- `backend/src/FamilyTracker.Infrastructure/Services/PasswordHashService.cs`
- `backend/src/FamilyTracker.Infrastructure/Seeders/DataSeeder.cs`

---

### 1.4 API Layer

**Prompt for AI Assistant:**
```
Create the API layer with authentication, authorization, and controllers.

1. Setup JWT authentication with role-based authorization
2. Create controllers:
   - AuthController (Login endpoint)
   - UsersController (CRUD, requires AdminUser role)
   - AppointmentsController (CRUD, requires User role minimum)
   - ShoppingController (CRUD, ClearList, SendEmail actions)
   - WeatherController (Get weather for Poznan)
   
3. Add Swagger/OpenAPI documentation
4. Implement global exception handling middleware
5. Configure CORS for frontend
6. Setup dependency injection for all layers
7. Configure database connection from environment variables
8. Add health check endpoint

Program.cs should:
- Configure services
- Run migrations on startup
- Seed initial data
- Configure middleware pipeline
```

**Files to create:**
- `backend/src/FamilyTracker.API/Controllers/AuthController.cs`
- `backend/src/FamilyTracker.API/Controllers/UsersController.cs`
- `backend/src/FamilyTracker.API/Controllers/AppointmentsController.cs`
- `backend/src/FamilyTracker.API/Controllers/ShoppingController.cs`
- `backend/src/FamilyTracker.API/Controllers/WeatherController.cs`
- `backend/src/FamilyTracker.API/Middleware/ExceptionHandlingMiddleware.cs`
- `backend/src/FamilyTracker.API/Extensions/ServiceCollectionExtensions.cs`
- `backend/src/FamilyTracker.API/Program.cs`
- `backend/src/FamilyTracker.API/appsettings.json`

---

### 1.5 Testing

**Prompt for AI Assistant:**
```
Create unit tests using xUnit, Moq, and FluentAssertions.

1. Test domain entities and value objects
2. Test command handlers with mocked repositories
3. Test query handlers
4. Test validators
5. Test repository implementations with in-memory database
6. Aim for >70% code coverage

Focus on:
- User authentication logic
- Appointment creation/validation
- Shopping list operations
- Age calculation from birthday
```

**Files to create:**
- `backend/tests/FamilyTracker.UnitTests/Domain/UserTests.cs`
- `backend/tests/FamilyTracker.UnitTests/Application/Commands/CreateAppointmentCommandHandlerTests.cs`
- `backend/tests/FamilyTracker.UnitTests/Application/Validators/CreateUserCommandValidatorTests.cs`

---

## Phase 2: Frontend Implementation (Week 3-4)

### 2.1 Project Setup & Configuration

**Prompt for AI Assistant:**
```
Initialize the React TypeScript project with all necessary dependencies.

1. Install dependencies from package.json (already created)
2. Setup Tailwind CSS with custom theme for tablet/mobile responsiveness
3. Configure Vite with proxy for backend API
4. Setup Redux store with slices for:
   - auth (user, token, role)
   - appointments (list, filters)
   - shopping (items)
   - weather (forecast data)
   - ui (activeTab, carouselPlaying)
5. Configure Axios instance with interceptors for auth token
6. Setup React Router with protected routes
```

**Files to create:**
- `frontend/src/main.tsx`
- `frontend/src/App.tsx`
- `frontend/src/store/store.ts`
- `frontend/src/store/slices/authSlice.ts`
- `frontend/src/store/slices/appointmentsSlice.ts`
- `frontend/src/store/slices/shoppingSlice.ts`
- `frontend/src/store/slices/weatherSlice.ts`
- `frontend/src/store/slices/uiSlice.ts`
- `frontend/src/services/api.ts`
- `frontend/src/styles/index.css`

---

### 2.2 Authentication & Routing

**Prompt for AI Assistant:**
```
Create authentication system and routing.

1. Create Login page with username/password form
2. Implement authentication logic (login, logout, persist token)
3. Create ProtectedRoute component for role-based access
4. Setup routes:
   - /login (public)
   - /dashboard (protected, all authenticated users)
   - /admin/users (protected, AdminUser only)
   
4. Add logout functionality
5. Handle token expiration and refresh
```

**Files to create:**
- `frontend/src/pages/Login.tsx`
- `frontend/src/pages/Dashboard.tsx`
- `frontend/src/pages/AdminUsers.tsx`
- `frontend/src/components/common/ProtectedRoute.tsx`
- `frontend/src/services/authService.ts`

---

### 2.3 Tab Carousel System

**Prompt for AI Assistant:**
```
Create auto-rotating tab carousel for the dashboard.

Requirements:
1. Create TabCarousel component with 3 tabs: Weather, Appointments, Shopping
2. Auto-rotate every 10 seconds
3. Stop rotation when user interacts (touch/click)
4. Resume rotation after 30 seconds of inactivity
5. Show navigation dots at the bottom
6. Support swipe gestures on mobile
7. Responsive design for 10-inch tablets and 5-7 inch phones

Use Tailwind for styling. Store carousel state in Redux uiSlice.
```

**Files to create:**
- `frontend/src/components/layout/TabCarousel.tsx`
- `frontend/src/components/layout/TabNavigation.tsx`
- `frontend/src/hooks/useCarousel.ts`

---

### 2.4 Weather Tab

**Prompt for AI Assistant:**
```
Create Weather tab displaying 7-day forecast for Poznan, Poland.

1. Fetch weather data from backend API
2. Display:
   - Daily forecast cards with date, temperature (min/max), weather icon, conditions
   - Use Recharts for temperature graph visualization
3. Responsive grid layout (3 columns on tablet, 2 on mobile)
4. Auto-refresh data every hour
5. Show loading state and error handling

Use weather icons from a free icon library or Unicode symbols.
```

**Files to create:**
- `frontend/src/components/weather/WeatherTab.tsx`
- `frontend/src/components/weather/WeatherCard.tsx`
- `frontend/src/components/weather/WeatherChart.tsx`
- `frontend/src/services/weatherService.ts`
- `frontend/src/types/weather.ts`

---

### 2.5 Doctor Appointments Tab

**Prompt for AI Assistant:**
```
Create Doctor Appointments tab with CRUD operations.

Features:
1. Display appointments for next 2 weeks in a list
2. Each item shows:
   - "Doc: [UserName]" (user for whom appointment is made)
   - Date and time in readable format
   - Location (street and building number)
   - Completion checkbox
   - Edit/Delete buttons (if user has permission)
   
3. Add "New Appointment" button
4. Create modal/form for creating/editing appointments with fields:
   - Select user (dropdown)
   - Date/time picker
   - Location (street, building number)
   
5. Implement role-based access:
   - User role: can create, edit, delete appointments
   - AdminUser: can also view history
   
6. Filter by date range and user
7. Mark appointment as completed with one click
```

**Files to create:**
- `frontend/src/components/appointments/AppointmentsTab.tsx`
- `frontend/src/components/appointments/AppointmentList.tsx`
- `frontend/src/components/appointments/AppointmentItem.tsx`
- `frontend/src/components/appointments/AppointmentForm.tsx`
- `frontend/src/components/appointments/AppointmentHistory.tsx`
- `frontend/src/services/appointmentService.ts`
- `frontend/src/types/appointment.ts`

---

### 2.6 Shopping Tab

**Prompt for AI Assistant:**
```
Create Shopping tab with list management and email feature.

Features:
1. Display shopping list with items and quantities
2. Add new item form (inline or modal)
3. Edit item (quantity, name)
4. Delete item button
5. "Clear All" button to empty the list
6. "Go Shop" button that:
   - Opens modal to select user
   - Sends shopping list to selected user's email
   - Shows confirmation message
   
7. Group by category (optional enhancement)
8. Optimistic updates for better UX

UI should be clean and touch-friendly for tablet use.
```

**Files to create:**
- `frontend/src/components/shopping/ShoppingTab.tsx`
- `frontend/src/components/shopping/ShoppingList.tsx`
- `frontend/src/components/shopping/ShoppingItem.tsx`
- `frontend/src/components/shopping/AddItemForm.tsx`
- `frontend/src/components/shopping/SendEmailModal.tsx`
- `frontend/src/services/shoppingService.ts`
- `frontend/src/types/shopping.ts`

---

### 2.7 Admin User Management

**Prompt for AI Assistant:**
```
Create Admin panel for user management (AdminUser role only).

Features:
1. Display all users in a table/list
2. Show: Username, Email, Birthday, Age, Role
3. Add new user button with form
4. Edit user (all fields except password)
5. Delete user (with confirmation)
6. Change password functionality
7. Filter/search users

Ensure this page is only accessible by AdminUser role.
```

**Files to create:**
- `frontend/src/components/admin/UserManagement.tsx`
- `frontend/src/components/admin/UserTable.tsx`
- `frontend/src/components/admin/UserForm.tsx`
- `frontend/src/services/userService.ts`
- `frontend/src/types/user.ts`

---

### 2.8 Common Components & UI

**Prompt for AI Assistant:**
```
Create reusable UI components following atomic design principles.

Components needed:
1. Button (primary, secondary, danger variants)
2. Input (text, password, date, number)
3. Select/Dropdown
4. Modal/Dialog
5. Card
6. Loader/Spinner
7. Toast/Notification
8. ConfirmDialog
9. DatePicker (or use a library like react-datepicker)

Style with Tailwind CSS. Make all components touch-friendly (min 44x44px tap targets).
Ensure accessibility (ARIA labels, keyboard navigation).
```

**Files to create:**
- `frontend/src/components/common/Button.tsx`
- `frontend/src/components/common/Input.tsx`
- `frontend/src/components/common/Select.tsx`
- `frontend/src/components/common/Modal.tsx`
- `frontend/src/components/common/Card.tsx`
- `frontend/src/components/common/Loader.tsx`
- `frontend/src/components/common/Toast.tsx`

---

### 2.9 Testing

**Prompt for AI Assistant:**
```
Create tests using Vitest and React Testing Library.

Test scenarios:
1. Component rendering tests
2. User interaction tests (clicks, form submissions)
3. Redux slice reducers and actions
4. API service mocks
5. Protected route behavior
6. Form validation
7. Carousel auto-rotation logic

Setup test utilities and mocks in src/test/setup.ts
```

**Files to create:**
- `frontend/src/test/setup.ts`
- `frontend/src/components/__tests__/TabCarousel.test.tsx`
- `frontend/src/components/__tests__/AppointmentForm.test.tsx`
- `frontend/src/store/__tests__/authSlice.test.ts`

---

## Phase 3: Integration & Deployment (Week 5)

### 3.1 Docker Integration

**Prompt for AI Assistant:**
```
Ensure all services work together in Docker environment.

1. Test backend builds and runs in container
2. Test frontend builds and runs in container
3. Verify database migrations run automatically
4. Verify data seeding works
5. Test inter-service communication
6. Check environment variable configuration
7. Optimize Docker images for production

Run: docker-compose up --build
Verify all services are accessible.
```

---

### 3.2 End-to-End Testing

**Manual Test Scenarios:**

1. **Authentication Flow:**
   - Login as each seeded user
   - Verify role-based access (User vs TabletUser vs AdminUser)
   - Test logout

2. **Weather Tab:**
   - Verify 7-day forecast displays
   - Check data refreshes
   - Verify responsive layout

3. **Appointments Tab:**
   - Create appointment as User role
   - Edit appointment
   - Mark as completed
   - Delete appointment
   - View history as AdminUser
   - Verify date filtering

4. **Shopping Tab:**
   - Add items to list
   - Edit quantities
   - Delete items
   - Clear all items
   - Send email (verify email received)
   - Verify user selection works

5. **Admin Panel:**
   - Login as Sergey (AdminUser)
   - Create new user
   - Edit user details
   - Delete user
   - Verify only AdminUser can access

6. **Carousel:**
   - Watch auto-rotation (10 second intervals)
   - Touch screen to stop
   - Verify resume after inactivity
   - Test swipe gestures on mobile

7. **Responsive Design:**
   - Test on 10-inch tablet simulation
   - Test on 5-7 inch mobile simulation
   - Verify touch targets are adequate

---

### 3.3 Documentation

**Create comprehensive documentation:**

1. API Documentation:
   - Auto-generated via Swagger
   - Accessible at http://localhost:5000/swagger

2. README updates:
   - Add deployment instructions
   - Add development setup guide
   - Add troubleshooting section

3. Code documentation:
   - Add XML comments to C# code
   - Add JSDoc comments to TypeScript code

---

## Environment Variables

### Backend (.env or docker-compose)
```
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=Host=postgres;Port=5432;Database=family_tracker;Username=postgres;Password=postgres123
JWT_SECRET=your-super-secret-jwt-key-change-in-production
JWT_EXPIRY_HOURS=24
SMTP_HOST=smtp.gmail.com
SMTP_PORT=587
SMTP_USER=your-email@gmail.com
SMTP_PASSWORD=your-app-password
WEATHER_API_KEY=your-openweathermap-api-key
```

### Frontend (.env.local)
```
VITE_API_URL=http://localhost:5000
```

---

## Technology Checklist

### Backend ✓
- [x] .NET 8
- [x] Clean Architecture (Domain, Application, Infrastructure, API)
- [x] MediatR (CQRS)
- [x] FluentValidation
- [x] Entity Framework Core
- [x] PostgreSQL
- [x] BCrypt for password hashing
- [x] xUnit, Moq, FluentAssertions
- [x] Docker

### Frontend ✓
- [x] React 18+
- [x] TypeScript
- [x] Vite
- [x] Tailwind CSS
- [x] React Router
- [x] Redux Toolkit
- [x] Recharts
- [x] Vitest
- [x] Docker

### Infrastructure ✓
- [x] Docker Compose
- [x] PostgreSQL 15+
- [x] Git

---

## Quick Start Commands

### Development Setup

```bash
# Clone repository
git clone <repository-url>
cd AiPracticalTask

# Start all services with Docker
./start.bat  # Windows
# or
./start.sh   # Linux/Mac

# Backend only (for development)
cd backend
dotnet restore
dotnet ef database update --project src/FamilyTracker.Infrastructure --startup-project src/FamilyTracker.API
dotnet run --project src/FamilyTracker.API

# Frontend only (for development)
cd frontend
npm install
npm run dev

# Run tests
cd backend && dotnet test
cd frontend && npm test
```

---

## Common Issues & Solutions

1. **Database connection fails:**
   - Ensure PostgreSQL container is running: `docker ps`
   - Check connection string in docker-compose.yml
   - Wait a few seconds for DB to initialize

2. **Frontend can't reach backend:**
   - Verify CORS is configured in backend
   - Check VITE_API_URL in frontend .env
   - Ensure backend is running on port 5000

3. **Email sending fails:**
   - Configure SMTP settings in backend
   - Use app-specific password for Gmail
   - Check firewall/network settings

4. **Weather data not loading:**
   - Get free API key from OpenWeatherMap
   - Configure WEATHER_API_KEY in backend

---

## Development Workflow

1. **Create feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make changes, test locally**
   ```bash
   docker-compose up --build
   ```

3. **Run tests**
   ```bash
   # Backend
   cd backend && dotnet test
   
   # Frontend
   cd frontend && npm test
   ```

4. **Commit and push**
   ```bash
   git add .
   git commit -m "feat: description of changes"
   git push origin feature/your-feature-name
   ```

5. **Create Pull Request**

---

## Production Deployment Considerations

1. **Security:**
   - Change all default passwords
   - Use strong JWT secret
   - Enable HTTPS
   - Configure proper CORS origins
   - Use environment variables for secrets

2. **Performance:**
   - Enable caching for weather data
   - Optimize database queries
   - Use CDN for frontend assets
   - Enable compression

3. **Monitoring:**
   - Add logging (Serilog for backend)
   - Setup error tracking (Sentry, Application Insights)
   - Monitor database performance
   - Setup health checks

4. **Backup:**
   - Regular PostgreSQL backups
   - Version control for all code
   - Document deployment process

---

## Next Steps After Infrastructure Setup

1. ✅ Infrastructure created
2. ⏭️ Implement Domain layer
3. ⏭️ Implement Application layer  
4. ⏭️ Implement Infrastructure layer
5. ⏭️ Implement API layer
6. ⏭️ Implement Frontend
7. ⏭️ Testing & Quality Assurance
8. ⏭️ Deployment

**Start with Phase 1.1 (Domain Layer) and work sequentially through each phase.**

Good luck with your AI course project! 🚀
