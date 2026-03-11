import "../Input/Input.css"

function Input({ 
    label, 
    type = 'text', 
    name, 
    value, 
    onChange, 
    placeholder, 
    error = false,
    className = '' 
}) {
    return (
        <div className="input-wrapper">
            {label && <p className="input-label">{label}</p>}
            <input
                type={type}
                name={name}
                value={value}
                onChange={onChange}
                placeholder={placeholder}
                className={`auth-input ${error ? 'error' : ''} ${className}`}
            />
        </div>
    )
}

export default Input