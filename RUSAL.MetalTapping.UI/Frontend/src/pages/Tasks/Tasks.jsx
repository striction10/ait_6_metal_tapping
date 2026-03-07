import { useState } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'
import './Tasks.css'

function Tasks () {
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    const [selectedSort, setSelectedSort] = useState('0')
    const [selectedCorpus, setSelectedCorpus] = useState('0')
    const [selectedShift, setSelectedShift] = useState('all')

    const shiftTaskData = [
        { 
            shift1Kg: 1200, shift1Time: '05:00',
            shift2Kg: 1150, shift2Time: '13:00',
            shift3Kg: 1180, shift3Time: '21:00',
            electrolyzerNum: 12, bucketNum: 3,
            fe: 0.3, si: 0.2, mark: 'A6'
        },
        { 
            shift1Kg: 1250, shift1Time: '05:00',
            shift2Kg: 1200, shift2Time: '13:00',
            shift3Kg: 1220, shift3Time: '21:00',
            electrolyzerNum: 15, bucketNum: 5,
            fe: 0.4, si: 0.3, mark: 'A7'
        },
        { 
            shift1Kg: 1100, shift1Time: '05:00',
            shift2Kg: 1080, shift2Time: '13:00',
            shift3Kg: 1120, shift3Time: '21:00',
            electrolyzerNum: 8, bucketNum: 2,
            fe: 0.2, si: 0.1, mark: 'A6'
        },
        { 
            shift1Kg: 1300, shift1Time: '05:00',
            shift2Kg: 1280, shift2Time: '13:00',
            shift3Kg: 1320, shift3Time: '21:00',
            electrolyzerNum: 20, bucketNum: 7,
            fe: 0.5, si: 0.4, mark: 'A8'
        },
    ]

    const shiftTaskHeaders = [
        'Смена 1, кг | Время',
        'Смена 2, кг | Время',
        'Смена 3, кг | Время',
        '№ Электролиза | № Ковша',
        'Fe, %',
        'Si, %',
        'Марка'
    ]

    const shiftTaskColumns = [
        { field: 'shift1Kg' },
        { field: 'shift2Kg' },
        { field: 'shift3Kg' },
        { field: 'electrolyzerNum' },
        { field: 'fe' },
        { field: 'si' },
        { field: 'mark' }
    ]

    const totalTaskData = [
        { task: 5000, mark: 'A6' }
    ]

    const totalTaskHeaders = [
        'Задание на выливку, кг',
        'Марка'
    ]

    const totalTaskColumns = [
        { field: 'task' },
        { field: 'mark' }
    ]

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
                            name: "sort",
                            options: [
                                { value: "0", label: "Выбрать сортность" },
                                { value: "A6", label: "А6" },
                                { value: "A7", label: "А7" },
                                { value: "A8", label: "А8" }
                            ],
                            value: selectedSort,
                            onChange: (e) => setSelectedSort(e.target.value)
                        },
                        {
                            name: "corpus",
                            options: [
                                { value: "0", label: "Выбрать корпус" },
                                { value: "1", label: "Корпус 1" },
                                { value: "2", label: "Корпус 2" },
                                { value: "3", label: "Корпус 3" }
                            ],
                            value: selectedCorpus,
                            onChange: (e) => setSelectedCorpus(e.target.value)
                        },
                        {
                            name: "shift",
                            options: [
                                { value: "all", label: "Все смены" },
                                { value: "1", label: "Смена 1 (05:00-13:00)" },
                                { value: "2", label: "Смена 2 (13:00-21:00)" },
                                { value: "3", label: "Смена 3 (21:00-05:00)" }
                            ],
                            value: selectedShift,
                            onChange: (e) => setSelectedShift(e.target.value)
                        }
                    ],
                    showDate: true,
                    dateValue: selectedDate,
                    onDateChange: (e) => setSelectedDate(e.target.value)
                }}
            />
            
            <div className="tasks-container">
                <div className="tables-wrapper">
                    <Table 
                        title="Задание по сменам"
                        headers={shiftTaskHeaders}
                        data={shiftTaskData}
                        columns={shiftTaskColumns}
                        colspan={7}
                    />
                        
                    <Table 
                        title="Итоговое задание на смену"
                        headers={totalTaskHeaders}
                        data={totalTaskData}
                        columns={totalTaskColumns}
                        colspan={2}
                    />
                </div>
            </div>
        </>
    );
}

export default Tasks