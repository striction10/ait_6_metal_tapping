import { useState } from 'react'
import './SendPopup.css'

function SendPopup({ isOpen, onClose, onSubmit }) {
    const [selectedFile, setSelectedFile] = useState(null)
    const [email, setEmail] = useState('')
    const [isSending, setIsSending] = useState(false)
    const [sendStatus, setSendStatus] = useState(null)

    if (!isOpen) return null

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) handleClose()
    }

    const handleKeyDown = (e) => {
        if (e.key === 'Escape') handleClose()
    }

    const handleClose = () => {
        setSelectedFile(null)
        setEmail('')
        setIsSending(false)
        setSendStatus(null)
        onClose()
    }

    const handleFileChange = (e) => {
        const file = e.target.files?.[0]
        if (file) {
            setSelectedFile(file)
            setSendStatus(null)
        }
    }

    const handleDrop = (e) => {
        e.preventDefault()
        const file = e.dataTransfer.files?.[0]
        if (file) {
            setSelectedFile(file)
            setSendStatus(null)
        }
    }

    const handleDragOver = (e) => {
        e.preventDefault()
    }

    const handleSend = async () => {
        if (!selectedFile || !email.trim()) return
        setIsSending(true)
        setSendStatus(null)
        
        try {
            onSubmit?.({ file: selectedFile, email: email.trim() })
            setSendStatus('success')
            setTimeout(() => handleClose(), 1500)
        } catch (err) {
            setSendStatus('error')
        } finally {
            setIsSending(false)
        }
    }

    const isFormValid = selectedFile && email.trim() && !isSending

    const isValidEmail = (val) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val)
    const emailHasError = email.trim() !== '' && !isValidEmail(email)

    return (
        <div className="sp-overlay" onClick={handleOverlayClick} onKeyDown={handleKeyDown} role="dialog" aria-modal="true" aria-label="Отправка файла">
            <div className="sp-card">
                <button className="sp-close" onClick={handleClose} aria-label="Закрыть">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                </button>

                {sendStatus === 'success' ? (
                    <div className="sp-success">
                        <div className="sp-success-icon">
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/>
                                <polyline points="22 4 12 14.01 9 11.01"/>
                            </svg>
                        </div>
                        <h3>Отправлено!</h3>
                        <p>Файл успешно отправлен на {email}</p>
                    </div>
                ) : (
                    <>
                        <div className="sp-header">
                            <div className="sp-icon">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                    <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"/>
                                    <polyline points="22,6 12,13 2,6"/>
                                </svg>
                            </div>
                            <h3>Отправить на email</h3>
                            <p>Прикрепите файл и укажите адрес получателя</p>
                        </div>

                        <div className="sp-body">
                            <div className="sp-field">
                                <label className="sp-field-label">Email получателя</label>
                                <div className={`sp-input-wrap ${emailHasError ? 'sp-input-error' : ''}`}>
                                    <svg className="sp-input-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                        <path d="M4 4h16c1.1 0 2 .9 2 2v12c0 1.1-.9 2-2 2H4c-1.1 0-2-.9-2-2V6c0-1.1.9-2 2-2z"/>
                                        <polyline points="22,6 12,13 2,6"/>
                                    </svg>
                                    <input 
                                        type="email" 
                                        className="sp-input" 
                                        placeholder="example@mail.ru" 
                                        value={email}
                                        onChange={(e) => { setEmail(e.target.value); if (sendStatus === 'error') setSendStatus(null) }}
                                    />
                                </div>
                                {emailHasError && <span className="sp-field-hint sp-field-hint-error">Введите корректный email</span>}
                            </div>
                            <div className="sp-field">
                                <label className="sp-field-label">Файл</label>
                                {selectedFile ? (
                                    <div className="sp-file-selected">
                                        <div className="sp-file-selected-icon">
                                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                                <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"/>
                                                <polyline points="14 2 14 8 20 8"/>
                                            </svg>
                                        </div>
                                        <div className="sp-file-selected-info">
                                            <span className="sp-file-name">{selectedFile.name}</span>
                                            <span className="sp-file-size">{(selectedFile.size / 1024).toFixed(1)} КБ</span>
                                        </div>
                                        <button className="sp-file-remove" onClick={() => setSelectedFile(null)} aria-label="Удалить файл">
                                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                                        </button>
                                    </div>
                                ) : (
                                    <label 
                                        className="sp-upload-area" 
                                        htmlFor="sp-file-input"
                                        onDrop={handleDrop}
                                        onDragOver={handleDragOver}
                                    >
                                        <div className="sp-upload-icon">
                                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
                                                <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"/>
                                                <polyline points="17 8 12 3 7 8"/>
                                                <line x1="12" y1="3" x2="12" y2="15"/>
                                            </svg>
                                        </div>
                                        <span className="sp-upload-text">Перетащите файл сюда</span>
                                        <span className="sp-upload-sub">или нажмите для выбора</span>
                                        <input type="file" id="sp-file-input" accept=".xlsx,.csv,.pdf" className="sp-file-input" onChange={handleFileChange} />
                                    </label>
                                )}
                            </div>
                        </div>

                        <div className="sp-footer">
                            <button className="sp-btn sp-btn-cancel" onClick={handleClose}>Отмена</button>
                            <button 
                                className={`sp-btn sp-btn-send ${isFormValid ? 'sp-btn-send-active' : ''}`} 
                                onClick={handleSend}
                                disabled={!isFormValid}
                            >
                                {isSending ? (
                                    <>
                                        <div className="sp-spinner"></div>
                                        <span>Отправка...</span>
                                    </>
                                ) : (
                                    <>
                                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                            <line x1="22" y1="2" x2="11" y2="13"/>
                                            <polygon points="22 2 15 22 11 13 2 9 22 2"/>
                                        </svg>
                                        <span>Отправить</span>
                                    </>
                                )}
                            </button>
                        </div>
                    </>
                )}
            </div>
        </div>
    )
}
export default SendPopup