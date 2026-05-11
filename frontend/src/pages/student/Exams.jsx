import { useState, useEffect } from 'react'
import { Link } from 'react-router-dom'
import api from '../../api/axiosInstance'
import Layout from '../../components/Layout'

export default function StudentExams() {
  const [exams, setExams] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    fetchExams()
  }, [])

  const fetchExams = async () => {
    try {
      const res = await api.get('/v1/exams/published')
      if (res.data.success) {
        setExams(res.data.data)
      }
    } catch (err) {
      setError('Failed to load exams')
    } finally {
      setLoading(false)
    }
  }

  const getStatusBadge = (exam) => {
    if (exam.hasInProgressAttempt) {
      return <span className="px-3 py-1 rounded-full text-xs font-medium bg-yellow-50 text-yellow-600">In Progress</span>
    }
    if (exam.mySubmittedAttempts > 0) {
      if (exam.isPassed) {
        return <span className="px-3 py-1 rounded-full text-xs font-medium bg-green-50 text-green-600">Passed</span>
      }
      return <span className="px-3 py-1 rounded-full text-xs font-medium bg-red-50 text-red-600">Not Passed</span>
    }
    return <span className="px-3 py-1 rounded-full text-xs font-medium bg-green-50 text-green-600">Available</span>
  }

  const getButtonText = (exam) => {
    if (exam.hasInProgressAttempt) return 'Continue'
    if (exam.mySubmittedAttempts >= (exam.maxAttempts || 999)) return 'No Attempts Left'
    return 'Start Exam'
  }

  const canTakeExam = (exam) => {
    return !exam.hasInProgressAttempt && (exam.mySubmittedAttempts < (exam.maxAttempts || 999))
  }

  if (loading) {
    return (
      <Layout title="Available Exams">
        <div className="flex items-center justify-center h-64">
          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-indigo-600"></div>
        </div>
      </Layout>
    )
  }

  return (
    <Layout title="Available Exams">
      {error && (
        <div className="mb-4 p-4 bg-red-50 text-red-600 rounded-xl">{error}</div>
      )}

      {exams.length === 0 ? (
        <div className="text-center py-12">
          <div className="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-4">
            <svg className="w-8 h-8 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
            </svg>
          </div>
          <p className="text-gray-500">No exams available yet</p>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {exams.map((exam) => (
            <div key={exam.id} className="bg-white rounded-2xl border border-gray-200 p-6 hover:shadow-lg transition-shadow">
              <div className="flex items-start justify-between mb-4">
                <div>
                  <h3 className="text-lg font-semibold text-gray-900 mb-1">{exam.title}</h3>
                  <p className="text-sm text-gray-500">{exam.subjectName}</p>
                </div>
                {getStatusBadge(exam)}
              </div>
              
              <div className="grid grid-cols-2 gap-4 mb-4 text-sm">
                <div>
                  <p className="text-gray-500">Duration</p>
                  <p className="font-medium text-gray-900">{exam.timeLimitSeconds ? Math.floor(exam.timeLimitSeconds / 60) : 'No limit'} min</p>
                </div>
                <div>
                  <p className="text-gray-500">Questions</p>
                  <p className="font-medium text-gray-900">{exam.questionCount || 0}</p>
                </div>
                <div>
                  <p className="text-gray-500">Attempts</p>
                  <p className="font-medium text-gray-900">{exam.mySubmittedAttempts} / {exam.maxAttempts || '∞'}</p>
                </div>
                <div>
                  <p className="text-gray-500">Best Score</p>
                  <p className="font-medium text-gray-900">{exam.bestScore !== null ? `${exam.bestScore}%` : '-'}</p>
                </div>
              </div>

              {exam.mySubmittedAttempts > 0 && exam.isResultsReleased && (
                <div className="mb-4 p-3 bg-gray-50 rounded-lg text-sm">
                  <p className="text-gray-600">
                    {exam.isPassed ? ' You passed this exam!' : 'You did not pass. Try again!'}
                  </p>
                </div>
              )}

              {canTakeExam(exam) ? (
                <Link
                  to={`/student/exam/${exam.id}`}
                  className="block w-full py-2.5 px-4 bg-indigo-600 text-white text-center rounded-xl font-medium hover:bg-indigo-700 transition-colors"
                >
                  {getButtonText(exam)}
                </Link>
              ) : exam.hasInProgressAttempt ? (
                <Link
                  to={`/student/exam/${exam.id}`}
                  className="block w-full py-2.5 px-4 bg-yellow-500 text-white text-center rounded-xl font-medium hover:bg-yellow-600 transition-colors"
                >
                  Continue Exam
                </Link>
              ) : (
                <button
                  disabled
                  className="block w-full py-2.5 px-4 bg-gray-300 text-gray-500 text-center rounded-xl font-medium cursor-not-allowed"
                >
                  No Attempts Left
                </button>
              )}
            </div>
          ))}
        </div>
      )}
    </Layout>
  )
}