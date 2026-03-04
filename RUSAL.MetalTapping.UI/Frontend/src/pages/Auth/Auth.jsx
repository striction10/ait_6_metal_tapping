import Header from '../../components/Header/Header'
import AuthBox from '../../components/AuthBox/AuthBox'
import AuthForm from '../../components/AuthForm/AuthForm'
import './Auth.css'

function Auth() {
    function handleLogin(formData) {
        console.log('Форма отправлена:', formData)
    }
    return (
        <>
            <Header />
            <div id="main">
                <AuthBox title="Войти">
                    <AuthForm onSubmit={handleLogin} />
                </AuthBox>
            </div>
        </>
    );
}

export default Auth