import './Input.css'

function Input({ label, type = 'text', name, value, onChange, placeholder, error = false }) {
    return (
        <div className="input-group">
            <label className="input-label-text">{label}</label>
            <div className="input-field-wrapper">
                <svg className="input-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                    {name === 'username' ? <><path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2"/><circle cx="12" cy="7" r="4"/></> 
                     : <><rect x="3" y="11" width="18" height="11" rx="2" ry="2"/><path d="M7 11V7a5 5 0 0 1 10 0v4"/></>}
                </svg>
                <input
                    type={type} name={name} value={value} onChange={onChange} placeholder={placeholder}
                    className={`input-field ${error ? 'error' : ''}`}
                />
            </div>
            {error && (
                <div className="input-error-msg">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                    <span>Поле обязательно для заполнения</span>
                </div>
            )}
        </div>
    )
}
export default Input