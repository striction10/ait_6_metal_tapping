import "./Header.css";
import logo from '../../assets/rusalLogoGrey.svg';
import userLogo from '../../assets/userLogo.svg';

function Header ({ 
    title = "Выливка металла",
    showNav = false,
    showUserBtn = false,
    activeNav = ""
}) {
    return (
        <header id="header">
            <div className="firstPart">
                <img src={logo} alt="rusalLogo" id="logo1" width="70px" />
                <h1>{title}</h1>
                {showUserBtn && (
                    <button className="popupBtn">
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
        </header>
    )
}

export default Header