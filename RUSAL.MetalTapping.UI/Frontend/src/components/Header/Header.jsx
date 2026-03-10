import { useState } from 'react'
import "./Header.css"
import logo from '../../assets/rusalLogoGrey.svg'
import userLogo from '../../assets/userLogo.svg'
import SelectGroup from "../SelectGroup/SelectGroup"
import UserPopup from "../UserPopup/UserPopup"
import { useUser } from '../../contexts/UserContext'

function Header ({ 
    title = "Выливка металла",
    showNav = false,
    showUserBtn = false,
    activeNav = "",
    selectConfig = null
}) {
    const [isPopupOpen, setIsPopupOpen] = useState(false)
    const { userData } = useUser()

    return (
        <header id="header">
            <div className="firstPart">
                <img src={logo} alt="rusalLogo" id="logo1" width="70px" />
                <h1>{title}</h1>
                {showUserBtn && (
                    <button className="popupBtn" onClick={() => setIsPopupOpen(true)}>
                        <img src={userLogo} alt="userLogo" width="30px" />
                    </button>
                )}
            </div>
            {showNav && (
                <div className="navPart">
                    <a 
                        href="/reglaments" 
                        className={activeNav === "reglaments" ? "active" : ""}
                    >
                        Регламенты
                    </a>
                    <a 
                        href="/tasks"
                        className={activeNav === "tasks" ? "active" : ""}
                    >
                        Задания
                    </a>
                    <a 
                        href="/parametres"
                        className={activeNav === "parametres" ? "active" : ""}
                    >
                        Параметры
                    </a>
                </div>
            )}
            {selectConfig && (
                <div className="selectPart">
                    <SelectGroup {...selectConfig} />
                </div>
            )}
            
            <UserPopup 
                isOpen={isPopupOpen}
                onClose={() => setIsPopupOpen(false)}
                userData={userData}
            />
        </header>
    )
}

export default Header