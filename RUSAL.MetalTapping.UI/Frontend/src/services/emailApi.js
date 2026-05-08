import api from './api'

export const emailApi = {
    sendEmail: (email, file, options = {}) => {
        const { subject, message, pageType, date } = options
        
        let finalSubject = subject
        let finalMessage = message
        
        if (!finalSubject && pageType) {
            if (pageType === 'reglaments') {
                finalSubject = date 
                    ? `Регламенты от ${new Date(date).toLocaleDateString()}`
                    : 'Регламенты'
            } else if (pageType === 'tasks') {
                finalSubject = date 
                    ? `Задания от ${new Date(date).toLocaleDateString()}`
                    : 'Задания'
            } else if (pageType === 'parameters') {
                finalSubject = date 
                    ? `Параметры от ${new Date(date).toLocaleDateString()}`
                    : 'Параметры'
            } else {
                finalSubject = 'Данные о выливке металла'
            }
        }
        
        if (!finalSubject && pageType && date) {
            finalSubject = `${pageType === 'reglaments' ? 'Регламенты' : pageType === 'tasks' ? 'Задания' : 'Параметры'} от ${new Date(date).toLocaleDateString()}`
        }
        
        if (!finalSubject) {
            finalSubject = 'Данные о выливке металла'
        }
        
        if (!finalMessage) {
            finalMessage = 'Файл с данными прикреплён. Пожалуйста, не отвечайте на это сообщение.'
        }
        
        const formData = new FormData()
        formData.append('email', email)
        formData.append('subject', finalSubject)
        formData.append('message', finalMessage)
        formData.append('pdf', file)
        
        return api.post('/api/Email/send', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
    }
}