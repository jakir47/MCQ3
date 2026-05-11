import api from './axiosInstance'

export const uploadImage = (file) => {
  const fd = new FormData()
  fd.append('file', file)
  return api.post('/v1/uploads/image', fd, { headers: { 'Content-Type': 'multipart/form-data' } })
}

export const uploadAudio = (file) => {
  const fd = new FormData()
  fd.append('file', file)
  return api.post('/v1/uploads/audio', fd, { headers: { 'Content-Type': 'multipart/form-data' } })
}

export const deleteFile = (path) => api.delete('/v1/uploads', { params: { path } })

export const getExamSummary = (examId) => api.get(`/v1/analytics/exams/${examId}/summary`)
export const getQuestionAnalytics = (examId) => api.get(`/v1/analytics/exams/${examId}/questions`)
export const getScoreDistribution = (examId) => api.get(`/v1/analytics/exams/${examId}/distribution`)
export const getChapterStudents = (chapterId) => api.get(`/v1/analytics/chapters/${chapterId}/students`)
export const getChapterTrends = (chapterId) => api.get(`/v1/analytics/chapters/${chapterId}/trends`)
export const getEnrolmentAnalytics = (chapterId) => api.get(`/v1/analytics/chapters/${chapterId}/enrolments`)