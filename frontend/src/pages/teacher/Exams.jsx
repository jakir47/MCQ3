import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { getSubjects, getChapters } from '../../api/subjects'
import { getExams, createExam, updateExam, deleteExam, publishExam, unpublishExam } from '../../api/exams'
import { getQuestions } from '../../api/questions'
import ExamEnrolmentModal from '../../components/ExamEnrolmentModal'

export default function TeacherExamsPage() {
  const [subjects, setSubjects] = useState([])
  const [chapters, setChapters] = useState([])
  const [exams, setExams] = useState([])
  const [loading, setLoading] = useState(true)
  const [availableQuestions, setAvailableQuestions] = useState([])
  const [showQuestionsModal, setShowQuestionsModal] = useState(false)
  const [selectedExamQuestions, setSelectedExamQuestions] = useState([])
  const [selectedSubject, setSelectedSubject] = useState('')
  const [selectedChapter, setSelectedChapter] = useState('')
  const [showModal, setShowModal] = useState(false)
  const [editData, setEditData] = useState(null)
  const [notification, setNotification] = useState(null)
  const [examToPublish, setExamToPublish] = useState(null)
  const [formData, setFormData] = useState({
    title: '',
    totalMarks: 10,
    passingScore: 5,
    timeLimitSeconds: 600,
    maxAttempts: 3,
    negativeMarking: true,
    shuffleQuestions: false,
    shuffleOptions: true,
    showAnswersAfter: true,
    autoReleaseResults: true,
    questionIds: []
  })

  const loadSubjects = async () => {
    try {
      const { data } = await getSubjects()
      if (data.success) { setSubjects(data.data); if (data.data.length > 0) setSelectedSubject(data.data[0].id) }
    } catch (err) { console.error(err) }
  }

  const loadChapters = async (subjectId) => {
    try {
      const { data } = await getChapters(subjectId)
      if (data.success) { setChapters(data.data); if (data.data.length > 0) setSelectedChapter(data.data[0].id); else setSelectedChapter('') }
    } catch (err) { console.error(err) }
  }

  const loadExams = async (chapterId) => {
    setLoading(true)
    try {
      const { data } = await getExams(chapterId)
      if (data.success) setExams(data.data)
      console.log(data.data)
    } catch (err) { console.error(err) }
    finally { setLoading(false) }
  }

  useEffect(() => { loadSubjects() }, [])
  useEffect(() => { if (selectedSubject) loadChapters(selectedSubject) }, [selectedSubject])
  useEffect(() => { if (selectedChapter) loadExams(selectedChapter) }, [selectedChapter])

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const payload = { ...formData }
      if (editData) {
        await updateExam(editData.id, payload)
      } else {
        await createExam(selectedChapter, payload)
      }
      setShowModal(false); setEditData(null); loadExams(selectedChapter)
    } catch (err) { console.error(err) }
  }

  const handleDelete = async (id) => {
    if (!confirm('Delete this exam?')) return
    try { await deleteExam(id); loadExams(selectedChapter) }
    catch (err) { console.error(err) }
  }

  const handlePublish = async (id, currentStatus) => {
    try {
      if (currentStatus === 'Published') {
        await unpublishExam(id)
        setNotification({ type: 'success', message: 'Exam unpublished successfully' })
      } else {
        await publishExam(id)
        setNotification({ type: 'success', message: 'Exam published successfully' })
      }
      loadExams(selectedChapter)
    } catch (err) { 
      console.error(err)
      setNotification({ type: 'error', message: 'Failed to publish/unpublish exam' })
    }
    setTimeout(() => setNotification(null), 3000)
  }

  const openEdit = async (exam) => {
    setEditData(exam)
    setFormData({
      title: exam.title, totalMarks: exam.totalMarks, passingScore: exam.passingScore,
      timeLimitSeconds: exam.timeLimitSeconds || 600, maxAttempts: exam.maxAttempts || 3,
      negativeMarking: exam.negativeMarking, shuffleQuestions: exam.shuffleQuestions,
      shuffleOptions: exam.shuffleOptions, showAnswersAfter: exam.showAnswersAfter, autoReleaseResults: exam.autoReleaseResults,
      questionIds: exam.examQuestions?.map(eq => eq.questionId) || []
    })
    try { const { data } = await getQuestions(selectedChapter); if (data.success) setAvailableQuestions(data.data); } catch (err) { console.error(err) }
    setShowModal(true)
  }

  const getStatusColor = (status) => {
    switch (status) {
      case 'Published': return 'bg-emerald-100 text-emerald-700'
      case 'Draft': return 'bg-amber-100 text-amber-700'
      case 'Archived': return 'bg-gray-100 text-gray-700'
      default: return 'bg-gray-100 text-gray-700'
    }
  }

  const formatStatus = (status) => {
    if (!status) return 'Unknown'
    if (status === 'Published' || status === 'Draft' || status === 'Archived') return status
    return status.toString()
  }

  return (
    <Layout title="Exams">
      {notification && (
        <div className={`mb-4 p-4 rounded-xl ${notification.type === 'success' ? 'bg-emerald-50 text-emerald-700 border border-emerald-200' : 'bg-red-50 text-red-700 border border-red-200'}`}>
          {notification.message}
        </div>
      )}
      <div className="bg-white rounded-2xl shadow-sm border border-gray-100">
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
            <button onClick={async () => { if (!selectedChapter) return; setEditData(null); setFormData({title:'',totalMarks:10,passingScore:5,timeLimitSeconds:600,maxAttempts:3,negativeMarking:true,shuffleQuestions:false,shuffleOptions:true,showAnswersAfter:true,autoReleaseResults:true,questionIds:[]}); try { const { data } = await getQuestions(selectedChapter); if (data.success) setAvailableQuestions(data.data); else setAvailableQuestions([]); } catch (err) { console.error(err); setAvailableQuestions([]); } setShowModal(true); }} disabled={!selectedChapter} className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 disabled:opacity-50 mt-5">
              Create Exam
            </button>
          </div>
        </div>

        {loading ? <div className="p-8 text-center text-gray-500">Loading...</div> : exams.length === 0 ? (
          <div className="p-8 text-center"><p className="text-gray-500">No exams in this chapter.</p></div>
        ) : (
          <div className="divide-y divide-gray-100">
            {exams.map((exam) => (
              <div key={exam.id} className="p-6 hover:bg-gray-50">
                <div className="flex items-center justify-between">
                  <div className="flex-1">
                    <div className="flex items-center gap-2 mb-2">
                      <span className={`px-2 py-0.5 rounded text-xs font-medium ${getStatusColor(exam.status)}`}>{formatStatus(exam.status)}</span>
                    </div>
                    <h4 className="font-semibold text-gray-900">{exam.title}</h4>
                    <div className="flex items-center gap-4 mt-2 text-sm text-gray-500">
                      <span>{exam.totalMarks} marks</span>
                      <span>Pass: {exam.passingScore}</span>
                      <span>{exam.timeLimitSeconds ? `${Math.floor(exam.timeLimitSeconds/60)} min` : 'No limit'}</span>
                      <button onClick={() => { setSelectedExamQuestions(exam.examQuestions || []); setShowQuestionsModal(true); }} className="text-indigo-600 hover:text-indigo-800 font-medium">{exam.questionCount} questions</button>
                    </div>
                  </div>
                  <div className="flex items-center gap-2">
                    {exam.status === 'Published' && (
                      <button onClick={() => setExamToPublish(exam)} className="px-3 py-1.5 text-sm bg-indigo-100 text-indigo-700 hover:bg-indigo-200 rounded-lg">
                        Enroll Students
                      </button>
                    )}
                    <button onClick={() => exam.status === 'Published' ? handlePublish(exam.id, exam.status) : setExamToPublish(exam)} className={`px-3 py-1.5 text-sm rounded-lg ${exam.status === 'Published' ? 'bg-amber-100 text-amber-700 hover:bg-amber-200' : 'bg-emerald-100 text-emerald-700 hover:bg-emerald-200'}`}>
                      {exam.status === 'Published' ? 'Unpublish' : 'Publish'}
                    </button>
                    <button onClick={() => openEdit(exam)} className="p-2 text-gray-600 hover:bg-gray-100 rounded-lg">
                      <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" /></svg>
                    </button>
                    <button onClick={() => handleDelete(exam.id)} className="p-2 text-red-600 hover:bg-red-50 rounded-lg">
                      <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" /></svg>
                    </button>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>

      {showModal && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-2xl p-6 w-full max-w-lg max-h-[90vh] overflow-y-auto">
            <h3 className="text-lg font-semibold text-gray-900 mb-4">{editData ? 'Edit Exam' : 'Create Exam'}</h3>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div><label className="block text-sm font-medium text-gray-700 mb-1">Title</label><input type="text" value={formData.title} onChange={(e) => setFormData({...formData, title: e.target.value})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" required /></div>
              <div className="grid grid-cols-2 gap-4">
                <div><label className="block text-sm font-medium text-gray-700 mb-1">Total Marks</label><input type="number" value={formData.totalMarks} onChange={(e) => setFormData({...formData, totalMarks: parseFloat(e.target.value)})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" min="1" /></div>
                <div><label className="block text-sm font-medium text-gray-700 mb-1">Passing Score</label><input type="number" value={formData.passingScore} onChange={(e) => setFormData({...formData, passingScore: parseFloat(e.target.value)})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" min="1" /></div>
              </div>
              <div className="grid grid-cols-2 gap-4">
                <div><label className="block text-sm font-medium text-gray-700 mb-1">Time Limit (min)</label><input type="number" value={formData.timeLimitSeconds/60} onChange={(e) => setFormData({...formData, timeLimitSeconds: parseInt(e.target.value)*60})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" min="1" /></div>
                <div><label className="block text-sm font-medium text-gray-700 mb-1">Max Attempts</label><input type="number" value={formData.maxAttempts} onChange={(e) => setFormData({...formData, maxAttempts: parseInt(e.target.value)})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" min="1" /></div>
              </div>
              <div className="space-y-2">
                {[
                  { key: 'negativeMarking', label: 'Negative Marking' },
                  { key: 'shuffleQuestions', label: 'Shuffle Questions' },
                  { key: 'shuffleOptions', label: 'Shuffle Options' },
                  { key: 'showAnswersAfter', label: 'Show Answers After Submit' },
                  { key: 'autoReleaseResults', label: 'Auto Release Results' }
                ].map(opt => (
                  <label key={opt.key} className="flex items-center gap-2">
                    <input type="checkbox" checked={formData[opt.key]} onChange={(e) => setFormData({...formData, [opt.key]: e.target.checked})} className="w-4 h-4" />
                    <span className="text-sm text-gray-700">{opt.label}</span>
                  </label>
                ))}
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Questions ({(formData.questionIds || []).length} selected)</label>
                <div className="max-h-48 overflow-y-auto border border-gray-200 rounded-xl p-2 space-y-1">
                  {(availableQuestions || []).length === 0 ? <p className="text-sm text-gray-500 p-2">No questions available in this chapter.</p> : (availableQuestions || []).map(q => (
                    <label key={q.id} className="flex items-center gap-2 p-2 hover:bg-gray-50 rounded-lg cursor-pointer">
                      <input type="checkbox" checked={(formData.questionIds || []).includes(q.id)} onChange={(e) => {
                        const currentIds = formData.questionIds || []
                        const ids = e.target.checked ? [...currentIds, q.id] : currentIds.filter(id => id !== q.id)
                        setFormData({...formData, questionIds: ids})
                      }} className="w-4 h-4" />
                      <span className="text-sm text-gray-700 flex-1 truncate">{q.stemText || '(No stem text)'}</span>
                      <span className="text-xs text-gray-400">{q.difficulty}</span>
                    </label>
                  ))}
                </div>
              </div>
              <div className="flex justify-end gap-3 pt-4">
                <button type="button" onClick={() => setShowModal(false)} className="px-4 py-2 text-gray-700 hover:bg-gray-100 rounded-xl">Cancel</button>
                <button type="submit" className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700">{editData ? 'Update' : 'Create'}</button>
              </div>
            </form>
          </div>
        </div>
      )}

      {showQuestionsModal && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-2xl p-6 w-full max-w-lg max-h-[80vh] overflow-y-auto">
            <div className="flex justify-between items-center mb-4">
              <h3 className="text-lg font-semibold text-gray-900">Exam Questions ({selectedExamQuestions.length})</h3>
              <button onClick={() => setShowQuestionsModal(false)} className="text-gray-400 hover:text-gray-600">
                <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" /></svg>
              </button>
            </div>
            <div className="space-y-3">
              {selectedExamQuestions.length === 0 ? <p className="text-gray-500 text-center py-4">No questions in this exam.</p> : selectedExamQuestions.map((q, idx) => (
                <div key={q.questionId || idx} className="p-3 border border-gray-200 rounded-lg">
                  <div className="flex items-center gap-2 mb-2">
                    <span className="text-sm font-medium text-gray-500">Q{idx + 1}</span>
                    <span className="text-xs px-2 py-0.5 bg-gray-100 text-gray-600 rounded">{q.difficulty}</span>
                  </div>
                  <p className="text-sm text-gray-700">{q.stemText || '(No stem text)'}</p>
                </div>
              ))}
            </div>
            <div className="mt-4 flex justify-end">
              <button onClick={() => setShowQuestionsModal(false)} className="px-4 py-2 bg-gray-100 text-gray-700 rounded-xl hover:bg-gray-200">Close</button>
            </div>
          </div>
        </div>
      )}

      {examToPublish && (
        <ExamEnrolmentModal
          examId={examToPublish.id}
          examTitle={examToPublish.title}
          onClose={() => setExamToPublish(null)}
          onSuccess={async (count, action) => {
            if (action === ' unenrolled') {
              setNotification({ type: 'success', message: `Student${action}` })
              setTimeout(() => setNotification(null), 3000)
              return
            }
            if (examToPublish.status !== 'Published') {
              try {
                await publishExam(examToPublish.id)
                setNotification({ type: 'success', message: count > 0 ? `Exam published with ${count} enrolled student(s)` : 'Exam published successfully' })
              } catch (err) {
                setNotification({ type: 'error', message: 'Students enrolled but failed to publish exam' })
              }
            } else {
              setNotification({ type: 'success', message: count > 0 ? `${count} student(s) enrolled` : 'Enrollment updated' })
            }
            setTimeout(() => setNotification(null), 3000)
            loadExams(selectedChapter)
          }}
        />
      )}
    </Layout>
  )
}