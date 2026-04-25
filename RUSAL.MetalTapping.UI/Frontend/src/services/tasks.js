import api from './api'

export const taskApi = {
    getTasks: (date, buildingId) => {
        return api.post('/api/Task', {
            date: date,
            buildingId: buildingId
        })
    }
}