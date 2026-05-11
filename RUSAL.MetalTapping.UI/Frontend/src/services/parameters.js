import api from './api'

export const parametersApi = {
    getParametersTable: (reglamentId, buildingId) => {
        return api.get('/api/parameters/table', {
            params: {
                reglamentId: reglamentId,
                buidlingId: buildingId
            }
        })
    },
    
    updateMetalLevel: (reglamentId, potId, actualMetalLevel) => {
        return api.post('/api/parameters', null, {
            params: {
                reglamentId: reglamentId,
                potId: potId,
                actualMetalLevel: actualMetalLevel
            }
        })
    },
    
    updateCalculatedTask: (potId, calculatedTask) => {
        return api.post(`/api/Parameters/calculated/${potId}`, null, {
            params: {
                calculatedTask: calculatedTask
            }
        })
    },
    
    updateRoundTask: (potId, roundTask) => {
        return api.post(`/api/parameters/round/${potId}`, null, {
            params: {
                roundTask: roundTask
            }
        })
    }
}