import { Navigate, Outlet } from 'react-router-dom'
import { useAuthStore } from '../store/authStore'

export default function ProtectedRoute({ roles }) {
  const { user, accessToken } = useAuthStore()
  
  console.log('ProtectedRoute check:', { user, accessToken, roles })
  
  if (!accessToken || !user) {
    console.log('No token or user, redirecting to login')
    return <Navigate to="/login" replace />
  }
  if (roles && !roles.includes(user.role)) {
    console.log('Role not allowed:', user.role, 'expected:', roles)
    return <Navigate to="/login" replace />
  }
  if (user.tempPassword && window.location.pathname !== '/change-password')
    return <Navigate to="/change-password" replace />
  return <Outlet />
}