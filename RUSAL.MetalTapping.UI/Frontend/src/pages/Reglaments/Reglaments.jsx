import { useState } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components//PageTitle'
import Table from '../../components/Table/Table'
import ActionButtons from '../../components/ActionButton/ActionButton'

function Reglaments () {
    const [selectedReglament, setSelectedReglament] = useState('0')
    const [selectedCorpus, setSelectedCorpus] = useState('0')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )

    const pouringData = [
        { electrolyzerNumber: 1, m2: 5, m1: 3, zero: 2, p1: 4, p2: 6, p3: 1, p4: 0 },
        { electrolyzerNumber: 2, m2: -2, m1: 4, zero: 3, p1: -1, p2: 5, p3: 2, p4: 1 },
        { electrolyzerNumber: 3, m2: 3, m1: 2, zero: 1, p1: 0, p2: 4, p3: 3, p4: 2 },
        { electrolyzerNumber: 4, m2: 1, m1: -3, zero: 2, p1: 3, p2: 2, p3: 1, p4: 4 },
        { electrolyzerNumber: 5, m2: 4, m1: 5, zero: 3, p1: 2, p2: 1, p3: 0, p4: 3 }
    ]

    const tableHeaders = [
        '№ Электролизёра',
        '-2',
        '-1',
        '0',
        '1',
        '2',
        '3',
        '4'
    ]

    const columns = [
        { field: 'electrolyzerNumber' },
        { field: 'm2' },
        { field: 'm1' },
        { field: 'zero' },
        { field: 'p1' },
        { field: 'p2' },
        { field: 'p3' },
        { field: 'p4' }
    ]

    const handleSave = () => {
        console.log('Сохранение данных...')
        alert('Данные сохранены')
    }

    const handleSubmit = () => {
        console.log('Отправка данных...')
        alert('Данные отправлены')
    }

    return (
        <>
            <PageTitle title={"Регламенты"} />
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
                                { value: "1", label: "Регламент 08.04.2026" },
                                { value: "2", label: "Регламент 08.05.2026" },
                                { value: "3", label: "Регламент 08.06.2026" }
                            ],
                            value: selectedReglament,
                            onChange: (e) => setSelectedReglament(e.target.value)
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
                        title="Таблица выливки, %"
                        headers={tableHeaders}
                        data={pouringData}
                        columns={columns}
                        colspan={8}
                    />
                </div>
                <ActionButtons 
                    onSave={handleSave}
                    onSubmit={handleSubmit}
                />
            </div>
        </>
    );
}

export default Reglaments