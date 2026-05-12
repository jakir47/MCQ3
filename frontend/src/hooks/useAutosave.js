import { useEffect, useRef } from 'react'
import { saveAttempt } from '../api/attempts'

export function useAutosave(attemptId, getPayload, intervalMs = 30000) {
  const timerRef = useRef(null)

  useEffect(() => {
    timerRef.current = setInterval(async () => {
      try {
        await saveAttempt(attemptId, getPayload())
      } catch { /* silent */ }
    }, intervalMs)
    return () => clearInterval(timerRef.current)
  }, [attemptId, intervalMs, getPayload])
}