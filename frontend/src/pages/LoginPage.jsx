import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuthStore } from '../store/authStore'
import { login } from '../api/auth'

export default function LoginPage() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const [loading, setLoading] = useState(false)
  const navigate = useNavigate()
  const setAuth = useAuthStore((s) => s.setAuth)

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    setLoading(true)
    try {
      const { data } = await login({ email, password })
      if (data.success) {
        setAuth(data.data, data.data.accessToken, data.data.refreshToken)
        if (data.data.tempPassword) {
          navigate('/change-password')
        } else {
          navigate(data.data.role === 'Admin' ? '/admin' :
                  data.data.role === 'Teacher' ? '/teacher' : '/student')
        }
      }
    } catch (err) {
      setError(err.response?.data?.error?.message || 'Invalid credentials')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="min-h-screen flex" style={{ background: '#f8f9fa' }}>
      {/* Left — Geographic brand panel */}
      <div className="hidden lg:flex lg:w-1/2 relative flex-col justify-between p-12 overflow-hidden" style={{ background: '#0f172a' }}>
        {/* Topographic contour lines background */}
        <svg className="absolute inset-0 w-full h-full opacity-[0.06]" viewBox="0 0 800 900" preserveAspectRatio="xMidYMid slice" xmlns="http://www.w3.org/2000/svg">
          <path d="M0 200 Q100 150 200 220 T400 180 T600 240 T800 200" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 280 Q120 220 240 300 T480 260 T720 320 T800 280" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 360 Q140 290 280 380 T520 340 T760 400 T800 360" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 440 Q160 360 320 460 T560 420 T800 480 T800 440" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 520 Q180 430 360 540 T600 500 T800 560 T800 520" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 600 Q200 500 400 620 T640 580 T800 640 T800 600" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 680 Q220 570 440 700 T680 660 T800 720 T800 680" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 760 Q240 640 480 780 T720 740 T800 800 T800 760" fill="none" stroke="white" strokeWidth="1.5" />
          <path d="M0 840 Q260 710 520 860 T760 820 T800 880 T800 840" fill="none" stroke="white" strokeWidth="1.5" />
          {/* Grid lines */}
          <line x1="200" y1="0" x2="200" y2="900" stroke="white" strokeWidth="0.5" opacity="0.3" />
          <line x1="400" y1="0" x2="400" y2="900" stroke="white" strokeWidth="0.5" opacity="0.3" />
          <line x1="600" y1="0" x2="600" y2="900" stroke="white" strokeWidth="0.5" opacity="0.3" />
          <line x1="0" y1="100" x2="800" y2="100" stroke="white" strokeWidth="0.5" opacity="0.3" />
          <line x1="0" y1="300" x2="800" y2="300" stroke="white" strokeWidth="0.5" opacity="0.3" />
          <line x1="0" y1="500" x2="800" y2="500" stroke="white" strokeWidth="0.5" opacity="0.3" />
          <line x1="0" y1="700" x2="800" y2="700" stroke="white" strokeWidth="0.5" opacity="0.3" />
          <circle cx="400" cy="450" r="120" fill="none" stroke="white" strokeWidth="0.8" opacity="0.15" />
          <circle cx="400" cy="450" r="200" fill="none" stroke="white" strokeWidth="0.8" opacity="0.1" />
          <circle cx="400" cy="450" r="280" fill="none" stroke="white" strokeWidth="0.8" opacity="0.07" />
        </svg>

        {/* Gradient overlays */}
        <div className="absolute top-0 left-0 w-full h-40" style={{ background: 'linear-gradient(180deg, #0f172a, transparent)' }} />
        <div className="absolute bottom-0 left-0 w-full h-40" style={{ background: 'linear-gradient(0deg, #0f172a, transparent)' }} />

        <div className="relative z-10">
          <div className="flex items-center gap-3">
            <svg className="w-8 h-8" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="16" cy="16" r="15" stroke="#3b82f6" strokeWidth="1.5" />
              <circle cx="16" cy="16" r="3" fill="#3b82f6" />
              <path d="M16 1v8M16 23v8M1 16h8M23 16h8" stroke="#3b82f6" strokeWidth="1.5" />
              <path d="M5 5l5.66 5.66M21.34 21.34L27 27M27 5l-5.66 5.66M10.34 21.34L5 27" stroke="#3b82f6" strokeWidth="1" opacity="0.5" />
            </svg>
            <span className="text-lg font-bold tracking-tight text-white">MCQ3</span>
          </div>
        </div>

        <div className="relative z-10 max-w-md">
          <h1 className="text-3xl font-bold text-white leading-tight tracking-tight">
            Navigate your<br />
            <span style={{ color: '#60a5fa' }}>assessment journey</span>
          </h1>
          <p className="mt-3 text-sm leading-relaxed" style={{ color: 'rgba(255,255,255,0.4)' }}>
            Create, manage, and take exams with precision analytics. Map your students' progress across subjects and chapters.
          </p>

          <div className="mt-10 flex gap-4">
            {[
              { value: '10K+', label: 'Students' },
              { value: '500+', label: 'Exams' },
              { value: '98%', label: 'Uptime' },
            ].map((s, i) => (
              <div key={i} className="flex-1 p-4 rounded-lg" style={{ background: 'rgba(255,255,255,0.03)', border: '1px solid rgba(255,255,255,0.06)' }}>
                <div className="text-lg font-bold text-white">{s.value}</div>
                <div className="text-xs mt-0.5" style={{ color: 'rgba(255,255,255,0.35)' }}>{s.label}</div>
              </div>
            ))}
          </div>
        </div>

        <div className="relative z-10 flex items-center gap-4 text-xs" style={{ color: 'rgba(255,255,255,0.15)' }}>
          <span>&copy; 2026 MCQ3</span>
          <span className="w-1 h-1 rounded-full" style={{ background: 'rgba(255,255,255,0.15)' }} />
          <span>All rights reserved</span>
        </div>
      </div>

      {/* Right — Login form */}
      <div className="w-full lg:w-1/2 flex items-center justify-center p-6 lg:p-10">
        <div className="w-full max-w-sm">
          <div className="lg:hidden flex items-center gap-2.5 mb-8">
            <svg className="w-7 h-7" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="16" cy="16" r="15" stroke="#3b82f6" strokeWidth="1.5" />
              <circle cx="16" cy="16" r="3" fill="#3b82f6" />
              <path d="M16 1v8M16 23v8M1 16h8M23 16h8" stroke="#3b82f6" strokeWidth="1.5" />
            </svg>
            <span className="text-lg font-bold tracking-tight" style={{ color: 'var(--text-primary)' }}>MCQ3</span>
          </div>

          <div className="mb-8">
            <h1 className="text-2xl font-bold tracking-tight" style={{ color: '#0f172a' }}>Sign in</h1>
            <p className="mt-1 text-sm" style={{ color: '#64748b' }}>Enter your credentials to continue</p>
          </div>

          {error && (
            <div className="mb-5 p-3 rounded-lg flex items-center gap-2.5" style={{ background: '#fef2f2', border: '1px solid #fecaca' }}>
              <svg className="w-4 h-4 shrink-0" style={{ color: '#dc2626' }} fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126zM12 15.75h.007v.008H12v-.008z" />
              </svg>
              <span className="text-sm" style={{ color: '#991b1b' }}>{error}</span>
            </div>
          )}

          <form onSubmit={handleSubmit}>
            <div className="space-y-4">
              <div>
                <label className="block text-sm font-medium mb-1.5" style={{ color: '#334155' }}>Email address</label>
                <input type="email" value={email} onChange={e => setEmail(e.target.value)}
                  className="w-full px-3.5 py-2.5 text-sm rounded-lg outline-none transition-all"
                  style={{
                    color: '#0f172a',
                    background: '#ffffff',
                    border: '1px solid #e2e8f0',
                  }}
                  placeholder="you@example.com"
                  required autoFocus
                  onFocus={e => { e.target.style.borderColor = '#3b82f6'; e.target.style.boxShadow = '0 0 0 3px rgba(59,130,246,0.1)' }}
                  onBlur={e => { e.target.style.borderColor = '#e2e8f0'; e.target.style.boxShadow = 'none' }} />
              </div>
              <div>
                <label className="block text-sm font-medium mb-1.5" style={{ color: '#334155' }}>Password</label>
                <input type="password" value={password} onChange={e => setPassword(e.target.value)}
                  className="w-full px-3.5 py-2.5 text-sm rounded-lg outline-none transition-all"
                  style={{
                    color: '#0f172a',
                    background: '#ffffff',
                    border: '1px solid #e2e8f0',
                  }}
                  placeholder="Enter your password"
                  required
                  onFocus={e => { e.target.style.borderColor = '#3b82f6'; e.target.style.boxShadow = '0 0 0 3px rgba(59,130,246,0.1)' }}
                  onBlur={e => { e.target.style.borderColor = '#e2e8f0'; e.target.style.boxShadow = 'none' }} />
              </div>

              <div className="flex items-center justify-between pt-1">
                <label className="flex items-center gap-2 cursor-pointer">
                  <input type="checkbox" defaultChecked className="w-4 h-4 rounded" style={{ accentColor: '#3b82f6' }} />
                  <span className="text-sm" style={{ color: '#64748b' }}>Remember me</span>
                </label>
                <button type="button" className="text-sm font-medium transition-colors" style={{ color: '#2563eb' }}
                  onMouseEnter={e => e.currentTarget.style.color = '#b45309'}
                  onMouseLeave={e => e.currentTarget.style.color = '#2563eb'}
                >Forgot password?</button>
              </div>

              <button type="submit" disabled={loading}
                className="w-full py-2.5 text-sm font-semibold text-white rounded-lg transition-all disabled:opacity-50"
                style={{ background: '#3b82f6' }}
                onMouseEnter={e => { if (!loading) e.currentTarget.style.background = '#2563eb' }}
                onMouseLeave={e => { if (!loading) e.currentTarget.style.background = '#3b82f6' }}
              >
                {loading ? (
                  <span className="flex items-center justify-center gap-2">
                    <svg className="animate-spin w-4 h-4" fill="none" viewBox="0 0 24 24">
                      <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" />
                      <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z" />
                    </svg>
                    Signing in...
                  </span>
                ) : 'Sign in'}
              </button>
            </div>
          </form>

          <div className="mt-8 pt-6" style={{ borderTop: '1px solid #e2e8f0' }}>
            <p className="text-center text-[11px] font-medium uppercase tracking-widest mb-3" style={{ color: '#94a3b8' }}>Demo access</p>
            <div className="grid grid-cols-3 gap-2">
              {[
                { role: 'Admin', email: 'admin@mcq2.com', pass: 'admin123' },
                { role: 'Teacher', email: 'teacher@mcq2.com', pass: 'teacher123' },
                { role: 'Student', email: 'student@mcq2.com', pass: 'student123' },
              ].map(d => (
                <button key={d.role}
                  onClick={() => { setEmail(d.email); setPassword(d.pass) }}
                  className="py-2 rounded-lg text-center transition-all text-xs font-medium"
                  style={{
                    background: '#f1f5f9',
                    border: '1px solid #e2e8f0',
                    color: '#475569',
                  }}
                  onMouseEnter={e => { e.currentTarget.style.background = '#e2e8f0'; e.currentTarget.style.borderColor = '#cbd5e1' }}
                  onMouseLeave={e => { e.currentTarget.style.background = '#f1f5f9'; e.currentTarget.style.borderColor = '#e2e8f0' }}
                >
                  {d.role}
                </button>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}