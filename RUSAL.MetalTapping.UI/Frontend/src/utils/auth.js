export const decodeToken = (token) => {
    try {
        const base64Url = token.split('.')[1]
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
        }).join(''))
        
        return JSON.parse(jsonPayload)
    } catch (error) {
        console.error('Ошибка декодирования токена:', error)
        return null
    }
}

export const saveUserFromToken = (token) => {
    const userData = decodeToken(token)
    if (userData) {
        localStorage.setItem('user', JSON.stringify({
            login: userData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
            role: userData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
            userId: userData.userId
        }))
        return true
    }
    return false
}

export const getUserData = () => {
    const user = localStorage.getItem('user')
    return user ? JSON.parse(user) : { login: 'Не указан', role: 'Не указана' }
}