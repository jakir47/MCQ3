import { useState, useEffect, useCallback } from 'react'
import Layout from '../../components/Layout'
import { getStudents, createStudent, updateStudent, deleteStudent } from '../../api/users'

export default function AdminStudentsPage() {
  const [students, setStudents] = useState([])
  const [loading, setLoading] = useState(true)
  const [searchQuery, setSearchQuery] = useState('')
  const [showPanel, setShowPanel] = useState(false)
  const [editData, setEditData] = useState(null)
  const [submitting, setSubmitting] = useState(false)
  const [notification, setNotification] = useState(null)
  const [formData, setFormData] = useState({
    fullName: '',
    email: '',
    code: '',
    nid: '',
    address: '',
    phone: '',
    fatherName: '',
    fatherContact: '',
    motherName: '',
    motherContact: '',
    isActive: true
  })
  const [formErrors, setFormErrors] = useState({})

  const loadStudents = useCallback(async () => {
    try {
      const { data } = await getStudents()
      if (data.success) setStudents(data.data)
    } catch (err) {
      console.error(err)
      showNotification('error', 'Failed to load students')
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    loadStudents()
  }, [loadStudents])

  const showNotification = (type, message) => {
    setNotification({ type, message })
    setTimeout(() => setNotification(null), 4000)
  }

  const validateForm = () => {
    const errors = {}
    if (!formData.fullName.trim()) errors.fullName = 'Full name is required'
    if (!editData && !formData.email.trim()) errors.email = 'Email is required'
    if (!editData && formData.email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      errors.email = 'Invalid email format'
    }
    setFormErrors(errors)
    return Object.keys(errors).length === 0
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    if (!validateForm()) return

    setSubmitting(true)
    try {
      if (editData) {
        await updateStudent(editData.id, {
          fullName: formData.fullName,
          isActive: formData.isActive,
          code: formData.code || undefined,
          nid: formData.nid || undefined,
          address: formData.address || undefined,
          phone: formData.phone || undefined,
          fatherName: formData.fatherName || undefined,
          fatherContact: formData.fatherContact || undefined,
          motherName: formData.motherName || undefined,
          motherContact: formData.motherContact || undefined
        })
        showNotification('success', 'Student updated successfully')
      } else {
        await createStudent({
          fullName: formData.fullName,
          email: formData.email,
          code: formData.code || undefined,
          nid: formData.nid || undefined,
          address: formData.address || undefined,
          phone: formData.phone || undefined,
          fatherName: formData.fatherName || undefined,
          fatherContact: formData.fatherContact || undefined,
          motherName: formData.motherName || undefined,
          motherContact: formData.motherContact || undefined
        })
        showNotification('success', 'Student created successfully')
      }
      setShowPanel(false)
      resetForm()
      loadStudents()
    } catch (err) {
      const msg = err.response?.data?.error?.message || 'Operation failed'
      showNotification('error', msg)
    } finally {
      setSubmitting(false)
    }
  }

  const handleDelete = async (student) => {
    if (!confirm(`Deactivate student "${student.fullName}"? This will disable their account.`)) return
    try {
      await deleteStudent(student.id)
      showNotification('success', 'Student deactivated successfully')
      loadStudents()
    } catch (err) {
      showNotification('error', 'Failed to deactivate student')
    }
  }

  const resetForm = () => {
    setEditData(null)
    setFormData({
      fullName: '',
      email: '',
      code: '',
      nid: '',
      address: '',
      phone: '',
      fatherName: '',
      fatherContact: '',
      motherName: '',
      motherContact: '',
      isActive: true
    })
    setFormErrors({})
  }

  const openEdit = (student) => {
    setEditData(student)
    setFormData({
      fullName: student.fullName,
      email: student.email,
      code: student.code || '',
      nid: student.nid || '',
      address: student.address || '',
      phone: student.phone || '',
      fatherName: student.fatherName || '',
      fatherContact: student.fatherContact || '',
      motherName: student.motherName || '',
      motherContact: student.motherContact || '',
      isActive: student.isActive
    })
    setShowPanel(true)
  }

  const openAdd = () => {
    resetForm()
    setShowPanel(true)
  }

  const filteredStudents = students.filter(s =>
    s.fullName?.toLowerCase().includes(searchQuery.toLowerCase()) ||
    s.email?.toLowerCase().includes(searchQuery.toLowerCase()) ||
    s.code?.toLowerCase().includes(searchQuery.toLowerCase())
  )

  const stats = {
    total: students.length,
    active: students.filter(s => s.isActive).length,
    inactive: students.filter(s => !s.isActive).length
  }

  if (loading) {
    return (
      <Layout title="Student Management">
        <div className="space-y-6">
          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            {[1, 2, 3].map(i => (
              <div key={i} className="bg-white rounded-2xl p-6 animate-pulse">
                <div className="h-4 bg-gray-200 rounded w-1/2 mb-4"></div>
                <div className="h-8 bg-gray-200 rounded w-3/4"></div>
              </div>
            ))}
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {[1, 2, 3, 4, 5, 6].map(i => (
              <div key={i} className="bg-white rounded-2xl p-6 animate-pulse">
                <div className="flex items-center gap-4 mb-4">
                  <div className="w-14 h-14 bg-gray-200 rounded-full"></div>
                  <div className="flex-1">
                    <div className="h-4 bg-gray-200 rounded w-2/3 mb-2"></div>
                    <div className="h-3 bg-gray-200 rounded w-1/2"></div>
                  </div>
                </div>
                <div className="h-3 bg-gray-200 rounded w-full mb-2"></div>
                <div className="h-3 bg-gray-200 rounded w-3/4"></div>
              </div>
            ))}
          </div>
        </div>
      </Layout>
    )
  }

  return (
    <Layout title="Student Management">
      {notification && (
        <div className={`fixed top-20 right-6 z-50 px-6 py-4 rounded-xl shadow-lg flex items-center gap-3 animate-slide-in ${
          notification.type === 'success' ? 'bg-emerald-50 text-emerald-700 border border-emerald-200' : 'bg-red-50 text-red-700 border border-red-200'
        }`}>
          {notification.type === 'success' ? (
            <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
            </svg>
          ) : (
            <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
            </svg>
          )}
          <span className="font-medium">{notification.message}</span>
        </div>
      )}

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
        <div className="bg-white rounded-2xl p-6 shadow-sm border border-gray-100">
          <div className="flex items-center gap-4">
            <div className="w-12 h-12 rounded-xl bg-indigo-100 flex items-center justify-center">
              <svg className="w-6 h-6 text-indigo-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z" />
              </svg>
            </div>
            <div>
              <p className="text-sm text-gray-500">Total Students</p>
              <p className="text-2xl font-bold text-gray-900">{stats.total}</p>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-2xl p-6 shadow-sm border border-gray-100">
          <div className="flex items-center gap-4">
            <div className="w-12 h-12 rounded-xl bg-emerald-100 flex items-center justify-center">
              <svg className="w-6 h-6 text-emerald-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
            </div>
            <div>
              <p className="text-sm text-gray-500">Active</p>
              <p className="text-2xl font-bold text-emerald-600">{stats.active}</p>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-2xl p-6 shadow-sm border border-gray-100">
          <div className="flex items-center gap-4">
            <div className="w-12 h-12 rounded-xl bg-amber-100 flex items-center justify-center">
              <svg className="w-6 h-6 text-amber-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" />
              </svg>
            </div>
            <div>
              <p className="text-sm text-gray-500">Inactive</p>
              <p className="text-2xl font-bold text-amber-600">{stats.inactive}</p>
            </div>
          </div>
        </div>
      </div>

      <div className="bg-white rounded-2xl shadow-sm border border-gray-100">
        <div className="p-6 border-b border-gray-100">
          <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
            <div>
              <h3 className="text-lg font-semibold text-gray-900">All Students</h3>
              <p className="text-sm text-gray-500 mt-1">Manage student accounts and profiles</p>
            </div>
            <div className="flex items-center gap-3">
              <div className="relative">
                <input
                  type="text"
                  placeholder="Search students..."
                  value={searchQuery}
                  onChange={(e) => setSearchQuery(e.target.value)}
                  className="w-full sm:w-64 pl-10 pr-4 py-2 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 focus:border-indigo-500"
                />
                <svg className="w-5 h-5 text-gray-400 absolute left-3 top-1/2 -translate-y-1/2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
                </svg>
              </div>
              <button
                onClick={openAdd}
                className="px-5 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-colors flex items-center gap-2 font-medium"
              >
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" />
                </svg>
                Add Student
              </button>
            </div>
          </div>
        </div>

        {filteredStudents.length === 0 ? (
          <div className="p-12 text-center">
            <div className="w-20 h-20 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <svg className="w-10 h-10 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z" />
              </svg>
            </div>
            <h3 className="text-lg font-medium text-gray-900 mb-2">No students found</h3>
            <p className="text-gray-500 mb-6">Get started by adding your first student account.</p>
            <button onClick={openAdd} className="px-5 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-colors font-medium">
              Add First Student
            </button>
          </div>
        ) : (
          <div className="p-6">
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {filteredStudents.map((student) => (
                <div
                  key={student.id}
                  className="bg-gray-50 rounded-2xl p-6 hover:bg-gray-100 transition-colors group"
                >
                  <div className="flex items-start justify-between mb-4">
                    <div className="flex items-center gap-4">
                      <div className="w-14 h-14 rounded-full bg-gradient-to-br from-emerald-500 to-teal-600 flex items-center justify-center text-white text-lg font-bold shadow-md">
                        {student.fullName?.charAt(0) || 'S'}
                      </div>
                      <div>
                        <h4 className="font-semibold text-gray-900">{student.fullName}</h4>
                        {student.code && (
                          <span className="inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium bg-emerald-100 text-emerald-700 mt-1">
                            {student.code}
                          </span>
                        )}
                      </div>
                    </div>
                    <span className={`px-3 py-1 rounded-full text-xs font-medium ${
                      student.isActive
                        ? 'bg-emerald-100 text-emerald-700'
                        : 'bg-gray-200 text-gray-600'
                    }`}>
                      {student.isActive ? 'Active' : 'Inactive'}
                    </span>
                  </div>

                  <div className="space-y-2 text-sm">
                    <div className="flex items-center gap-2 text-gray-600">
                      <svg className="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
                      </svg>
                      <span className="truncate">{student.email}</span>
                    </div>
                    {student.phone && (
                      <div className="flex items-center gap-2 text-gray-600">
                        <svg className="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z" />
                        </svg>
                        <span>{student.phone}</span>
                      </div>
                    )}
                    {student.fatherName && (
                      <div className="flex items-center gap-2 text-gray-600">
                        <svg className="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                        </svg>
                        <span>Father: {student.fatherName}</span>
                      </div>
                    )}
                    {student.nid && (
                      <div className="flex items-center gap-2 text-gray-600">
                        <svg className="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M10 6H5a2 2 0 00-2 2v9a2 2 0 002 2h14a2 2 0 002-2V8a2 2 0 00-2-2h-5m-4 0V5a2 2 0 114 0v1m-4 0a2 2 0 104 0m-5 8a2 2 0 100-4 2 2 0 000 4zm0 0c1.306 0 2.417.835 2.83 2M9 14a3.001 3.001 0 00-2.83 2M15 11h3m-3 4h2" />
                        </svg>
                        <span>NID: {student.nid}</span>
                      </div>
                    )}
                  </div>

                  <div className="mt-4 pt-4 border-t border-gray-200 flex items-center justify-between opacity-0 group-hover:opacity-100 transition-opacity">
                    <span className="text-xs text-gray-500">
                      Added {new Date(student.createdAt).toLocaleDateString()}
                    </span>
                    <div className="flex items-center gap-2">
                      <button
                        onClick={() => openEdit(student)}
                        className="p-2 text-indigo-600 hover:bg-indigo-100 rounded-lg transition-colors"
                        title="Edit"
                      >
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" />
                        </svg>
                      </button>
                      <button
                        onClick={() => handleDelete(student)}
                        className="p-2 text-red-600 hover:bg-red-100 rounded-lg transition-colors"
                        title="Deactivate"
                      >
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                        </svg>
                      </button>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>

      {showPanel && (
        <>
          <div
            className="fixed inset-0 bg-black/40 backdrop-blur-sm z-40 transition-opacity"
            onClick={() => setShowPanel(false)}
          />
          <div className="fixed inset-y-0 right-0 w-full max-w-lg bg-white shadow-2xl z-50 flex flex-col">
            <div className="flex items-center justify-between p-6 border-b border-gray-100">
              <h2 className="text-xl font-semibold text-gray-900">
                {editData ? 'Edit Student' : 'Add New Student'}
              </h2>
              <button
                onClick={() => setShowPanel(false)}
                className="p-2 text-gray-400 hover:text-gray-600 hover:bg-gray-100 rounded-lg transition-colors"
              >
                <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M6 18L18 6M6 6l12 12" />
                </svg>
              </button>
            </div>

            <form onSubmit={handleSubmit} className="flex-1 overflow-y-auto">
              <div className="p-6 space-y-6">
                <div>
                  <h3 className="text-sm font-medium text-gray-900 mb-4 flex items-center gap-2">
                    <span className="w-6 h-6 rounded-full bg-indigo-100 text-indigo-600 flex items-center justify-center text-xs font-bold">1</span>
                    Basic Information
                  </h3>
                  <div className="space-y-4 pl-8">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1.5">
                        Full Name <span className="text-red-500">*</span>
                      </label>
                      <input
                        type="text"
                        value={formData.fullName}
                        onChange={(e) => setFormData({ ...formData, fullName: e.target.value })}
                        className={`w-full px-4 py-2.5 border rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 transition-colors ${
                          formErrors.fullName ? 'border-red-300 bg-red-50' : 'border-gray-200 hover:border-gray-300'
                        }`}
                        placeholder="Enter full name"
                      />
                      {formErrors.fullName && (
                        <p className="mt-1 text-sm text-red-600">{formErrors.fullName}</p>
                      )}
                    </div>

                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          Student Code
                        </label>
                        <input
                          type="text"
                          value={formData.code}
                          onChange={(e) => setFormData({ ...formData, code: e.target.value })}
                          className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors"
                          placeholder="e.g. STU-001"
                        />
                      </div>
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          National ID (NID)
                        </label>
                        <input
                          type="text"
                          value={formData.nid}
                          onChange={(e) => setFormData({ ...formData, nid: e.target.value })}
                          className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors"
                          placeholder="NID number"
                        />
                      </div>
                    </div>

                    {!editData && (
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          Email Address <span className="text-red-500">*</span>
                        </label>
                        <input
                          type="email"
                          value={formData.email}
                          onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                          className={`w-full px-4 py-2.5 border rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 transition-colors ${
                            formErrors.email ? 'border-red-300 bg-red-50' : 'border-gray-200 hover:border-gray-300'
                          }`}
                          placeholder="student@email.com"
                        />
                        {formErrors.email && (
                          <p className="mt-1 text-sm text-red-600">{formErrors.email}</p>
                        )}
                      </div>
                    )}

                    {editData && (
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          Status
                        </label>
                        <div className="flex items-center gap-4">
                          <label className="flex items-center gap-2 cursor-pointer">
                            <input
                              type="radio"
                              name="isActive"
                              checked={formData.isActive}
                              onChange={() => setFormData({ ...formData, isActive: true })}
                              className="w-4 h-4 text-indigo-600"
                            />
                            <span className="text-sm text-gray-700">Active</span>
                          </label>
                          <label className="flex items-center gap-2 cursor-pointer">
                            <input
                              type="radio"
                              name="isActive"
                              checked={!formData.isActive}
                              onChange={() => setFormData({ ...formData, isActive: false })}
                              className="w-4 h-4 text-indigo-600"
                            />
                            <span className="text-sm text-gray-700">Inactive</span>
                          </label>
                        </div>
                      </div>
                    )}
                  </div>
                </div>

                <div>
                  <h3 className="text-sm font-medium text-gray-900 mb-4 flex items-center gap-2">
                    <span className="w-6 h-6 rounded-full bg-indigo-100 text-indigo-600 flex items-center justify-center text-xs font-bold">2</span>
                    Contact Details
                  </h3>
                  <div className="space-y-4 pl-8">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1.5">
                        Phone Number
                      </label>
                      <input
                        type="tel"
                        value={formData.phone}
                        onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
                        className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors"
                        placeholder="+880 1XXX XXXXXX"
                      />
                    </div>

                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1.5">
                        Address
                      </label>
                      <textarea
                        value={formData.address}
                        onChange={(e) => setFormData({ ...formData, address: e.target.value })}
                        rows={2}
                        className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors resize-none"
                        placeholder="Full address"
                      />
                    </div>
                  </div>
                </div>

                <div>
                  <h3 className="text-sm font-medium text-gray-900 mb-4 flex items-center gap-2">
                    <span className="w-6 h-6 rounded-full bg-indigo-100 text-indigo-600 flex items-center justify-center text-xs font-bold">3</span>
                    Parent Information
                  </h3>
                  <div className="space-y-4 pl-8">
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          Father&apos;s Name
                        </label>
                        <input
                          type="text"
                          value={formData.fatherName}
                          onChange={(e) => setFormData({ ...formData, fatherName: e.target.value })}
                          className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors"
                          placeholder="Father's full name"
                        />
                      </div>
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          Father&apos;s Contact
                        </label>
                        <input
                          type="tel"
                          value={formData.fatherContact}
                          onChange={(e) => setFormData({ ...formData, fatherContact: e.target.value })}
                          className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors"
                          placeholder="+880 1XXX XXXXXX"
                        />
                      </div>
                    </div>

                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          Mother&apos;s Name
                        </label>
                        <input
                          type="text"
                          value={formData.motherName}
                          onChange={(e) => setFormData({ ...formData, motherName: e.target.value })}
                          className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors"
                          placeholder="Mother's full name"
                        />
                      </div>
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1.5">
                          Mother&apos;s Contact
                        </label>
                        <input
                          type="tel"
                          value={formData.motherContact}
                          onChange={(e) => setFormData({ ...formData, motherContact: e.target.value })}
                          className="w-full px-4 py-2.5 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 hover:border-gray-300 transition-colors"
                          placeholder="+880 1XXX XXXXXX"
                        />
                      </div>
                    </div>
                  </div>
                </div>

                {!editData && (
                  <div className="bg-amber-50 border border-amber-200 rounded-xl p-4">
                    <div className="flex items-start gap-3">
                      <svg className="w-5 h-5 text-amber-600 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                      </svg>
                      <div>
                        <p className="text-sm font-medium text-amber-800">Auto-generated password</p>
                        <p className="text-sm text-amber-700 mt-1">
                          A temporary password will be generated and sent to the student&apos;s email. They can change it on first login.
                        </p>
                      </div>
                    </div>
                  </div>
                )}
              </div>

              <div className="p-6 border-t border-gray-100 bg-gray-50">
                <div className="flex items-center justify-end gap-3">
                  <button
                    type="button"
                    onClick={() => setShowPanel(false)}
                    className="px-5 py-2.5 text-gray-700 hover:bg-gray-100 rounded-xl transition-colors font-medium"
                  >
                    Cancel
                  </button>
                  <button
                    type="submit"
                    disabled={submitting}
                    className="px-6 py-2.5 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-colors font-medium flex items-center gap-2 disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    {submitting ? (
                      <>
                        <svg className="w-4 h-4 animate-spin" fill="none" viewBox="0 0 24 24">
                          <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" />
                          <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z" />
                        </svg>
                        Processing...
                      </>
                    ) : (
                      <>
                        {editData ? 'Update Student' : 'Create Student'}
                      </>
                    )}
                  </button>
                </div>
              </div>
            </form>
          </div>
        </>
      )}

      <style>{`
        @keyframes slide-in {
          from { transform: translateX(100%); opacity: 0; }
          to { transform: translateX(0); opacity: 1; }
        }
        .animate-slide-in { animation: slide-in 0.3s ease-out; }
      `}</style>
    </Layout>
  )
}