import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { getSubjects, getChapters } from '../../api/subjects'
import { getQuestions, createQuestion, updateQuestion, deleteQuestion } from '../../api/questions'

export default function TeacherQuestionsPage() {
  const [subjects, setSubjects] = useState([])
  const [chapters, setChapters] = useState([])
  const [questions, setQuestions] = useState([])
  const [loading, setLoading] = useState(true)
  const [selectedSubject, setSelectedSubject] = useState('')
  const [selectedChapter, setSelectedChapter] = useState('')
  const [showModal, setShowModal] = useState(false)
  const [editData, setEditData] = useState(null)
  const [formData, setFormData] = useState({
    stemText: '',
    explanation: '',
    positiveMarks: 1,
    negativeMarks: 0.25,
    difficulty: 'Easy',
    options: [
      { optionText: '', isCorrect: true },
      { optionText: '', isCorrect: false },
      { optionText: '', isCorrect: false },
      { optionText: '', isCorrect: false }
    ]
  })

  useEffect(() => {
    loadSubjects()
  }, [])

  useEffect(() => {
    if (selectedSubject) {
      loadChapters(selectedSubject)
    }
  }, [selectedSubject])

  useEffect(() => {
    if (selectedChapter) {
      loadQuestions(selectedChapter)
    }
  }, [selectedChapter])

  const loadSubjects = async () => {
    try {
      const { data } = await getSubjects()
      if (data.success) {
        setSubjects(data.data)
        if (data.data.length > 0) setSelectedSubject(data.data[0].id)
      }
    } catch (err) { console.error(err) }
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

  const loadQuestions = async (chapterId) => {
    setLoading(true)
    try {
      const { data } = await getQuestions(chapterId)
      if (data.success) setQuestions(data.data)
    } catch (err) { console.error(err) }
    finally { setLoading(false) }
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    try {
      const payload = {
        stemText: formData.stemText,
        explanation: formData.explanation,
        positiveMarks: formData.positiveMarks,
        negativeMarks: formData.negativeMarks,
        difficulty: formData.difficulty,
        tags: '[]',
        options: formData.options.map((o, i) => ({ ...o, orderIndex: i }))
      }
      if (editData) {
        await updateQuestion(editData.id, payload)
      } else {
        await createQuestion(selectedChapter, payload)
      }
      setShowModal(false)
      setEditData(null)
      resetForm()
      loadQuestions(selectedChapter)
    } catch (err) { console.error(err) }
  }

  const handleDelete = async (id) => {
    if (!confirm('Delete this question?')) return
    try {
      await deleteQuestion(id)
      loadQuestions(selectedChapter)
    } catch (err) { console.error(err) }
  }

  const resetForm = () => {
    setFormData({
      stemText: '',
      explanation: '',
      positiveMarks: 1,
      negativeMarks: 0.25,
      difficulty: 'Easy',
      options: [
        { optionText: '', isCorrect: true },
        { optionText: '', isCorrect: false },
        { optionText: '', isCorrect: false },
        { optionText: '', isCorrect: false }
      ]
    })
  }

  const openEdit = (q) => {
    setEditData(q)
    setFormData({
      stemText: q.stemText || '',
      explanation: q.explanation || '',
      positiveMarks: q.positiveMarks,
      negativeMarks: q.negativeMarks,
      difficulty: q.difficulty || 'Easy',
      options: q.options?.map(o => ({ optionText: o.optionText || '', isCorrect: o.isCorrect })) || []
    })
    setShowModal(true)
  }

  const getDifficultyColor = (d) => {
    switch (d) {
      case 'Easy': return 'bg-emerald-100 text-emerald-700'
      case 'Medium': return 'bg-amber-100 text-amber-700'
      case 'Hard': return 'bg-red-100 text-red-700'
      default: return 'bg-gray-100 text-gray-700'
    }
  }

  return (
    <Layout title="Questions">
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
            <button onClick={() => { if (!selectedChapter) return; setEditData(null); resetForm(); setShowModal(true); }} disabled={!selectedChapter} className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 disabled:opacity-50 mt-5">
              Add Question
            </button>
          </div>
        </div>

        {loading ? (
          <div className="p-8 text-center text-gray-500">Loading...</div>
        ) : questions.length === 0 ? (
          <div className="p-8 text-center"><p className="text-gray-500">No questions in this chapter.</p></div>
        ) : (
          <div className="divide-y divide-gray-100">
            {questions.map((q) => (
              <div key={q.id} className="p-6 hover:bg-gray-50">
                <div className="flex items-start justify-between">
                  <div className="flex-1">
                    <div className="flex items-center gap-2 mb-2">
                      {q.difficulty && <span className={`px-2 py-0.5 rounded text-xs font-medium ${getDifficultyColor(q.difficulty)}`}>{q.difficulty}</span>}
                      <span className="text-xs text-gray-500">{q.positiveMarks} marks</span>
                    </div>
                    <p className="text-gray-900 font-medium">{q.stemText}</p>
                    <div className="mt-3 grid grid-cols-2 gap-2">
                      {q.options?.map((o, i) => (
                        <div key={i} className={`p-2 rounded-lg text-sm ${o.isCorrect ? 'bg-emerald-50 border border-emerald-200' : 'bg-gray-50'}`}>
                          <span className="text-gray-700">{o.optionText}</span>
                          {o.isCorrect && <span className="ml-2 text-emerald-600 text-xs">✓</span>}
                        </div>
                      ))}
                    </div>
                  </div>
                  <div className="flex items-center gap-2">
                    <button onClick={() => openEdit(q)} className="p-2 text-gray-600 hover:bg-gray-100 rounded-lg">
                      <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z" /></svg>
                    </button>
                    <button onClick={() => handleDelete(q.id)} className="p-2 text-red-600 hover:bg-red-50 rounded-lg">
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
          <div className="bg-white rounded-2xl p-6 w-full max-w-2xl max-h-[90vh] overflow-y-auto">
            <h3 className="text-lg font-semibold text-gray-900 mb-4">{editData ? 'Edit Question' : 'Add Question'}</h3>
            <form onSubmit={handleSubmit} className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Question Text</label>
                <textarea value={formData.stemText} onChange={(e) => setFormData({...formData, stemText: e.target.value})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" rows={2} required />
              </div>
              <div className="grid grid-cols-3 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Positive Marks</label>
                  <input type="number" value={formData.positiveMarks} onChange={(e) => setFormData({...formData, positiveMarks: parseFloat(e.target.value)})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" min="0" step="0.5" />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Negative Marks</label>
                  <input type="number" value={formData.negativeMarks} onChange={(e) => setFormData({...formData, negativeMarks: parseFloat(e.target.value)})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" min="0" step="0.25" />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Difficulty</label>
                  <select value={formData.difficulty} onChange={(e) => setFormData({...formData, difficulty: e.target.value})} className="w-full px-4 py-2 border border-gray-200 rounded-xl">
                    <option>Easy</option><option>Medium</option><option>Hard</option>
                  </select>
                </div>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Answer Options (check correct)</label>
                {formData.options.map((opt, i) => (
                  <div key={i} className="flex items-center gap-2 mb-2">
                    <input type="checkbox" checked={opt.isCorrect} onChange={(e) => { const opts = [...formData.options]; opts[i].isCorrect = e.target.checked; setFormData({...formData, options: opts}); }} className="w-4 h-4" />
                    <input type="text" value={opt.optionText} onChange={(e) => { const opts = [...formData.options]; opts[i].optionText = e.target.value; setFormData({...formData, options: opts}); }} placeholder={`Option ${i+1}`} className="flex-1 px-3 py-2 border border-gray-200 rounded-xl" required />
                  </div>
                ))}
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Explanation (optional)</label>
                <textarea value={formData.explanation} onChange={(e) => setFormData({...formData, explanation: e.target.value})} className="w-full px-4 py-2 border border-gray-200 rounded-xl" rows={2} />
              </div>
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