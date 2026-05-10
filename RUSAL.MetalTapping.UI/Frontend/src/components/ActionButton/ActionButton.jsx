import './ActionButton.css'

function ActionButtons({ onSave, onSubmit, onMap, selectedCorpus }) {
    return (
        <div className="action-bar">
            {onMap && selectedCorpus && selectedCorpus !== '0' && (
                <button className="act-btn act-btn-map" onClick={onMap}>
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                        <rect x="3" y="3" width="7" height="7"/><rect x="14" y="3" width="7" height="7"/>
                        <rect x="3" y="14" width="7" height="7"/><rect x="14" y="14" width="7" height="7"/>
                    </svg>
                    <span>Карта корпуса</span>
                </button>
            )}
            <button className="act-btn act-btn-save" onClick={onSave}>
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                    <path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"/>
                    <polyline points="17 21 17 13 7 13 7 21"/>
                    <polyline points="7 3 7 8 15 8"/>
                </svg>
                <span>Сохранить PDF</span>
            </button>
            <button className="act-btn act-btn-submit" onClick={onSubmit}>
                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                    <path d="M4 12v8a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2v-8"/>
                    <polyline points="16 6 12 2 8 6"/>
                    <line x1="12" y1="2" x2="12" y2="15"/>
                </svg>
                <span>Отправить по Email</span>
            </button>
        </div>
    )
}
export default ActionButtons