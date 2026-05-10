import { useState } from 'react'
import './Header.css'
import logo from '../../assets/rusalLogoGrey.png'
import SelectGroup from "../SelectGroup/SelectGroup"
import UserPopup from "../UserPopup/UserPopup"
import AdminUsersPopup from "../AdminUsersPopup/AdminUsersPopup"

function Header({ 
    title = "Выливка металла",
    showNav = false,
    showUserBtn = false,
    activeNav = "",
    selectConfig = null
}) {
    const [isUserPopupOpen, setIsUserPopupOpen] = useState(false)
    const [isAdminPopupOpen, setIsAdminPopupOpen] = useState(false)

    const getUser = () => {
        try { return JSON.parse(localStorage.getItem('user')) || {}; } 
        catch { return {}; }
    }
    const isAdmin = getUser().role === 'Admin'

    return (
        <>
            <header className="hd-header" id="header">
                <div className="hd-container">
                    <div className="hd-brand">
                        <img src={logo} alt="rusalLogo" className="hd-logo" id="logo1" />
                        <h1 className="hd-title">{title}</h1>
                    </div>
                    {showNav && (
                        <nav className="hd-nav">
                            <a href="/reglaments" className={`hd-link ${activeNav === "reglaments" ? "active" : ""}`}>Регламенты</a>
                            <a href="/tasks" className={`hd-link ${activeNav === "tasks" ? "active" : ""}`}>Задания</a>
                            <a href="/parametres" className={`hd-link ${activeNav === "parametres" ? "active" : ""}`}>Параметры</a>
                        </nav>
                    )}
                    <div className="hd-actions">
                        {selectConfig && <div className="hd-select-wrapper"><SelectGroup {...selectConfig} /></div>}
                        
                        {showUserBtn && (
                            <>
                                {selectConfig && <div className="hd-divider" />}
                                
                                {isAdmin && (
                                    <>
                                        <div className="hd-divider" />
                                        <button 
                                            className="hd-admin-btn" 
                                            onClick={() => setIsAdminPopupOpen(true)} 
                                            aria-label="Управление пользователями" 
                                            title="Управление пользователями"
                                        >
                                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                                <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/>
                                                <circle cx="9" cy="7" r="4"/>
                                                <path d="M23 21v-2a4 4 0 0 0-3-3.87"/>
                                                <path d="M16 3.13a4 4 0 0 1 0 7.75"/>
                                            </svg>
                                        </button>
                                    </>
                                )}
                                
                                <button className="hd-user-btn popupBtn" onClick={() => setIsUserPopupOpen(true)} aria-label="Открыть профиль пользователя">
                                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                        <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/>
                                        <circle cx="12" cy="7" r="4"/>
                                    </svg>
                                </button>
                            </>
                        )}
                    </div>
                </div>
            </header>
            
            <UserPopup isOpen={isUserPopupOpen} onClose={() => setIsUserPopupOpen(false)} />
            <AdminUsersPopup isOpen={isAdminPopupOpen} onClose={() => setIsAdminPopupOpen(false)} />
        </>
    )
}

export default Header