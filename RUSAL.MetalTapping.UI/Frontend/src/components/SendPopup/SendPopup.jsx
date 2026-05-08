import { useState, useRef } from 'react'
import { emailApi } from '../../services/emailApi'
import './SendPopup.css'

function SendPopup({ isOpen, onClose, pageType, selectedDate }) {
    const [email, setEmail] = useState('')
    const [selectedFile, setSelectedFile] = useState(null)
    const [isSending, setIsSending] = useState(false)
    const fileInputRef = useRef(null)

    if (!isOpen) return null

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) handleClose()
    }

    const handleClose = () => {
        setEmail('')
        setSelectedFile(null)
        setIsSending(false)
        onClose()
    }

    const handleFileChange = (e) => {
        const file = e.target.files?.[0]
        if (file) setSelectedFile(file)
    }

    const handleSelectFile = () => {
        fileInputRef.current?.click()
    }

    const handleRemoveFile = () => {
        setSelectedFile(null)
        if (fileInputRef.current) {
            fileInputRef.current.value = ''
        }
    }

    const handleSend = async () => {
        if (!selectedFile || !email.trim()) return
        
        setIsSending(true)
        
        try {
            await emailApi.sendEmail(email.trim(), selectedFile, {
                pageType: pageType,
                date: selectedDate
            })
            handleClose()
        } catch (err) {
            console.error('Ошибка:', err)
        } finally {
            setIsSending(false)
        }
    }

    const isValidEmail = (val) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val)
    const isFormValid = selectedFile && email.trim() && isValidEmail(email) && !isSending

    return (
        <div className="sp-overlay" onClick={handleOverlayClick} role="dialog" aria-modal="true">
            <div className="sp-card">
                <button className="sp-close" onClick={handleClose} aria-label="Закрыть">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                        <line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/>
                    </svg>
                </button>

                <div className="sp-header">
                    <div className="sp-icon">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                            <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"/>
                            <polyline points="22,6 12,13 2,6"/>
                        </svg>
                    </div>
                    <h3>Отправить на email</h3>
                </div>

                <div className="sp-body">
                    <div className="sp-field">
                        <label className="sp-field-label">Email получателя</label>
                        <input 
                            type="email" 
                            className="sp-input" 
                            placeholder="example@mail.ru" 
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>

                    <div className="sp-field">
                        <label className="sp-field-label">Файл</label>
                        <input 
                            type="file" 
                            accept=".pdf" 
                            onChange={handleFileChange} 
                            ref={fileInputRef}
                            style={{ display: 'none' }}
                        />
                        {selectedFile ? (
                            <div className="sp-file-selected">
                                <span>{selectedFile.name}</span>
                                <button onClick={handleRemoveFile} type="button">✕</button>
                            </div>
                        ) : (
                            <button type="button" className="sp-select-file-btn" onClick={handleSelectFile}>
                                Выберите файл
                            </button>
                        )}
                    </div>
                </div>

                <div className="sp-footer">
                    <button className="sp-btn-cancel" onClick={handleClose}>Отмена</button>
                    <button 
                        className={`sp-btn-send ${isFormValid ? 'sp-btn-send-active' : ''}`}
                        onClick={handleSend}
                        disabled={!isFormValid}
                    >
                        {isSending ? 'Отправка...' : 'Отправить'}
                    </button>
                </div>
                
            </div>
        </div>
    )
}

export default SendPopup