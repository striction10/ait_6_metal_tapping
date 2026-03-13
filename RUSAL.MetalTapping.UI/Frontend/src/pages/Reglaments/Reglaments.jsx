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
    )
    
    const [tableData, setTableData] = useState([])
    const [sortedData, setSortedData] = useState([])
    
    const { reglaments, buildings } = useReglamentsData()

    const extractNumber = (name) => {
        const match = name?.match(/\d+/)
        return match ? parseInt(match[0]) : 0
    }

    useEffect(() => {
        const fetchTableData = async () => {
            if (!selectedReglament || !selectedCorpus || 
                selectedReglament === '0' || selectedCorpus === '0') {
                setTableData([])
                setSortedData([])
                return
            }

            try {
                const response = await api.get('/api/reglament/table', {
                    params: {
                        buildingId: selectedCorpus,
                        reglamentId: selectedReglament
                    }
                })
                
                if (response.data && response.data.pots && Array.isArray(response.data.pots)) {
                    const sorted = [...response.data.pots].sort((a, b) => {
                        const numA = extractNumber(a.name)
                        const numB = extractNumber(b.name)
                        return numA - numB
                    })
                    
                    setTableData(response.data.pots)
                    setSortedData(sorted)
                } else {
                    setTableData([])
                    setSortedData([])
                }
                
            } catch (err) {
                console.error('Ошибка загрузки данных:', err)
                setTableData([])
                setSortedData([])
            }
        }

        fetchTableData()
    }, [selectedReglament, selectedCorpus, selectedDate])

    const getHeaders = () => {
        if (sortedData.length === 0) return []
        
        const firstItem = sortedData[0]
        const ratioKeys = Object.keys(firstItem.castingRatio || {})
            .sort((a, b) => Number(a) - Number(b))
        
        return ['№ Электролиза', ...ratioKeys]
    }

    const getColumns = () => {
        if (sortedData.length === 0) return []
        
        const firstItem = sortedData[0]
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
        console.log('Сохранение данных...', sortedData)
    }

    const handleSubmit = () => {
        console.log('Отправка данных...', sortedData)
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
                    data={sortedData}
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