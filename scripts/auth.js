const admin = {
    login: 'admin',
    password: 'admin',
    role: 'admin'
}
const operator = {
    login: 'operator',
    password: 'operator',
    role: 'operator'
}
const technologist = {
    login: 'technologist',
    password: 'technologist',
    role: 'technologist'
}
const users_array = [admin, operator, technologist];

document.addEventListener("DOMContentLoaded", () => {
    const auth_btn = document.getElementById('auth_button');
    auth_btn.addEventListener("click", () => {
    const login = document.querySelector('.auth_input.login');
    const password = document.querySelector('.auth_input.password');
    const users = JSON.parse(localStorage.getItem('user_logins')) || [];
    const authenticated = Array.isArray(users) && users.some(user => user.login === login.value && user.password === password.value);
    if (authenticated){
        alert('Успешный вход!!');
        login.classList.add('correct');
        password.classList.add('correct');
    }
    else{
        if (users_array.some(user => user.login === login.value && user.password === password.value)){
            alert('Успешный вход!');
            login.classList.add('correct');
            password.classList.add('correct');
            localStorage.setItem('user_logins', JSON.stringify(users_array.find(user => user.login === login.value && user.password === password.value)));        
        }
        else{
            alert('Неправильный логин или пароль.');
            login.classList.add('error');
            password.classList.add('error');
        }
    }
    });
});