import { useState } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import SendPopup from "../../components/SendPopup/SendPopup"
import { useReglamentsData } from '../../hooks/useReglamentsData'
import { useParametersData } from '../../hooks/useParametresData'
import { exportTableToPDF } from '../../utils/exportToPDFParametres'
import { getUserData } from '../../utils/auth'
import { parametersApi } from '../../services/parameters'

function Parametres() {
    const [selectedCorpus, setSelectedCorpus] = useState('')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    
    const { buildings } = useReglamentsData()
    const { sortedData, setSortedData, headers, columns } = useParametersData(selectedCorpus, selectedDate)

    const [isUploadOpen, setIsUploadOpen] = useState(false)
    const { role } = getUserData()

    const validateNumberInput = (value) => {
        let cleaned = value.replace(/[^\d.-]/g, '')
        
        const minusCount = (cleaned.match(/-/g) || []).length
        if (minusCount > 1) {
            cleaned = cleaned.replace(/-/g, '')
        }
        if (cleaned.indexOf('-') > 0) {
            cleaned = cleaned.replace(/-/g, '')
        }
        
        const dotCount = (cleaned.match(/\./g) || []).length
        if (dotCount > 1) {
            cleaned = cleaned.replace(/\./g, '')
        }
        
        return cleaned
    }

    const canEdit = (field) => {
        if (role === 'Technologist') {
            return true
        }
        return field === 'actualMetalLevel'
    }

    const getCellClassName = (row, col, rowIndex, colIndex) => {
        const deviation = parseFloat(row.deviationValue)
        const isDeviationOutOfRange = !isNaN(deviation) && deviation <= -5 //TODO: get value from db
        
        if (role === 'Technologist') {
            if (isDeviationOutOfRange && (
                col.field === 'deviationValue' || 
                col.field === 'calculatedTask' || 
                col.field === 'roundCalculatedTask'
            )) {
                return 'technologist-blue'
            }
            return ''
        }
        
        if (isDeviationOutOfRange && (
            col.field === 'deviationValue' || 
            col.field === 'calculatedTask' || 
            col.field === 'roundCalculatedTask'
        )) {
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
                    ? { 
                        ...item, 
                        actualMetalLevel: numValue,
                        deviationValue: response.data.deviation,
                        calculatedTask: response.data.calculatedTask,
                        roundCalculatedTask: response.data.roundCalculatedTask
                      }
                    : item
            ))
        } 
        else if (field === 'calculatedTask') {
            const response = await parametersApi.updateCalculatedTask(row.potId, numValue)
            
            setSortedData(prev => prev.map(item => 
                item.potId === row.potId 
                    ? { 
                        ...item, 
                        calculatedTask: numValue,
                        roundCalculatedTask: response.data.roundCalculatedTask
                      }
                    : item
            ))
        }
        else if (field === 'roundCalculatedTask') {
            const response = await parametersApi.updateRoundTask(row.potId, numValue)
            
            setSortedData(prev => prev.map(item => 
                item.potId === row.potId 
                    ? { 
                        ...item, 
                        roundCalculatedTask: numValue
                      }
                    : item
            ))
        }
    }

    const handleSave = () => {
        exportTableToPDF(sortedData, selectedCorpus)
    }

    const handleSubmit = () => {
        setIsUploadOpen(true)
    }

    const handleFileSubmit = (file) => {
        setIsUploadOpen(false)
    }

    return (
        <>
            <PageTitle title="Параметры" />
            <Header 
                title="Параметры"
                showNav={true}
                showUserBtn={true}
                activeNav="parametres"
                selectConfig={{
                    selects: [
                        {
                            name: "corpus",
                            options: [
                                { value: "0", label: "Выбрать корпус" },
                                ...buildings
                            ],
                            value: selectedCorpus,
                            onChange: (e) => setSelectedCorpus(e.target.value)
                        }
                    ],
                    showDate: true,
                    dateValue: selectedDate,
                    onDateChange: (e) => setSelectedDate(e.target.value)
                }}
            />
            <div className="table-container">
                <div className="tables-wrapper">
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
                </div>
                <ActionButtons 
                    onSave={handleSave}
                    onSubmit={handleSubmit}
                />
            </div>

            <SendPopup
                isOpen={isUploadOpen}
                onClose={() => setIsUploadOpen(false)}
                onSubmit={handleFileSubmit}
            />
        </>
    )
}

export default Parametres