import { useState, useEffect } from 'react'
import { getAvailableStudentsForExam, getEnrolledStudentsForExam, enrolStudentsInExam } from '../api/attempts'

export default function ExamEnrolmentModal({ examId, examTitle, onClose, onSuccess }) {
  const [availableStudents, setAvailableStudents] = useState([])
  const [enrolledStudents, setEnrolledStudents] = useState([])
  const [selectedStudents, setSelectedStudents] = useState([])
  const [loading, setLoading] = useState(true)
  const [submitting, setSubmitting] = useState(false)
  const [error, setError] = useState('')

  useEffect(() => {
    loadData()
  }, [examId])

  const loadData = async () => {
    try {
      const [availableRes, enrolledRes] = await Promise.all([
        getAvailableStudentsForExam(examId),
        getEnrolledStudentsForExam(examId)
      ])
      if (availableRes.data.success) setAvailableStudents(availableRes.data.data)
      if (enrolledRes.data.success) setEnrolledStudents(enrolledRes.data.data)
    } catch (err) {
      setError('Failed to load students')
    } finally {
      setLoading(false)
    }
  }

  const handleToggle = (studentId) => {
    setSelectedStudents(prev => 
      prev.includes(studentId) 
        ? prev.filter(id => id !== studentId)
        : [...prev, studentId]
    )
  }

  const handleSelectAll = () => {
    if (selectedStudents.length === availableStudents.length) {
      setSelectedStudents([])
    } else {
      setSelectedStudents(availableStudents.map(s => s.studentId))
    }
  }

  const handleSubmit = async () => {
    if (selectedStudents.length === 0) {
      setError('Please select at least one student')
      return
    }
    setSubmitting(true)
    setError('')
    try {
      const { data } = await enrolStudentsInExam(examId, { studentIds: selectedStudents })
      if (data.success) {
        onSuccess(data.data)
        onClose()
      }
    } catch (err) {
      setError(err.response?.data?.error?.message || 'Failed to enroll students')
    } finally {
      setSubmitting(false)
    }
  }

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-2xl w-full max-w-2xl max-h-[80vh] overflow-hidden">
        <div className="p-6 border-b border-gray-100">
          <h3 className="text-lg font-semibold text-gray-900">Enroll Students in Exam</h3>
          <p className="text-sm text-gray-500 mt-1">{examTitle}</p>
        </div>

        {loading ? (
          <div className="p-8 text-center text-gray-500">Loading...</div>
        ) : (
          <div className="p-6 overflow-y-auto max-h-[50vh]">
            {enrolledStudents.length > 0 && (
              <div className="mb-6">
                <h4 className="text-sm font-medium text-gray-700 mb-3">Already Enrolled ({enrolledStudents.length})</h4>
                <div className="flex flex-wrap gap-2">
                  {enrolledStudents.map(e => (
                    <span key={e.id} className="px-3 py-1 bg-green-100 text-green-700 rounded-full text-sm">
                      {e.studentName}
                    </span>
                  ))}
                </div>
              </div>
            )}

            <div>
              <div className="flex items-center justify-between mb-3">
                <h4 className="text-sm font-medium text-gray-700">
                  Available Students ({availableStudents.length})
                </h4>
                {availableStudents.length > 0 && (
                  <button 
                    onClick={handleSelectAll}
                    className="text-sm text-indigo-600 hover:text-indigo-700"
                  >
                    {selectedStudents.length === availableStudents.length ? 'Deselect All' : 'Select All'}
                  </button>
                )}
              </div>

              {error && (
                <div className="mb-4 p-3 bg-red-50 text-red-600 rounded-lg text-sm">{error}</div>
              )}

              {availableStudents.length === 0 ? (
                <p className="text-gray-500 text-center py-4">
                  All students from this exam's chapter are already enrolled, or no students assigned to the chapter yet.
                </p>
              ) : (
                <div className="space-y-2 max-h-64 overflow-y-auto">
                  {availableStudents.map((student) => (
                    <label
                      key={student.studentId}
                      className={`flex items-center p-3 border rounded-lg cursor-pointer transition-colors ${
                        selectedStudents.includes(student.studentId)
                          ? 'border-indigo-600 bg-indigo-50'
                          : 'border-gray-200 hover:border-gray-300'
                      }`}
                    >
                      <input
                        type="checkbox"
                        checked={selectedStudents.includes(student.studentId)}
                        onChange={() => handleToggle(student.studentId)}
                        className="w-4 h-4 text-indigo-600 rounded"
                      />
                      <div className="ml-3">
                        <p className="font-medium text-gray-900">{student.studentName}</p>
                        <p className="text-sm text-gray-500">{student.studentEmail}</p>
                      </div>
                    </label>
                  ))}
                </div>
              )}
            </div>
          </div>
        )}

        <div className="p-6 border-t border-gray-100 flex justify-between items-center">
          <p className="text-sm text-gray-500">
            {selectedStudents.length} student{selectedStudents.length !== 1 ? 's' : ''} selected
          </p>
          <div className="flex gap-3">
            <button 
              onClick={onClose}
              className="px-4 py-2 text-gray-700 hover:bg-gray-100 rounded-xl"
            >
              Cancel
            </button>
            <button 
              onClick={handleSubmit}
              disabled={submitting || selectedStudents.length === 0}
              className="px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 disabled:opacity-50"
            >
              {submitting ? 'Enrolling...' : 'Enroll Selected'}
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}