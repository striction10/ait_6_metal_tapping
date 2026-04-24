import { useState } from 'react'
import './Table.css'

function Table({ 
    title,
    headers = [],
    data = [],
    columns = [],
    colspan = 10,
    onCellChange,
    canEdit,
    getCellClassName
}) {
    const [editingCell, setEditingCell] = useState(null)

    const handleCellClick = (rowIndex, colField, value) => {
        setEditingCell({ rowIndex, colField, value })
    }

    const handleCellBlur = (row, colField, newValue) => {
        if (onCellChange && editingCell) {
            onCellChange(row, colField, newValue)
        }
        setEditingCell(null)
    }

    const handleKeyDown = (e, row, colField) => {
        if (e.key === 'Enter') {
            handleCellBlur(row, colField, e.target.value)
        }
        if (e.key === 'Escape') {
            setEditingCell(null)
        }
    }

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

    const isEditable = (colField) => {
        if (canEdit) {
            return canEdit(colField)
        }
        return ['actualMetalLevel', 'calculatedTask', 'roundCalculatedTask'].includes(colField)
    }

    return (
        <div className="main-table">
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
                        <tr key={row.id || rowIndex}>
                            {columns.map((col, colIndex) => {
                                const colField = col.field
                                const isEditing = editingCell?.rowIndex === rowIndex && 
                                                editingCell?.colField === colField
                                let cellContent

                                if (col.render) {
                                    cellContent = col.render(row)
                                } else if (typeof col === 'string') {
                                    cellContent = getNestedValue(row, col)
                                } else if (col.field) {
                                    cellContent = getNestedValue(row, col.field)
                                } else {
                                    cellContent = '-'
                                }

                                const cellClass = getCellClassName 
                                    ? getCellClassName(row, col, rowIndex, colIndex) 
                                    : ''

                                if (isEditable(colField) && isEditing) {
                                    return (
                                        <td key={colIndex}>
                                            <input
                                                type="number"
                                                step="0.1"
                                                defaultValue={cellContent}
                                                onBlur={(e) => handleCellBlur(row, colField, e.target.value)}
                                                onKeyDown={(e) => handleKeyDown(e, row, colField)}
                                                autoFocus
                                            />
                                        </td>
                                    )
                                }

                                return (
                                    <td 
                                        key={colIndex}
                                        className={cellClass}
                                        onClick={() => isEditable(colField) && 
                                            handleCellClick(rowIndex, colField, cellContent)}
                                        style={isEditable(colField) ? { cursor: 'pointer' } : {}}
                                    >
                                        {cellContent}
                                    </td>
                                )
                            })}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default Table