import { useState, useEffect } from 'react'
import api from '../services/api'

export function useReglamentsData() {
    const [reglaments, setReglaments] = useState([])
    const [buildings, setBuildings] = useState([])

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [reglamentsRes, buildingsRes] = await Promise.all([
                    api.get('/api/reglament'),
                    api.get('/api/building')
                ]);

                const reglamentsOptions = reglamentsRes.data.map(reg => ({
                    value: reg.id,
                    label: reg.name || `Регламент ${new Date(reg.date).toLocaleDateString()}`
                }));

                const buildingsOptions = buildingsRes.data.map(building => ({
                    value: building.id,
                    label: building.name || `Корпус ${building.number || ''}`
                }));

                setReglaments(reglamentsOptions);
                setBuildings(buildingsOptions);
            } catch (err) {
                console.error('Ошибка загрузки данных:', err);
            }
        };

        fetchData();
    }, [])

    return { reglaments, buildings }
}