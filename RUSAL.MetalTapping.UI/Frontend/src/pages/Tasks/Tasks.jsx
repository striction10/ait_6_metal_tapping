import { useState } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import SendPopup from '../../components/SendPopup/SendPopup'
import { useReglamentsData } from '../../hooks/useReglamentsData'
import { useTasksData } from '../../hooks/useTasksData'
import { exportTasksToPDF } from '../../utils/exportToPDFTasks'
import './Tasks.css'

function Tasks () {
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    const [selectedSort, setSelectedSort] = useState('0')
    const [selectedCorpus, setSelectedCorpus] = useState('0')
    const [selectedShift, setSelectedShift] = useState('all')
    const [isUploadOpen, setIsUploadOpen] = useState(false)
    const { buildings } = useReglamentsData()
    const { 
        totalTaskData, 
        headers, 
        columns,
        getFilteredShiftData,
        getSortOptions,
        availableMarks
    } = useTasksData(selectedCorpus, selectedDate)

    const filteredShiftData = getFilteredShiftData(selectedSort, selectedShift)
    const isCorpusSelected = selectedCorpus && selectedCorpus !== '0'
    const sortOptions = getSortOptions()

    const handleSave = () => {
        const corpusName = buildings.find(b => b.value === selectedCorpus)?.label || selectedCorpus
        exportTasksToPDF(filteredShiftData, totalTaskData, corpusName, selectedDate)
    }

    const handleSubmit = () => {
        setIsUploadOpen(true)
    }

     const handleFileSubmit = (file) => {
        console.log('Файл отправлен:', file)
        setIsUploadOpen(false)
    }

    const handleCorpusChange = (e) => {
        setSelectedCorpus(e.target.value)
        setSelectedSort('0')
    }

    return (
        <>
            <PageTitle title="Задания" />
            <Header 
                title="Задания"
                showNav={true}
                showUserBtn={true}
                activeNav="tasks"
                selectConfig={{
                    selects: [
                        {
                            name: "corpus",
                            options: [
                                { value: "0", label: "Выбрать корпус" },
                                ...buildings
                            ],
                            value: selectedCorpus,
                            onChange: handleCorpusChange
                        },
                        {
                            name: "sort",
                            options: sortOptions,
                            value: selectedSort,
                            onChange: (e) => setSelectedSort(e.target.value),
                            disabled: !isCorpusSelected || availableMarks.length === 0
                        },
                        {
                            name: "shift",
                            options: [
                                { value: "all", label: "Все смены" },
                                { value: "1", label: "Смена 1 (00:00-08:00)" },
                                { value: "2", label: "Смена 2 (08:00-20:00)" },
                                { value: "3", label: "Смена 3 (20:00-00:00)" }
                            ],
                            value: selectedShift,
                            onChange: (e) => setSelectedShift(e.target.value),
                            disabled: !isCorpusSelected
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
                        title="Задание по сменам"
                        headers={headers.shiftTaskHeaders}
                        data={filteredShiftData}
                        columns={columns.shiftTaskColumns}
                        colspan={9}
                    />
                        
                    <Table 
                        title="Итоговое задание на смену"
                        headers={headers.totalTaskHeaders}
                        data={totalTaskData}
                        columns={columns.totalTaskColumns}
                        colspan={2}
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

export default Tasks