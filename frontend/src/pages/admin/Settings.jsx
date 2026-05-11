import { useState } from 'react'
import Layout from '../../components/Layout'
import { useAuthStore } from '../../store/authStore'

export default function SettingsPage() {
  const { user } = useAuthStore()
  const [activeTab, setActiveTab] = useState('profile')

  return (
    <Layout title="Settings">
      <div className="flex flex-col lg:flex-row gap-6">
        {/* Sidebar */}
        <div className="lg:w-64 flex-shrink-0">
          <div className="bg-white rounded-2xl shadow-sm border border-gray-100 p-4">
            <nav className="space-y-1">
              {[
                { id: 'profile', label: 'Profile', icon: 'M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z' },
                { id: 'security', label: 'Security', icon: 'M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z' },
                { id: 'notifications', label: 'Notifications', icon: 'M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9' },
                { id: 'appearance', label: 'Appearance', icon: 'M7 21a4 4 0 01-4-4V5a2 2 0 012-2h4a2 2 0 012 2v12a4 4 0 01-4 4zm0 0h12a2 2 0 002-2v-4a2 2 0 00-2-2h-2.343M11 7.343l1.657-1.657a2 2 0 012.828 0l2.829 2.829a2 2 0 010 2.828l-8.486 8.485M7 17h.01' },
              ].map((tab) => (
                <button
                  key={tab.id}
                  onClick={() => setActiveTab(tab.id)}
                  className={`w-full flex items-center gap-3 px-4 py-3 rounded-xl text-sm font-medium transition-all ${
                    activeTab === tab.id
                      ? 'bg-indigo-50 text-indigo-600'
                      : 'text-gray-600 hover:bg-gray-50'
                  }`}
                >
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d={tab.icon} />
                  </svg>
                  {tab.label}
                </button>
              ))}
            </nav>
          </div>
        </div>

        {/* Content */}
        <div className="flex-1">
          <div className="bg-white rounded-2xl shadow-sm border border-gray-100 p-6">
            {activeTab === 'profile' && (
              <div>
                <h3 className="text-lg font-semibold text-gray-900 mb-6">Profile Settings</h3>
                <div className="space-y-6">
                  <div className="flex items-center gap-6">
                    <div className="w-20 h-20 bg-gradient-to-br from-indigo-500 to-purple-500 rounded-full flex items-center justify-center text-white text-2xl font-bold">
                      {user?.fullName?.charAt(0) || 'U'}
                    </div>
                    <div>
                      <button className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-colors">
                        Change Photo
                      </button>
                      <p className="text-sm text-gray-500 mt-2">JPG, PNG or GIF. Max 2MB.</p>
                    </div>
                  </div>
                  <div className="grid gap-6 md:grid-cols-2">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-2">Full Name</label>
                      <input
                        type="text"
                        defaultValue={user?.fullName}
                        className="w-full px-4 py-2 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 focus:border-indigo-500"
                      />
                    </div>
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-2">Email</label>
                      <input
                        type="email"
                        defaultValue={user?.email}
                        disabled
                        className="w-full px-4 py-2 border border-gray-200 rounded-xl bg-gray-50 text-gray-500"
                      />
                    </div>
                  </div>
                  <div className="flex justify-end">
                    <button className="px-6 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-colors">
                      Save Changes
                    </button>
                  </div>
                </div>
              </div>
            )}

            {activeTab === 'security' && (
              <div>
                <h3 className="text-lg font-semibold text-gray-900 mb-6">Security Settings</h3>
                <div className="space-y-6">
                  <div className="p-4 bg-gray-50 rounded-xl">
                    <h4 className="font-medium text-gray-900">Change Password</h4>
                    <p className="text-sm text-gray-500 mt-1">Update your password regularly for security</p>
                    <button className="mt-4 px-4 py-2 border border-gray-200 rounded-xl hover:bg-white transition-colors">
                      Change Password
                    </button>
                  </div>
                  <div className="p-4 bg-gray-50 rounded-xl">
                    <h4 className="font-medium text-gray-900">Two-Factor Authentication</h4>
                    <p className="text-sm text-gray-500 mt-1">Add an extra layer of security to your account</p>
                    <button className="mt-4 px-4 py-2 border border-gray-200 rounded-xl hover:bg-white transition-colors">
                      Enable 2FA
                    </button>
                  </div>
                </div>
              </div>
            )}

            {activeTab === 'notifications' && (
              <div>
                <h3 className="text-lg font-semibold text-gray-900 mb-6">Notification Settings</h3>
                <div className="space-y-4">
                  {['Email notifications', 'Push notifications', 'SMS notifications'].map((item) => (
                    <div key={item} className="flex items-center justify-between p-4 border border-gray-100 rounded-xl">
                      <span className="font-medium text-gray-700">{item}</span>
                      <label className="relative inline-flex items-center cursor-pointer">
                        <input type="checkbox" className="sr-only peer" defaultChecked />
                        <div className="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-indigo-100 rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-indigo-600"></div>
                      </label>
                    </div>
                  ))}
                </div>
              </div>
            )}

            {activeTab === 'appearance' && (
              <div>
                <h3 className="text-lg font-semibold text-gray-900 mb-6">Appearance Settings</h3>
                <div className="space-y-4">
                  <div className="flex items-center justify-between p-4 border border-gray-100 rounded-xl">
                    <div>
                      <span className="font-medium text-gray-700">Dark Mode</span>
                      <p className="text-sm text-gray-500">Switch between light and dark theme</p>
                    </div>
                    <label className="relative inline-flex items-center cursor-pointer">
                      <input type="checkbox" className="sr-only peer" />
                      <div className="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-indigo-100 rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-indigo-600"></div>
                    </label>
                  </div>
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </Layout>
  )
}