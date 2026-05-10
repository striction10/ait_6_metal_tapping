import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import AuthBox from '../../components/AuthBox/AuthBox'
import AuthForm from '../../components/AuthForm/AuthForm'
import { authApi } from '../../services/auth'
import { saveUserFromToken } from '../../utils/auth'
import './Auth.css'

function Auth() {
    const navigate = useNavigate()
    const [serverError, setServerError] = useState('')
    const [isLoading, setIsLoading] = useState(false)
    const [shake, setShake] = useState(false)

    useEffect(() => {
        document.title = "Выливка металла"
        
        if (shake) {
            const timer = setTimeout(() => setShake(false), 450)
            return () => clearTimeout(timer)
        }
    }, [shake])

    const handleLogin = async (formData) => {
        setIsLoading(true)
        setServerError('')
        try {
            const response = await authApi.login(formData.username, formData.password)
            localStorage.setItem('token', response.data.token)
            saveUserFromToken(response.data.token)
            navigate('/reglaments')
        } catch (err) {
            const msg = err.response?.status === 401 || err.response?.status === 500 
                ? 'Неверный логин или пароль' 
                : 'Ошибка сервера. Попробуйте позже'
            setServerError(msg)
            setShake(true)
        } finally {
            setIsLoading(false)
        }
    }

    return (
        <div className="auth-container">
            <div className="auth-panel auth-left">
                <div className="auth-left-bg-pattern" />
                <div className="auth-left-content">
                    <h1 className="auth-left-title">Модуль <br /> Выливка металла </h1>
                    <p className="auth-left-desc">Контроль процесса выливки металла в режиме реального времени</p>
                    <div className="auth-left-features">
                        <div className="auth-feature">
                            <div className="auth-feature-icon"><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2"><path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="8.5" cy="7" r="4"/><line x1="20" y1="8" x2="20" y2="14"/><line x1="23" y1="11" x2="17" y2="11"/></svg></div>
                            <span className="auth-feature-text">Регламенты и инструкции</span>
                        </div>
                        <div className="auth-feature">
                            <div className="auth-feature-icon"><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2"><polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/></svg></div>
                            <span className="auth-feature-text">Редактирование параметров выливки</span>
                        </div>
                        <div className="auth-feature">
                            <div className="auth-feature-icon"><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2"><rect x="3" y="3" width="18" height="18" rx="2"/><path d="M3 9h18"/><path d="M9 21V9"/></svg></div>
                            <span className="auth-feature-text">Формирование заданий на смены</span>
                        </div>
                    </div>
                </div>
            </div>

            <div className="auth-panel auth-right">
                <div className="auth-right-inner">
                    <AuthBox title="Вход в систему" subtitle="Введите данные вашей учётной записи" className={`auth-form-wrapper ${shake ? 'shake-form' : ''}`}>
                        {serverError && (
                            <div className="error-banner-auth">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><circle cx="12" cy="12" r="10"/><line x1="12" y1="8" x2="12" y2="12"/><line x1="12" y1="16" x2="12.01" y2="16"/></svg>
                                <span>{serverError}</span>
                            </div>
                        )}
                        <AuthForm onSubmit={handleLogin} disabled={isLoading} />
                    </AuthBox>
                </div>
                <div className="auth-right-footer">© 2026 Команда №6</div>
            </div>
        </div>
    )
}

export default Auth
