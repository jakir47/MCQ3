import { useState, useEffect, useRef, useCallback } from 'react'

export function useExamTimer(initialSeconds, onExpire) {
  const [remaining, setRemaining] = useState(initialSeconds)
  const intervalRef = useRef(null)

  useEffect(() => {
    if (remaining <= 0) { onExpire?.(); return }
    intervalRef.current = setInterval(() => {
      setRemaining(s => {
        if (s <= 1) {
          clearInterval(intervalRef.current)
          onExpire?.()
          return 0
        }
        return s - 1
      })
    }, 1000)
    return () => clearInterval(intervalRef.current)
  }, [])

  const format = useCallback(() => {
    const m = Math.floor(remaining / 60).toString().padStart(2, '0')
    const s = (remaining % 60).toString().padStart(2, '0')
    return `${m}:${s}`
  }, [remaining])

  return { remaining, formatted: format(), isWarning: remaining <= 300 }
}