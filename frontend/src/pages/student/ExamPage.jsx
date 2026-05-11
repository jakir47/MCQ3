import { useState, useEffect, useRef } from 'react'
import { useParams, useNavigate } from 'react-router-dom'
import { startExam, getAttempt, saveAttempt, submitAttempt } from '../../api/attempts'
import Layout from '../../components/Layout'

export default function ExamPage() {
  const { id } = useParams()
  const navigate = useNavigate()
  const [exam, setExam] = useState(null)
  const [attemptId, setAttemptId] = useState(null)
  const [currentIndex, setCurrentIndex] = useState(0)
  const [answers, setAnswers] = useState({})
  const [loading, setLoading] = useState(true)
  const [remainingSecs, setRemainingSecs] = useState(0)
  const [submitting, setSubmitting] = useState(false)
  const [error, setError] = useState(null)
  const initialized = useRef(false)

  useEffect(() => {
    if (initialized.current) return
    initialized.current = true
    initExam()
  }, [id])

  const initExam = async () => {
    try {
      const { data } = await startExam(id)
      if (data.success && data.data) {
        const attempt = data.data
        setAttemptId(attempt.id)
        setExam(attempt)
        const resumeData = attempt.resumeData ? JSON.parse(attempt.resumeData) : {}
        setAnswers(resumeData.answers || {})
        if (attempt.timeSpentSecs) {
          setRemainingSecs(attempt.timeSpentSecs)
        } else if (attempt.exam?.timeLimitSeconds) {
          setRemainingSecs(attempt.exam.timeLimitSeconds)
        }
      } else if (data.error) {
        setError(data.error.message || 'Cannot start exam')
      }
    } catch (err) {
      setError(err.response?.data?.error?.message || 'Error starting exam')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    if (!exam?.exam?.timeLimitSeconds || remainingSecs <= 0) return
    const timer = setInterval(() => {
      setRemainingSecs(prev => {
        if (prev <= 1) {
          handleSubmit()
          return 0
        }
        return prev - 1
      })
    }, 1000)
    return () => clearInterval(timer)
  }, [exam, attemptId])

  useEffect(() => {
    if (!attemptId || submitting) return
    const interval = setInterval(async () => {
      try {
        await saveAttempt(attemptId, { answers, timeSpentSecs: remainingSecs })
      } catch (err) { console.error('Autosave failed') }
    }, 30000)
    return () => clearInterval(interval)
  }, [attemptId, answers, remainingSecs, submitting])

  const handleSubmit = async () => {
    if (submitting) return
    setSubmitting(true)
    try {
      const timeSpent = exam.exam?.timeLimitSeconds 
        ? exam.exam.timeLimitSeconds - remainingSecs 
        : 0
      
      const payload = {
        Answers: answers,
        TimeSpentSecs: timeSpent
      }
      
      const { data } = await submitAttempt(attemptId, payload)
      if (data.success) {
        navigate(`/student/results`)
      }
    } catch (err) {
      console.error(err)
      alert('Error submitting exam')
      setSubmitting(false)
    }
  }

  const handleAnswer = (questionId, optionId) => {
    setAnswers(prev => ({ ...prev, [questionId]: optionId }))
  }

  const formatTime = (secs) => {
    const m = Math.floor(secs / 60)
    const s = secs % 60
    return `${m.toString().padStart(2, '0')}:${s.toString().padStart(2, '0')}`
  }

  if (loading) return <Layout title="Exam"><div className="p-8 text-center">Loading exam...</div></Layout>
  if (error) return (
    <Layout title="Exam">
      <div className="p-8 text-center">
        <div className="bg-red-50 border border-red-200 rounded-lg p-6 max-w-md mx-auto">
          <div className="text-red-600 text-lg font-medium mb-2">Cannot Start Exam</div>
          <div className="text-red-500">{error}</div>
          <button 
            onClick={() => navigate('/student/exams')}
            className="mt-4 px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700"
          >
            Back to Exams
          </button>
        </div>
      </div>
    </Layout>
  )
  if (!exam) return <Layout title="Exam"><div className="p-8 text-center">Exam not found</div></Layout>

  const questions = exam.exam?.examQuestions || []
  const currentQ = questions[currentIndex]

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-white border-b px-6 py-4 flex justify-between items-center sticky top-0">
        <div>
          <h1 className="font-semibold text-gray-900">{exam.examTitle}</h1>
          <p className="text-sm text-gray-500">Question {currentIndex + 1} of {questions.length}</p>
        </div>
        <div className={`text-xl font-mono font-bold ${remainingSecs < 60 ? 'text-red-600' : 'text-gray-700'}`}>
          {formatTime(remainingSecs)}
        </div>
      </div>

      <div className="p-6 max-w-3xl mx-auto">
        <div className="bg-white rounded-2xl shadow-sm border border-gray-200 p-6">
          <div className="mb-6">
            <div className="text-xs font-medium text-gray-500 mb-2">Question</div>
            <div className="text-lg text-gray-900">{currentQ?.stemText || 'No question text'}</div>
          </div>

          <div className="space-y-3">
            {currentQ?.options?.map((opt) => (
              <label 
                key={opt.id} 
                className={`flex items-center p-4 border-2 rounded-xl cursor-pointer transition-all ${
                  answers[currentQ.id] === opt.id 
                    ? 'border-indigo-600 bg-indigo-50' 
                    : 'border-gray-200 hover:border-gray-300'
                }`}
              >
                <input
                  type="radio"
                  name="answer"
                  checked={answers[currentQ.id] === opt.id}
                  onChange={() => handleAnswer(currentQ.id, opt.id)}
                  className="w-5 h-5 text-indigo-600"
                />
                <span className="ml-3 text-gray-700">{opt.optionText}</span>
              </label>
            ))}
          </div>

          <div className="mt-8 flex justify-between items-center">
            <button
              onClick={() => setCurrentIndex(Math.max(0, currentIndex - 1))}
              disabled={currentIndex === 0}
              className="px-6 py-3 border-2 border-gray-200 rounded-xl font-medium disabled:opacity-50 hover:border-gray-300 transition-colors"
            >
              Previous
            </button>
            
            {currentIndex < questions.length - 1 ? (
              <button
                onClick={() => setCurrentIndex(currentIndex + 1)}
                className="px-6 py-3 bg-indigo-600 text-white rounded-xl font-medium hover:bg-indigo-700 transition-colors"
              >
                Next
              </button>
            ) : (
              <button
                onClick={handleSubmit}
                disabled={submitting}
                className="px-6 py-3 bg-green-600 text-white rounded-xl font-medium hover:bg-green-700 transition-colors disabled:opacity-50"
              >
                {submitting ? 'Submitting...' : 'Submit Exam'}
              </button>
            )}
          </div>

          <div className="mt-6 flex justify-center gap-2">
            {questions.map((_, idx) => (
              <button
                key={idx}
                onClick={() => setCurrentIndex(idx)}
                className={`w-3 h-3 rounded-full transition-colors ${
                  idx === currentIndex ? 'bg-indigo-600' : answers[questions[idx]?.id] ? 'bg-green-500' : 'bg-gray-300'
                }`}
              />
            ))}
          </div>
        </div>
      </div>
    </div>
  )
}