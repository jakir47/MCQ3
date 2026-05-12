import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import ProtectedRoute from './components/ProtectedRoute'
import LoginPage from './pages/LoginPage'
import ChangePasswordPage from './pages/ChangePasswordPage'
import StudentDashboard from './pages/student/Dashboard'
import StudentExams from './pages/student/Exams'
import StudentResults from './pages/student/Results'
import ExamPage from './pages/student/ExamPage'
import ExamReview from './pages/student/ExamReview'
import TeacherDashboard from './pages/teacher/Dashboard'
import TeacherSubjects from './pages/teacher/Subjects'
import TeacherChapters from './pages/teacher/Chapters'
import TeacherQuestions from './pages/teacher/Questions'
import TeacherExams from './pages/teacher/Exams'
import TeacherAnalytics from './pages/teacher/Analytics'
import TeacherStudents from './pages/teacher/Students'
import AdminDashboard from './pages/admin/Dashboard'
import AdminUsers from './pages/admin/Users'
import AdminTeachers from './pages/admin/Teachers'
import AdminAnalytics from './pages/admin/Analytics'
import AdminSettings from './pages/admin/Settings'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/change-password" element={<ChangePasswordPage />} />
        
        {/* Student Routes */}
        <Route element={<ProtectedRoute roles={['Student']} />}>
          <Route path="/student" element={<StudentDashboard />} />
          <Route path="/student/exams" element={<StudentExams />} />
          <Route path="/student/results" element={<StudentResults />} />
          <Route path="/student/results/:id" element={<ExamReview />} />
          <Route path="/student/exam/:id" element={<ExamPage />} />
          <Route path="/" element={<Navigate to="/student" replace />} />
        </Route>

        {/* Teacher Routes */}
        <Route element={<ProtectedRoute roles={['Teacher']} />}>
          <Route path="/teacher" element={<TeacherDashboard />} />
          <Route path="/teacher/subjects" element={<TeacherSubjects />} />
          <Route path="/teacher/chapters" element={<TeacherChapters />} />
          <Route path="/teacher/questions" element={<TeacherQuestions />} />
          <Route path="/teacher/exams" element={<TeacherExams />} />
          <Route path="/teacher/students" element={<TeacherStudents />} />
          <Route path="/teacher/analytics" element={<TeacherAnalytics />} />
          <Route path="/" element={<Navigate to="/teacher" replace />} />
        </Route>

        {/* Admin Routes */}
        <Route element={<ProtectedRoute roles={['Admin']} />}>
          <Route path="/admin" element={<AdminDashboard />} />
          <Route path="/admin/users" element={<AdminUsers />} />
          <Route path="/admin/teachers" element={<AdminTeachers />} />
          <Route path="/admin/analytics" element={<AdminAnalytics />} />
          <Route path="/admin/settings" element={<AdminSettings />} />
          <Route path="/" element={<Navigate to="/admin" replace />} />
        </Route>

        <Route path="/" element={<Navigate to="/login" replace />} />
        <Route path="*" element={<Navigate to="/login" replace />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App