import Header from '../../components/Header/Header'
import AuthBox from '../../components/AuthBox/AuthBox'
import AuthForm from '../../components/AuthForm/AuthForm'
import PageTitle from '../../components/PageTitle'
import './Auth.css'

function Auth() {
    function handleLogin(formData) {
        console.log('Форма отправлена:', formData)
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