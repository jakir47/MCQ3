import api from './axiosInstance'

export const getMyChapters = () => api.get('/v1/attempts/my-chapters')
export const startExam = (examId) => api.post(`/v1/attempts/exams/${examId}/start`)
export const getAttempt = (id) => api.get(`/v1/attempts/${id}`)
export const saveAttempt = (id, data) => api.patch(`/v1/attempts/${id}/save`, data)
export const submitAttempt = (id, data) => api.post(`/v1/attempts/${id}/submit`, data)
export const getResult = (id) => api.get(`/v1/attempts/${id}/result`)
export const getReview = (id) => api.get(`/v1/attempts/${id}/review`)
export const releaseResult = (id) => api.patch(`/v1/attempts/${id}/release`)

export const getAvailableStudentsForExam = (examId) => api.get(`/v1/enrolments/exam/${examId}/available-students`)
export const getEnrolledStudentsForExam = (examId) => api.get(`/v1/enrolments/exam/${examId}/enrolled`)
export const enrolStudentsInExam = (examId, data) => api.post(`/v1/enrolments/exam/${examId}/enrol`, data)
export const unenrolStudentsFromExam = (examId, data) => api.post(`/v1/enrolments/exam/${examId}/unenrol`, data)