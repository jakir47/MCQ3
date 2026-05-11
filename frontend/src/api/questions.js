import api from './axiosInstance'

export const getQuestions = (chapterId) => api.get(`/v1/questions/chapter/${chapterId}`)
export const getQuestion = (id) => api.get(`/v1/questions/${id}`)
export const createQuestion = (chapterId, data) => api.post(`/v1/questions/chapter/${chapterId}`, data)
export const updateQuestion = (id, data) => api.put(`/v1/questions/${id}`, data)
export const deleteQuestion = (id) => api.delete(`/v1/questions/${id}`)
export const searchGlobalBank = (params) => api.get('/v1/bank/global', { params })
export const importQuestions = (chapterId, ids) => api.post(`/v1/questions/chapter/${chapterId}/import`, { questionIds: ids })