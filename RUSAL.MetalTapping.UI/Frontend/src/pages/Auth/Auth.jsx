import { useNavigate } from 'react-router-dom'
import Header from '../../components/Header/Header'
import AuthBox from '../../components/AuthBox/AuthBox'
import AuthForm from '../../components/AuthForm/AuthForm'
import PageTitle from '../../components/PageTitle'
import api from '../../services/api'
import './Auth.css'

function Auth() {
    const navigate = useNavigate()

    const handleLogin = async (formData) => {
        const response = await api.post('/api/Auth/login', {
            email: formData.username,
            password: formData.password
        })
        
        localStorage.setItem('token', response.data)
        
        navigate('/reglaments')
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
                    <AuthForm onSubmit={handleLogin} />
                </AuthBox>
            </div>
        </>
    );
}

export default Auth