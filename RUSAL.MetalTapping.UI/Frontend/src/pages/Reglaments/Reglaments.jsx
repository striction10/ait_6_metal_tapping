import { useState, useEffect } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import SendPopup from "../../components/SendPopup/SendPopup"
import { useReglamentsData } from '../../hooks/useReglamentsData'
import { exportReglamentsToPDF } from '../../utils/exportToPDFReglaments'
import api from '../../services/api'

function Reglaments() {
    const [selectedReglament, setSelectedReglament] = useState('')
    const [selectedCorpus, setSelectedCorpus] = useState('')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    
    const [sortedData, setSortedData] = useState([])
    const [isUploadOpen, setIsUploadOpen] = useState(false)
    
    const { reglaments, buildings } = useReglamentsData()

    const extractNumber = (name) => {
        const match = name?.match(/\d+/)
        return match ? parseInt(match[0]) : 0
    }

    useEffect(() => {
        const fetchTableData = async () => {
            if (!selectedReglament || !selectedCorpus || 
                selectedReglament === '0' || selectedCorpus === '0') {
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
                    
                    setSortedData(sorted)
                } else {
                    setSortedData([])
                }
                
            } catch (err) {
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
        const corpusName = buildings.find(b => b.value === selectedCorpus)?.label || selectedCorpus
        const reglamentName = reglaments.find(r => r.value === selectedReglament)?.label || selectedReglament
        
        exportReglamentsToPDF(sortedData, corpusName, reglamentName)
    }

    const handleSubmit = () => {
        setIsUploadOpen(true)
    }

     const handleFileSubmit = (file) => {
        console.log('Файл отправлен:', file)
        setIsUploadOpen(false)
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

            <SendPopup
                isOpen={isUploadOpen}
                onClose={() => setIsUploadOpen(false)}
                onSubmit={handleFileSubmit}
            />
        </>
    )
}

export default Reglaments