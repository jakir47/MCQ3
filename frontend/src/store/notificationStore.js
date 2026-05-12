import { create } from 'zustand'
import { notificationsApi } from '../api/notifications'

export const useNotificationStore = create((set) => ({
  notifications: [],
  unreadCount: 0,
  loading: false,

  fetchNotifications: async () => {
    set({ loading: true })
    try {
      const res = await notificationsApi.getAll()
      if (res.data.success) {
        set({ notifications: res.data.data })
      }
    } catch (e) {
      console.error('Failed to fetch notifications', e)
    } finally {
      set({ loading: false })
    }
  },

  fetchUnreadCount: async () => {
    try {
      const res = await notificationsApi.getUnreadCount()
      if (res.data.success) {
        set({ unreadCount: res.data.data })
      }
    } catch (e) {
      console.error('Failed to fetch unread count', e)
    }
  },

  markAsRead: async (id) => {
    try {
      await notificationsApi.markAsRead(id)
      set(state => ({
        notifications: state.notifications.map(n => n.id === id ? { ...n, isRead: true } : n),
        unreadCount: Math.max(0, state.unreadCount - 1)
      }))
    } catch (e) {
      console.error('Failed to mark as read', e)
    }
  },

  markAllAsRead: async () => {
    try {
      await notificationsApi.markAllAsRead()
      set(state => ({
        notifications: state.notifications.map(n => ({ ...n, isRead: true })),
        unreadCount: 0
      }))
    } catch (e) {
      console.error('Failed to mark all as read', e)
    }
  }
}))