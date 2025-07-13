const admin = {
    login: 'admin',
    password: 'admin',
    role: 'admin'
};
const operator = {
    login: 'operator',
    password: 'operator',
    role: 'operator'
};
const technologist = {
    login: 'technologist',
    password: 'technologist',
    role: 'technologist'
};
const users_array = [admin, operator, technologist];
const current_user = {
    login: 'technologist',
    password: 'technologist',
    role: 'technologist'
};

document.addEventListener('DOMContentLoaded', () => {
    const popupBtn = document.querySelector('.popupBtn');
    const popupContainer = document.querySelector('.popupContainer');
    const popupContent = document.querySelector('.popupContent');
    const dateInput = document.querySelector('input[type="date"]');
    dateInput.valueAsDate = new Date();
    for(let i = 0; i < users_array.length; i++)
    {
        localStorage.setItem(users_array[i].login, JSON.stringify(users_array[i]));
    }
    if(!localStorage.getItem(current_user.login)){
        window.location.href = '/auth/auth';
    }
    document.querySelector('.userInfo.Login').textContent = current_user.login;
    document.querySelector('.userInfo.Role').textContent = current_user.role;
    popupBtn.addEventListener('click', (e) => {
        e.stopPropagation();
        popupContainer.style.display = 'flex';
    });
    document.addEventListener('click', (e) => {
        if (!popupContent.contains(e.target)) {
            popupContainer.style.display = 'none';
        }
    });
});