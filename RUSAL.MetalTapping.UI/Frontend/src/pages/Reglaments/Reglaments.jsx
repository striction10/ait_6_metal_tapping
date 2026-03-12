import { useState, useEffect } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import { useReglamentsData } from '../../hooks/useReglamentsData'
import api from '../../services/api'

function Reglaments() {
    const [selectedReglament, setSelectedReglament] = useState('')
    const [selectedCorpus, setSelectedCorpus] = useState('')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    );
    
    const [tableData, setTableData] = useState([])
    
    const { reglaments, buildings } = useReglamentsData()

    useEffect(() => {
        const fetchTableData = async () => {
            if (!selectedReglament || !selectedCorpus || 
                selectedReglament === '0' || selectedCorpus === '0') {
                setTableData([])
                return
            }

            try {
                const response = await api.get('/api/reglament/table', {
                    params: {
                        buildingId: selectedCorpus,
                        reglamentId: selectedReglament
                    }
                })
                
                console.log('Данные с бэка:', response.data)
                
                if (response.data && response.data.pots && Array.isArray(response.data.pots)) {
                    setTableData(response.data.pots)
                } else {
                    setTableData([])
                }
                
            } catch (err) {
                console.error('Ошибка загрузки данных:', err)
                setTableData([])
            }
        }

        fetchTableData()
    }, [selectedReglament, selectedCorpus, selectedDate])

    const getHeaders = () => {
        if (tableData.length === 0) return []
        
        const firstItem = tableData[0]
        const ratioKeys = Object.keys(firstItem.castingRatio || {})
            .sort((a, b) => Number(a) - Number(b))
        
        return ['№ Электролиза', ...ratioKeys]
    }

    const getColumns = () => {
        if (tableData.length === 0) return []
        
        const firstItem = tableData[0];
        const ratioKeys = Object.keys(firstItem.castingRatio || {})
            .sort((a, b) => Number(a) - Number(b))
        
        return [
            { 
                field: 'name',
                render: (row) => row.name || row.id 
            },
            ...ratioKeys.map(key => ({
                field: `castingRatio.${key}`,
                render: (row) => row.castingRatio?.[key] ?? '-'
            }))
        ]
    }

    const headers = getHeaders()
    const columns = getColumns()

    const handleSave = () => {
        console.log('Сохранение данных...', tableData)
    }

    const handleSubmit = () => {
        console.log('Отправка данных...', tableData)
    }

    return (
        <>
            <PageTitle title="Регламенты" />
            <Header 
                title="Регламенты"
                showNav={true}
                showUserBtn={true}
                activeNav="reglaments"
                selectConfig={{
                    selects: [
                        {
                            name: "reglament",
                            options: [
                                { value: "0", label: "Выбрать регламент" },
                                ...reglaments
                            ],
                            value: selectedReglament,
                            onChange: (e) => setSelectedReglament(e.target.value)
                        },
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
                    title="Таблица выливки, %"
                    headers={headers}
                    data={tableData}
                    columns={columns}
                    colspan={headers.length}
                />
                
                <ActionButtons 
                    onSave={handleSave}
                    onSubmit={handleSubmit}
                />
            </div>
        </>
    )
}

export default Reglaments