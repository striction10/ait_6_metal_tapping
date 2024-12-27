const admin = {
    login: 'admin',
    password: 'admin'
}
const user = {
    login: 'user',
    password: 'user'
}
const users_array = [admin, user];
localStorage.setItem('users', JSON.stringify(users_array));

document.addEventListener("DOMContentLoaded", () => {
    const auth_btn = document.getElementById('auth_button');
    auth_btn.addEventListener("click", () => {
    const login = document.getElementById('login_input').value;
    const password = document.getElementById('password_input').value;
    const users = JSON.parse(localStorage.getItem('users'));
    const authenticated = users.some(user => user.login === login && user.password === password);
    if (authenticated) {
        alert('Успешный вход!');
        login_input.classList.add('correct');
        password_input.classList.add('correct'); 
    } else {
        alert('Неправильный логин или пароль.');
        login_input.classList.add('error');
        password_input.classList.add('error'); 
    }
    });
});



