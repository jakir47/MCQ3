import { useState, useEffect, useRef } from 'react'

export function useExamTimer(initialSeconds, onExpire) {
  const [remaining, setRemaining] = useState(initialSeconds)
  const onExpireRef = useRef(onExpire)

  useEffect(() => {
    onExpireRef.current = onExpire
  })

  useEffect(() => {
    setRemaining(initialSeconds)
  }, [initialSeconds])

  useEffect(() => {
    if (remaining <= 0) return

    const id = setInterval(() => {
      setRemaining(s => {
        if (s <= 1) {
          clearInterval(id)
          onExpireRef.current?.()
          return 0
        }
        return s - 1
      })
    }, 1000)
    return () => clearInterval(id)
  }, [remaining])

  const formatted = `${Math.floor(remaining / 60).toString().padStart(2, '0')}:${(remaining % 60).toString().padStart(2, '0')}`

  return { remaining, formatted, isWarning: remaining <= 300 }
}
