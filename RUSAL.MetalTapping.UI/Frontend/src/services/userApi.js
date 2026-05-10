const API_BASE = 'https://localhost:7167/api';

const getAuthHeaders = (isJson = true) => {
    const token = localStorage.getItem('token');
    const headers = {
        'Authorization': `Bearer ${token}`,
        'Accept': isJson ? 'application/json' : 'text/plain'
    };
    if (isJson) headers['Content-Type'] = 'application/json';
    return headers;
};

export const userApi = {
    getAllUsers: async () => {
        const res = await fetch(`${API_BASE}/User/all`, { headers: getAuthHeaders(false) });
        if (!res.ok) throw new Error('Ошибка загрузки пользователей');
        return res.json();
    },
    registerUser: async (data) => {
        const res = await fetch(`${API_BASE}/Auth/register`, {
            method: 'POST',
            headers: getAuthHeaders(true),
            body: JSON.stringify(data)
        });
        const text = await res.text();
        if (!res.ok) throw new Error(text || 'Ошибка регистрации пользователя');
        return text ? JSON.parse(text) : null;
    },
    deleteUser: async (email) => {
        const res = await fetch(`${API_BASE}/User?email=${encodeURIComponent(email)}`, {
            method: 'DELETE',
            headers: getAuthHeaders(false)
        });
        if (!res.ok) throw new Error('Ошибка удаления пользователя');
        return res.text();
    }
};