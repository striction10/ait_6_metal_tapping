import { useState, useEffect } from 'react'
import Input from "../../components/Input/Input"
import './SendPopup.css'

function SendPopup({ isOpen, onClose, onUpload }) {
    const [selectedFile, setSelectedFile] = useState(null)
    const [fileName, setFileName] = useState('')
    const [email, setEmail] = useState('')

    useEffect(() => {
        if (!isOpen) {
            setSelectedFile(null)
            setFileName('')
            setEmail('')
        }
    }, [isOpen])

    if (!isOpen) return null

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) {
            onClose()
        }
    }

    const handleFileChange = (e) => {
        const file = e.target.files[0]
        if (file) {
            setSelectedFile(file)
            setFileName(file.name)
        }
    }

    const handleUpload = () => {
        if (selectedFile && email.includes('@')) {
            onUpload(selectedFile)
            onClose()
        }
    }

    const isValidEmail = email.includes('@') && email.split('@')[1]?.includes('.')
    const isValid = selectedFile && isValidEmail

    return (
        <div className="popupContainer" onClick={handleOverlayClick}>
            <div className="popupContent">
                <div className="userForm">
                    <h2>Загрузка файла</h2>
                    
                    <label>Выберите файл</label>
                    <input
                        type="file"
                        onChange={handleFileChange}
                        className="file-input"
                        accept=".pdf"
                    />
                    
                    {fileName && (
                        <p className="file-name">Выбран: {fileName}</p>
                    )}

                    <Input
                        label="Получатель"
                        type="email"
                        name="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="Введите почту"
                    />
                </div>
                
                <div className="popup-buttons">
                    <button 
                        id="close" 
                        onClick={handleUpload}
                        disabled={!isValid}
                        style={{
                            opacity: isValid ? 1 : 0.6,
                            cursor: isValid ? 'pointer' : 'not-allowed'
                        }}
                    >
                        Отправить
                    </button>
                </div>
            </div>
        </div>
    )
}

export default SendPopup