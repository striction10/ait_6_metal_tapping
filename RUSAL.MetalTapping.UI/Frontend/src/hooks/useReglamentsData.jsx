import { useState, useEffect } from 'react'
import api from '../services/api'

export function useReglamentsData() {
    const [reglaments, setReglaments] = useState([])
    const [buildings, setBuildings] = useState([])

    const extractNumber = (str) => {
        const match = str?.match(/\d+/)
        return match ? parseInt(match[0]) : 0
    }

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [reglamentsRes, buildingsRes] = await Promise.all([
                    api.get('/api/reglament'),
                    api.get('/api/building')
                ])

                const sortedBuildings = buildingsRes.data
                    .map(building => ({
                        value: building.id,
                        label: building.name || `Корпус ${building.number || ''}`
                    }))
                    .sort((a, b) => {
                        const numA = extractNumber(a.label);
                        const numB = extractNumber(b.label);
                        return numA - numB;
                    })

                const sortedReglaments = reglamentsRes.data
                    .map(reg => ({
                        value: reg.id,
                        label: reg.name || `Регламент ${new Date(reg.dateStart).toLocaleDateString()}`
                    }))
                    .sort((a, b) => {
                        const numA = extractNumber(a.label)
                        const numB = extractNumber(b.label)
                        return numA - numB
                    })

                setReglaments(sortedReglaments)
                setBuildings(sortedBuildings)
                
            } catch (err) {
                console.error('Ошибка загрузки данных:', err)
            }
        };

        fetchData()
    }, [])

    return { reglaments, buildings }
}