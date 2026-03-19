import { useState } from 'react'
import Input from "../../components/Input/Input"
import './SendPopup.css'

function SendPopup({ isOpen, onClose, onUpload }) {
    const [selectedFile, setSelectedFile] = useState(null)
    const [fileName, setFileName] = useState('')

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
        if (selectedFile) {
            onUpload(selectedFile)
            setSelectedFile(null)
            setFileName('')
            onClose()
        }
    }

    const handleCancel = () => {
        setSelectedFile(null)
        setFileName('')
        onClose()
    }

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
                        accept=".xlsx,.xls,.csv,.txt"
                    />
                    
                    {fileName && (
                        <p className="file-name">Выбран: {fileName}</p>
                    )}

                    <Input
                        label="Получатель"
                        type="text"
                        name="email"
                        placeholder="Введите получателя"
                    />
                </div>
                
                <div className="popup-buttons">
                    <button 
                        id="close" 
                        onClick={handleUpload}
                        disabled={!selectedFile}
                    >
                        Отправить
                    </button>
                </div>
            </div>
        </div>
    )
}

export default SendPopup