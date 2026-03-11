import '../ActionButton/ActionButton.css'

function ActionButtons({ onSave, onSubmit }) {
    return (
        <div className="btn-down">
            <button onClick={onSave}>Сохранить</button>
            <span>/</span>
            <button onClick={onSubmit}>Отправить</button>
        </div>
    );
}

export default ActionButtons