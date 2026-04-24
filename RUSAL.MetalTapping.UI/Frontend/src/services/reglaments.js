import api from './api'

export const reglamentsApi = {
    getReglamentTable: (buildingId, reglamentId) => {
        return api.get('/api/reglament/table', {
            params: {
                buildingId: buildingId,
                reglamentId: reglamentId
            }
        })
    },
    
    getAllReglaments: () => {
        return api.get('/api/reglament')
    },
    
    getAllBuildings: () => {
        return api.get('/api/building')
    }
}