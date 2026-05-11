import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { getTeachers, createTeacher, updateTeacher } from '../../api/users'

export default function AdminTeachersPage() {
  const [teachers, setTeachers] = useState([])
  const [loading, setLoading] = useState(true)
  const [showModal, setShowModal] = useState(false)
  const [editData, setEditData] = useState(null)
  const [formData, setFormData] = useState({ fullName: '', email: '' })

  useEffect(() => { loadTeachers() }, [])

  const loadTeachers = async () => {
    try {
      const { data } = await getTeachers()
      if (data.success) setTeachers(data.data)
    } catch (err) { console.error(err) }
    finally { setLoading(false) }
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      if (editData) {
        await updateTeacher(editData.id, { fullName: formData.fullName, isActive: formData.isActive })
      } else {
        await createTeacher(formData)
      }
      setShowModal(false); setEditData(null); setFormData({ fullName: '', email: '' })
      loadTeachers()
    } catch (err) { console.error(err) }
  }

  const openEdit = (teacher) => {
    setEditData(teacher)
    setFormData({ fullName: teacher.fullName, email: teacher.email, isActive: teacher.isActive })
    setShowModal(true)
  }

  return (
    <Layout title="Teacher Management">
      <div className="bg-white rounded-2xl shadow-sm border border-gray-100">
        <div className="p-6 border-b border-gray-100">
          <div className="flex justify-between items-center">
            <div>
              <h3 className="text-lg font-semibold text-gray-900">Teachers</h3>
              <p className="text-sm text-gray-500 mt-1">Create and manage teacher accounts</p>
            </div>
            <button onClick={() => { setEditData(null); setFormData({ fullName: '', email: '' }); setShowModal(true) }} className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700">
              Add Teacher
            </button>
          </div>
        </div>

        {loading ? <div className="p-8 text-center text-gray-500">Loading...</div> : (
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Name</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Email</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Subjects</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Status</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Actions</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-100">
                {teachers.map((t) => (
                  <tr key={t.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4">
                      <div className="flex items-center gap-3">
                        <div className="w-10 h-10 bg-violet-100 rounded-full flex items-center justify-center text-violet-700 font-medium">
                          {t.fullName?.charAt(0) || 'T'}
                        </div>
                        <span className="font-medium text-gray-900">{t.fullName}</span>
                      </div>
                    </td>
                    <td className="px-6 py-4 text-gray-600">{t.email}</td>
                    <td className="px-6 py-4 text-gray-600">{t.subjectCount}</td>
                    <td className="px-6 py-4">
                      <span className={`px-3 py-1 rounded-full text-xs font-medium ${t.isActive ? 'bg-emerald-100 text-emerald-700' : 'bg-gray-100 text-gray-600'}`}>
                        {t.isActive ? 'Active' : 'Inactive'}
                      </span>
                    </td>
                    <td className="px-6 py-4">
                      <button onClick={() => openEdit(t)} className="p-2 text-indigo-600 hover:bg-indigo-50 rounded-lg">
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" />
                        </svg>
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>

      {showModal && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-2xl p-6 w-full max-w-md">
            <h3 className="text-lg font-semibold text-gray-900 mb-4">{editData ? 'Edit Teacher' : 'Add Teacher'}</h3>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Full Name</label>
                <input type="text" value={formData.fullName} onChange={(e) => setFormData({...formData, fullName: e.target.value})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" required />
              </div>
              {!editData && (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Email</label>
                  <input type="email" value={formData.email} onChange={(e) => setFormData({...formData, email: e.target.value})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" required />
                </div>
              )}
              {editData && (
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Status</label>
                  <select value={formData.isActive ? 'true' : 'false'} onChange={(e) => setFormData({...formData, isActive: e.target.value === 'true'})} className="w-full px-4 py-2 border border-gray-200 rounded-xl">
                    <option value="true">Active</option>
                    <option value="false">Inactive</option>
                  </select>
                </div>
              )}
              <div className="flex justify-end gap-3 pt-4">
                <button type="button" onClick={() => setShowModal(false)} className="px-4 py-2 text-gray-700 hover:bg-gray-100 rounded-xl">Cancel</button>
                <button type="submit" className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700">{editData ? 'Update' : 'Create'}</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </Layout>
  )
}