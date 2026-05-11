import api from './axiosInstance'

export const getExams = (chapterId) => api.get(`/v1/exams/chapter/${chapterId}`)
export const getExam = (id) => api.get(`/v1/exams/${id}`)
export const createExam = (chapterId, data) => api.post(`/v1/exams/chapter/${chapterId}`, data)
export const updateExam = (id, data) => api.put(`/v1/exams/${id}`, data)
export const deleteExam = (id) => api.delete(`/v1/exams/${id}`)
export const publishExam = (id) => api.post(`/v1/exams/${id}/publish`)
export const unpublishExam = (id) => api.post(`/v1/exams/${id}/unpublish`)
export const archiveExam = (id) => api.post(`/v1/exams/${id}/archive`)
export const duplicateExam = (id) => api.post(`/v1/exams/${id}/duplicate`)
export const getSubmissions = (id) => api.get(`/v1/exams/${id}/submissions`)
export const releaseResults = (id) => api.post(`/v1/exams/${id}/results/release`)
export const exportResults = (id, format) => api.get(`/v1/exams/${id}/export`, { params: { format }, responseType: 'blob' })