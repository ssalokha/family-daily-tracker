# Screenshot Guide for Family Daily Tracker

## 📸 Required Screenshots

This guide will help you capture all the necessary screenshots for the GitHub README.

---

## 🎯 Prerequisites

1. **Start the Application:**
   ```bash
   cd "c:\Users\s.salokha\OneDrive - Godel Technologies Europe LTD\Courses\AiPracticalTask"
   ./start.bat
   ```

2. **Wait for Services:**
   - Backend: http://localhost:5000 (Ready when you see "Application started")
   - Frontend: http://localhost:3000
   - Database: PostgreSQL on port 5432

3. **Open Browser:**
   - Navigate to http://localhost:3000
   - Use Chrome DevTools for responsive testing (F12 → Device Toolbar)

---

## 📋 Screenshot Checklist

### 1. Login Page (`login.png`)
**URL:** http://localhost:3000/login

**What to Show:**
- Clean login form with username and password fields
- "Family Tracker" title
- "Sign In" button
- Demo users hint at bottom

**Credentials for Testing:**
- Username: `Sergey`
- Password: `111111`

**Screenshot Settings:**
- Viewport: 1280x800 (Desktop)
- Full page or centered login card

---

### 2. Weather Tab (`weather-tab.png`)
**URL:** http://localhost:3000/dashboard (after login)

**What to Show:**
- 7-day weather forecast grid
- Daily cards showing:
  - Date
  - Temperature (min/max)
  - Weather conditions
  - Weather emoji/icon
- Tab indicator showing "Weather" is active
- Responsive 3-column layout (tablet view)

**How to Capture:**
1. Login as any user
2. Ensure you're on the Weather tab (first tab, auto-selected)
3. Wait for weather data to load
4. Capture the full tab content

**Screenshot Settings:**
- Viewport: 1024x768 (Tablet - iPad)
- Show at least 6-7 weather cards

---

### 3. Appointments Tab (`appointments-tab.png`)
**URL:** http://localhost:3000/dashboard

**What to Show:**
- List of upcoming appointments
- Each appointment showing:
  - "Doc: [UserName]"
  - Date and time (24-hour format)
  - Location (street and building)
  - Action buttons (Edit, Delete, Mark Complete)
- "New Appointment" button
- Empty state or appointments list

**How to Capture:**
1. Click on the "Appointments" tab
2. If no appointments exist, create 2-3 sample appointments first
3. Show appointments in list format
4. Capture the full tab with appointments

**Sample Appointment Data:**
- For: Sergey
- Date: Tomorrow
- Time: 14:00
- Street: Main Street
- Building: 15A

**Screenshot Settings:**
- Viewport: 1024x768 (Tablet)
- Show at least 2-3 appointments

---

### 4. Appointment Form (`appointment-form.png`)
**URL:** Modal on http://localhost:3000/dashboard

**What to Show:**
- Modal dialog with appointment form
- Fields visible:
  - "Appointment For" dropdown (user selection)
  - Date picker
  - Time picker (24-hour format)
  - Street input
  - Building Number input
- "Create" or "Save" button
- "Cancel" button
- Modal overlay in background

**How to Capture:**
1. On Appointments tab, click "New Appointment" button
2. Modal should open
3. Optionally fill in some sample data to show filled state
4. Capture the modal with background dimmed

**Screenshot Settings:**
- Viewport: 1024x768
- Center the modal
- Show dimmed background

---

### 5. Shopping Tab (`shopping-tab.png`)
**URL:** http://localhost:3000/dashboard

**What to Show:**
- Shopping list items with:
  - Item name
  - Quantity
  - Edit and Delete buttons
  - Created by (username)
- "Add Item" form at top
- "Clear All" button
- "Go Shop" button
- List of 3-5 sample items

**How to Capture:**
1. Click on "Shopping" tab
2. Add 3-5 sample items:
   - Milk (2)
   - Bread (1)
   - Eggs (12)
   - Butter (500g)
   - Coffee (1 pack)
3. Show the populated shopping list
4. Capture the full tab

**Screenshot Settings:**
- Viewport: 1024x768 (Tablet)
- Show full list with controls

---

### 6. Shopping Email Modal (`shopping-email-modal.png`)
**URL:** Modal on http://localhost:3000/dashboard

**What to Show:**
- "Send Shopping List" modal
- Dropdown to select user (recipient)
- Current shopping list preview
- "Send Email" button
- "Cancel" button

**How to Capture:**
1. On Shopping tab (with items in list)
2. Click "Go Shop" button
3. Modal opens with user selection
4. Show the modal with list preview
5. Capture modal with background

**Screenshot Settings:**
- Viewport: 1024x768
- Center modal
- Show shopping list items in modal

---

### 7. Admin Tab (`admin-tab.png`)
**URL:** http://localhost:3000/dashboard (AdminUser only)

**What to Show:**
- User management table
- Columns:
  - Username
  - Email
  - Birthday
  - Age (calculated)
  - Role
  - Actions (Edit, Delete)
- "Add User" button
- All 5 default users visible

**How to Capture:**
1. **Important:** Login as `Sergey` (AdminUser role)
2. The Admin tab will appear in the carousel
3. Click on "Admin" tab
4. Table shows all users
5. Capture the full user management table

**Screenshot Settings:**
- Viewport: 1280x800 (Desktop) - better for table view
- Show all columns clearly

---

### 8. User Form (`user-form.png`)
**URL:** Modal on http://localhost:3000/dashboard (Admin tab)

**What to Show:**
- Modal with user form
- Fields:
  - Username
  - Email
  - Birthday (date picker)
  - Password
  - Role (dropdown: User, TabletUser, AdminUser)
- "Save" button
- "Cancel" button

**How to Capture:**
1. On Admin tab, click "Add User" button
2. Modal opens with empty form
3. Optionally fill sample data
4. Capture the modal

**Screenshot Settings:**
- Viewport: 1280x800
- Center modal

---

### 9. Tablet View (`tablet-view.png`)
**What to Show:**
- Full dashboard on tablet viewport
- Weather tab active (3-column layout)
- Tab carousel indicators visible
- Touch-friendly design evident

**How to Capture:**
1. Open Chrome DevTools (F12)
2. Click Device Toolbar icon (or Ctrl+Shift+M)
3. Select "iPad" or set to 768x1024
4. Capture the dashboard

**Screenshot Settings:**
- Viewport: 768x1024 (Portrait) or 1024x768 (Landscape)
- Show full page

---

### 10. Mobile View (`mobile-view.png`)
**What to Show:**
- Dashboard on mobile viewport
- Weather tab (2-column layout)
- Compact design
- Touch-friendly buttons (44x44px minimum)

**How to Capture:**
1. Open Chrome DevTools (F12)
2. Select "iPhone 12 Pro" or similar
3. Set to 390x844 or 375x667
4. Capture the dashboard

**Screenshot Settings:**
- Viewport: 375x667 (iPhone SE) or 390x844 (iPhone 12)
- Show full page or scrolled view

---

## 🛠️ Screenshot Tools

### Recommended Tools:

1. **Windows Snipping Tool:**
   - Press `Win + Shift + S`
   - Select area to capture
   - Save to `docs/screenshots/`

2. **Chrome DevTools:**
   - F12 → Device Toolbar
   - Click "..." → Capture screenshot
   - Full size or screenshot

3. **ShareX (Free):**
   - Download: https://getsharex.com/
   - Advanced screenshot tool
   - Auto-save to folder

4. **Greenshot (Free):**
   - Download: https://getgreenshot.org/
   - Quick capture with editor

---

## 📐 Image Specifications

**Format:** PNG (best quality)  
**Resolution:** At least 1280x800 for desktop, 768x1024 for tablet  
**File Size:** Try to keep under 500KB per image  
**Naming:** Use lowercase with hyphens (e.g., `login-page.png`)

---

## ✅ After Capturing Screenshots

1. **Save Images:**
   ```bash
   # Save all images to:
   docs/screenshots/
   ```

2. **Optimize Images (Optional):**
   - Use TinyPNG: https://tinypng.com/
   - Or ImageOptim for batch processing

3. **Verify File Names:**
   ```
   ✓ login.png
   ✓ weather-tab.png
   ✓ appointments-tab.png
   ✓ appointment-form.png
   ✓ shopping-tab.png
   ✓ shopping-email-modal.png
   ✓ admin-tab.png
   ✓ user-form.png
   ✓ tablet-view.png
   ✓ mobile-view.png
   ```

4. **Commit and Push:**
   ```bash
   cd "c:\Users\s.salokha\OneDrive - Godel Technologies Europe LTD\Courses\AiPracticalTask"
   git add docs/screenshots/
   git commit -m "Add application screenshots to README"
   git push
   ```

---

## 🎬 Bonus Screenshots (Optional)

### Carousel in Action
- Create a GIF showing the auto-rotation
- Tools: ScreenToGif, LICEcap, or Gifox

### Dark Mode (if implemented)
- Show the app in dark mode

### Error States
- Show validation errors
- Empty states
- Loading states

### Success Messages
- After creating an appointment
- After sending email
- After adding shopping item

---

## 🌐 Viewing on GitHub

After pushing, screenshots will be visible at:
```
https://github.com/ssalokha/family-daily-tracker/blob/main/docs/screenshots/
```

And in README:
```
https://github.com/ssalokha/family-daily-tracker#-application-screenshots
```

---

## 📝 Tips for Great Screenshots

1. **Clean Data:** Use realistic but clean sample data
2. **Consistency:** Use same user/data across screenshots
3. **Clarity:** Ensure text is readable
4. **Lighting:** Use light theme for better visibility
5. **Context:** Show enough UI to understand the feature
6. **Privacy:** Don't include real personal data

---

**Happy Screenshot Taking! 📸**
