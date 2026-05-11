import { create } from 'zustand'
import { persist } from 'zustand/middleware'

export const useAuthStore = create(persist(
  (set, get) => ({
    user: null,
    accessToken: null,
    refreshToken: null,

    setAuth: (user, accessToken, refreshToken) => {
      console.log('Setting auth:', { user, accessToken })
      set({ user, accessToken, refreshToken })
    },

    setTokens: (accessToken, refreshToken) => {
      console.log('Setting tokens:', { accessToken })
      set({ accessToken, refreshToken })
    },

    logout: () => {
      console.log('Logging out')
      set({ user: null, accessToken: null, refreshToken: null })
    },
  }),
  { 
    name: 'mcq2-auth',
    partialize: (state) => ({ user: state.user, accessToken: state.accessToken, refreshToken: state.refreshToken })
  }
))