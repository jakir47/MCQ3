import { useState, useEffect, useCallback, useRef } from 'react'
import Layout from '../../components/Layout'
import {
  uploadVideoLink, uploadBulk,
  getMediaFiles, updateAltText, softDelete, restore, hardDelete
} from '../../api/mediaFiles'
import { useAuthStore } from '../../store/authStore'

const TYPE_LABELS = { Image: 'Image', Audio: 'Audio', Document: 'Document', VideoLink: 'Video Link' }
const TYPE_ICONS = {
  Image: <svg className="w-5 h-5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M2.25 15.75l5.159-5.159a2.25 2.25 0 013.182 0l5.159 5.159m-1.5-1.5l1.409-1.409a2.25 2.25 0 013.182 0l2.909 2.909M3.75 21h16.5A2.25 2.25 0 0022.5 18.75V5.25A2.25 2.25 0 0020.25 3H3.75A2.25 2.25 0 001.5 5.25v13.5A2.25 2.25 0 003.75 21zm16.5-13.5h-16.5" /></svg>,
  Audio: <svg className="w-5 h-5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M19.114 5.636a9 9 0 010 12.728M16.463 8.288a5.25 5.25 0 010 7.424M6.75 8.25l4.72-4.72a.75.75 0 011.28.53v15.88a.75.75 0 01-1.28.53l-4.72-4.72H4.51c-.88 0-1.704-.507-1.938-1.354A9.01 9.01 0 012.25 12c0-.83.112-1.633.322-2.396C2.806 8.756 3.63 8.25 4.51 8.25H6.75z" /></svg>,
  Document: <svg className="w-5 h-5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m2.25 0H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" /></svg>,
  VideoLink: <svg className="w-5 h-5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M4.5 18.75h9a2.25 2.25 0 002.25-2.25v-9a2.25 2.25 0 00-2.25-2.25h-9A2.25 2.25 0 002.25 7.5v9a2.25 2.25 0 002.25 2.25z" /></svg>,
}

const STAT_CARDS = [
  { key: 'total', label: 'Total Files', gradient: 'from-indigo-500 to-purple-600', icon: 'M3.75 6A2.25 2.25 0 016 3.75h2.25A2.25 2.25 0 0110.5 6v2.25a2.25 2.25 0 01-2.25 2.25H6a2.25 2.25 0 01-2.25-2.25V6zM3.75 15.75A2.25 2.25 0 016 13.5h2.25a2.25 2.25 0 012.25 2.25V18a2.25 2.25 0 01-2.25 2.25H6A2.25 2.25 0 013.75 18v-2.25zM13.5 6a2.25 2.25 0 012.25-2.25H18A2.25 2.25 0 0120.25 6v2.25A2.25 2.25 0 0118 10.5h-2.25a2.25 2.25 0 01-2.25-2.25V6zM13.5 15.75a2.25 2.25 0 012.25-2.25H18a2.25 2.25 0 012.25 2.25V18A2.25 2.25 0 0118 20.25h-2.25A2.25 2.25 0 0113.5 18v-2.25z' },
  { key: 'images', label: 'Images', gradient: 'from-sky-500 to-cyan-600', icon: 'M2.25 15.75l5.159-5.159a2.25 2.25 0 013.182 0l5.159 5.159m-1.5-1.5l1.409-1.409a2.25 2.25 0 013.182 0l2.909 2.909M3.75 21h16.5A2.25 2.25 0 0022.5 18.75V5.25A2.25 2.25 0 0020.25 3H3.75A2.25 2.25 0 001.5 5.25v13.5A2.25 2.25 0 003.75 21zm16.5-13.5h-16.5' },
  { key: 'audio', label: 'Audio', gradient: 'from-violet-500 to-purple-600', icon: 'M19.114 5.636a9 9 0 010 12.728M16.463 8.288a5.25 5.25 0 010 7.424M6.75 8.25l4.72-4.72a.75.75 0 011.28.53v15.88a.75.75 0 01-1.28.53l-4.72-4.72H4.51c-.88 0-1.704-.507-1.938-1.354A9.01 9.01 0 012.25 12c0-.83.112-1.633.322-2.396C2.806 8.756 3.63 8.25 4.51 8.25H6.75z' },
  { key: 'docs', label: 'Documents', gradient: 'from-amber-500 to-orange-600', icon: 'M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m2.25 0H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z' },
  { key: 'videoLinks', label: 'Video Links', gradient: 'from-rose-500 to-pink-600', icon: 'M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M4.5 18.75h9a2.25 2.25 0 002.25-2.25v-9a2.25 2.25 0 00-2.25-2.25h-9A2.25 2.25 0 002.25 7.5v9a2.25 2.25 0 002.25 2.25z' },
]

function formatBytes(bytes) {
  if (!bytes) return '0 B'
  if (bytes < 1024) return bytes + ' B'
  if (bytes < 1024 * 1024) return (bytes / 1024).toFixed(1) + ' KB'
  return (bytes / (1024 * 1024)).toFixed(1) + ' MB'
}

function formatDate(d) {
  if (!d) return ''
  return new Date(d).toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' })
}

function getYouTubeThumbnail(url) {
  if (!url) return null
  const m = url.match(/(?:youtube\.com\/(?:watch\?v=|embed\/)|youtu\.be\/)([A-Za-z0-9_-]{11})/)
  return m ? `https://img.youtube.com/vi/${m[1]}/hqdefault.jpg` : null
}

function isYouTube(url) { return /youtube\.com|youtu\.be/.test(url || '') }
function isVimeo(url) { return /vimeo\.com/.test(url || '') }

function formatTime(secs) {
  if (!secs || isNaN(secs)) return '0:00'
  const m = Math.floor(secs / 60)
  const s = Math.floor(secs % 60)
  return `${m}:${s.toString().padStart(2, '0')}`
}

function getVideoEmbedSrc(url) {
  const ytMatch = (url || '').match(/(?:youtube\.com\/(?:watch\?v=|embed\/)|youtu\.be\/)([A-Za-z0-9_-]{11})/)
  if (ytMatch) return `https://www.youtube.com/embed/${ytMatch[1]}?autoplay=1`
  const vimeoMatch = (url || '').match(/vimeo\.com\/(\d+)/)
  if (vimeoMatch) return `https://player.vimeo.com/video/${vimeoMatch[1]}?autoplay=1`
  return null
}

/* ─── Standalone Audio Player Modal ─── */
function AudioPlayerModal({ audioFile, onClose }) {
  const [audioState, setAudioState] = useState({ playing: false, currentTime: 0, duration: 0, volume: 1 })
  const audioRef = useRef(null)
  const progressPct = audioState.duration ? Math.round((audioState.currentTime / audioState.duration) * 100) : 0

  useEffect(() => {
    if (audioFile) {
      const timer = setTimeout(() => {
        if (audioRef.current) {
          audioRef.current.play().catch(() => {})
          setAudioState(s => ({ ...s, playing: true }))
        }
      }, 100)
      return () => clearTimeout(timer)
    }
  }, [audioFile])

  const handleAudioTimeUpdate = () => {
    if (audioRef.current) setAudioState(s => ({ ...s, currentTime: audioRef.current.currentTime, duration: audioRef.current.duration || 0 }))
  }
  const handleAudioEnded = () => setAudioState(s => ({ ...s, playing: false, currentTime: 0 }))
  const togglePlay = () => {
    if (!audioRef.current) return
    audioState.playing ? audioRef.current.pause() : audioRef.current.play()
    setAudioState(s => ({ ...s, playing: !s.playing }))
  }
  const seekAudio = (e) => {
    if (!audioRef.current || !audioState.duration) return
    const rect = e.currentTarget.getBoundingClientRect()
    audioRef.current.currentTime = Math.max(0, Math.min(1, (e.clientX - rect.left) / rect.width)) * audioState.duration
  }
  const setVolume = (v) => {
    if (!audioRef.current) return
    audioRef.current.volume = Math.max(0, Math.min(1, v))
    setAudioState(s => ({ ...s, volume: v }))
  }

  const handleClose = () => {
    if (audioRef.current) { audioRef.current.pause(); audioRef.current.src = '' }
    onClose()
  }

  return (
    <div className="fixed inset-0 bg-black/60 z-[60] flex items-center justify-center p-4" onClick={handleClose}>
      <div className="w-full max-w-xs rounded-xl overflow-hidden" style={{ background: '#fff', boxShadow: '0 25px 60px rgba(0,0,0,0.25)' }} onClick={e => e.stopPropagation()}>
        <audio ref={audioRef} src={audioFile.url} autoPlay onTimeUpdate={handleAudioTimeUpdate} onLoadedMetadata={handleAudioTimeUpdate} onEnded={handleAudioEnded} className="hidden" />

        {/* Gradient header */}
        <div className="relative px-5 pt-5 pb-4" style={{ background: 'linear-gradient(135deg, #7c3aed, #a855f7, #d946ef)' }}>
          <div className="flex items-center justify-between">
            <div className="min-w-0 flex-1">
              <p className="text-sm font-semibold text-white truncate pr-2">{audioFile.originalFileName}</p>
              <p className="text-xs mt-0.5 text-white/60">{formatBytes(audioFile.fileSizeBytes)}</p>
            </div>
            <button onClick={handleClose}
              className="w-7 h-7 rounded-full flex items-center justify-center shrink-0"
              style={{ background: 'rgba(255,255,255,0.15)' }}
              onMouseEnter={e => e.currentTarget.style.background = 'rgba(255,255,255,0.25)'}
              onMouseLeave={e => e.currentTarget.style.background = 'rgba(255,255,255,0.15)'}
            >
              <svg className="w-3.5 h-3.5 text-white/70" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" /></svg>
            </button>
          </div>
        </div>

        {/* Equalizer bars */}
        <div className="flex items-end justify-center gap-[2px] h-10 px-5 pt-4" style={{ background: '#fafafa' }}>
          {[4, 8, 6, 14, 20, 24, 18, 12, 8, 16, 22, 20, 10, 6, 12, 18, 14, 8, 4, 6, 10, 16, 20, 14, 8].map((h, i) => (
            <div key={i}
              className="w-[3px] rounded-full transition-all duration-200"
              style={{
                height: `${h}px`,
                background: audioState.playing
                  ? ['#7c3aed', '#8b5cf6', '#a855f7', '#c084fc'][i % 4]
                  : '#e5e5e5',
                opacity: audioState.playing ? 0.7 + Math.random() * 0.3 : 0.4,
              }}
            />
          ))}
        </div>

        {/* Progress section */}
        <div className="px-5 pt-3 pb-1" style={{ background: '#fafafa' }}>
          <div className="relative h-1 rounded-full cursor-pointer group" style={{ background: '#e5e5e5' }} onClick={seekAudio}>
            <div className="absolute top-0 left-0 h-full rounded-full transition-all duration-100" style={{ width: `${progressPct}%`, background: '#7c3aed' }} />
            <div
              className="absolute top-1/2 -translate-y-1/2 w-3 h-3 rounded-full opacity-0 group-hover:opacity-100 transition-opacity duration-150"
              style={{ left: `calc(${progressPct}% - 6px)`, background: '#7c3aed', boxShadow: '0 0 0 2px white' }}
            />
          </div>
          <div className="flex items-center justify-between mt-1.5">
            <span className="text-[11px] tabular-nums" style={{ color: '#a3a3a3' }}>{formatTime(audioState.currentTime)}</span>
            <span className="text-[11px] tabular-nums" style={{ color: '#a3a3a3' }}>{formatTime(audioState.duration)}</span>
          </div>
        </div>

        {/* Controls */}
        <div className="flex items-center justify-between px-5 pt-3 pb-5" style={{ background: '#fafafa' }}>
          <div className="flex items-center gap-1.5 w-28">
            <svg className="w-3.5 h-3.5 shrink-0" style={{ color: '#a3a3a3' }} fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" d="M19.114 5.636a9 9 0 010 12.728M16.463 8.288a5.25 5.25 0 010 7.424M6.75 8.25l4.72-4.72a.75.75 0 011.28.53v15.88a.75.75 0 01-1.28.53l-4.72-4.72H4.51c-.88 0-1.704-.507-1.938-1.354A9.01 9.01 0 012.25 12c0-.83.112-1.633.322-2.396C2.806 8.756 3.63 8.25 4.51 8.25H6.75z" />
            </svg>
            <input type="range" min="0" max="1" step="0.05" value={audioState.volume} onChange={e => setVolume(parseFloat(e.target.value))}
              className="flex-1 h-0.5 rounded-full cursor-pointer appearance-none"
              style={{ background: `linear-gradient(to right, #7c3aed ${audioState.volume * 100}%, #e5e5e5 ${audioState.volume * 100}%)` }} />
          </div>
          <div className="flex items-center gap-3">
            <button onClick={() => { if (audioRef.current) audioRef.current.currentTime = Math.max(0, audioRef.current.currentTime - 10) }}
              className="p-0.5 transition-colors" style={{ color: '#a3a3a3' }} onMouseEnter={e => e.currentTarget.style.color = '#525252'} onMouseLeave={e => e.currentTarget.style.color = '#a3a3a3'}>
              <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={1.5} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M9 15L3 9m0 0l6-6M3 9h12a6 6 0 010 12h-3" /></svg>
            </button>
            <button onClick={togglePlay}
              className="w-9 h-9 rounded-full flex items-center justify-center transition-all active:scale-90"
              style={{ background: 'linear-gradient(135deg, #7c3aed, #a855f7)' }}
            >
              {audioState.playing ? (
                <svg className="w-4 h-4 text-white" fill="currentColor" viewBox="0 0 24 24"><path d="M6 4h4v16H6V4zm8 0h4v16h-4V4z" /></svg>
              ) : (
                <svg className="w-4 h-4 text-white ml-0.5" fill="currentColor" viewBox="0 0 24 24"><path d="M8 5v14l11-7z" /></svg>
              )}
            </button>
            <button onClick={() => { if (audioRef.current) audioRef.current.currentTime = Math.min(audioState.duration, audioRef.current.currentTime + 10) }}
              className="p-0.5 transition-colors" style={{ color: '#a3a3a3' }} onMouseEnter={e => e.currentTarget.style.color = '#525252'} onMouseLeave={e => e.currentTarget.style.color = '#a3a3a3'}>
              <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={1.5} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M15 15l6-6m0 0l-6-6m6 6H9a6 6 0 000 12h3" /></svg>
            </button>
          </div>
        </div>
      </div>
    </div>
  )
}

const FILTER_TABS = [
  { key: 'All', label: 'All', icon: <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M3.75 6A2.25 2.25 0 016 3.75h2.25A2.25 2.25 0 0110.5 6v2.25a2.25 2.25 0 01-2.25 2.25H6a2.25 2.25 0 01-2.25-2.25V6zM3.75 15.75A2.25 2.25 0 016 13.5h2.25a2.25 2.25 0 012.25 2.25V18a2.25 2.25 0 01-2.25 2.25H6A2.25 2.25 0 013.75 18v-2.25zM13.5 6a2.25 2.25 0 012.25-2.25H18A2.25 2.25 0 0120.25 6v2.25A2.25 2.25 0 0118 10.5h-2.25a2.25 2.25 0 01-2.25-2.25V6zM13.5 15.75a2.25 2.25 0 012.25-2.25H18a2.25 2.25 0 012.25 2.25V18A2.25 2.25 0 0118 20.25h-2.25A2.25 2.25 0 0113.5 18v-2.25z" /></svg> },
  { key: 'Image', label: 'Image', icon: <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M2.25 15.75l5.159-5.159a2.25 2.25 0 013.182 0l5.159 5.159m-1.5-1.5l1.409-1.409a2.25 2.25 0 013.182 0l2.909 2.909M3.75 21h16.5A2.25 2.25 0 0022.5 18.75V5.25A2.25 2.25 0 0020.25 3H3.75A2.25 2.25 0 001.5 5.25v13.5A2.25 2.25 0 003.75 21zm16.5-13.5h-16.5" /></svg> },
  { key: 'Audio', label: 'Audio', icon: <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M19.114 5.636a9 9 0 010 12.728M16.463 8.288a5.25 5.25 0 010 7.424M6.75 8.25l4.72-4.72a.75.75 0 011.28.53v15.88a.75.75 0 01-1.28.53l-4.72-4.72H4.51c-.88 0-1.704-.507-1.938-1.354A9.01 9.01 0 012.25 12c0-.83.112-1.633.322-2.396C2.806 8.756 3.63 8.25 4.51 8.25H6.75z" /></svg> },
  { key: 'Document', label: 'Docs', icon: <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m2.25 0H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" /></svg> },
  { key: 'VideoLink', label: 'Video Link', icon: <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M4.5 18.75h9a2.25 2.25 0 002.25-2.25v-9a2.25 2.25 0 00-2.25-2.25h-9A2.25 2.25 0 002.25 7.5v9a2.25 2.25 0 002.25 2.25z" /></svg> },
]

export default function AdminMediaPage() {
  const { user } = useAuthStore()
  const isAdmin = user?.role === 'Admin'

  const [files, setFiles] = useState([])
  const [deletedFiles, setDeletedFiles] = useState([])
  const [loading, setLoading] = useState(true)
  const [totalPages, setTotalPages] = useState(1)
  const [currentPage, setCurrentPage] = useState(1)
  const [activeTab, setActiveTab] = useState('All')
  const [notification, setNotification] = useState(null)
  const [uploading, setUploading] = useState(false)
  const [showDeleted, setShowDeleted] = useState(false)
  const [editingAlt, setEditingAlt] = useState(null)
  const [altValue, setAltValue] = useState('')
  const [showVideoPanel, setShowVideoPanel] = useState(false)
  const [videoForm, setVideoForm] = useState({ videoUrl: '', title: '' })
  const [videoErrors, setVideoErrors] = useState({})
  const [dragOver, setDragOver] = useState(false)
  const [searchQuery, setSearchQuery] = useState('')

  const [lightboxFile, setLightboxFile] = useState(null)
  const [audioFile, setAudioFile] = useState(null)
  const [videoLinkFile, setVideoLinkFile] = useState(null)
  const [totalStats, setTotalStats] = useState({ total: 0, images: 0, audio: 0, docs: 0, videoLinks: 0 })
  const searchRef = useRef()

  const typeParam = activeTab === 'All' ? null : activeTab

  const loadStats = useCallback(async () => {
    try {
      const { data } = await getMediaFiles(null, 1, 1000)
      if (data.success) {
        const all = data.data.items
        setTotalStats({
          total: all.filter(f => !f.isDeleted).length,
          images: all.filter(f => f.mediaType === 'Image' && !f.isDeleted).length,
          audio: all.filter(f => f.mediaType === 'Audio' && !f.isDeleted).length,
          docs: all.filter(f => f.mediaType === 'Document' && !f.isDeleted).length,
          videoLinks: all.filter(f => f.mediaType === 'VideoLink' && !f.isDeleted).length,
        })
      }
    } catch (e) { console.error(e) }
  }, [])

  const loadDeleted = useCallback(async () => {
    try {
      const { data } = await getMediaFiles(null, 1, 100)
      if (data.success) setDeletedFiles(data.data.items.filter(f => f.isDeleted))
    } catch (e) { console.error(e) }
  }, [])

  const loadFiles = useCallback(async (page = 1, type = typeParam) => {
    setLoading(true)
    try {
      const { data } = await getMediaFiles(type, page, 20)
      if (data.success) {
        setFiles(data.data.items)
        setTotalPages(data.data.totalPages)
        setCurrentPage(data.data.currentPage)
      }
    } catch (e) {
      showNotif('error', 'Failed to load media files')
    } finally {
      setLoading(false)
    }
  }, [typeParam])

  useEffect(() => { loadFiles() }, [loadFiles])
  useEffect(() => { loadDeleted(); loadStats() }, [loadDeleted, loadStats])

  const showNotif = (type, msg) => {
    setNotification({ type, message: msg })
    setTimeout(() => setNotification(null), 4000)
  }

  const handleBulkUpload = async (fileList) => {
    if (fileList.length > 20) { showNotif('error', 'Maximum 20 files per batch'); return }
    setUploading(true)
    try {
      const { data } = await uploadBulk(fileList)
      if (data.success) {
        const r = data.data
        showNotif(r.failedCount > 0 ? 'warning' : 'success', r.failedCount > 0
          ? `Uploaded ${r.successCount} file(s), ${r.failedCount} failed`
          : `${r.successCount} file(s) uploaded successfully`)
        loadFiles(); loadDeleted(); loadStats()
      }
    } catch (e) {
      showNotif('error', e.response?.data?.error?.message || 'Bulk upload failed')
    } finally { setUploading(false) }
  }

  const handleDrop = (e) => {
    e.preventDefault(); setDragOver(false)
    const dropped = Array.from(e.dataTransfer.files)
    if (dropped.length) handleBulkUpload(dropped)
  }

  const handleDelete = async (f) => {
    if (!confirm(`Delete "${f.originalFileName}"?`)) return
    try {
      await softDelete(f.id)
      showNotif('success', 'File moved to trash')
      loadFiles(); loadDeleted(); loadStats()
    } catch (e) {
      showNotif('error', e.response?.data?.error?.message || 'Delete failed')
    }
  }

  const handleRestore = async (f) => {
    try {
      await restore(f.id)
      showNotif('success', 'File restored')
      loadFiles(); loadDeleted(); loadStats()
    } catch (e) {
      showNotif('error', e.response?.data?.error?.message || 'Restore failed')
    }
  }

  const handleHardDelete = async (f) => {
    if (!confirm(`Permanently delete "${f.originalFileName}"?`)) return
    try {
      await hardDelete(f.id)
      showNotif('success', 'File permanently deleted')
      loadFiles(); loadDeleted(); loadStats()
    } catch (e) {
      showNotif('error', e.response?.data?.error?.message || 'Delete failed')
    }
  }

  const startEditAlt = (f) => { setEditingAlt(f.id); setAltValue(f.altText || '') }

  const saveAlt = async (id) => {
    try {
      const { data } = await updateAltText(id, altValue)
      if (data.success) {
        setFiles(prev => prev.map(f => f.id === id ? data.data : f))
        showNotif('success', 'Alt text updated')
      }
    } catch (e) { showNotif('error', 'Failed to update alt text') }
    setEditingAlt(null)
  }

  const handleVideoSubmit = async (e) => {
    e.preventDefault()
    if (!videoForm.videoUrl.trim()) { setVideoErrors({ videoUrl: 'URL is required' }); return }
    if (!/^https?:\/\/.+/.test(videoForm.videoUrl)) { setVideoErrors({ videoUrl: 'Invalid URL' }); return }
    setUploading(true)
    try {
      const { data } = await uploadVideoLink(videoForm.videoUrl, videoForm.title || undefined)
      if (data.success) {
        showNotif('success', 'Video link added')
        setShowVideoPanel(false); setVideoForm({ videoUrl: '', title: '' })
        loadFiles(); loadStats()
      } else { showNotif('error', data.error?.message || 'Failed to add video link') }
    } catch (e) { showNotif('error', e.response?.data?.error?.message || 'Failed to add video link') }
    finally { setUploading(false) }
  }

  const filteredFiles = searchQuery
    ? files.filter(f => (f.originalFileName || '').toLowerCase().includes(searchQuery.toLowerCase()))
    : files

  const stats = totalStats

  const FileCard = ({ f }) => {
    const [hovered, setHovered] = useState(false)

    if (f.mediaType === 'VideoLink') {
      const thumb = getYouTubeThumbnail(f.videoLinkUrl)
      return (
        <div className="group bg-white rounded-2xl overflow-hidden border border-gray-100 shadow-sm hover:shadow-xl transition-all duration-300" onMouseEnter={() => setHovered(true)} onMouseLeave={() => setHovered(false)}>
          <div className="relative h-32 bg-gray-900 overflow-hidden cursor-pointer" onClick={() => setVideoLinkFile(f)}>
            {thumb ? (
              <img src={thumb} alt="" className={`w-full h-full object-cover transition-all duration-500 ${hovered ? 'scale-110 opacity-40' : 'opacity-70'}`} />
            ) : (
              <div className="w-full h-full flex items-center justify-center"><svg className="w-14 h-14 text-gray-500" fill="none" stroke="currentColor" strokeWidth={1} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.752 11.168l-3.197-2.132A1 1 0 0010 9.87v4.263a1 1 0 001.555.832l3.197-2.132a1 1 0 000-1.664z" /><path strokeLinecap="round" strokeLinejoin="round" d="M21 12a9 9 0 11-18 0 9 9 0 0118 0z" /></svg></div>
            )}
            <div className={`absolute inset-0 flex items-center justify-center transition-all duration-300 ${hovered ? 'scale-110' : ''}`}>
              <div className="w-14 h-14 bg-white/90 backdrop-blur-sm rounded-full flex items-center justify-center shadow-lg">
                <svg className="w-6 h-6 text-gray-900 ml-0.5" fill="currentColor" viewBox="0 0 24 24"><path d="M8 5v14l11-7z" /></svg>
              </div>
            </div>

          </div>
          <div className="p-3">
            <p className="text-sm font-semibold text-gray-900 truncate">{f.originalFileName || 'Untitled'}</p>
            <p className="text-xs text-gray-400 truncate mt-0.5 font-mono">{f.videoLinkUrl}</p>
            <div className="flex items-center justify-between mt-2 pt-2 border-t border-gray-50">
              <span className="text-xs text-gray-400">{formatDate(f.createdAt)}</span>
              <div className="flex items-center gap-1">
                <button onClick={() => setVideoLinkFile(f)} className="p-1.5 text-pink-600 bg-pink-50 rounded-lg hover:bg-pink-100 transition-colors" title="Play"><svg className="w-4 h-4" fill="currentColor" viewBox="0 0 24 24"><path d="M8 5v14l11-7z" /></svg></button>
                <a href={f.videoLinkUrl} target="_blank" rel="noopener noreferrer" className="p-1.5 text-gray-500 hover:text-indigo-600 rounded-lg hover:bg-indigo-50 transition-colors" title="Open in new tab"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M13.5 6H5.25A2.25 2.25 0 003 8.25v10.5A2.25 2.25 0 005.25 21h10.5A2.25 2.25 0 0018 18.75V10.5m-10.5 6L21 3m0 0h-5.25M21 3v5.25" /></svg></a>
                <button onClick={() => handleDelete(f)} className="p-1.5 text-gray-500 hover:text-red-600 rounded-lg hover:bg-red-50 transition-colors" title="Delete"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" /></svg></button>
              </div>
            </div>
          </div>
        </div>
      )
    }

    if (f.mediaType === 'Image') {
      return (
        <div className="group bg-white rounded-2xl overflow-hidden border border-gray-100 shadow-sm hover:shadow-xl transition-all duration-300 hover:-translate-y-0.5" onMouseEnter={() => setHovered(true)} onMouseLeave={() => setHovered(false)}>
          <div className="relative h-32 overflow-hidden cursor-pointer" onClick={() => setLightboxFile(f)}>
            <img src={f.url} alt={f.altText || ''} className={`w-full h-full object-cover transition-all duration-500 ${hovered ? 'scale-110' : ''}`} />
            <div className={`absolute inset-0 bg-gradient-to-t from-black/50 via-transparent to-transparent transition-opacity duration-300 ${hovered ? 'opacity-100' : 'opacity-0'}`} />

          </div>
          <div className="p-3">
            <div className="flex items-center justify-between">
              <p className="text-sm font-semibold text-gray-900 truncate flex-1 min-w-0">{f.originalFileName}</p>
              <div className="flex items-center gap-2 shrink-0">
                {f.fileType && <span className="text-[10px] font-semibold uppercase text-gray-400 bg-gray-100 px-1.5 py-0.5 rounded">{f.fileType}</span>}
                <span className="text-xs text-gray-400">{formatBytes(f.fileSizeBytes)}</span>
              </div>
            </div>
            {editingAlt === f.id ? (
              <div className="flex gap-2 mt-1.5">
                <input value={altValue} onChange={e => setAltValue(e.target.value)} className="flex-1 text-xs px-2 py-1.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-sky-200 focus:border-sky-400" placeholder="Alt text..." autoFocus />
                <button onClick={() => saveAlt(f.id)} className="text-xs font-semibold text-sky-600 hover:text-sky-700">Save</button>
                <button onClick={() => setEditingAlt(null)} className="text-xs text-gray-400 hover:text-gray-600">Cancel</button>
              </div>
            ) : (
               <p className="text-xs text-gray-400 truncate mt-0.5 cursor-pointer hover:text-gray-600" onClick={() => startEditAlt(f)}>{f.altText || <span className="italic">Add alt text...</span>}</p>
            )}
            <div className="flex items-center justify-between mt-2 pt-2 border-t border-gray-50">
              <span className="text-xs text-gray-400">{formatDate(f.createdAt)}</span>
              <div className="flex items-center gap-1">
                <button onClick={() => setLightboxFile(f)} className="p-1.5 text-gray-500 hover:text-indigo-600 rounded-lg hover:bg-indigo-50 transition-colors" title="View"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" /><path strokeLinecap="round" strokeLinejoin="round" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" /></svg></button>
                <a href={f.url} target="_blank" rel="noopener noreferrer" className="p-1.5 text-gray-500 hover:text-amber-600 rounded-lg hover:bg-amber-50 transition-colors" title="Open"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M13.5 6H5.25A2.25 2.25 0 003 8.25v10.5A2.25 2.25 0 005.25 21h10.5A2.25 2.25 0 0018 18.75V10.5m-10.5 6L21 3m0 0h-5.25M21 3v5.25" /></svg></a>
                <button onClick={() => startEditAlt(f)} className="p-1.5 text-gray-500 hover:text-amber-600 rounded-lg hover:bg-amber-50 transition-colors" title="Edit alt text"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M16.862 4.487l1.687-1.688a1.875 1.875 0 112.652 2.652L10.582 16.07a4.5 4.5 0 01-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 011.13-1.897l8.932-8.931zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0115.75 21H5.25A2.25 2.25 0 013 18.75V8.25A2.25 2.25 0 015.25 6H10" /></svg></button>
                <button onClick={() => handleDelete(f)} className="p-1.5 text-gray-500 hover:text-red-600 rounded-lg hover:bg-red-50 transition-colors" title="Delete"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" /></svg></button>
              </div>
            </div>
          </div>
        </div>
      )
    }

    if (f.mediaType === 'Audio') {
      return (
        <div className="group bg-white rounded-2xl overflow-hidden border border-gray-100 shadow-sm hover:shadow-xl transition-all duration-300 hover:-translate-y-0.5" onMouseEnter={() => setHovered(true)} onMouseLeave={() => setHovered(false)}>
          <div className="relative h-32 bg-gradient-to-br from-violet-500 via-purple-500 to-fuchsia-500 flex items-center justify-center cursor-pointer overflow-hidden" onClick={() => setAudioFile(f)}>
            <div className="absolute inset-0 opacity-10">
              <svg className="w-full h-full" viewBox="0 0 200 100" preserveAspectRatio="none"><path d="M0,50 Q20,20 40,50 T80,50 T120,50 T160,50 T200,50 L200,100 L0,100 Z" fill="white" /><path d="M0,30 Q25,0 50,30 T100,30 T150,30 T200,30 L200,100 L0,100 Z" fill="white" opacity="0.5" /></svg>
            </div>
            <div className={`w-16 h-16 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center transition-all duration-300 ${hovered ? 'scale-110' : ''}`}>
              <svg className="w-8 h-8 text-white ml-0.5" fill="currentColor" viewBox="0 0 24 24"><path d="M8 5v14l11-7z" /></svg>
            </div>

            <div className="absolute bottom-3 left-0 right-0 text-center">
              <span className="text-xs text-white/70 font-medium bg-black/20 px-3 py-1 rounded-full backdrop-blur-sm">Click to play</span>
            </div>
          </div>
          <div className="p-3">
            <div className="flex items-center justify-between">
              <p className="text-sm font-semibold text-gray-900 truncate flex-1 min-w-0">{f.originalFileName}</p>
              <div className="flex items-center gap-2 shrink-0">
                {f.fileType && <span className="text-[10px] font-semibold uppercase text-gray-400 bg-gray-100 px-1.5 py-0.5 rounded">{f.fileType}</span>}
                <span className="text-xs text-gray-400">{formatBytes(f.fileSizeBytes)}</span>
              </div>
            </div>
            {editingAlt === f.id ? (
              <div className="flex gap-2 mt-1.5">
                <input value={altValue} onChange={e => setAltValue(e.target.value)} className="flex-1 text-xs px-2 py-1.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-200 focus:border-purple-400" placeholder="Alt text..." autoFocus />
                <button onClick={() => saveAlt(f.id)} className="text-xs font-semibold text-purple-600">Save</button>
                <button onClick={() => setEditingAlt(null)} className="text-xs text-gray-400">Cancel</button>
              </div>
            ) : (
              <p className="text-xs text-gray-400 truncate mt-0.5">{f.altText || <span className="italic">No description</span>}</p>
            )}
            <div className="flex items-center justify-between mt-2 pt-2 border-t border-gray-50">
              <span className="text-xs text-gray-400">{formatDate(f.createdAt)}</span>
              <div className="flex items-center gap-1">
                <button onClick={() => setAudioFile(f)} className="p-1.5 text-purple-600 bg-purple-50 rounded-lg hover:bg-purple-100 transition-colors" title="Play"><svg className="w-4 h-4" fill="currentColor" viewBox="0 0 24 24"><path d="M8 5v14l11-7z" /></svg></button>
                <a href={f.url} target="_blank" rel="noopener noreferrer" className="p-1.5 text-gray-500 hover:text-amber-600 rounded-lg hover:bg-amber-50 transition-colors" title="Open"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M13.5 6H5.25A2.25 2.25 0 003 8.25v10.5A2.25 2.25 0 005.25 21h10.5A2.25 2.25 0 0018 18.75V10.5m-10.5 6L21 3m0 0h-5.25M21 3v5.25" /></svg></a>
                <button onClick={() => handleDelete(f)} className="p-1.5 text-gray-500 hover:text-red-600 rounded-lg hover:bg-red-50 transition-colors" title="Delete"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" /></svg></button>
              </div>
            </div>
          </div>
        </div>
      )
    }

    return (
      <div className="group bg-white rounded-2xl overflow-hidden border border-gray-100 shadow-sm hover:shadow-xl transition-all duration-300 hover:-translate-y-0.5">
        <div className="relative h-32 bg-gradient-to-br from-amber-50 to-orange-100 flex items-center justify-center">
          <svg className="w-14 h-14 text-amber-400" fill="none" stroke="currentColor" strokeWidth={1} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m2.25 0H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" /></svg>
        </div>
        <div className="p-3">
          <p className="text-sm font-semibold text-gray-900 truncate">{f.originalFileName}</p>
          <div className="flex items-center gap-2 mt-0.5">
            {f.fileType && <span className="text-[10px] font-semibold uppercase text-gray-400 bg-gray-100 px-1.5 py-0.5 rounded">{f.fileType}</span>}
            <p className="text-xs text-gray-400">{formatBytes(f.fileSizeBytes)}</p>
          </div>
          <div className="flex items-center justify-between mt-2 pt-2 border-t border-gray-50">
            <span className="text-xs text-gray-400">{formatDate(f.createdAt)}</span>
            <div className="flex items-center gap-1">
              <a href={f.url} target="_blank" rel="noopener noreferrer" className="p-1.5 text-gray-500 hover:text-amber-600 rounded-lg hover:bg-amber-50 transition-colors"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M3 16.5v2.25A2.25 2.25 0 005.25 21h13.5A2.25 2.25 0 0021 18.75V16.5M16.5 12L12 16.5m0 0L7.5 12m4.5 4.5V3" /></svg></a>
              <button onClick={() => handleDelete(f)} className="p-1.5 text-gray-500 hover:text-red-600 rounded-lg hover:bg-red-50 transition-colors"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" /></svg></button>
            </div>
          </div>
        </div>
      </div>
    )
  }

  return (
    <Layout title="Media Library">
      <style>{`
        @keyframes slide-in { from { transform: translateX(100%); opacity: 0; } to { transform: translateX(0); opacity: 1; } }
        @keyframes fade-up { from { opacity: 0; transform: translateY(12px); } to { opacity: 1; transform: translateY(0); } }
        @keyframes scale-in { from { opacity: 0; transform: scale(0.95); } to { opacity: 1; transform: scale(1); } }
        @keyframes pulse-glow { 0%, 100% { box-shadow: 0 0 20px rgba(99,102,241,0.1); } 50% { box-shadow: 0 0 30px rgba(99,102,241,0.2); } }
        .animate-slide-in { animation: slide-in 0.3s ease-out; }
        .animate-fade-up { animation: fade-up 0.4s ease-out; }
        .animate-scale-in { animation: scale-in 0.3s ease-out; }
        .animate-pulse-glow { animation: pulse-glow 2s infinite; }
        .media-grid > * { animation: fade-up 0.4s ease-out; animation-fill-mode: both; }
        .media-grid > *:nth-child(1) { animation-delay: 0.02s; }
        .media-grid > *:nth-child(2) { animation-delay: 0.04s; }
        .media-grid > *:nth-child(3) { animation-delay: 0.06s; }
        .media-grid > *:nth-child(4) { animation-delay: 0.08s; }
        .media-grid > *:nth-child(5) { animation-delay: 0.1s; }
        .media-grid > *:nth-child(6) { animation-delay: 0.12s; }
        .media-grid > *:nth-child(7) { animation-delay: 0.14s; }
        .media-grid > *:nth-child(8) { animation-delay: 0.16s; }
        input[type="range"] { -webkit-appearance: none; appearance: none; background: transparent; outline: none; cursor: pointer; }
        input[type="range"]::-webkit-slider-thumb { -webkit-appearance: none; appearance: none; width: 10px; height: 10px; border-radius: 50%; background: rgba(255,255,255,0.6); border: none; cursor: pointer; }
        input[type="range"]::-moz-range-thumb { width: 10px; height: 10px; border-radius: 50%; background: rgba(255,255,255,0.6); border: none; cursor: pointer; }
        input[type="range"]:hover::-webkit-slider-thumb { background: rgba(255,255,255,0.8); }
        input[type="range"]:hover::-moz-range-thumb { background: rgba(255,255,255,0.8); }
        @keyframes spotify-bar { 0%, 100% { transform: scaleY(0.4); } 50% { transform: scaleY(1); } }
        .animate-spotify-bar { animation: spotify-bar 0.8s ease-in-out infinite; transform-origin: bottom; }
      `}</style>

      {notification && (
        <div className={`fixed top-20 right-6 z-50 px-5 py-3.5 rounded-2xl shadow-2xl flex items-center gap-3 animate-slide-in backdrop-blur-sm ${
          notification.type === 'success' ? 'bg-emerald-50/95 border border-emerald-200 text-emerald-700' :
          notification.type === 'warning' ? 'bg-amber-50/95 border border-amber-200 text-amber-700' :
          'bg-red-50/95 border border-red-200 text-red-700'
        }`}>
          {notification.type === 'success' ? (
            <svg className="w-5 h-5 shrink-0" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M9 12.75L11.25 15 15 9.75M21 12a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
          ) : (
            <svg className="w-5 h-5 shrink-0" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M12 9v3.75m9-.75a9 9 0 11-18 0 9 9 0 0118 0zm-9 3.75h.008v.008H12v-.008z" /></svg>
          )}
          <span className="font-medium text-sm">{notification.message}</span>
        </div>
      )}

      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-4 mb-8">
        {STAT_CARDS.map((card, i) => (
          <div key={card.key} className="bg-white rounded-2xl p-5 shadow-sm border border-gray-100 hover:shadow-lg transition-all duration-300 hover:-translate-y-0.5 relative overflow-hidden group">
            <div className={`absolute inset-0 bg-gradient-to-br ${card.gradient} opacity-0 group-hover:opacity-5 transition-opacity duration-500`} />
            <div className="flex items-center gap-3">
              <div className={`w-11 h-11 rounded-xl bg-gradient-to-br ${card.gradient} flex items-center justify-center text-white shadow-sm`}>
                <svg className="w-5 h-5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d={card.icon} /></svg>
              </div>
              <div>
                <p className="text-2xl font-bold text-gray-900 tabular-nums">{stats[card.key]}</p>
                <p className="text-xs text-gray-500 font-medium">{card.label}</p>
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className="bg-white rounded-2xl shadow-sm border border-gray-100 mb-6 overflow-hidden">
        <div className="flex flex-col lg:flex-row lg:items-center lg:justify-between gap-4 p-5 border-b border-gray-100">
          <div className="flex items-center gap-2 overflow-x-auto scrollbar-none">
            {FILTER_TABS.map(t => (
              <button key={t.key} onClick={() => { setActiveTab(t.key); loadFiles(1, t.key === 'All' ? null : t.key) }}
                className={`flex items-center gap-1.5 px-3.5 py-2 rounded-xl text-sm font-medium transition-all duration-200 whitespace-nowrap ${
                  activeTab === t.key
                    ? 'bg-indigo-50 text-indigo-700 shadow-sm'
                    : 'text-gray-500 hover:text-gray-700 hover:bg-gray-50'
                }`}>
                <span className={activeTab === t.key ? 'text-indigo-500' : 'text-gray-400'}>{t.icon}</span>
                {t.label}
              </button>
            ))}
          </div>
          <div className="flex items-center gap-3">
            <div className="relative">
              <svg className="w-4 h-4 text-gray-400 absolute left-3 top-1/2 -translate-y-1/2" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z" /></svg>
              <input ref={searchRef} type="text" value={searchQuery} onChange={e => setSearchQuery(e.target.value)}
                className="w-48 lg:w-56 pl-9 pr-3 py-2 text-sm border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-indigo-100 focus:border-indigo-400 transition-all placeholder:text-gray-400"
                placeholder="Search files..." />
            </div>
            <label className="inline-flex items-center gap-2 px-4 py-2 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-all duration-200 font-medium text-sm cursor-pointer shadow-sm hover:shadow-md active:scale-95">
              <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M12 4.5v15m7.5-7.5h-15" /></svg>
              Upload
              <input type="file" multiple className="hidden" accept="image/*,audio/*,.pdf,.doc,.docx,.xls,.xlsx,.ppt,.pptx"
                onChange={e => { if (e.target.files.length) handleBulkUpload(Array.from(e.target.files)); e.target.value = '' }} />
            </label>
            <button onClick={() => setShowVideoPanel(true)}
              className="inline-flex items-center gap-2 px-4 py-2 bg-gradient-to-r from-rose-500 to-pink-600 text-white rounded-xl hover:from-rose-600 hover:to-pink-700 transition-all duration-200 font-medium text-sm shadow-sm hover:shadow-md active:scale-95">
              <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M15.75 10.5l4.72-4.72a.75.75 0 011.28.53v11.38a.75.75 0 01-1.28.53l-4.72-4.72M4.5 18.75h9a2.25 2.25 0 002.25-2.25v-9a2.25 2.25 0 00-2.25-2.25h-9A2.25 2.25 0 002.25 7.5v9a2.25 2.25 0 002.25 2.25z" /></svg>
              Video Link
            </button>
            <button onClick={() => { setShowDeleted(!showDeleted); if (!showDeleted) loadDeleted() }}
              className={`inline-flex items-center gap-2 px-4 py-2 rounded-xl transition-all duration-200 font-medium text-sm active:scale-95 ${
                showDeleted
                  ? 'bg-red-50 text-red-700 border border-red-200 shadow-sm'
                  : 'bg-gray-50 text-gray-600 hover:bg-gray-100 border border-gray-200'
              }`}>
              <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" /></svg>
              Trash {deletedFiles.length > 0 && <span className="bg-red-500 text-white text-xs w-5 h-5 rounded-full flex items-center justify-center">{deletedFiles.length}</span>}
            </button>
          </div>
        </div>

        {!showDeleted ? (
          loading ? (
            <div className="p-16 text-center">
              <div className="w-14 h-14 border-4 border-indigo-100 border-t-indigo-500 rounded-full animate-spin mx-auto" />
              <p className="mt-4 text-gray-500 text-sm font-medium">Loading your media...</p>
            </div>
          ) : filteredFiles.length === 0 ? (
            <div className="p-16 text-center"
              onDragOver={e => { e.preventDefault(); setDragOver(true) }}
              onDragLeave={() => setDragOver(false)}
              onDrop={handleDrop}>
              <div className={`w-24 h-24 rounded-2xl flex items-center justify-center mx-auto mb-5 transition-all duration-300 ${dragOver ? 'bg-indigo-100 scale-110' : 'bg-gray-50'}`}>
                <svg className={`w-12 h-12 transition-colors duration-300 ${dragOver ? 'text-indigo-500' : 'text-gray-300'}`} fill="none" stroke="currentColor" strokeWidth={1} viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" d="M12 16.5V9.75m0 0l3 3m-3-3l-3 3M6.75 19.5a4.5 4.5 0 01-1.41-8.775 5.25 5.25 0 0110.233-2.33 3 3 0 013.758 3.848A3.752 3.752 0 0118 19.5H6.75z" />
                </svg>
              </div>
              <h3 className="text-lg font-semibold text-gray-900 mb-1">No files {searchQuery ? 'matched' : 'yet'}</h3>
              <p className="text-gray-500 text-sm mb-6">{searchQuery ? 'Try a different search term' : 'Drop files or use the upload buttons above'}</p>
              {!searchQuery && (
                <label className="inline-flex items-center gap-2 px-5 py-2.5 bg-indigo-600 text-white rounded-xl hover:bg-indigo-700 transition-all font-medium text-sm cursor-pointer shadow-sm hover:shadow-md active:scale-95">
                  <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M12 4.5v15m7.5-7.5h-15" /></svg>
                  Upload your first file
                  <input type="file" className="hidden" onChange={e => { if (e.target.files.length) handleBulkUpload(Array.from(e.target.files)); e.target.value = '' }} />
                </label>
              )}
            </div>
          ) : (
            <div className="p-6"
              onDragOver={e => { e.preventDefault(); setDragOver(true) }}
              onDragLeave={() => setDragOver(false)}
              onDrop={handleDrop}>
              {dragOver && (
                <div className="absolute inset-0 bg-indigo-50/80 backdrop-blur-sm z-10 flex items-center justify-center rounded-2xl">
                  <div className="text-center">
                    <svg className="w-16 h-16 text-indigo-400 mx-auto mb-3" fill="none" stroke="currentColor" strokeWidth={1} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M12 16.5V9.75m0 0l3 3m-3-3l-3 3M6.75 19.5a4.5 4.5 0 01-1.41-8.775 5.25 5.25 0 0110.233-2.33 3 3 0 013.758 3.848A3.752 3.752 0 0118 19.5H6.75z" /></svg>
                    <p className="text-lg font-semibold text-indigo-600">Drop files to upload</p>
                  </div>
                </div>
              )}
              <div className={`relative grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-5 media-grid ${dragOver ? 'opacity-30' : ''}`}>
                {filteredFiles.map(f => <FileCard key={f.id} f={f} />)}
              </div>

              {totalPages > 1 && (
                <div className="flex items-center justify-center gap-3 mt-8 pt-6 border-t border-gray-100">
                  <button disabled={currentPage <= 1} onClick={() => loadFiles(currentPage - 1)}
                    className="px-4 py-2 text-sm font-medium text-gray-600 bg-gray-50 border border-gray-200 rounded-xl hover:bg-gray-100 disabled:opacity-40 disabled:cursor-not-allowed transition-all active:scale-95">
                    <svg className="w-4 h-4 inline mr-1 -mt-0.5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M15.75 19.5L8.25 12l7.5-7.5" /></svg>
                    Previous
                  </button>
                  <span className="text-sm text-gray-500 font-medium px-3">{currentPage} / {totalPages}</span>
                  <button disabled={currentPage >= totalPages} onClick={() => loadFiles(currentPage + 1)}
                    className="px-4 py-2 text-sm font-medium text-gray-600 bg-gray-50 border border-gray-200 rounded-xl hover:bg-gray-100 disabled:opacity-40 disabled:cursor-not-allowed transition-all active:scale-95">
                    Next
                    <svg className="w-4 h-4 inline ml-1 -mt-0.5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M8.25 4.5l7.5 7.5-7.5 7.5" /></svg>
                  </button>
                </div>
              )}
            </div>
          )
        ) : (
          <div className="p-6">
            {deletedFiles.length === 0 ? (
              <div className="text-center py-16">
                <svg className="w-16 h-16 text-gray-200 mx-auto mb-4" fill="none" stroke="currentColor" strokeWidth={1} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" /></svg>
                <h3 className="text-lg font-semibold text-gray-900 mb-1">Trash is empty</h3>
                <p className="text-gray-500 text-sm">Deleted files will appear here</p>
              </div>
            ) : (
              <div className="space-y-2">
                {deletedFiles.map(f => (
                  <div key={f.id} className="flex items-center justify-between p-4 bg-gray-50 rounded-xl hover:bg-gray-100 transition-colors group">
                    <div className="flex items-center gap-4 min-w-0">
                      <div className="w-10 h-10 bg-white rounded-xl flex items-center justify-center shadow-sm shrink-0">
                        {f.mediaType === 'Image' && f.url ? (
                          <img src={f.url} alt="" className="w-full h-full object-cover rounded-xl" />
                        ) : (
                          <svg className="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M19.5 14.25v-2.625a3.375 3.375 0 00-3.375-3.375h-1.5A1.125 1.125 0 0113.5 7.125v-1.5a3.375 3.375 0 00-3.375-3.375H8.25m2.25 0H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 00-9-9z" /></svg>
                        )}
                      </div>
                      <div className="min-w-0">
                        <p className="text-sm font-medium text-gray-700 truncate">{f.originalFileName}</p>
                        <p className="text-xs text-gray-400 flex items-center gap-2">
                          {f.fileType && <span className="text-[10px] font-semibold uppercase text-gray-400 bg-white px-1.5 py-0.5 rounded">{f.fileType}</span>}
                          <span className={`px-1.5 py-0.5 rounded text-[10px] font-medium ${f.mediaType === 'Image' ? 'bg-sky-100 text-sky-600' : f.mediaType === 'Audio' ? 'bg-purple-100 text-purple-600' : f.mediaType === 'VideoLink' ? 'bg-pink-100 text-pink-600' : 'bg-amber-100 text-amber-600'}`}>
                            {TYPE_LABELS[f.mediaType]}
                          </span>
                          Deleted {formatDate(f.deletedAt)}
                        </p>
                      </div>
                    </div>
                    <div className="flex items-center gap-2 shrink-0">
                      <button onClick={() => handleRestore(f)} className="px-3.5 py-1.5 text-sm font-medium text-indigo-600 hover:bg-indigo-50 rounded-xl transition-colors flex items-center gap-1.5">
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M9 15L3 9m0 0l6-6M3 9h12a6 6 0 010 12h-3" /></svg>
                        Restore
                      </button>
                      {isAdmin && (
                        <button onClick={() => handleHardDelete(f)} className="px-3.5 py-1.5 text-sm font-medium text-red-600 hover:bg-red-50 rounded-xl transition-colors flex items-center gap-1.5">
                          <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0" /></svg>
                          Delete Forever
                        </button>
                      )}
                    </div>
                  </div>
                ))}
              </div>
            )}
          </div>
        )}
      </div>

      {showVideoPanel && (
        <>
          <div className="fixed inset-0 bg-black/30 backdrop-blur-sm z-40 transition-opacity" onClick={() => setShowVideoPanel(false)} />
          <div className="fixed inset-y-0 right-0 w-full max-w-md bg-white shadow-2xl z-50 flex flex-col">
            <div className="flex items-center justify-between p-6 border-b border-gray-100">
              <div>
                <h2 className="text-xl font-semibold text-gray-900">Add Video Link</h2>
                <p className="text-sm text-gray-500 mt-0.5">Embed from YouTube or Vimeo</p>
              </div>
              <button onClick={() => setShowVideoPanel(false)} className="p-2 text-gray-400 hover:text-gray-600 hover:bg-gray-100 rounded-xl transition-colors">
                <svg className="w-5 h-5" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" /></svg>
              </button>
            </div>
            <form onSubmit={handleVideoSubmit} className="flex-1 overflow-y-auto">
              <div className="p-6 space-y-5">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1.5">Video URL <span className="text-red-400">*</span></label>
                  <input type="url" value={videoForm.videoUrl} onChange={e => setVideoForm({ ...videoForm, videoUrl: e.target.value })}
                    className={`w-full px-4 py-3 border rounded-xl focus:outline-none focus:ring-2 focus:ring-rose-200 focus:border-rose-400 transition-all text-sm ${videoErrors.videoUrl ? 'border-red-300 bg-red-50' : 'border-gray-200 hover:border-gray-300'}`}
                    placeholder="https://youtube.com/watch?v=..." />
                  {videoErrors.videoUrl && <p className="mt-1.5 text-sm text-red-500 flex items-center gap-1"><svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M12 9v3.75m-9.303 3.376c-.866 1.5.217 3.374 1.948 3.374h14.71c1.73 0 2.813-1.874 1.948-3.374L13.949 3.378c-.866-1.5-3.032-1.5-3.898 0L2.697 16.126zM12 15.75h.007v.008H12v-.008z" /></svg>{videoErrors.videoUrl}</p>}
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1.5">Title</label>
                  <input type="text" value={videoForm.title} onChange={e => setVideoForm({ ...videoForm, title: e.target.value })}
                    className="w-full px-4 py-3 border border-gray-200 rounded-xl focus:outline-none focus:ring-2 focus:ring-rose-200 focus:border-rose-400 transition-all text-sm hover:border-gray-300"
                    placeholder="Optional title for display" />
                </div>
                {(isYouTube(videoForm.videoUrl) || isVimeo(videoForm.videoUrl)) && (
                  <div className="bg-emerald-50 border border-emerald-200 rounded-xl p-4">
                    <div className="flex items-start gap-3">
                      <svg className="w-5 h-5 text-emerald-600 mt-0.5 shrink-0" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M9 12.75L11.25 15 15 9.75M21 12a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
                      <p className="text-sm text-emerald-700">Video source recognized. It will be embedded with autoplay.</p>
                    </div>
                  </div>
                )}
              </div>
              <div className="p-6 border-t border-gray-100 bg-gray-50/50">
                <div className="flex items-center justify-end gap-3">
                  <button type="button" onClick={() => setShowVideoPanel(false)} className="px-5 py-2.5 text-gray-600 hover:bg-gray-100 rounded-xl transition-colors font-medium text-sm">
                    Cancel
                  </button>
                  <button type="submit" disabled={uploading}
                    className="px-6 py-2.5 bg-gradient-to-r from-rose-500 to-pink-600 text-white rounded-xl hover:from-rose-600 hover:to-pink-700 transition-all font-medium text-sm flex items-center gap-2 disabled:opacity-50 shadow-sm hover:shadow-md active:scale-95">
                    {uploading ? (
                      <><svg className="w-4 h-4 animate-spin" fill="none" viewBox="0 0 24 24"><circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4" /><path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z" /></svg> Adding...</>
                    ) : 'Add Video Link'}
                  </button>
                </div>
              </div>
            </form>
          </div>
        </>
      )}

      {uploading && (
        <div className="fixed inset-0 bg-black/20 backdrop-blur-sm z-50 flex items-center justify-center">
          <div className="bg-white rounded-3xl p-8 shadow-2xl flex flex-col items-center gap-4 animate-scale-in">
            <div className="w-14 h-14 border-4 border-indigo-100 border-t-indigo-500 rounded-full animate-spin" />
            <p className="text-sm font-medium text-gray-600">Uploading files...</p>
          </div>
        </div>
      )}

      {lightboxFile && (
        <div className="fixed inset-0 bg-black/95 z-[60] flex items-center justify-center p-4 animate-scale-in" onClick={() => setLightboxFile(null)}>
          <button className="absolute top-5 right-5 text-white/60 hover:text-white p-2 rounded-full hover:bg-white/10 transition-all" onClick={() => setLightboxFile(null)}>
            <svg className="w-7 h-7" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" /></svg>
          </button>
          <div className="max-w-6xl max-h-[90vh] w-full flex flex-col items-center" onClick={e => e.stopPropagation()}>
            <img src={lightboxFile.url} alt={lightboxFile.altText || lightboxFile.originalFileName} className="max-w-full max-h-[75vh] rounded-2xl shadow-2xl object-contain" />
            <div className="mt-5 flex items-center gap-4 text-sm">
              <span className="text-white font-semibold">{lightboxFile.originalFileName}</span>
              <span className="text-white/40">&middot;</span>
              <span className="text-white/60">{formatBytes(lightboxFile.fileSizeBytes)}</span>
              <span className="text-white/40">&middot;</span>
              <span className="text-white/60">{lightboxFile.altText || 'No alt text'}</span>
            </div>
          </div>
        </div>
      )}

      {audioFile && <AudioPlayerModal audioFile={audioFile} onClose={() => setAudioFile(null)} />}

      {videoLinkFile && (
        <div className="fixed inset-0 bg-black/60 z-[60] flex items-center justify-center p-4" onClick={() => setVideoLinkFile(null)}>
          <div className="w-full max-w-lg rounded-xl overflow-hidden" style={{ background: '#fff', boxShadow: '0 25px 60px rgba(0,0,0,0.25)' }} onClick={e => e.stopPropagation()}>
            {/* Gradient header */}
            <div className="px-5 pt-5 pb-4" style={{ background: 'linear-gradient(135deg, #7c3aed, #a855f7, #d946ef)' }}>
              <div className="flex items-center justify-between">
                <div className="min-w-0 flex-1">
                  <p className="text-sm font-semibold text-white truncate pr-2">{videoLinkFile.title || videoLinkFile.originalFileName || 'Untitled Video'}</p>
                  <p className="text-xs mt-0.5 text-white/60 truncate max-w-[300px]">{videoLinkFile.videoLinkUrl}</p>
                </div>
                <button onClick={() => setVideoLinkFile(null)}
                  className="w-7 h-7 rounded-full flex items-center justify-center shrink-0"
                  style={{ background: 'rgba(255,255,255,0.15)' }}
                  onMouseEnter={e => e.currentTarget.style.background = 'rgba(255,255,255,0.25)'}
                  onMouseLeave={e => e.currentTarget.style.background = 'rgba(255,255,255,0.15)'}
                >
                  <svg className="w-3.5 h-3.5 text-white/70" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" /></svg>
                </button>
              </div>
            </div>

            {/* Video area */}
            <div className="aspect-video bg-black relative group">
              {(() => {
                const src = getVideoEmbedSrc(videoLinkFile.videoLinkUrl)
                return src ? (
                  <iframe src={src} className="w-full h-full" frameBorder="0" allow="autoplay; encrypted-media; fullscreen" allowFullScreen title={videoLinkFile.title || 'Video'} />
                ) : (
                  <div className="w-full h-full flex items-center justify-center" style={{ background: '#fafafa' }}>
                    <div className="text-center px-6">
                      <div className="w-14 h-14 mx-auto rounded-full flex items-center justify-center mb-3" style={{ background: '#f3e8ff' }}>
                        <svg className="w-6 h-6" style={{ color: '#7c3aed' }} fill="none" stroke="currentColor" strokeWidth={1.5} viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" d="M14.752 11.168l-3.197-2.132A1 1 0 0010 9.87v4.263a1 1 0 001.555.832l3.197-2.132a1 1 0 000-1.664z" /><path strokeLinecap="round" strokeLinejoin="round" d="M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                        </svg>
                      </div>
                      <p className="text-sm font-medium mb-3" style={{ color: '#525252' }}>This platform is not supported for embedding</p>
                      <a href={videoLinkFile.videoLinkUrl} target="_blank" rel="noopener noreferrer"
                        className="inline-flex items-center gap-2 px-4 py-2 text-sm font-medium rounded-lg transition-colors"
                        style={{ background: 'linear-gradient(135deg, #7c3aed, #a855f7)', color: '#fff' }}>
                        Open in new tab
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M13.5 6H5.25A2.25 2.25 0 003 8.25v10.5A2.25 2.25 0 005.25 21h10.5A2.25 2.25 0 0018 18.75V10.5m-10.5 6L21 3m0 0h-5.25M21 3v5.25" /></svg>
                      </a>
                    </div>
                  </div>
                )
              })()}

              {/* Play button overlay */}
              {!getVideoEmbedSrc(videoLinkFile.videoLinkUrl) && (
                <div className="absolute inset-0 flex items-center justify-center pointer-events-none">
                  <div className="w-16 h-16 rounded-full flex items-center justify-center" style={{ background: 'rgba(124,58,237,0.15)', backdropFilter: 'blur(4px)' }}>
                    <svg className="w-7 h-7 text-white ml-0.5" fill="currentColor" viewBox="0 0 24 24"><path d="M8 5v14l11-7z" /></svg>
                  </div>
                </div>
              )}
            </div>

            {/* Footer */}
            <div className="flex items-center justify-between px-5 py-3" style={{ background: '#fafafa' }}>
              <a href={videoLinkFile.videoLinkUrl} target="_blank" rel="noopener noreferrer"
                className="inline-flex items-center gap-1.5 text-sm font-medium transition-colors"
                style={{ color: '#7c3aed' }}
                onMouseEnter={e => e.currentTarget.style.color = '#6d28d9'}
                onMouseLeave={e => e.currentTarget.style.color = '#7c3aed'}
              >
                <svg className="w-4 h-4" fill="none" stroke="currentColor" strokeWidth={2} viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" d="M13.5 6H5.25A2.25 2.25 0 003 8.25v10.5A2.25 2.25 0 005.25 21h10.5A2.25 2.25 0 0018 18.75V10.5m-10.5 6L21 3m0 0h-5.25M21 3v5.25" /></svg>
                Open original
              </a>
              <span className="text-xs" style={{ color: '#a3a3a3' }}>{videoLinkFile.mediaType === 'VideoLink' ? 'Video Link' : 'Media'}</span>
            </div>
          </div>
        </div>
      )}
    </Layout>
  )
}