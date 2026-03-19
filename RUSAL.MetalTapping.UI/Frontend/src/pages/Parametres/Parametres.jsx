import { useState } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import SendPopup from "../../components/SendPopup/SendPopup"
import { useReglamentsData } from '../../hooks/useReglamentsData'
import { useParametersData } from '../../hooks/useParametresData'
import { exportTableToPDF } from '../../utils/exportToPDFParametres'
import api from '../../services/api'

function Parametres() {
    const [selectedCorpus, setSelectedCorpus] = useState('')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    
    const { buildings } = useReglamentsData()
    const { sortedData, setSortedData, headers, columns } = useParametersData(selectedCorpus, selectedDate)

    const [isUploadOpen, setIsUploadOpen] = useState(false)

    const handleCellChange = async (row, field, newValue) => {
        const numValue = parseFloat(newValue)
        if (isNaN(numValue)) return

        if (field === 'actualMetalLevel') {
            const response = await api.post('/api/parameters', null, {
                params: {
                    potId: row.potId,
                    actualMetalLevel: numValue
                }
            })
            
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
            const response = await api.post(`/api/Parameters/calculated/${row.potId}`, null, {
                params: {
                    calculatedTask: numValue
                }
            })
            
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
            const response = await api.post(`/api/parameters/round/${row.potId}`, null, {
                params: {
                    roundTask: numValue
                }
            })
            
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
        console.log('Сохранение данных...', sortedData)
        exportTableToPDF(sortedData, selectedCorpus)
    }

    const handleSubmit = () => {  // TODO: in process
        console.log('Отправка данных...', sortedData)
        setIsUploadOpen(true)
    }

     const handleFileSubmit = (file) => {  //TODO: in process
        console.log('Файл отправлен:', file)
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
            
            <div className="tasks-container">
                <Table 
                    title="Таблица параметров"
                    headers={headers}
                    data={sortedData}
                    columns={columns}
                    colspan={headers.length}
                    onCellChange={handleCellChange}
                />
                
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