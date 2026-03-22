import './UserPopup.css'
import { useNavigate } from 'react-router-dom'
import { getUserData } from '../../utils/auth'

function UserPopup({ isOpen, onClose, userData }) {
    const navigate = useNavigate()
    const { login, role } = getUserData()

    if (!isOpen) {
        return null
    }

    const handleOverlayClick = (e) => {
        if (e.target === e.currentTarget) {
            onClose()
        }
    };

    const handleLogout = () => {
        localStorage.removeItem('token')
        localStorage.removeItem('user')
        onClose()
        navigate('/auth')
    };

    return (
        <div className="popupContainer" onClick={handleOverlayClick}>
            <div className="popupContent">
                <div className="userForm">
                    <label>Логин</label>
                    <h2 className="userInfo Login">{login}</h2>
                    <label>Роль</label>
                    <h2 className="userInfo Role">{role}</h2>
                </div>
                <button id="close" onClick={handleLogout}>
                    Выйти
                </button>
            </div>
        </div>
    );
}

export default UserPopup