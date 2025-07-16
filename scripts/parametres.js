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

fetch('http://localhost:3000/parametres')
.then(response => {
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    return response.json();
})
.then(data => {
    const table = document.getElementById('data-table');
    const buildingSelector = document.querySelector("select");
    const saveBtn = document.querySelector('.btn-down button:first-child');
    const currentUser  = JSON.parse(localStorage.getItem('current_user'));
    const calculateDeviation = (target, actual) => {
        return (actual - target).toFixed(2);
    };
    const createCell = (field, value, isEditable = false, rowData = null) => {
        const cell = document.createElement('td');
        if(isEditable){
            const current_input = document.createElement('input');
            current_input.type = 'number';
            current_input.value = value ?? '';
            current_input.dataset.field = field;
            if(currentUser.role == 'operator'){
                current_input.classList.add('operator');
            } else {
                current_input.classList.add('technologist');
            }
            if (field === 'target_metal_level' || field === 'actual_metal_level') {
                current_input.addEventListener('input', function() {
                    const row = this.closest('tr');
                    const targetInput = row.querySelector('input[data-field="target_metal_level"]');
                    const actualInput = row.querySelector('input[data-field="actual_metal_level"]');
                    const targetValue = parseFloat(targetInput.value) || 0;
                    const actualValue = parseFloat(actualInput.value) || 0;
                    const deviationCell = row.querySelector('.deviation-cell');
                    if (deviationCell) {
                        deviationCell.textContent = calculateDeviation(targetValue, actualValue);
                    }
                    if (currentUser.role === 'operator') {
                        const zprCell = row.querySelector('.zpr-cell');
                        if (zprCell) {
                            zprCell.textContent = calculateDeviation(targetValue, actualValue);
                        }
                    }
                });
            }       
            cell.appendChild(current_input);
        } 
        else {
            cell.textContent = value !== null ? value : '';
            if (value == null) {
                cell.classList.add('red-item');
            }
        }
        return cell;
    };
    const createDevCell = (value) => {
        const cell = document.createElement('td');
        cell.textContent = value;
        cell.classList.add('deviation-cell');
        return cell;
    };
    const createTechCell = (value, item) => {
        const cell = document.createElement('td');
        cell.classList.add('zpr-cell');
        if (currentUser.role == 'operator') {
            const zprValue = calculateDeviation(item.target_metal_level, item.actual_metal_level);
            cell.textContent = zprValue;
        } 
        else {
            const current_input = document.createElement('input');
            current_input.type = 'number';
            current_input.value = value ?? '';
            current_input.classList.add('technologist');
            cell.appendChild(current_input);
        }
        return cell;
    };
    const updateTable = (buildingNumber) => {
        while (table.rows.length > 2) {
            table.deleteRow(2);
        }
        const filteredData = buildingNumber == 0 ? data : data.filter(item => item.building_number == buildingNumber);
        filteredData.forEach(item => {
            const row = document.createElement('tr');
            const initialDeviation = calculateDeviation(item.target_metal_level, item.actual_metal_level);
            row.appendChild(createCell('electrolysis_number', item.id));
            row.appendChild(createCell('target_metal_level', item.target_metal_level, true, item));
            row.appendChild(createCell('actual_metal_level', item.actual_metal_level, true, item));
            row.appendChild(createDevCell(initialDeviation));
            row.appendChild(createCell('current_strength', item.current_strength));
            row.appendChild(createCell('current_output', item.current_output));
            row.appendChild(createCell('calculated_task', item.calculated_task));
            row.appendChild(createTechCell(item.zpr, item));
            row.appendChild(createCell('grade', item.grade));
            table.appendChild(row);
        });
    };
    buildingSelector.addEventListener("change", () => {
        updateTable(buildingSelector.value);
    });   
    updateTable(buildingSelector.value);
    saveBtn.addEventListener("click", async () => {
    const newParams = [];
    function findBuildingNumber(electrolysisId) {
        const id = parseInt(electrolysisId);
        if (isNaN(id)) return '1';
        const item = data.find(item => item.id == electrolysisId);
        return item ? item.building_number : '1';
    }
    for(let i = 2, row; row = table.rows[i]; i++) {
            const getCellValue = (cell) => {
                const input = cell.querySelector('input');
                if (input) {
                    return input.value;
                }
                return cell.textContent.trim();
            };
            const current_row = {
                'id': getCellValue(row.cells[0]),
                'building_number': findBuildingNumber(getCellValue(row.cells[0])),
                "target_metal_level": getCellValue(row.cells[1]),
                "actual_metal_level": getCellValue(row.cells[2]),
                "deviation": getCellValue(row.cells[3]),
                "current_strength": getCellValue(row.cells[4]),
                "current_output": getCellValue(row.cells[5]),
                "calculated_task": getCellValue(row.cells[6]),
                "zpr": getCellValue(row.cells[7]),
                "grade": getCellValue(row.cells[8])
            };
            newParams.push(current_row);
        }
        try {
            await Promise.all(
                newParams.map(item => 
                    fetch(`http://localhost:3000/parametres/${item.id}`, {
                        method: 'PATCH',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify(item)
                    })
                    .then(response => {
                        if (!response.ok) throw new Error(`Failed to save item ${item.id}`);
                        return response.json();
                    })
                )
            );
            alert('Все изменения сохранены успешно!');
        } catch (err) {
            console.error('Ошибка сохранения:', err);
            alert(`Ошибка при сохранении: ${err.message}`);
        }
    });
})
.catch(error => console.error('Ошибка при загрузке данных:', error));