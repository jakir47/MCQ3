import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { getSubjects, getChapters } from '../../api/subjects'
import { getStudents, assignStudent, removeStudentAssignment, getChapterAssignments } from '../../api/users'

export default function TeacherStudentsPage() {
  const [subjects, setSubjects] = useState([])
  const [chapters, setChapters] = useState([])
  const [students, setStudents] = useState([])
  const [assignments, setAssignments] = useState([])
  const [selectedSubject, setSelectedSubject] = useState('')
  const [selectedChapter, setSelectedChapter] = useState('')
  const [loading, setLoading] = useState(true)
  const [showAssignModal, setShowAssignModal] = useState(false)
  const [notification, setNotification] = useState(null)

  const loadData = async () => {
    try {
      const [subjectsRes, studentsRes] = await Promise.all([getSubjects(), getStudents()])
      if (subjectsRes.data.success) {
        setSubjects(subjectsRes.data.data)
        if (subjectsRes.data.data.length > 0) setSelectedSubject(subjectsRes.data.data[0].id)
      }
      if (studentsRes.data.success) setStudents(studentsRes.data.data)
    } catch (err) { console.error(err) }
    finally { setLoading(false) }
  }

  const loadChapters = async (subjectId) => {
    try {
      const { data } = await getChapters(subjectId)
      if (data.success) {
        setChapters(data.data)
        if (data.data.length > 0) setSelectedChapter(data.data[0].id)
        else setSelectedChapter('')
      }
    } catch (err) { console.error(err) }
  }

  const loadAssignments = async (chapterId) => {
    try {
      const { data } = await getChapterAssignments(chapterId)
      if (data.success) setAssignments(data.data)
    } catch (err) { console.error(err) }
  }

  useEffect(() => { loadData() }, [])

  useEffect(() => { if (selectedSubject) loadChapters(selectedSubject) }, [selectedSubject])
  useEffect(() => { if (selectedChapter) loadAssignments(selectedChapter) }, [selectedChapter])

  const handleAssign = async (studentId) => {
    try {
      await assignStudent({ studentId, chapterId: selectedChapter })
      setShowAssignModal(false)
      loadAssignments(selectedChapter)
      setNotification({ type: 'success', message: 'Student assigned successfully' })
    } catch (err) { 
      console.error(err)
      setNotification({ type: 'error', message: 'Failed to assign student' })
    }
    setTimeout(() => setNotification(null), 3000)
  }

  const handleRemove = async (studentId) => {
    if (!confirm('Remove this student from chapter? If they have activity, they will be marked as inactive instead.')) return
    try {
      await removeStudentAssignment(studentId, selectedChapter)
      loadAssignments(selectedChapter)
      setNotification({ type: 'success', message: 'Student removed successfully' })
    } catch (err) { 
      console.error(err)
      setNotification({ type: 'error', message: 'Failed to remove student' })
    }
    setTimeout(() => setNotification(null), 3000)
  }

  const assignedStudentIds = assignments.map(a => a.studentId)
  const unassignedStudents = students.filter(s => !assignedStudentIds.includes(s.id))

  return (
    <Layout title="Student Management">
      {notification && (
        <div className={`mb-4 p-4 rounded-xl ${notification.type === 'success' ? 'bg-emerald-50 text-emerald-700 border border-emerald-200' : 'bg-red-50 text-red-700 border border-red-200'}`}>
          {notification.message}
        </div>
      )}
      <div className="bg-white rounded-2xl shadow-sm border border-gray-100 mb-6">
        <div className="p-6 border-b border-gray-100">
          <div className="flex flex-wrap items-center gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Subject</label>
              <select value={selectedSubject} onChange={(e) => setSelectedSubject(e.target.value)} className="px-3 py-2 border border-gray-200 rounded-xl">
                {subjects.map(s => <option key={s.id} value={s.id}>{s.title}</option>)}
              </select>
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Chapter</label>
              <select value={selectedChapter} onChange={(e) => setSelectedChapter(e.target.value)} className="px-3 py-2 border border-gray-200 rounded-xl">
                {chapters.map(c => <option key={c.id} value={c.id}>{c.title}</option>)}
              </select>
            </div>
            <button onClick={() => setShowAssignModal(true)} disabled={!selectedChapter} className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 disabled:opacity-50 mt-5">
              Assign Students
            </button>
          </div>
        </div>

        {loading ? <div className="p-8 text-center text-gray-500">Loading...</div> : (
          <div className="overflow-x-auto">
            <table className="w-full">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Student</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Email</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Assigned Date</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase">Actions</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-100">
                {assignments.length === 0 ? (
                  <tr><td colSpan={4} className="px-6 py-8 text-center text-gray-500">No students assigned to this chapter</td></tr>
                ) : assignments.map((a) => (
                  <tr key={a.id} className="hover:bg-gray-50">
                    <td className="px-6 py-4">
                      <div className="flex items-center gap-3">
                        <div className="w-10 h-10 bg-emerald-100 rounded-full flex items-center justify-center text-emerald-700 font-medium">
                          {a.studentName?.charAt(0) || 'S'}
                        </div>
                        <span className="font-medium text-gray-900">{a.studentName}</span>
                      </div>
                    </td>
                    <td className="px-6 py-4 text-gray-600">{a.studentEmail}</td>
                    <td className="px-6 py-4 text-gray-600">{new Date(a.createdAt).toLocaleDateString()}</td>
                    <td className="px-6 py-4">
                      <button onClick={() => handleRemove(a.studentId)} className="p-2 text-red-600 hover:bg-red-50 rounded-lg">
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
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

      {showAssignModal && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-2xl p-6 w-full max-w-lg max-h-[80vh] overflow-y-auto">
            <h3 className="text-lg font-semibold text-gray-900 mb-4">Assign Students to Chapter</h3>
            {unassignedStudents.length === 0 ? (
              <p className="text-gray-500 text-center py-4">All students are already assigned to this chapter</p>
            ) : (
              <div className="space-y-2 max-h-96 overflow-y-auto">
                {unassignedStudents.map((s) => (
                  <div key={s.id} className="flex items-center justify-between p-3 border border-gray-200 rounded-lg hover:bg-gray-50">
                    <div className="flex items-center gap-3">
                      <div className="w-10 h-10 bg-emerald-100 rounded-full flex items-center justify-center text-emerald-700 font-medium">
                        {s.fullName?.charAt(0) || 'S'}
                      </div>
                      <div>
                        <p className="font-medium text-gray-900">{s.fullName}</p>
                        <p className="text-sm text-gray-500">{s.email}</p>
                      </div>
                    </div>
                    <button onClick={() => handleAssign(s.id)} className="px-3 py-1.5 bg-indigo-600 text-white text-sm rounded-lg hover:bg-indigo-700">
                      Assign
                    </button>
                  </div>
                ))}
              </div>
            )}
            <div className="mt-4 flex justify-end">
              <button onClick={() => setShowAssignModal(false)} className="px-4 py-2 bg-gray-100 text-gray-700 rounded-xl hover:bg-gray-200">Close</button>
            </div>
          </div>
        </div>
      )}
    </Layout>
  )
}