import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../store/store';
import { setActiveTab, updateInteraction, resumeCarousel } from '../../store/slices/uiSlice';
import WeatherTab from '../weather/WeatherTab';
import AppointmentsTab from '../appointments/AppointmentsTab';
import ShoppingTab from '../shopping/ShoppingTab';
import { logout } from '../../store/slices/authSlice';
import { useNavigate } from 'react-router-dom';

const TABS = [
  { id: 0, name: 'Weather', icon: '☀️' },
  { id: 1, name: 'Appointments', icon: '📅' },
  { id: 2, name: 'Shopping', icon: '🛒' },
];

const CAROUSEL_INTERVAL = 10000; // 10 seconds
const RESUME_DELAY = 30000; // 30 seconds

const TabCarousel: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { activeTab, carouselPlaying, lastInteraction } = useSelector((state: RootState) => state.ui);
  const { user } = useSelector((state: RootState) => state.auth);
  const [touchStart, setTouchStart] = useState<number | null>(null);
  const [touchEnd, setTouchEnd] = useState<number | null>(null);

  // Auto-rotate carousel
  useEffect(() => {
    if (!carouselPlaying) return;

    const interval = setInterval(() => {
      dispatch(setActiveTab((activeTab + 1) % TABS.length));
    }, CAROUSEL_INTERVAL);

    return () => clearInterval(interval);
  }, [activeTab, carouselPlaying, dispatch]);

  // Resume carousel after inactivity
  useEffect(() => {
    if (carouselPlaying) return;

    const timeout = setTimeout(() => {
      const timeSinceInteraction = Date.now() - lastInteraction;
      if (timeSinceInteraction >= RESUME_DELAY) {
        dispatch(resumeCarousel());
      }
    }, RESUME_DELAY);

    return () => clearTimeout(timeout);
  }, [lastInteraction, carouselPlaying, dispatch]);

  const handleTabClick = (tabId: number) => {
    dispatch(updateInteraction());
    dispatch(setActiveTab(tabId));
  };

  const handleTouchStart = (e: React.TouchEvent) => {
    setTouchEnd(null);
    setTouchStart(e.targetTouches[0].clientX);
    dispatch(updateInteraction());
  };

  const handleTouchMove = (e: React.TouchEvent) => {
    setTouchEnd(e.targetTouches[0].clientX);
  };

  const handleTouchEnd = () => {
    if (!touchStart || !touchEnd) return;
    
    const distance = touchStart - touchEnd;
    const isLeftSwipe = distance > 50;
    const isRightSwipe = distance < -50;

    if (isLeftSwipe) {
      dispatch(setActiveTab((activeTab + 1) % TABS.length));
    }
    if (isRightSwipe) {
      dispatch(setActiveTab((activeTab - 1 + TABS.length) % TABS.length));
    }
  };

  const handleLogout = () => {
    dispatch(logout());
    navigate('/login');
  };

  const isAdmin = user && (user.role & 4) === 4;

  return (
    <div className="h-screen flex flex-col">
      {/* Header */}
      <header className="bg-white shadow-sm border-b">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-4">
          <div className="flex items-center justify-between">
            <div className="flex items-center space-x-3">
              <span className="text-3xl">🏠</span>
              <div>
                <h1 className="text-2xl font-bold text-gray-900">Family Tracker</h1>
                <p className="text-sm text-gray-500">Welcome, {user?.userName}</p>
              </div>
            </div>
            <div className="flex items-center space-x-4">
              {isAdmin && (
                <button
                  onClick={() => navigate('/admin/users')}
                  className="px-4 py-2 text-sm font-medium text-blue-600 hover:text-blue-700 hover:bg-blue-50 rounded-lg transition"
                >
                  👥 Manage Users
                </button>
              )}
              <button
                onClick={handleLogout}
                className="px-4 py-2 text-sm font-medium text-red-600 hover:text-red-700 hover:bg-red-50 rounded-lg transition"
              >
                Logout
              </button>
            </div>
          </div>
        </div>
      </header>

      {/* Tab Navigation */}
      <nav className="bg-white shadow-sm">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex space-x-1">
            {TABS.map((tab) => (
              <button
                key={tab.id}
                onClick={() => handleTabClick(tab.id)}
                className={`flex-1 px-6 py-4 text-center font-medium transition-all ${
                  activeTab === tab.id
                    ? 'text-blue-600 border-b-2 border-blue-600 bg-blue-50'
                    : 'text-gray-600 hover:text-gray-900 hover:bg-gray-50'
                }`}
              >
                <span className="text-2xl mr-2">{tab.icon}</span>
                <span className="hidden sm:inline">{tab.name}</span>
              </button>
            ))}
          </div>
        </div>
      </nav>

      {/* Tab Content */}
      <main
        className="flex-1 overflow-y-auto"
        onTouchStart={handleTouchStart}
        onTouchMove={handleTouchMove}
        onTouchEnd={handleTouchEnd}
        onClick={() => dispatch(updateInteraction())}
      >
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-6">
          {activeTab === 0 && <WeatherTab />}
          {activeTab === 1 && <AppointmentsTab />}
          {activeTab === 2 && <ShoppingTab />}
        </div>
      </main>

      {/* Navigation Dots */}
      <div className="bg-white py-4 flex justify-center space-x-2">
        {TABS.map((tab) => (
          <button
            key={tab.id}
            onClick={() => handleTabClick(tab.id)}
            className={`w-3 h-3 rounded-full transition-all ${
              activeTab === tab.id ? 'bg-blue-600 w-8' : 'bg-gray-300'
            }`}
            aria-label={`Go to ${tab.name}`}
          />
        ))}
      </div>
    </div>
  );
};

export default TabCarousel;
