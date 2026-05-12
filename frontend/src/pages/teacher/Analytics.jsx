import { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import { getSubjects } from '../../api/subjects'

export default function TeacherAnalyticsPage() {
  const [subjects, setSubjects] = useState([])
  const [selectedSubject, setSelectedSubject] = useState('')
  const [examStats, setExamStats] = useState(null)
  const [loading, setLoading] = useState(false)

  const loadSubjects = async () => {
    try {
      const { data } = await getSubjects()
      if (data.success) { setSubjects(data.data); if (data.data.length > 0) setSelectedSubject(data.data[0].id) }
    } catch (err) { console.error(err) }
  }

  const loadAnalytics = async () => {
    setLoading(true)
    try {
      setExamStats({
        totalStudents: 150,
        totalExams: 12,
        avgScore: 72.5,
        passRate: 85,
        topPerformers: [
          { name: 'John Doe', score: 95 },
          { name: 'Jane Smith', score: 92 },
          { name: 'Bob Wilson', score: 88 }
        ]
      })
    } catch (err) { console.error(err) }
    finally { setLoading(false) }
  }

  useEffect(() => { loadSubjects() }, [])

  useEffect(() => { if (selectedSubject) loadAnalytics() }, [selectedSubject])

  const statCards = [
    { label: 'Total Students', value: examStats?.totalStudents || 0, icon: 'M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z', color: 'indigo' },
    { label: 'Total Exams', value: examStats?.totalExams || 0, icon: 'M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-6 9l2 2 4-4', color: 'emerald' },
    { label: 'Average Score', value: `${examStats?.avgScore || 0}%`, icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z', color: 'violet' },
    { label: 'Pass Rate', value: `${examStats?.passRate || 0}%`, icon: 'M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z', color: 'amber' },
  ]

  return (
    <Layout title="Analytics">
      <div className="mb-6">
        <label className="text-sm font-medium text-gray-700">Select Subject</label>
        <select value={selectedSubject} onChange={(e) => setSelectedSubject(e.target.value)} className="ml-4 px-4 py-2 border border-gray-200 rounded-xl">
          {subjects.map(s => <option key={s.id} value={s.id}>{s.title}</option>)}
        </select>
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

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <div className="bg-white rounded-2xl shadow-sm border border-gray-100 p-6">
          <h3 className="text-lg font-semibold text-gray-900 mb-4">Score Distribution</h3>
          {loading ? <div className="text-gray-500">Loading...</div> : (
            <div className="space-y-3">
              {[
                { range: '90-100%', count: 15, color: 'bg-emerald-500' },
                { range: '80-89%', count: 25, color: 'bg-emerald-400' },
                { range: '70-79%', count: 35, color: 'bg-emerald-300' },
                { range: '60-69%', count: 20, color: 'bg-amber-400' },
                { range: 'Below 60%', count: 5, color: 'bg-red-400' }
              ].map((item, idx) => (
                <div key={idx} className="flex items-center gap-3">
                  <span className="w-16 text-sm text-gray-600">{item.range}</span>
                  <div className="flex-1 h-6 bg-gray-100 rounded-full overflow-hidden">
                    <div className={`h-full ${item.color} rounded-full`} style={{ width: `${item.count}%` }}></div>
                  </div>
                  <span className="w-8 text-sm text-gray-500 text-right">{item.count}</span>
                </div>
              ))}
            </div>
          )}
        </div>

        <div className="bg-white rounded-2xl shadow-sm border border-gray-100 p-6">
          <h3 className="text-lg font-semibold text-gray-900 mb-4">Top Performers</h3>
          {loading ? <div className="text-gray-500">Loading...</div> : (
            <div className="space-y-4">
              {examStats?.topPerformers?.map((student, idx) => (
                <div key={idx} className="flex items-center justify-between p-3 bg-gray-50 rounded-xl">
                  <div className="flex items-center gap-3">
                    <div className="w-8 h-8 bg-gradient-to-br from-indigo-500 to-purple-500 rounded-full flex items-center justify-center text-white text-sm font-medium">
                      {student.name.charAt(0)}
                    </div>
                    <span className="font-medium text-gray-900">{student.name}</span>
                  </div>
                  <span className="text-indigo-600 font-semibold">{student.score}%</span>
                </div>
              ))}
            </div>
          )}
        </div>

        <div className="bg-white rounded-2xl shadow-sm border border-gray-100 p-6 lg:col-span-2">
          <h3 className="text-lg font-semibold text-gray-900 mb-4">Performance Over Time</h3>
          <div className="h-48 flex items-center justify-center text-gray-500">
            Chart visualization would appear here
          </div>
        </div>
      </div>
    </Layout>
  )
}