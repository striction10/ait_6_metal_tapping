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

document.addEventListener('DOMContentLoaded', () => {
    const popupBtn = document.querySelector('.popupBtn');
    const popupContainer = document.querySelector('.popupContainer');
    const popupContent = document.querySelector('.popupContent');
    const dateInput = document.querySelector('input[type="date"]');
    const currentUser  = JSON.parse(localStorage.getItem('current_user'));
    dateInput.valueAsDate = new Date();
    for(let i = 0; i < users_array.length; i++)
    {
        localStorage.setItem(users_array[i].login, JSON.stringify(users_array[i]));
    }
    if(!localStorage.getItem(currentUser.login)){
        window.location.href = '/auth/auth';
    }
    else {
        document.querySelector('.userInfo.Login').textContent = currentUser.login;
        document.querySelector('.userInfo.Role').textContent = currentUser.role;
    }
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

fetch('http://localhost:3000/reglaments')
.then(response => {
    if (!response.ok){
        throw new Error('Network response was not ok');
    }
    return response.json();
})
.then(data => {
    const table = document.getElementById('data-table');
    const selects = document.querySelectorAll("select");
    const reglamentSelect = selects[0];
    const buildingSelect = selects[1];
    const createCell = (value) => {
        const cell = document.createElement('td');
        cell.textContent = value !== null && value !== undefined ? value : '';
        return cell;
    };
    const updateTable = (buildingNumber, reglamentDate) => {
        while(table.rows.length > 2) {
            table.deleteRow(2);
        }
        let filteredData = data;
        if (reglamentDate !== "0") {
            filteredData = filteredData.filter(reg => reg.date_of_reglament === reglamentDate);
        }
        filteredData.forEach(reglament => {
            const electrolysers = buildingNumber === "0" 
                ? reglament.electrolysers 
                : reglament.electrolysers.filter(el => el.building == buildingNumber);
            electrolysers.forEach(item => {
                const row = document.createElement('tr');
                row.appendChild(createCell(item.id));
                row.appendChild(createCell(item.building));
                row.appendChild(createCell(item["-2"]));
                row.appendChild(createCell(item["-1"]));
                row.appendChild(createCell(item["0"]));
                row.appendChild(createCell(item["1"]));
                row.appendChild(createCell(item["2"]));
                row.appendChild(createCell(item["3"]));
                row.appendChild(createCell(item["4"]));
                
                table.appendChild(row);
            });
        });
    };
    reglamentSelect.addEventListener("change", () => {
        updateTable(buildingSelect.value, reglamentSelect.value);
    });
    buildingSelect.addEventListener("change", () => {
        updateTable(buildingSelect.value, reglamentSelect.value);
    });
    updateTable(buildingSelect.value, reglamentSelect.value);
})
.catch(error => console.error('Ошибка:', error));