import api from './axiosInstance'

export const uploadFile = (file) => {
  const fd = new FormData()
  fd.append('file', file)
  return api.post('/media-file', fd, { headers: { 'Content-Type': 'multipart/form-data' } })
}

export const uploadVideoLink = (videoUrl, title) =>
  api.post('/media-file/link', { VideoUrl: videoUrl, Title: title })

export const uploadBulk = (files) => {
  const fd = new FormData()
  files.forEach(f => fd.append('files', f))
  return api.post('/media-file/bulk', fd, { headers: { 'Content-Type': 'multipart/form-data' } })
}

export const getMediaFiles = (type, page = 1, pageSize = 20) =>
  api.get('/media-file', { params: { type, page, pageSize } })

export const getMediaById = (id) => api.get(`/media-file/${id}`)

export const updateAltText = (id, altText) =>
  api.patch(`/media-file/${id}/alt`, { AltText: altText })

export const softDelete = (id) => api.delete(`/media-file/${id}`)

export const restore = (id) => api.post(`/media-file/${id}/restore`)

export const hardDelete = (id) => api.delete(`/media-file/${id}/purge`)