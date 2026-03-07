import './SelectGroup.css'

function SelectGroup({ 
    selects = [],
    showDate = false,
    dateValue = "",
    onDateChange,
    className = "",
    layout = "row"
}) {
    return (
        <div className={`select-group ${className} layout-${layout}`}>
            {selects.map((select, index) => (
                <select 
                    key={index}
                    name={select.name}
                    id={select.id}
                    value={select.value}
                    onChange={select.onChange}
                    className={select.className || ''}
                    disabled={select.disabled || false}
                >
                    {select.options.map((option, optIndex) => (
                        <option key={optIndex} value={option.value}>
                            {option.label}
                        </option>
                    ))}
                </select>
            ))}
            
            {showDate && (
                <input 
                    type="date" 
                    value={dateValue}
                    onChange={onDateChange}
                    className="date-input"
                />
            )}
        </div>
    );
}

export default SelectGroup