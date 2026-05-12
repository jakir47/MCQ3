import api from './axiosInstance'

export const getUsers = () => api.get('/v1/users')
export const getUser = (id) => api.get(`/v1/users/${id}`)
export const createUser = (data) => api.post('/v1/users', data)
export const updateUser = (id, data) => api.put(`/v1/users/${id}`, data)
export const deleteUser = (id) => api.delete(`/v1/users/${id}`)

export const getTeachers = () => api.get('/v1/users/teachers')
export const createTeacher = (data) => api.post('/v1/users/teachers', data)
export const updateTeacher = (id, data) => api.put(`/v1/users/teachers/${id}`, data)
export const deleteTeacher = (id) => api.delete(`/v1/users/${id}`)

export const getStudents = () => api.get('/v1/users/students')

export const assignStudent = (data) => api.post('/v1/student-assignments/assign', data)
export const removeStudentAssignment = (studentId, chapterId) => api.delete(`/v1/student-assignments/${studentId}/chapter/${chapterId}`)
export const getChapterAssignments = (chapterId) => api.get(`/v1/student-assignments/chapter/${chapterId}`)