import './AuthBox.css'

function AuthBox({ title, children, className = '' }) {
    return (
        <div id="auth_box" className={className}>
            <h2>{title}</h2>
            {children}
        </div>
    );
}
export default AuthBox