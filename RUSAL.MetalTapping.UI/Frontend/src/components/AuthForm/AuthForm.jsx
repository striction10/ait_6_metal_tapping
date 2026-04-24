import { useState } from 'react';
import Input from '../Input/Input';
import Button from '../Button/Button';
import './AuthForm.css';

function AuthForm({ onSubmit }) {
    const [formData, setFormData] = useState({
        username: '',
        password: ''
    });

    const [errors, setErrors] = useState({
        username: false,
        password: false
    });

    function handleChange(e) {
        const { name, value } = e.target;
        const lettersOnly = value.replace(/[^a-zA-Zа-яА-Я]/g, '');
        setFormData(prev => ({
            ...prev,
            [name]: lettersOnly
        }));
        
        if (errors[name]) {
            setErrors(prev => ({
                ...prev,
                [name]: false
            }));
        }
    }

    function handleSubmit(e) {
        e.preventDefault();
        
        const newErrors = {
            username: !formData.username,
            password: !formData.password
        };
        
        setErrors(newErrors);
        
        if (!newErrors.username && !newErrors.password) {
            onSubmit?.(formData);
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <Input
                label="Логин"
                type="text"
                name="username"
                value={formData.username}
                onChange={handleChange}
                placeholder="Введите логин"
                error={errors.username}
            />
            
            <Input
                label="Пароль"
                type="password"
                name="password"
                value={formData.password}
                onChange={handleChange}
                placeholder="Введите пароль"
                error={errors.password}
            />
            
            <div className="button-container">
                <Button type="submit" variant="primary">
                    Войти
                </Button>
            </div>
        </form>
    );
}

export default AuthForm