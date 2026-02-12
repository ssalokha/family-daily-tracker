# Quick Screenshot Capture Guide

## 🚀 Steps to Capture Screenshots

### 1. Wait for Application to Start
The application is currently starting. Wait for the message:
```
✅ Application ready at http://localhost:3000
```

### 2. Open Browser
- Open Chrome or Edge
- Navigate to: **http://localhost:3000**

### 3. Capture Screenshots (Use Windows Snipping Tool)

Press `Win + Shift + S` to activate Windows Snipping Tool, then capture each screen:

#### Screenshot 1: Login Page (`login.png`)
1. You should see the login page
2. Press `Win + Shift + S`
3. Drag to select the login form area
4. Screenshot saved to clipboard
5. Open Paint → Ctrl+V → Save as `docs\screenshots\login.png`

#### Screenshot 2: Weather Tab (`weather-tab.png`)
1. Login with: **Sergey** / **111111**
2. You'll see the Weather tab (default first tab)
3. Wait for weather data to load
4. Press `Win + Shift + S`
5. Capture the weather forecast grid
6. Save as `docs\screenshots\weather-tab.png`

#### Screenshot 3: Appointments Tab (`appointments-tab.png`)
1. Click on the "Appointments" tab
2. If no appointments exist, create 2-3 sample ones first
3. Press `Win + Shift + S`
4. Capture the appointments list
5. Save as `docs\screenshots\appointments-tab.png`

#### Screenshot 4: Appointment Form (`appointment-form.png`)
1. On Appointments tab, click "New Appointment"
2. Modal opens with form
3. Press `Win + Shift + S`
4. Capture the modal
5. Save as `docs\screenshots\appointment-form.png`

#### Screenshot 5: Shopping Tab (`shopping-tab.png`)
1. Click on "Shopping" tab
2. Add 3-5 items to the list
3. Press `Win + Shift + S`
4. Capture the shopping list
5. Save as `docs\screenshots\shopping-tab.png`

#### Screenshot 6: Admin Tab (`admin-tab.png`)
1. You're already logged in as Sergey (AdminUser)
2. Navigate through carousel or click to find "Admin" tab
3. Press `Win + Shift + S`
4. Capture the user management table
5. Save as `docs\screenshots\admin-tab.png`

---

## 📁 File Locations
All screenshots should be saved in:
```
c:\Users\s.salokha\OneDrive - Godel Technologies Europe LTD\Courses\AiPracticalTask\docs\screenshots\
```

## ✅ After Capturing Screenshots

Run these commands to commit and push:
```bash
git add docs/screenshots/
git commit -m "Add application UI screenshots"
git push
```

---

## 🎯 Minimum Required Screenshots
For a good README, you need at least these 3:
1. **login.png** - Login page
2. **weather-tab.png** - Weather forecast
3. **appointments-tab.png** - Appointments list

The others are optional but recommended!
