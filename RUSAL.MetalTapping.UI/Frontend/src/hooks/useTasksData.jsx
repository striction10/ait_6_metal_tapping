import { useState, useEffect } from 'react'
import { taskApi } from '../services/tasks'

export function useTasksData(selectedCorpus, selectedDate) {
    const [shiftTaskData, setShiftTaskData] = useState([])
    const [totalTaskData, setTotalTaskData] = useState([])
    const [availableMarks, setAvailableMarks] = useState([])

    const getElementValue = (elements, name) => {
        const element = elements?.find(e => e.name === name)
        return element?.value ?? '-'
    }

    useEffect(() => {
        const fetchTaskData = async () => {
            if (!selectedCorpus || selectedCorpus === '0') {
                setShiftTaskData([])
                setTotalTaskData([])
                setAvailableMarks([])
                return
            }

            try {
                const response = await taskApi.getTasks(
                    new Date(selectedDate).toISOString(),
                    selectedCorpus
                )
                
                const formattedShiftData = []
                const marksSet = new Set()
                
                if (response.data.nightShift?.items) {
                    response.data.nightShift.items.forEach(item => {
                        const mark = item.metalGrade || '-'
                        marksSet.add(mark)
                        
                        formattedShiftData.push({
                            shift1Kg: item.weight || '-',
                            shift1Time: item.time ? new Date(item.time).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }) : '-',
                            shift2Kg: '-',
                            shift2Time: '-',
                            shift3Kg: '-',
                            shift3Time: '-',
                            electrolyzerNum: item.potName || '-',
                            bucketNum: item.scoopName || '-',
                            fe: getElementValue(item.elements, 'Fe'),
                            si: getElementValue(item.elements, 'Si'),
                            cu: getElementValue(item.elements, 'Cu'),
                            mn: getElementValue(item.elements, 'Mn'),
                            mark: mark
                        })
                    })
                }
                
                if (response.data.dayShift?.items) {
                    response.data.dayShift.items.forEach(item => {
                        const mark = item.metalGrade || '-'
                        marksSet.add(mark)
                        
                        formattedShiftData.push({
                            shift1Kg: '-',
                            shift1Time: '-',
                            shift2Kg: item.weight || '-',
                            shift2Time: item.time ? new Date(item.time).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }) : '-',
                            shift3Kg: '-',
                            shift3Time: '-',
                            electrolyzerNum: item.potName || '-',
                            bucketNum: item.scoopName || '-',
                            fe: getElementValue(item.elements, 'Fe'),
                            si: getElementValue(item.elements, 'Si'),
                            cu: getElementValue(item.elements, 'Cu'),
                            mn: getElementValue(item.elements, 'Mn'),
                            mark: mark
                        })
                    })
                }
                
                setShiftTaskData(formattedShiftData)
                
                const uniqueMarks = Array.from(marksSet).filter(m => m !== '-').sort()
                setAvailableMarks(uniqueMarks)
                
                if (response.data.summary) {
                    // const calculateTotalWeight = () => {
                    //     let total = 0
                    //     if (response.data.nightShift?.items) {
                    //         total += response.data.nightShift.items.reduce((sum, item) => sum + (item.weight || 0), 0)
                    //     }
                    //     if (response.data.dayShift?.items) {
                    //         total += response.data.dayShift.items.reduce((sum, item) => sum + (item.weight || 0), 0)
                    //     }
                    //     return total
                    // }
                    setTotalTaskData([{
                        task: response.data.summary.totalWeight ?? 0,
                        mark: response.data.summary.metalGrade || '-'
                    }])
                } else {
                    setTotalTaskData([{ task: 0, mark: '-' }])
                }
                
            } catch (err) {
                setShiftTaskData([])
                setTotalTaskData([])
                setAvailableMarks([])
            }
        }

        fetchTaskData()
    }, [selectedCorpus, selectedDate])

    const filterByMark = (data, mark) => {
        if (!mark || mark === '0') return data
        return data.filter(item => item.mark === mark)
    }

    const filterByShift = (data, shift) => {
        if (shift === 'all') return data
        
        return data.filter(item => {
            if (shift === '1') return item.shift1Kg !== '-'
            if (shift === '2') return item.shift2Kg !== '-'
            if (shift === '3') return item.shift3Kg !== '-'
            return true
        })
    }

    const getFilteredShiftData = (selectedSort, selectedShift) => {
        let filtered = [...shiftTaskData]
        filtered = filterByMark(filtered, selectedSort)
        filtered = filterByShift(filtered, selectedShift)
        return filtered
    }

    const getSortOptions = () => {
        const options = [{ value: "0", label: "Выбрать сортность" }]
        availableMarks.forEach(mark => {
            options.push({ value: mark, label: mark })
        })
        return options
    }

    const headers = {
        shiftTaskHeaders: [
            'Смена 1, кг | Время',
            'Смена 2, кг | Время',
            'Смена 3, кг | Время',
            '№ Электролиза | № Ковша',
            'Fe, %',
            'Si, %',
            'Cu, %',
            'Mn, %',
            'Марка'
        ],
        totalTaskHeaders: [
            'Задание на выливку, кг',
            'Марка'
        ]
    }

    const columns = {
        shiftTaskColumns: [
            { 
                field: 'shift1Kg',
                render: (row) => `${row.shift1Kg || '-'} | ${row.shift1Time || '-'}`
            },
            { 
                field: 'shift2Kg',
                render: (row) => `${row.shift2Kg || '-'} | ${row.shift2Time || '-'}`
            },
            { 
                field: 'shift3Kg',
                render: (row) => `${row.shift3Kg || '-'} | ${row.shift3Time || '-'}`
            },
            { 
                field: 'electrolyzerNum',
                render: (row) => `${row.electrolyzerNum || '-'} | ${row.bucketNum || '-'}`
            },
            { field: 'fe', render: (row) => row.fe || '-' },
            { field: 'si', render: (row) => row.si || '-' },
            { field: 'cu', render: (row) => row.cu || '-' },
            { field: 'mn', render: (row) => row.mn || '-' },
            { field: 'mark', render: (row) => row.mark || '-' }
        ],
        totalTaskColumns: [
            { field: 'task', render: (row) => row.task !== undefined && row.task !== null ? row.task : 0 },
            { field: 'mark', render: (row) => row.mark || '-' }
        ]
    }

    return {
        shiftTaskData,
        totalTaskData,
        headers,
        columns,
        getFilteredShiftData,
        getSortOptions,
        availableMarks
    }
}