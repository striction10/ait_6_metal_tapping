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

fetch('http://localhost:3000/tasks')
.then(response => {
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    return response.json();
})
.then(data => {
    const table1 = document.getElementsByClassName('data-table')[0];
    const table2 = document.getElementsByClassName('data-table')[1];
    const selects = document.querySelectorAll("select");
    const sortSelect = selects[0];
    const buildingSelect = selects[1];
    const timeSelect = selects[2];
    const dateInput = document.querySelector("input[type='date']");

    const createCell = (value) => {
        const cell = document.createElement('td');
        cell.textContent = value !== null && value !== undefined ? value : '';
        return cell;
    };

    const formatDate = (dateString) => {
        const [day, month, year] = dateString.split('.');
        return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
    };

    const isInShift = (timeString, shift) => {
        if (!timeString) return false;
        const [hours, minutes] = timeString.split(':').map(Number);
        const totalMinutes = hours * 60 + minutes;
        switch(shift) {
            case '1': return totalMinutes >= 300 && totalMinutes < 780;
            case '2': return totalMinutes >= 780 && totalMinutes < 1260;
            case '3': return totalMinutes >= 1260 || totalMinutes < 300;
            default: return true;
        }
    };

    const updateTable = () => {
        const selectedBuilding = buildingSelect.value;
        const selectedSort = sortSelect.value;
        const selectedDate = dateInput.value;
        const selectedShift = timeSelect.value;

        while(table1.rows.length > 2) {
            table1.deleteRow(2);
        }
        while(table2.rows.length > 2) {
            table1.deleteRow(2);
        }
        
        const filteredData = data.filter(item => {
            if (selectedBuilding !== "0" && item.electrolyzer_number !== selectedBuilding) {
                return false;
            }
            
            if (selectedSort !== "0" && item.grade !== selectedSort) {
                return false;
            }
            
            if (selectedDate && formatDate(item.date) !== selectedDate) {
                return false;
            }

            if (selectedShift !== "all") {
                const hasShift1 = item.shift_1_time && isInShift(item.shift_1_time, selectedShift);
                const hasShift2 = item.shift_2_time && isInShift(item.shift_2_time, selectedShift);
                const hasShift3 = item.shift_3_time && isInShift(item.shift_3_time, selectedShift);
                
                if (!hasShift1 && !hasShift2 && !hasShift3) {
                    return false;
                }
            }
            
            return true;
        });
        
        filteredData.forEach(item => {
            const row = document.createElement('tr');
            row.appendChild(createCell(item.shift_1_kg));
            row.appendChild(createCell(item.shift_1_time));
            row.appendChild(createCell(item.shift_2_kg));
            row.appendChild(createCell(item.shift_2_time));
            row.appendChild(createCell(item.shift_3_kg));
            row.appendChild(createCell(item.shift_3_time));
            row.appendChild(createCell(item.electrolyzer_number));
            row.appendChild(createCell(item.ladle));
            row.appendChild(createCell(item.percentage_1));
            row.appendChild(createCell(item.percentage_2));
            row.appendChild(createCell(item.grade));
            table1.appendChild(row);
        });
        const gradeGroups = filteredData.reduce((acc, item) => {
            const grade = item.grade;
            if (!acc[grade]) {
                acc[grade] = {
                    totalKg: 0,
                    count: 0
                };
            }
            
            acc[grade].totalKg += 
                (parseInt(item.shift_1_kg) || 0) + 
                (parseInt(item.shift_2_kg) || 0) + 
                (parseInt(item.shift_3_kg) || 0);
            acc[grade].count++;
            
            return acc;
        }, {});
        
        for (const [grade, data] of Object.entries(gradeGroups)) {
            const row = document.createElement('tr');
            row.appendChild(createCell(data.totalKg));
            row.appendChild(createCell(grade));
            table2.appendChild(row);
        }
    };

    sortSelect.addEventListener("change", updateTable);
    buildingSelect.addEventListener("change", updateTable);
    dateInput.addEventListener("change", updateTable);
    timeSelect.addEventListener("change", updateTable);
    updateTable();
})
.catch(error => console.error('Ошибка:', error));