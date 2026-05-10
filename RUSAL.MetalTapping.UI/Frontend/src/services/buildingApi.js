const API_BASE = 'https://localhost:7167/api';

const getAuthHeaders = () => {
    const token = localStorage.getItem('token');
    return {
        'Authorization': `Bearer ${token}`,
        'Accept': 'application/json'
    };
};

export const buildingApi = {
    getBuildingMap: async (buildingId) => {
        const res = await fetch(`${API_BASE}/Building/map?builidngId=${buildingId}`, {
            headers: getAuthHeaders()
        });
        if (!res.ok) throw new Error('Ошибка загрузки карты корпуса');
        return res.json();
    }
}