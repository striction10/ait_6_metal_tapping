import { useState, useEffect } from 'react'
import api from '../services/api'

export function useParametersData(selectedCorpus, selectedDate) {
    const [sortedData, setSortedData] = useState([])

    const extractNumber = (name) => {
        const match = name?.match(/\d+/)
        return match ? parseInt(match[0]) : 0
    }

    useEffect(() => {
        const fetchAllReglamentsData = async () => {
            if (!selectedCorpus || selectedCorpus === '0') {
                setSortedData([])
                return
            }

            try {
                const reglamentsRes = await api.get('/api/reglament')
                const reglamentsForCorpus = reglamentsRes.data

                if (reglamentsForCorpus.length === 0) {
                    setSortedData([])
                    return
                }

                const allDataPromises = reglamentsForCorpus.map(async (reglament) => {
                    try {
                        const response = await api.get('/api/parameters/table', {
                            params: {
                                reglamentId: reglament.id,
                                buidlingId: selectedCorpus
                            }
                        })
                        
                        if (response.data && response.data.pots && Array.isArray(response.data.pots)) {
                            return response.data.pots.map(pot => ({
                                ...pot,
                                reglamentName: reglament.name || `Регламент ${new Date(reglament.dateStart).toLocaleDateString()}`,
                                reglamentId: reglament.id
                            }))
                        }
                        return []
                    } catch (err) {
                        console.error(`Ошибка загрузки для регламента ${reglament.name}:`, err)
                        return []
                    }
                })

                const allResults = await Promise.all(allDataPromises)
                const combinedData = allResults.flat()
                
                if (combinedData.length > 0) {
                    const sorted = [...combinedData].sort((a, b) => {
                        const numA = extractNumber(a.potName)
                        const numB = extractNumber(b.potName)
                        return numA - numB
                    })
                    
                    setSortedData(sorted)
                } else {
                    setSortedData([])
                }
                
            } catch (err) {
                console.error('Ошибка загрузки данных:', err)
                setSortedData([])
            }
        };

        fetchAllReglamentsData()
    }, [selectedCorpus, selectedDate])

    const headers = [
        '№ Электролиза',
        'Уровень металла, цель',
        'Уровень металла, факт',
        'Отклонение, см',
        'Сила тока, кА',
        'Выход по току, %',
        'Расчетное задание, кг',
        'ЗПР, кг',
        'Марка'
    ]

    const columns = [
        { 
            field: 'potName',
            render: (row) => row.potName || '-'
        },
        { 
            field: 'targetMetalLevel',
            render: (row) => row.targetMetalLevel?.toFixed(2) ?? '-'
        },
        { 
            field: 'actualMetalLevel',
            render: (row) => row.actualMetalLevel?.toFixed(2) ?? '-'
        },
        { 
            field: 'deviationValue',
            render: (row) => {
                if (row.deviationValue !== undefined && row.deviationValue !== null) {
                    return row.deviationValue.toFixed(2);
                }
                if (row.targetMetalLevel && row.actualMetalLevel) {
                    return (row.actualMetalLevel - row.targetMetalLevel).toFixed(2);
                }
                return '-';
            }
        },
        { 
            field: 'amperage',
            render: (row) => row.amperage?.toFixed(0) ?? '-'
        },
        { 
            field: 'avgAmperage',
            render: (row) => row.avgAmperage?.toFixed(0) ?? '-'
        },
        { 
            field: 'calculatedTask',
            render: (row) => row.calculatedTask?.toFixed(2) ?? '-'
        },
        { 
            field: 'roundCalculatedTask',
            render: (row) => row.roundCalculatedTask?.toFixed(2) ?? '-'
        },
        { 
            field: 'metalMarkName',
            render: (row) => row.metalMarkName || '-'
        }
    ]

    return {
        sortedData,
        setSortedData,
        headers,
        columns
    }
}