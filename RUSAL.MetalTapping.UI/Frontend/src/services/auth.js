import api from './api'

export const authApi = {
    login: (email, password) => {
        return api.post('/api/auth/login', {
            email: email,
            password: password
        })
    }
}