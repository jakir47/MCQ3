import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { useAuthStore } from '../../store/authStore'
import { get, getTeacherStats } from '../../api'

export default function TeacherDashboard() {
  const { user } = useAuthStore()
  const [subjects, setSubjects] = useState([])
  const [stats, setStats] = useState({ subjects: 0, chapters: 0, students: 0, exams: 0, questions: 0, attempts: 0 })
  const [loading, setLoading] = useState(true)

  const loadData = async () => {
    try {
      const [subjectsRes, statsRes] = await Promise.all([
        get('/v1/subjects'),
        getTeacherStats()
      ])
      if (subjectsRes.data.success) {
        setSubjects(subjectsRes.data.data)
      }
      if (statsRes.data.success) {
        setStats({
          subjects: statsRes.data.data.totalSubjects,
          chapters: statsRes.data.data.totalChapters,
          students: statsRes.data.data.totalStudents,
          exams: statsRes.data.data.activeExams,
          questions: statsRes.data.data.totalQuestions,
          attempts: statsRes.data.data.totalAttempts
        })
      }
    } catch (err) {
      console.error(err)
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    loadData()
  }, [])

  const statCards = [
    { label: 'Subjects', value: stats.subjects, icon: 'M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253', color: 'indigo' },
    { label: 'Chapters', value: stats.chapters, icon: 'M4 6h16M4 10h16M4 14h16M4 18h16', color: 'emerald' },
    { label: 'Students', value: stats.students, icon: 'M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z', color: 'violet' },
    { label: 'Exams', value: stats.exams, icon: 'M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-6 9l2 2 4-4', color: 'amber' },
    { label: 'Questions', value: stats.questions, icon: 'M8.228 9c.549-1.165 2.03-2 3.772-2 2.21 0 4 1.343 4 3 0 1.4-1.278 2.575-3.006 2.907-.542.104-.994.54-.994 1.093m0 3h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z', color: 'cyan' },
    { label: 'Attempts', value: stats.attempts, icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z', color: 'rose' },
  ]

  const quickActions = [
    { label: 'Create Subject', icon: 'M12 6v6m0 0v6m0-6h6m-6 0H6', href: '/teacher/subjects?new=true', color: 'indigo' },
    { label: 'Add Question', icon: 'M12 4v16m8-8H4', href: '/teacher/questions?new=true', color: 'emerald' },
    { label: 'Create Exam', icon: 'M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2', href: '/teacher/exams?new=true', color: 'violet' },
    { label: 'View Analytics', icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z', href: '/teacher/analytics', color: 'amber' },
  ]

  return (
    <Layout title="Teacher Dashboard">
      <div className="bg-gradient-to-r from-emerald-500 to-teal-600 rounded-2xl p-8 mb-8 text-white">
        <h2 className="text-2xl font-bold mb-2">Welcome back, {user?.fullName?.split(' ')[0]}!</h2>
        <p className="text-emerald-100">Manage your subjects, create exams, and track student performance.</p>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-4 gap-4 mb-8">
        {quickActions.map((action, idx) => (
          <a
            key={idx}
            href={action.href}
            className="flex items-center gap-3 p-4 bg-white rounded-xl border border-gray-100 hover:shadow-md hover:border-gray-200 transition-all group"
          >
            <div className={`w-10 h-10 rounded-lg bg-${action.color}-50 flex items-center justify-center group-hover:bg-${action.color}-100 transition-colors`}>
              <svg className={`w-5 h-5 text-${action.color}-600`} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d={action.icon} />
              </svg>
            </div>
            <span className="font-medium text-gray-700">{action.label}</span>
          </a>
        ))}
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        {statCards.map((stat, idx) => (
          <div key={idx} className="bg-white rounded-2xl p-6 shadow-sm border border-gray-100">
            <div className="flex items-center justify-between mb-4">
              <div className={`w-12 h-12 rounded-xl bg-${stat.color}-50 flex items-center justify-center`}>
                <svg className={`w-6 h-6 text-${stat.color}-600`} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d={stat.icon} />
                </svg>
              </div>
            </div>
            <div className="text-3xl font-bold text-gray-900 mb-1">{stat.value}</div>
            <div className="text-sm text-gray-500">{stat.label}</div>
          </div>
        ))}
      </div>

      <div className="bg-white rounded-2xl shadow-sm border border-gray-100">
        <div className="p-6 border-b border-gray-100 flex items-center justify-between">
          <h3 className="text-lg font-semibold text-gray-900">Your Subjects</h3>
          <button className="text-indigo-600 hover:text-indigo-700 text-sm font-medium">
            View All →
          </button>
        </div>
        
        {loading ? (
          <div className="p-8 text-center text-gray-500">Loading...</div>
        ) : subjects.length === 0 ? (
          <div className="p-8 text-center">
            <div className="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-4">
              <svg className="w-8 h-8 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M12 6.253v13m0-13C10.832 5.477 9.246 5 7.5 5S4.168 5.477 3 6.253v13C4.168 18.477 5.754 18 7.5 18s3.332.477 4.5 1.253m0-13C13.168 5.477 14.754 5 16.5 5c1.747 0 3.332.477 4.5 1.253v13C19.832 18.477 18.247 18 16.5 18c-1.746 0-3.332.477-4.5 1.253" />
              </svg>
            </div>
            <p className="text-gray-500 mb-4">No subjects created yet.</p>
            <button className="px-4 py-2 bg-indigo-600 text-white text-sm font-medium rounded-xl hover:bg-indigo-700">
              Create Subject
            </button>
          </div>
        ) : (
          <div className="divide-y divide-gray-100">
            {subjects.slice(0, 5).map((subject) => (
              <div key={subject.id} className="p-6 hover:bg-gray-50 transition-colors cursor-pointer">
                <div className="flex items-center justify-between">
                  <div>
                    <h4 className="font-semibold text-gray-900">{subject.title}</h4>
                    <p className="text-sm text-gray-500 mt-1">{subject.description || 'No description'}</p>
                  </div>
                  <div className="text-right">
                    <div className="text-2xl font-bold text-indigo-600">{subject.chapterCount || 0}</div>
                    <div className="text-xs text-gray-500">Chapters</div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </Layout>
  )
}