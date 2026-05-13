import { useEffect, useRef } from 'react'
import { saveAttempt } from '../api/attempts'

export function useAutosave(attemptId, getPayload, intervalMs = 30000) {
  const getPayloadRef = useRef(getPayload)

  useEffect(() => {
    getPayloadRef.current = getPayload
  })

  useEffect(() => {
    if (!attemptId) return

    const id = setInterval(async () => {
      try {
        await saveAttempt(attemptId, getPayloadRef.current())
      } catch { /* silent */ }
    }, intervalMs)
    return () => clearInterval(id)
  }, [attemptId, intervalMs])
}
