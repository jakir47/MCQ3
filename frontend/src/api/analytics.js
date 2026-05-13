import api from './axiosInstance'

export const getStats = () => api.get('/v1/analytics/stats')
export const getTeacherStats = () => api.get('/v1/analytics/teacher-stats')
export const getSubjectAnalytics = (subjectId) => api.get(`/v1/analytics/subjects/${subjectId}/analytics`)