import './Table.css'

function Table({ 
    title,
    headers = [],
    data = [],
    columns = [],
    colspan = 10,
    className = '',
    renderCell,
    getCellClassName
}) {
    const getNestedValue = (obj, path) => {
        if (!obj || !path) return '-'
        
        const parts = path.split('.')
        let value = obj
        
        for (const part of parts) {
            if (value === null || value === undefined) return '-'
            value = value[part]
        }
        
        return value !== undefined && value !== null ? value : '-'
    }

    return (
        <div className={`main-table ${className}`}>
            <table className="data-table">
                <thead>
                    <tr id="color-item">
                        <th colSpan={colspan}>{title}</th>
                    </tr>
                    <tr>
                        {headers.map((header, index) => (
                            <th key={index}>{header}</th>
                        ))}
                    </tr>
                </thead>
                <tbody>
                    {data.map((row, rowIndex) => (
                        <tr key={rowIndex}>
                            {columns.map((col, colIndex) => {
                                const cellClass = getCellClassName ? getCellClassName(row, col, rowIndex, colIndex) : ''
                                let cellContent
                                
                                if (renderCell) {
                                    cellContent = renderCell(row, col, rowIndex, colIndex)
                                } else if (typeof col === 'string') {
                                    cellContent = getNestedValue(row, col)
                                } else if (col.field) {
                                    cellContent = getNestedValue(row, col.field)
                                } else if (col.render) {
                                    cellContent = col.render(row)
                                } else {
                                    cellContent = '-'
                                }
                                
                                return (
                                    <td key={colIndex} className={cellClass}>
                                        {cellContent}
                                    </td>
                                )
                            })}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default Table