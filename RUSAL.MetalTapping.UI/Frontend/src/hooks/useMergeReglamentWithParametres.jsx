import { useState, useEffect } from 'react'
import { reglamentsApi } from '../services/reglaments'
import { parametersApi } from '../services/parameters'

export function useParametersWithReglaments(selectedCorpus, selectedReglament, selectedDate) {
    const [sortedData, setSortedData] = useState([])

    useEffect(() => {
        const fetchData = async () => {
            if (!selectedCorpus || selectedCorpus === '0' || 
                !selectedReglament || selectedReglament === '0') {
                setSortedData([])
                return
            }

            try {
                const reglamentResponse = await reglamentsApi.getReglamentTable(selectedCorpus, selectedReglament)
                const reglamentPots = reglamentResponse.data?.pots || []
                
                const reglamentMap = new Map()
                reglamentPots.forEach(pot => {
                    reglamentMap.set(pot.name, pot.castingRatio || {})
                })
                
                const parametersResponse = await parametersApi.getParametersTable(selectedReglament, selectedCorpus)
                const parametersPots = parametersResponse.data?.pots || []
                
                const mergedData = parametersPots.map(pot => {
                    const castingRatio = reglamentMap.get(pot.potName) || {}
                    
                    const ratioKeys = Object.keys(castingRatio).map(Number).filter(k => !isNaN(k))
                    const minDeviation = ratioKeys.length > 0 ? Math.min(...ratioKeys) : -5
                    const maxDeviation = ratioKeys.length > 0 ? Math.max(...ratioKeys) : 5
                    
                    return {
                        ...pot,
                        castingRatio,
                        minDeviation,
                        maxDeviation
                    }
                })
                
                const sorted = [...mergedData].sort((a, b) => {
                    const numA = parseInt(a.potName?.match(/\d+/)?.[0] || 0)
                    const numB = parseInt(b.potName?.match(/\d+/)?.[0] || 0)
                    return numA - numB
                })
                
                setSortedData(sorted)
            } catch (err) {
                console.error('Ошибка загрузки данных:', err)
                setSortedData([])
            }
        }

        fetchData()
    }, [selectedCorpus, selectedReglament, selectedDate])

    return { sortedData, setSortedData }
}