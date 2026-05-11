import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { getSubjects, getChapters, createChapter, updateChapter, deleteChapter } from '../../api/subjects'

export default function TeacherChaptersPage() {
  const [subjects, setSubjects] = useState([])
  const [chapters, setChapters] = useState([])
  const [loading, setLoading] = useState(true)
  const [selectedSubject, setSelectedSubject] = useState('')
  const [showModal, setShowModal] = useState(false)
  const [editData, setEditData] = useState(null)
  const [formData, setFormData] = useState({ title: '', description: '', orderIndex: 0 })

  useEffect(() => {
    loadSubjects()
  }, [])

  useEffect(() => {
    if (selectedSubject) {
      loadChapters(selectedSubject)
    }
  }, [selectedSubject])

  const loadSubjects = async () => {
    try {
      const { data } = await getSubjects()
      if (data.success) {
        setSubjects(data.data)
        if (data.data.length > 0) {
          setSelectedSubject(data.data[0].id)
        }
      }
    } catch (err) {
      console.error(err)
    }
  }

  const loadChapters = async (subjectId) => {
    setLoading(true)
    try {
      const { data } = await getChapters(subjectId)
      if (data.success) setChapters(data.data)
    } catch (err) {
      console.error(err)
    } finally {
      setLoading(false)
    }
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      if (editData) {
        await updateChapter(editData.id, formData)
      } else {
        await createChapter(selectedSubject, formData)
      }
      setShowModal(false)
      setEditData(null)
      setFormData({ title: '', description: '', orderIndex: 0 })
      loadChapters(selectedSubject)
    } catch (err) {
      console.error(err)
    }
  }

  const handleDelete = async (id) => {
    if (!confirm('Delete this chapter?')) return
    try {
      await deleteChapter(id)
      loadChapters(selectedSubject)
    } catch (err) {
      console.error(err)
    }
  }

  const openEdit = (chapter) => {
    setEditData(chapter)
    setFormData({ title: chapter.title, description: chapter.description || '', orderIndex: chapter.orderIndex })
    setShowModal(true)
  }

  return (
    <Layout title="Chapters">
      <div className="bg-white rounded-2xl shadow-sm border border-gray-100">
        <div className="p-6 border-b border-gray-100">
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-4">
              <label className="text-sm font-medium text-gray-700">Select Subject:</label>
              <select
                value={selectedSubject}
                onChange={(e) => setSelectedSubject(e.target.value)}
                className="px-4 py-2 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100"
              >
                {subjects.map((s) => (
                  <option key={s.id} value={s.id}>{s.title}</option>
                ))}
              </select>
            </div>
            <button 
              onClick={() => { setEditData(null); setFormData({ title: '', description: '', orderIndex: 0 }); setShowModal(true); }}
              disabled={!selectedSubject}
              className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 disabled:opacity-50"
            >
              Add Chapter
            </button>
          </div>
        </div>

        {loading ? (
          <div className="p-8 text-center text-gray-500">Loading...</div>
        ) : chapters.length === 0 ? (
          <div className="p-8 text-center">
            <p className="text-gray-500">No chapters in this subject.</p>
          </div>
        ) : (
          <div className="divide-y divide-gray-100">
            {chapters.map((chapter) => (
              <div key={chapter.id} className="p-6 hover:bg-gray-50 transition-colors">
                <div className="flex items-center justify-between">
                  <div className="flex-1">
                    <div className="flex items-center gap-3">
                      <span className="w-8 h-8 bg-indigo-100 text-indigo-600 rounded-lg flex items-center justify-center text-sm font-medium">
                        {chapter.orderIndex + 1}
                      </span>
                      <div>
                        <h4 className="font-semibold text-gray-900">{chapter.title}</h4>
                        <p className="text-sm text-gray-500 mt-1">{chapter.description || 'No description'}</p>
                        <div className="flex items-center gap-4 mt-2">
                          <span className="text-xs text-gray-500">{chapter.questionCount || 0} questions</span>
                          <span className="text-xs text-gray-500">{chapter.examCount || 0} exams</span>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className="flex items-center gap-2">
                    <a href={`/teacher/questions?chapter=${chapter.id}`} className="px-3 py-1.5 text-sm text-indigo-600 hover:bg-indigo-50 rounded-lg">
                      Questions
                    </a>
                    <a href={`/teacher/exams?chapter=${chapter.id}`} className="px-3 py-1.5 text-sm text-indigo-600 hover:bg-indigo-50 rounded-lg">
                      Exams
                    </a>
                    <button onClick={() => openEdit(chapter)} className="p-2 text-gray-600 hover:bg-gray-100 rounded-lg">
                      <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" />
                      </svg>
                    </button>
                    <button onClick={() => handleDelete(chapter.id)} className="p-2 text-red-600 hover:bg-red-50 rounded-lg">
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

      {showModal && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
          <div className="bg-white rounded-2xl p-6 w-full max-w-md">
            <h3 className="text-lg font-semibold text-gray-900 mb-4">
              {editData ? 'Edit Chapter' : 'Add Chapter'}
            </h3>
            <form onSubmit={handleSubmit}>
              <div className="space-y-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Title</label>
                  <input
                    type="text"
                    value={formData.title}
                    onChange={(e) => setFormData({ ...formData, title: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100"
                    required
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Description</label>
                  <textarea
                    value={formData.description}
                    onChange={(e) => setFormData({ ...formData, description: e.target.value })}
                    className="w-full px-4 py-2 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100"
                    rows={2}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Order Index</label>
                  <input
                    type="number"
                    value={formData.orderIndex}
                    onChange={(e) => setFormData({ ...formData, orderIndex: parseInt(e.target.value) })}
                    className="w-full px-4 py-2 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100"
                    min={0}
                  />
                </div>
              </div>
              <div className="flex justify-end gap-3 mt-6">
                <button type="button" onClick={() => setShowModal(false)} className="px-4 py-2 text-gray-700 hover:bg-gray-100 rounded-xl">
                  Cancel
                </button>
                <button type="submit" className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700">
                  {editData ? 'Update' : 'Create'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </Layout>
  )
}