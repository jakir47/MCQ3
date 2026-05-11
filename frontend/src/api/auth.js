import api from './axiosInstance'

export const login = (data) => api.post('/v1/auth/login', data)
export const logout = () => api.post('/v1/auth/logout')
export const refresh = (data) => api.post('/v1/auth/refresh', data)
export const forgotPassword = (data) => api.post('/v1/auth/forgot-password', data)
export const resetPassword = (data) => api.post('/v1/auth/reset-password', data)
export const verifyEmail = (token) => api.post(`/v1/auth/verify-email?token=${token}`)
export const resendVerification = () => api.post('/v1/auth/resend-verification')
export const changePassword = (data) => api.post('/v1/auth/change-password', data)