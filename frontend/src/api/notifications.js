import axios from './axiosInstance'

export const notificationsApi = {
  getAll: () => axios.get('/v1/notifications'),
  getUnreadCount: () => axios.get('/v1/notifications/unread-count'),
  markAsRead: (id) => axios.put(`/v1/notifications/${id}/read`),
  markAllAsRead: () => axios.put('/v1/notifications/read-all'),
  delete: (id) => axios.delete(`/v1/notifications/${id}`)
}