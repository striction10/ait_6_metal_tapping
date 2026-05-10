import { useState, useEffect } from 'react';
import { userApi } from '../../services/userApi';
import './AdminUsersPopup.css';

function AdminUsersPopup({ isOpen, onClose }) {
    const [users, setUsers] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');
    const [successMsg, setSuccessMsg] = useState('');
    const [showForm, setShowForm] = useState(false);
    const [formLoading, setFormLoading] = useState(false);
    const [formData, setFormData] = useState({
        email: '', password: '', firstName: '', lastName: '', role: 'User'
    });

    useEffect(() => {
        if (isOpen) fetchUsers();
    }, [isOpen]);

    const fetchUsers = async () => {
        setIsLoading(true);
        setError('');
        try {
            const data = await userApi.getAllUsers();
            setUsers(Array.isArray(data) ? data : []);
        } catch (err) {
            setError(err.message);
        } finally {
            setIsLoading(false);
        }
    };

    const handleFormChange = (e) => {
        setFormData(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        setFormLoading(true);
        setError('');
        setSuccessMsg('');
        try {
            await userApi.registerUser(formData);
            setSuccessMsg('Пользователь успешно добавлен');
            setFormData({ email: '', password: '', firstName: '', lastName: '', role: 'User' });
            setShowForm(false);
            fetchUsers();
        } catch (err) {
            setError(err.message);
        } finally {
            setFormLoading(false);
        }
    };

    const handleDelete = async (email) => {
        if (!window.confirm(`Удалить пользователя ${email}?`)) return;
        setError('');
        setSuccessMsg('');
        try {
            await userApi.deleteUser(email);
            setSuccessMsg('Пользователь удалён');
            fetchUsers();
        } catch (err) {
            setError(err.message);
        }
    };

    if (!isOpen) return null;

    return (
        <div className="aup-overlay" onClick={(e) => e.target === e.currentTarget && onClose()} onKeyDown={(e) => e.key === 'Escape' && onClose()} role="dialog" aria-modal="true">
            <div className="aup-card">
                <button className="aup-close" onClick={onClose} aria-label="Закрыть">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/></svg>
                </button>

                <div className="aup-header">
                    <div className="aup-icon">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                            <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/>
                            <path d="M23 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/>
                        </svg>
                    </div>
                    <h3>Управление пользователями</h3>
                    <p>Просмотр, добавление и удаление учётных записей</p>
                </div>

                <div className="aup-body">
                    {error && <div className="aup-msg aup-msg-error">{error}</div>}
                    {successMsg && <div className="aup-msg aup-msg-success">{successMsg}</div>}

                    <div className="aup-list">
                        {isLoading ? (
                            <div className="aup-loading">Загрузка данных...</div>
                        ) : users.length === 0 ? (
                            <div className="aup-empty">Нет зарегистрированных пользователей</div>
                        ) : (
                            users.map((user, index) => (
                                <div key={user.id || user.email || index} className="aup-item">
                                    <div className="aup-item-info">
                                        <span className="aup-name">{user.firstName} {user.lastName}</span>
                                        <span className="aup-email">{user.email}</span>
                                        <span className={`aup-badge ${user.role?.toLowerCase()}`}>{user.role}</span>
                                    </div>
                                    <button 
                                        className="aup-del-btn" 
                                        onClick={() => handleDelete(user.email)} 
                                        aria-label={`Удалить ${user.email}`}
                                    >
                                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"/></svg>
                                    </button>
                                </div>
                            ))
                        )}
                    </div>

                    <button className="aup-toggle-form" onClick={() => { setShowForm(!showForm); setError(''); setSuccessMsg(''); }}>
                        {showForm ? 'Отменить' : '+ Добавить пользователя'}
                    </button>

                    {showForm && (
                        <form className="aup-form" onSubmit={handleRegister}>
                            <div className="aup-form-grid">
                                <div className="aup-field">
                                    <label>Email</label>
                                    <input type="email" name="email" value={formData.email} onChange={handleFormChange} required placeholder="user@mail.ru" />
                                </div>
                                <div className="aup-field">
                                    <label>Пароль</label>
                                    <input type="password" name="password" value={formData.password} onChange={handleFormChange} required placeholder="••••••••" />
                                </div>
                                <div className="aup-field">
                                    <label>Имя</label>
                                    <input type="text" name="firstName" value={formData.firstName} onChange={handleFormChange} required placeholder="Иван" />
                                </div>
                                <div className="aup-field">
                                    <label>Фамилия</label>
                                    <input type="text" name="lastName" value={formData.lastName} onChange={handleFormChange} required placeholder="Иванов" />
                                </div>
                                <div className="aup-field aup-field-full">
                                    <label>Роль</label>
                                    <select name="role" value={formData.role} onChange={handleFormChange}>
                                        <option value="User">Пользователь</option>
                                        <option value="Technologist">Технолог</option>
                                        <option value="Admin">Администратор</option>
                                    </select>
                                </div>
                            </div>
                            <button type="submit" className="aup-submit" disabled={formLoading}>
                                {formLoading ? <span className="aup-spinner"></span> : 'Зарегистрировать'}
                            </button>
                        </form>
                    )}
                </div>
            </div>
        </div>
    );
}

export default AdminUsersPopup;