import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Header from '../../components/Header/Header'
import AuthBox from '../../components/AuthBox/AuthBox'
import AuthForm from '../../components/AuthForm/AuthForm'
import PageTitle from '../../components/PageTitle'
import { authApi } from '../../services/auth'
import { saveUserFromToken } from '../../utils/auth'
import './Auth.css'

function Auth() {
    const navigate = useNavigate()
    const [error, setError] = useState('')

    const handleLogin = async (formData) => {
        try {
            const response = await authApi.login(formData.username, formData.password)
            
            const token = response.data.token
            
            localStorage.setItem('token', token)
            saveUserFromToken(token)
            navigate('/reglaments')
        } catch (err) {
            if (err.response?.status === 401 || err.response?.status === 404) {
                setError('Неверный логин или пароль')
            } else {
                setError('Ошибка сервера')
            }
        }
    }

    return (
        <>
            <PageTitle title={"Выливка металла"} />
            <Header title="Выливка металла"
                showNav={false}
                showUserBtn={false}
                activeNav = ""
            />
            <div id="main">
                <AuthBox title="Войти">
                    {error && <div className="error-message">{error}</div>}
                    <AuthForm onSubmit={handleLogin} />
                </AuthBox>
            </div>
        </>
    )
}

export default Auth