import './Button.css'

function Button({ children, type = 'button', variant = 'primary', disabled = false, loading = false, onClick }) {
    return (
        <button type={type} disabled={disabled || loading} onClick={onClick} className={`auth-btn-submit ${variant === 'secondary' ? 'secondary' : ''}`}>
            <span className="btn-text">{children}</span>
            {!loading && <svg className="btn-arrow" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><line x1="5" y1="12" x2="19" y2="12"/><polyline points="12 5 19 12 12 19"/></svg>}
            {loading && <div className="auth-btn-spinner"></div>}
        </button>
    )
}
export default Button