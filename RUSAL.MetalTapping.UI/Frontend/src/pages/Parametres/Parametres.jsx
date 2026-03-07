import { useState } from 'react'
import Header from '../../components/Header/Header'
import PageTitle from '../../components/PageTitle'
import Table from '../../components/Table/Table'

function Parametres() {
    const [selectedCorpus, setSelectedCorpus] = useState('0')
    const [selectedDate, setSelectedDate] = useState(
        new Date().toISOString().split('T')[0]
    )
    const headers = [
        '№ Электролиза',
        'Уровень металла, цель',
        'Уровень металла, факт',
        'Отклонение',
        'Сила тока, кА',
        'Выход по току, %',
        'Расчетное задание, кг',
        'ЗПР, кг',
        'Марка'
    ]

    const data = [
        { id: 1, levelTarget: 10, levelFact: 9.5, deviation: -0.5, current: 150, efficiency: 95, task: 1000, zpr: 950, mark: 'A6' },
        { id: 2, levelTarget: 10, levelFact: 9.5, deviation: -0.5, current: 150, efficiency: 95, task: 1000, zpr: 950, mark: 'A6' },
    ]

    const columns = [
        { field: 'id' },
        { field: 'levelTarget' },
        { field: 'levelFact' },
        { field: 'deviation' },
        { field: 'current' },
        { field: 'efficiency' },
        { field: 'task' },
        { field: 'zpr' },
        { field: 'mark' }
    ]
    return (
        <>
            <PageTitle title={"Параметры"} />
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
                                { value: "", label: "Выбрать корпус" },
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
            <Table 
                title="Таблица параметров"
                headers={headers}
                data={data}
                columns={columns}
                colspan={10}
            />
        </>
    )
}

export default Parametres