import { useState, useEffect } from 'react'
import Header from '../../components/Header/Header'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import SendPopup from "../../components/SendPopup/SendPopup"
import { useReglamentsData } from '../../hooks/useReglamentsData'
import { useParametersWithReglaments } from '../../hooks/useMergeReglamentWithParametres'
import { exportTableToPDF } from '../../utils/exportToPDFParametres'
import { getUserData } from '../../utils/auth'
import { parametersApi } from '../../services/parameters'
import './Parametres.css'

function Parametres() {
    const [selectedReglament, setSelectedReglament] = useState('')
    const [selectedCorpus, setSelectedCorpus] = useState('')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    
    const { reglaments, buildings } = useReglamentsData()
    const { sortedData, setSortedData } = useParametersWithReglaments(selectedCorpus, selectedReglament, selectedDate)

    const [isUploadOpen, setIsUploadOpen] = useState(false)
    const { role } = getUserData()

    useEffect(() => {
        document.title = "Параметры"
    }, []);

    const validateNumberInput = (value) => {
        let cleaned = value.replace(/[^\d.-]/g, '')
        const minusCount = (cleaned.match(/-/g) || []).length
        if (minusCount > 1) cleaned = cleaned.replace(/-/g, '')
        if (cleaned.indexOf('-') > 0) cleaned = cleaned.replace(/-/g, '')
        const dotCount = (cleaned.match(/\./g) || []).length
        if (dotCount > 1) cleaned = cleaned.replace(/\./g, '')
        return cleaned
    }

    const canEdit = (field) => {
        const readonlyFields = [
            'potName',
            'deviationValue',
            'amperage',
            'avgAmperage',
            'metalMarkName'
        ]

        if (readonlyFields.includes(field)) {
            return false
        }

        if (role === 'Technologist') {
            return true
        }
        return field === 'actualMetalLevel'
    }

    const getCellClassName = (row, col, rowIndex, colIndex) => {
        const deviation = parseFloat(row.deviationValue)

        const minDeviation = row.minDeviation ?? -5
        const maxDeviation = row.maxDeviation ?? 5
    
        const isDeviationOutOfRange = !isNaN(deviation) && (deviation < minDeviation || deviation > maxDeviation)

        if (role === 'Technologist') {
            if (isDeviationOutOfRange && (col.field === 'deviationValue' || col.field === 'calculatedTask' || col.field === 'roundCalculatedTask')) {
                return 'technologist-blue'
            }
            return ''
        }
        if (isDeviationOutOfRange && (col.field === 'deviationValue' || col.field === 'calculatedTask' || col.field === 'roundCalculatedTask')) {
            return 'red-item'
        }
        return ''
    }

    const handleCellChange = async (row, field, newValue) => {
        if (!canEdit(field)) return
        const validatedValue = validateNumberInput(newValue)
        const numValue = parseFloat(validatedValue)
        if (isNaN(numValue)) return

        if (field === 'actualMetalLevel') {
            const response = await parametersApi.updateMetalLevel(row.potId, numValue)
            setSortedData(prev => prev.map(item =>
                item.potId === row.potId
                    ? { ...item, actualMetalLevel: numValue, deviationValue: response.data.deviation, calculatedTask: response.data.calculatedTask, roundCalculatedTask: response.data.roundCalculatedTask }
                    : item
            ))
        } else if (field === 'calculatedTask') {
            const response = await parametersApi.updateCalculatedTask(row.potId, numValue)
            setSortedData(prev => prev.map(item =>
                item.potId === row.potId
                    ? { ...item, calculatedTask: numValue, roundCalculatedTask: response.data.roundCalculatedTask }
                    : item
            ))
        } else if (field === 'roundCalculatedTask') {
            await parametersApi.updateRoundTask(row.potId, numValue)
            setSortedData(prev => prev.map(item =>
                item.potId === row.potId ? { ...item, roundCalculatedTask: numValue } : item
            ))
        }
    }

    const headers = [
        '№ Электролиза',
        'Уровень металла, цель',
        'Уровень металла, факт',
        'Отклонение, см',
        'Сила тока, кА',
        'Выход по току, %',
        'Расчетное задание, кг',
        'ЗПР, кг',
        'Марка'
    ]

    const columns = [
        { field: 'potName', render: (row) => row.potName || '-' },
        { field: 'targetMetalLevel', render: (row) => row.targetMetalLevel?.toFixed(1) ?? '-' },
        { field: 'actualMetalLevel', render: (row) => row.actualMetalLevel?.toFixed(1) ?? '-' },
        { field: 'deviationValue', render: (row) => row.deviationValue?.toFixed(1) ?? '-' },
        { field: 'amperage', render: (row) => row.amperage?.toFixed(0) ?? '-' },
        { field: 'avgAmperage', render: (row) => row.avgAmperage?.toFixed(1) ?? '-' },
        { field: 'calculatedTask', render: (row) => row.calculatedTask?.toFixed(0) ?? '-' },
        { field: 'roundCalculatedTask', render: (row) => row.roundCalculatedTask?.toFixed(0) ?? '-' },
        { field: 'metalMarkName', render: (row) => row.metalMarkName || '-' }
    ]

    const handleSave = () => {
        const corpusName = buildings.find(b => b.value === selectedCorpus)?.label || selectedCorpus
        const reglamentName = reglaments.find(r => r.value === selectedReglament)?.label || selectedReglament
        
        exportTableToPDF(sortedData, selectedCorpus, corpusName, reglamentName, selectedDate)
    }
    
    const handleSubmit = () => setIsUploadOpen(true)
    const handleFileSubmit = () => setIsUploadOpen(false)

    return (
        <>
            <Header 
                title="Параметры"
                showNav={true}
                showUserBtn={true}
                activeNav="parametres"
                selectConfig={{
                    selects: [
                        {
                            name: "corpus",
                            options: [{ value: "0", label: "Выбрать корпус" }, ...buildings],
                            value: selectedCorpus,
                            onChange: (e) => setSelectedCorpus(e.target.value)
                        },
                        {
                            name: "reglament",
                            options: [
                                { value: "0", label: "Выбрать регламент" },
                                ...reglaments
                            ],
                            value: selectedReglament,
                            onChange: (e) => setSelectedReglament(e.target.value),
                            disabled: !selectedCorpus || selectedCorpus === '0'
                        }
                    ],
                    showDate: true,
                    dateValue: selectedDate,
                    onDateChange: (e) => setSelectedDate(e.target.value)
                }}
            />
            <div className="param-main">
                <div className="param-content">
                    {sortedData.length === 0 ? (
                        <div className="param-empty">
                            <div className="param-empty-icon">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
                                    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/>
                                </svg>
                            </div>
                            <h3>Выберите корпус</h3>
                            <p>Для отображения данных выберите корпус в панели выше</p>
                        </div>
                    ) : (
                        <>
                            <Table 
                                title="Таблица параметров"
                                headers={headers}
                                data={sortedData}
                                columns={columns}
                                colspan={headers.length}
                                onCellChange={handleCellChange}
                                canEdit={canEdit}
                                getCellClassName={getCellClassName}
                            />
                            <ActionButtons onSave={handleSave} onSubmit={handleSubmit} />
                        </>
                    )}
                </div>
            </div>

            <SendPopup
                isOpen={isUploadOpen}
                onClose={() => setIsUploadOpen(false)}
                pageType="parameters"
                selectedDate={selectedDate}
            />

        </>
    )
}
export default Parametres
