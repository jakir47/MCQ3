import api from './axiosInstance'

export const get = (url, config) => api.get(url, config)
export const post = (url, data, config) => api.post(url, data, config)
export const put = (url, data, config) => api.put(url, data, config)
export const del = (url, config) => api.delete(url, config)

export * from './analytics'
export * from './auth'
export * from './attempts'
export * from './exams'
export * from './questions'
export * from './subjects'
export * from './uploads'
export * from './users'