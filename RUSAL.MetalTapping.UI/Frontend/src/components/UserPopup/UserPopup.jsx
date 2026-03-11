import './UserPopup.css'

function UserPopup({ isOpen, onClose, userData }) {
    if (!isOpen) {
        return null
    }

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) {
            onClose()
        }
    };

    const handleLogout = () => {
        console.log('Выход из системы')
        onClose()
    };

    return (
        <div className="popupContainer" onClick={handleOverlayClick}>
            <div className="popupContent">
                <div className="userForm">
                    <label>Логин</label>
                    <h2 className="userInfo Login">{userData?.login || "Не указан"}</h2>
                    <label>Роль</label>
                    <h2 className="userInfo Role">{userData?.role || "Не указана"}</h2>
                </div>
                <button id="close" onClick={handleLogout}>
                    Выйти
                </button>
            </div>
        </div>
    );
}

export default UserPopup