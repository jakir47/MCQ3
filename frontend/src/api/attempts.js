import api from './axiosInstance'

export const getMyChapters = () => api.get('/v1/attempts/my-chapters')
export const startExam = (examId) => api.post(`/v1/attempts/exams/${examId}/start`)
export const getAttempt = (id) => api.get(`/v1/attempts/${id}`)
export const saveAttempt = (id, data) => api.patch(`/v1/attempts/${id}/save`, data)
export const submitAttempt = (id, data) => api.post(`/v1/attempts/${id}/submit`, data)
export const getResult = (id) => api.get(`/v1/attempts/${id}/result`)
export const getReview = (id) => api.get(`/v1/attempts/${id}/review`)
export const releaseResult = (id) => api.patch(`/v1/attempts/${id}/release`)

export const getEnrolments = (chapterId) => api.get(`/v1/enrolments/chapter/${chapterId}`)
export const enrolStudent = (chapterId, data) => api.post(`/v1/enrolments/chapter/${chapterId}/enrol`, data)
export const registerStudent = (chapterId, data) => api.post(`/v1/enrolments/chapter/${chapterId}/register`, data)
export const bulkEnrol = (chapterId, formData) => api.post(`/v1/enrolments/chapter/${chapterId}/bulk`, formData, {
  headers: { 'Content-Type': 'multipart/form-data' }
})
export const removeEnrolment = (chapterId, studentId) => api.delete(`/v1/enrolments/chapter/${chapterId}/students/${studentId}`)
export const reEnrolStudent = (chapterId, studentId) => api.post(`/v1/enrolments/chapter/${chapterId}/students/${studentId}/reenrol`)