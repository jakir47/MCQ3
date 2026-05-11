import api from './axiosInstance'

export const getSubjects = () => api.get('/v1/subjects')
export const getSubject = (id) => api.get(`/v1/subjects/${id}`)
export const createSubject = (data) => api.post('/v1/subjects', data)
export const updateSubject = (id, data) => api.put(`/v1/subjects/${id}`, data)
export const deleteSubject = (id) => api.delete(`/v1/subjects/${id}`)
export const getChapters = (subjectId) => api.get(`/v1/chapters/subject/${subjectId}`)
export const createChapter = (subjectId, data) => api.post(`/v1/chapters/subject/${subjectId}`, data)
export const updateChapter = (id, data) => api.put(`/v1/chapters/${id}`, data)
export const deleteChapter = (id) => api.delete(`/v1/chapters/${id}`)