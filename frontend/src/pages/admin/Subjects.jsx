import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { getSubjects, deleteSubject } from '../../api/subjects'

export default function AdminSubjectsPage() {
  const [subjects, setSubjects] = useState([])
  const [loading, setLoading] = useState(true)

  const loadData = async () => {
    try {
      const { data } = await getSubjects()
      if (data.success) setSubjects(data.data)
    } catch (err) {
      console.error(err)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    loadData()
  }, [])

  const handleDelete = async (id) => {
    if (!confirm('Are you sure you want to delete this subject?')) return
    try {
      await deleteSubject(id)
      loadData()
    } catch (err) {
      console.error(err)
    }
  }

  return (
    <Layout title="Subject Management">
      <div className="bg-white rounded-2xl shadow-sm border border-gray-100">
        <div className="p-6 border-b border-gray-100">
          <div className="flex items-center justify-between">
            <div>
              <h3 className="text-lg font-semibold text-gray-900">All Subjects</h3>
              <p className="text-sm text-gray-500 mt-1">View and manage all subjects in the system</p>
            </div>
            <button className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-colors">
              Add Subject
            </button>
          </div>
        </div>

        {loading ? (
          <div className="p-8 text-center text-gray-500">Loading...</div>
        ) : subjects.length === 0 ? (
          <div className="p-8 text-center">
            <p className="text-gray-500">No subjects found.</p>
          </div>
        ) : (
          <div className="divide-y divide-gray-100">
            {subjects.map((subject) => (
              <div key={subject.id} className="p-6 hover:bg-gray-50 transition-colors">
                <div className="flex items-center justify-between">
                  <div>
                    <h4 className="font-semibold text-gray-900">{subject.title}</h4>
                    <p className="text-sm text-gray-500 mt-1">{subject.description || 'No description'}</p>
                    <div className="flex items-center gap-4 mt-2 text-xs text-gray-500">
                      <span>{subject.chapterCount || 0} chapters</span>
                      <span>{subject.questionCount || 0} questions</span>
                    </div>
                  </div>
                  <div className="flex items-center gap-2">
                    <button className="p-2 text-indigo-600 hover:bg-indigo-50 rounded-lg">
                      <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" />
                      </svg>
                    </button>
                    <button onClick={() => handleDelete(subject.id)} className="p-2 text-red-600 hover:bg-red-50 rounded-lg">
                      <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                      </svg>
                    </button>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </Layout>
  )
}