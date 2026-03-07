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
                                return (
                                    <td key={colIndex} className={cellClass}>
                                        {renderCell 
                                            ? renderCell(row, col, rowIndex, colIndex)
                                            : row[col.field]
                                        }
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