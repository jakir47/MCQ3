import { useState, useEffect } from 'react'
import { useParams, Link } from 'react-router-dom'
import { getReview } from '../../api/attempts'
import Layout from '../../components/Layout'

export default function ExamReview() {
  const { id } = useParams()
  const [review, setReview] = useState(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')
  const [filter, setFilter] = useState('all')

  const fetchReview = async () => {
    try {
      const res = await getReview(id)
      console.log('Review API response:', res.data)
      if (res.data.success) {
        setReview(res.data.data)
        console.log('Review loaded:', res.data.data)
        console.log('QuestionReviews:', res.data.data.QuestionReviews)
        console.log('CorrectCount:', res.data.data.CorrectCount)
        console.log('IncorrectCount:', res.data.data.IncorrectCount)
        console.log('SkippedCount:', res.data.data.SkippedCount)
        console.log('TotalQuestions:', res.data.data.TotalQuestions)
      }
    } catch (err) {
      setError('Failed to load review')
      console.error('Error:', err)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    fetchReview()
  }, [id])

  const getFilteredQuestions = () => {
    if (!review) return []
    const questions = review.QuestionReviews || review.questionReviews || []
    if (filter === 'all') return questions
    if (filter === 'correct') return questions.filter(q => q.IsCorrect)
    if (filter === 'incorrect') return questions.filter(q => !q.IsCorrect && q.SelectedOptionId)
    if (filter === 'skipped') return questions.filter(q => !q.SelectedOptionId)
    return questions
  }

  if (loading) {
    return (
      <Layout title="Exam Review">
        <div className="flex items-center justify-center h-64">
          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-indigo-600"></div>
        </div>
      </Layout>
    )
  }

  if (error || !review) {
    return (
      <Layout title="Exam Review">
        <div className="text-center py-12">
          <p className="text-red-600 mb-4">{error || 'Review not found'}</p>
          <Link to="/student/results" className="text-indigo-600 hover:text-indigo-800">
            Back to Results
          </Link>
        </div>
      </Layout>
    )
  }

  const filteredQuestions = getFilteredQuestions()
  console.log('Filtered questions:', filteredQuestions)

  const examTitle = review.ExamTitle || review.examTitle
  const subjectName = review.SubjectName || review.subjectName
  const isPassed = review.IsPassed ?? review.isPassed
  const score = review.Score ?? review.score
  const correctCount = review.CorrectCount ?? review.correctCount
  const incorrectCount = review.IncorrectCount ?? review.incorrectCount
  const skippedCount = review.SkippedCount ?? review.skippedCount
  const totalQuestions = review.TotalQuestions ?? review.totalQuestions

  return (
    <Layout title={`Review: ${examTitle}`}>
      <div className="mb-6">
        <Link to="/student/results" className="text-indigo-600 hover:text-indigo-800 text-sm">
          ← Back to Results
        </Link>
      </div>

      <div className="bg-white rounded-2xl border border-gray-200 p-6 mb-6">
        <div className="flex items-center justify-between mb-4">
          <div>
            <h2 className="text-xl font-bold text-gray-900">{examTitle}</h2>
            <p className="text-gray-500">{subjectName}</p>
          </div>
          <div className={`px-4 py-2 rounded-full font-bold ${
            isPassed ? 'bg-green-100 text-green-700' : 'bg-red-100 text-red-700'
          }`}>
            {isPassed ? 'PASSED' : 'FAILED'}
          </div>
        </div>

        <div className="grid grid-cols-2 md:grid-cols-5 gap-4 text-center">
          <div className="bg-gray-50 rounded-lg p-3">
            <p className="text-2xl font-bold text-gray-900">{score}%</p>
            <p className="text-sm text-gray-500">Score</p>
          </div>
          <div className="bg-green-50 rounded-lg p-3">
            <p className="text-2xl font-bold text-green-600">{correctCount}</p>
            <p className="text-sm text-gray-500">Correct</p>
          </div>
          <div className="bg-red-50 rounded-lg p-3">
            <p className="text-2xl font-bold text-red-600">{incorrectCount}</p>
            <p className="text-sm text-gray-500">Incorrect</p>
          </div>
          <div className="bg-yellow-50 rounded-lg p-3">
            <p className="text-2xl font-bold text-yellow-600">{skippedCount}</p>
            <p className="text-sm text-gray-500">Skipped</p>
          </div>
          <div className="bg-blue-50 rounded-lg p-3">
            <p className="text-2xl font-bold text-blue-600">{totalQuestions}</p>
            <p className="text-sm text-gray-500">Total</p>
          </div>
        </div>
      </div>

      <div className="mb-4 flex gap-2">
        {[
          { key: 'all', label: 'All' },
          { key: 'correct', label: 'Correct' },
          { key: 'incorrect', label: 'Incorrect' },
          { key: 'skipped', label: 'Skipped' }
        ].map(f => (
          <button
            key={f.key}
            onClick={() => setFilter(f.key)}
            className={`px-4 py-2 rounded-lg text-sm font-medium transition-colors ${
              filter === f.key
                ? 'bg-indigo-600 text-white'
                : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
            }`}
          >
            {f.label}
          </button>
        ))}
      </div>

      <div className="space-y-6">
        {filteredQuestions.map((question, index) => {
          const questionId = question.QuestionId || question.questionId
          const questionText = question.QuestionText || question.questionText
          const isCorrect = question.IsCorrect ?? question.isCorrect
          const selectedOptionId = question.SelectedOptionId ?? question.selectedOptionId
          const marksAwarded = question.MarksAwarded ?? question.marksAwarded
          const options = question.Options || question.options
          const explanation = question.Explanation || question.explanation

          return (
          <div key={questionId} className="bg-white rounded-2xl border border-gray-200 p-6">
            <div className="flex items-start gap-3 mb-4">
              <span className="flex-shrink-0 w-8 h-8 bg-gray-100 rounded-full flex items-center justify-center text-sm font-medium text-gray-600">
                {index + 1}
              </span>
              <div className="flex-1">
                <p className="text-gray-900 font-medium">{questionText}</p>
                <div className="mt-2 flex gap-2">
                  {isCorrect && (
                    <span className="px-2 py-1 bg-green-100 text-green-700 text-xs rounded-full">Correct</span>
                  )}
                  {!isCorrect && selectedOptionId && (
                    <span className="px-2 py-1 bg-red-100 text-red-700 text-xs rounded-full">Incorrect</span>
                  )}
                  {!selectedOptionId && (
                    <span className="px-2 py-1 bg-yellow-100 text-yellow-700 text-xs rounded-full">Skipped</span>
                  )}
                  <span className="px-2 py-1 bg-gray-100 text-gray-600 text-xs rounded-full">
                    {marksAwarded} marks
                  </span>
                </div>
              </div>
            </div>

            <div className="ml-11 space-y-2">
              {options?.map(option => {
                const optionId = option.Id || option.id
                const optionText = option.OptionText || option.optionText
                const isOptionCorrect = option.IsCorrect ?? option.isCorrect
                const isSelected = option.IsSelected ?? option.isSelected

                return (
                <div
                  key={optionId}
                  className={`p-3 rounded-lg border-2 ${
                    isOptionCorrect
                      ? 'border-green-500 bg-green-50'
                      : isSelected && !isOptionCorrect
                        ? 'border-red-500 bg-red-50'
                        : 'border-gray-200'
                  }`}
                >
                  <div className="flex items-center gap-2">
                    {isSelected && (
                      <svg className="w-5 h-5 text-indigo-600" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clipRule="evenodd" />
                      </svg>
                    )}
                    {isOptionCorrect && !isSelected && (
                      <svg className="w-5 h-5 text-green-600" fill="currentColor" viewBox="0 0 20 20">
                        <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clipRule="evenodd" />
                      </svg>
                    )}
                    <span className={`${
                      isOptionCorrect ? 'text-green-700 font-medium' : 'text-gray-700'
                    }`}>
                      {optionText}
                    </span>
                  </div>
                  {isOptionCorrect && (
                    <p className="text-xs text-green-600 mt-1 ml-7">Correct Answer</p>
                  )}
                  {isSelected && !isOptionCorrect && (
                    <p className="text-xs text-red-600 mt-1 ml-7">Your Answer</p>
                  )}
                </div>
              )})}
            </div>

            {explanation && (
              <div className="mt-4 ml-11 p-4 bg-blue-50 rounded-lg">
                <p className="text-sm font-medium text-blue-700 mb-1">Explanation</p>
                <p className="text-sm text-blue-600">{explanation}</p>
              </div>
            )}
          </div>
        )})}
      </div>

      {filteredQuestions.length === 0 && (
        <div className="text-center py-12 text-gray-500">
          No questions in this category
        </div>
      )}
    </Layout>
  )
}