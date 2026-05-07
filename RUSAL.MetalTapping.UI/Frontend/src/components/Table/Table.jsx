import { useState, useRef, useEffect } from 'react'
import './Table.css'

function Table({ title, headers = [], data = [], columns = [], colspan = 10, onCellChange, canEdit, getCellClassName }) {
    const [editingCell, setEditingCell] = useState(null)
    const tableRef = useRef(null)

    const handleCellClick = (rowIndex, colField, value) => {
        setEditingCell({ rowIndex, colField, value })
    }

    const handleCellBlur = (row, colField, newValue) => {
        if (onCellChange && editingCell) onCellChange(row, colField, newValue)
        setEditingCell(null)
    }

    const handleKeyDown = (e, row, colField) => {
        if (e.key === 'Enter') handleCellBlur(row, colField, e.target.value)
        if (e.key === 'Escape') setEditingCell(null)
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
        if (canEdit) return canEdit(colField)
        return ['actualMetalLevel', 'calculatedTask', 'roundCalculatedTask'].includes(colField)
    }

    const getPencilIcon = (editable) => {
        if (!editable) return null
        return (
            <svg viewBox="0 0 12 12" fill="none" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" className="tbl-pencil">
                <path d="M8.5 1.5a1.5 1.5 0 0 1 2.12 2.12L4.5 9.74 3 10l.26-1.5 6.24-6.24z"/>
            </svg>
        )
    }

    return (
        <div className="tbl-wrapper" ref={tableRef}>
            <div className="tbl-header">
                <h3 className="tbl-title">{title}</h3>
                <span className="tbl-count">{data.length} записей</span>
            </div>
            <div className="tbl-scroll">
                <table className="tbl">
                    <thead>
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
                                    const isEditing = editingCell?.rowIndex === rowIndex && editingCell?.colField === colField
                                    let cellContent

                                    if (col.render) cellContent = col.render(row)
                                    else if (typeof col === 'string') cellContent = getNestedValue(row, col)
                                    else if (col.field) cellContent = getNestedValue(row, col.field)
                                    else cellContent = '-'

                                    const cellClass = getCellClassName ? getCellClassName(row, col, rowIndex, colIndex) : ''
                                    const editable = isEditable(colField)

                                    if (editable && isEditing) {
                                        return (
                                            <td key={colIndex} className="tbl-cell-editing">
                                                <input
                                                    type="text"
                                                    defaultValue={cellContent}
                                                    onBlur={(e) => handleCellBlur(row, colField, e.target.value)}
                                                    onKeyDown={(e) => handleKeyDown(e, row, colField)}
                                                    autoFocus
                                                    className="tbl-input"
                                                />
                                            </td>
                                        )
                                    }

                                    return (
                                        <td 
                                            key={colIndex}
                                            className={`tbl-cell ${cellClass} ${editable ? 'tbl-cell-editable' : ''}`}
                                            onClick={() => editable && handleCellClick(rowIndex, colField, cellContent)}
                                        >
                                            <span className="tbl-cell-inner">
                                                <span className="tbl-cell-value">{cellContent}</span>
                                                {getPencilIcon(editable)}
                                            </span>
                                        </td>
                                    )
                                })}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}
export default Table