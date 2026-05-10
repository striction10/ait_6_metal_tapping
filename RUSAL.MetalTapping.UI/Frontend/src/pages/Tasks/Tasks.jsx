import { useState, useEffect } from 'react'
import Header from '../../components/Header/Header'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'
import SendPopup from '../../components/SendPopup/SendPopup'
import BuildingMapPopup from "../../components/BuildingMapPopup/BuildingMapPopup"
import { useReglamentsData } from '../../hooks/useReglamentsData'
import { useTasksData } from '../../hooks/useTasksData'
import { exportTasksToPDF } from '../../utils/exportToPDFTasks'
import './Tasks.css'

function Tasks() {
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    const [selectedSort, setSelectedSort] = useState('0')
    const [selectedCorpus, setSelectedCorpus] = useState('0')
    const [selectedShift, setSelectedShift] = useState('all')
    const [isUploadOpen, setIsUploadOpen] = useState(false)
    const [isMapOpen, setIsMapOpen] = useState(false)
    
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
    const hasData = filteredShiftData.length > 0 || totalTaskData.length > 0

    const handleSave = () => {
        const corpusName = buildings.find(b => b.value === selectedCorpus)?.label || selectedCorpus
        exportTasksToPDF(filteredShiftData, totalTaskData, corpusName, selectedDate)
    }

    const handleSubmit = () => setIsUploadOpen(true)
    const handleFileSubmit = () => setIsUploadOpen(false)

    useEffect(() => {
            document.title = "Задания"
    }, []);

    const handleCorpusChange = (e) => {
        setSelectedCorpus(e.target.value)
        setSelectedSort('0')
    }

    return (
        <>
            <Header 
                title="Задания"
                showNav={true}
                showUserBtn={true}
                activeNav="tasks"
                selectConfig={{
                    selects: [
                        {
                            name: "corpus",
                            options: [{ value: "0", label: "Выбрать корпус" }, ...buildings],
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
            
            <div className="tasks-main">
                <div className="tasks-content">
                    {hasData ? (
                        <>
                            <Table 
                                title="Задание по сменам"
                                headers={headers.shiftTaskHeaders}
                                data={filteredShiftData}
                                columns={columns.shiftTaskColumns}
                                colspan={9}
                                canEdit={() => false}
                            />
                            
                            <Table 
                                title="Итоговое задание на смену"
                                headers={headers.totalTaskHeaders}
                                data={totalTaskData}
                                columns={columns.totalTaskColumns}
                                colspan={2}
                                canEdit={() => false}
                            />
                            <ActionButtons 
                                onSave={handleSave}
                                onSubmit={handleSubmit}
                                onMap={() => setIsMapOpen(true)}
                                selectedCorpus={selectedCorpus}
                            />
                        </>
                    ) : (
                        <div className="tasks-empty">
                            <div className="tasks-empty-icon">
                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.5">
                                    <rect x="3" y="3" width="18" height="18" rx="2"/>
                                    <path d="M3 9h18"/><path d="M9 21V9"/>
                                </svg>
                            </div>
                            <h3>Выберите корпус</h3>
                            <p>Для отображения заданий выберите корпус и дату в панели выше</p>
                        </div>
                    )}
                </div>
            </div>

            <SendPopup
                isOpen={isUploadOpen}
                onClose={() => setIsUploadOpen(false)}
                pageType="tasks"
                selectedDate={selectedDate}
            />

            <BuildingMapPopup 
                isOpen={isMapOpen} 
                onClose={() => setIsMapOpen(false)} 
                buildingId={selectedCorpus} 
            />

        </>
    )
}

export default Tasks