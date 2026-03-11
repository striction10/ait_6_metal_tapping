import './AuthBox.css'

function AuthBox({ title, children }) {
    return (
        <div id="auth_box">
            <h2>{title}</h2>
            {children}
        </div>
    );
}

export default AuthBox