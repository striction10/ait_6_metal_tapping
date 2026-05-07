import { useState } from 'react'
import './Header.css'
import logo from '../../assets/rusalLogoGrey.png'
import userLogo from '../../assets/userLogo.svg'
import SelectGroup from "../SelectGroup/SelectGroup"
import UserPopup from "../UserPopup/UserPopup"

function Header({ 
    title = "Выливка металла",
    showNav = false,
    showUserBtn = false,
    activeNav = "",
    selectConfig = null
}) {
    const [isPopupOpen, setIsPopupOpen] = useState(false)
    return (
        <>
            <header className="hd-header" id="header">
                <div className="hd-container">
                    <div className="hd-brand">
                        <img src={logo} alt="rusalLogo" className="hd-logo" id="logo1" />
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
                                <button className="hd-user-btn popupBtn" onClick={() => setIsPopupOpen(true)} aria-label="Открыть профиль пользователя">
                                    <img src={userLogo} alt="userLogo" />
                                </button>
                            </>
                        )}
                    </div>
                </div>
            </header>
            <UserPopup isOpen={isPopupOpen} onClose={() => setIsPopupOpen(false)} />
        </>
    )
}
export default Header