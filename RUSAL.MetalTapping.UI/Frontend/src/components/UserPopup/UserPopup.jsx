import { useNavigate } from 'react-router-dom'
import { getUserData } from '../../utils/auth'
import "./UserPopup.css"

function UserPopup({ isOpen, onClose }) {
    const navigate = useNavigate()
    const { login, role } = getUserData()
    const initial = login?.charAt(0).toUpperCase() || 'U'

    if (!isOpen) return null

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) onClose()
    }

    const handleKeyDown = (e) => {
        if (e.key === 'Escape') onClose()
    }

    const handleLogout = () => {
        localStorage.removeItem('token')
        localStorage.removeItem('user')
        onClose()
        navigate('/auth')
    }

    return (
        <>
            <div className="cp-overlay" onClick={handleOverlayClick} onKeyDown={handleKeyDown} role="dialog" aria-modal="true" aria-label="Профиль пользователя">
                <div className="cp-card">
                    <button className="cp-close-btn" onClick={onClose} aria-label="Закрыть">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeLinecap="round" strokeLinejoin="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                    </button>

                    <div className="cp-header">
                        <div className="cp-avatar-wrapper">
                            <div className="cp-avatar-ring" />
                            <div className="cp-avatar" aria-hidden="true">{initial}</div>
                        </div>
                        <div className="cp-username">{login}</div>
                        <div className="cp-subtitle">Выливка металла</div>
                    </div>

                    <div className="cp-body">
                        <div className="cp-info-row">
                            <div className="cp-info-left">
                                <div className="cp-icon">
                                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeLinecap="round" strokeLinejoin="round"><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></svg>
                                </div>
                                <div><div className="cp-label">Логин</div><div className="cp-value">{login}</div></div>
                            </div>
                        </div>

                        <div className="cp-info-row">
                            <div className="cp-info-left">
                                <div className="cp-icon">
                                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeLinecap="round" strokeLinejoin="round"><path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z"/></svg>
                                </div>
                                <div><div className="cp-label">Роль</div><div className="cp-value"><span className="cp-badge">{role}</span></div></div>
                            </div>
                        </div>

                        <div className="cp-divider" />
                    </div>

                    <div className="cp-footer">
                        <button className="cp-btn-logout" onClick={handleLogout}>
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeLinecap="round" strokeLinejoin="round"><path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/><polyline points="16 17 21 12 16 7"/><line x1="21" y1="12" x2="9" y2="12"/></svg>
                            <span>Выйти из системы</span>
                        </button>
                    </div>

                    <div className="cp-status">
                        <div className="cp-dot" />
                        <span>Сессия активна</span>
                    </div>
                </div>
            </div>
        </>
    )
}

export default UserPopup
