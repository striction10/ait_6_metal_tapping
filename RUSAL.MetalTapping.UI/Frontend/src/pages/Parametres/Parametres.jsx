import { useState } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import { useReglamentsData } from '../../hooks/useReglamentsData'
import { useParametersData } from '../../hooks/useParametresData'
import api from '../../services/api'

function Parametres() {
    const [selectedCorpus, setSelectedCorpus] = useState('')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    
    const { buildings } = useReglamentsData()
    const { sortedData, setSortedData, headers, columns } = useParametersData(selectedCorpus, selectedDate)

    const handleCellChange = async (row, field, newValue) => {
        const numValue = parseFloat(newValue)
        if (isNaN(numValue)) return

        try {
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

            } else if (field === 'calculatedTask' || field === 'roundCalculatedTask') {
                const params = {
                    potId: row.potId,
                    calculatedTask: field === 'calculatedTask' ? numValue : row.calculatedTask,
                    roundCalculatedTask: field === 'roundCalculatedTask' ? numValue : row.roundCalculatedTask
                }

                const response = await api.post('/api/parameters/calculated', null, { params })
                
                setSortedData(prev => prev.map(item => 
                    item.potId === row.potId 
                        ? { 
                            ...item, 
                            [field]: numValue,
                            calculatedTask: response.data.calculatedTask,
                            roundCalculatedTask: response.data.roundCalculatedTask
                          }
                        : item
                ))
            }
        } catch (err) {
            console.error('Ошибка при обновлении:', err)
            alert('Ошибка при сохранении данных')
        }
    }

    const handleSave = () => {
        console.log('Сохранение данных...', sortedData) //TODO: save
        alert('Данные сохранены')
    }

    const handleSubmit = () => {
        console.log('Отправка данных...', sortedData) //TODO: send
        alert('Данные отправлены')
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
        </>
    )
}

export default Parametres