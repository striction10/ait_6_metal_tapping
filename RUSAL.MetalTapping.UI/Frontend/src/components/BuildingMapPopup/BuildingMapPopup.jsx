import { useState, useEffect } from 'react';
import { buildingApi } from '../../services/buildingApi';
import './BuildingMapPopup.css';

const extractPotNumber = (name) => {
    const match = name?.match(/\d+/);
    return match ? parseInt(match[0], 10) : 999;
};

function BuildingMapPopup({ isOpen, onClose, buildingId }) {
    const [mapData, setMapData] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState('');
    const [hoveredPot, setHoveredPot] = useState(null);
    const [tooltipPos, setTooltipPos] = useState({ x: 0, y: 0 });

    useEffect(() => {
        if (isOpen && buildingId) {
            fetchMap();
        }
    }, [isOpen, buildingId]);

    const fetchMap = async () => {
        setIsLoading(true);
        setError('');
        try {
            const data = await buildingApi.getBuildingMap(buildingId);
            setMapData(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setIsLoading(false);
        }
    };

    const processGroupPots = (pots) => {
        if (!pots) return { topRow: [], bottomRow: [] };
        const sorted = [...pots].sort((a, b) => extractPotNumber(a.name) - extractPotNumber(b.name));
        return {
            topRow: sorted.slice(0, 5),
            bottomRow: sorted.slice(5, 10)
        };
    };

    const handlePotHover = (pot, e) => {
        setHoveredPot(pot);
        setTooltipPos({ x: e.clientX, y: e.clientY });
    };

    if (!isOpen) return null;

    return (
        <div className="bmp-overlay" onClick={(e) => e.target === e.currentTarget && onClose()} onKeyDown={(e) => e.key === 'Escape' && onClose()} role="dialog" aria-modal="true">
            <div className="bmp-card">
                <button className="bmp-close" onClick={onClose} aria-label="Закрыть">
                    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                        <line x1="18" y1="6" x2="6" y2="18"/><line x1="6" y1="6" x2="18" y2="18"/>
                    </svg>
                </button>

                <div className="bmp-header">
                    <div className="bmp-icon">
                        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                            <rect x="3" y="3" width="7" height="7"/><rect x="14" y="3" width="7" height="7"/>
                            <rect x="3" y="14" width="7" height="7"/><rect x="14" y="14" width="7" height="7"/>
                        </svg>
                    </div>
                    <h3>Карта корпуса {mapData?.name || ''}</h3>
                    <p>Визуальная схема расположения электролизёров и ковшей</p>
                </div>

                <div className="bmp-body">
                    {error && <div className="bmp-error">{error}</div>}
                    
                    {isLoading ? (
                        <div className="bmp-loading">
                            <div className="bmp-spinner"></div>
                            <span>Загрузка карты корпуса...</span>
                        </div>
                    ) : mapData?.groups ? (
                        <>
                            <div className="bmp-legend">
                                <div className="bmp-legend-item">
                                    <span className="bmp-legend-dot bmp-dot-active"></span>
                                    <span>Активен</span>
                                </div>
                                <div className="bmp-legend-item">
                                    <span className="bmp-legend-dot bmp-dot-warning"></span>
                                    <span>Предупреждение</span>
                                </div>
                                <div className="bmp-legend-item">
                                    <span className="bmp-legend-dot bmp-dot-inactive"></span>
                                    <span>Отключён</span>
                                </div>
                                <div className="bmp-legend-item">
                                    <span className="bmp-legend-dot bmp-dot-busy"></span>
                                    <span>Занят</span>
                                </div>
                            </div>

                            <div className="bmp-map">
                                {mapData.groups.map((group, gIdx) => {
                                    const { topRow, bottomRow } = processGroupPots(group.pots);
                                    const scoop = group.scoop;
                                    const isScoopBusy = scoop?.isBusy;
                                    const scoopState = scoop?.state || 'Неизвестно';

                                    return (
                                        <div key={group.id} className="bmp-group">
                                            <div className="bmp-group-label">Группа {gIdx + 1}</div>

                                            <div className={`bmp-scoop ${isScoopBusy ? 'bmp-scoop-busy' : 'bmp-scoop-free'}`}>
                                                <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                                    <path d="M18 8A3 3 0 0 0 6 8v7a6 6 0 0 0 12 0V8z"/>
                                                    <path d="M3 8h18"/>
                                                    <path d="M12 2v4"/>
                                                </svg>
                                                <span className="bmp-scoop-name">Ковш {gIdx + 1}</span>
                                                <span className={`bmp-scoop-badge ${isScoopBusy ? 'badge-busy' : 'badge-free'}`}>
                                                    {isScoopBusy ? 'Занят' : 'Свободен'}
                                                </span>
                                                <span className="bmp-scoop-state">{scoopState}</span>
                                            </div>

                                            <div className="bmp-pots-container">
                                                <div className="bmp-pots-row">
                                                    {topRow.map((pot) => (
                                                        <div 
                                                            key={pot.id}
                                                            className={`bmp-pot bmp-pot-${pot.state?.toLowerCase() || 'unknown'}`}
                                                            onMouseEnter={(e) => handlePotHover(pot, e)}
                                                            onMouseLeave={() => setHoveredPot(null)}
                                                        >
                                                            <span className="bmp-pot-id">{extractPotNumber(pot.name)}</span>
                                                            <span className="bmp-pot-level">{pot.metalLevel?.toFixed(1) ?? '-'} кг</span>
                                                        </div>
                                                    ))}
                                                </div>

                                                <div className="bmp-aisle">
                                                    <span className="bmp-aisle-label"></span>
                                                </div>

                                                <div className="bmp-pots-row">
                                                    {bottomRow.map((pot) => (
                                                        <div 
                                                            key={pot.id}
                                                            className={`bmp-pot bmp-pot-${pot.state?.toLowerCase() || 'unknown'}`}
                                                            onMouseEnter={(e) => handlePotHover(pot, e)}
                                                            onMouseLeave={() => setHoveredPot(null)}
                                                        >
                                                            <span className="bmp-pot-id">{extractPotNumber(pot.name)}</span>
                                                            <span className="bmp-pot-level">{pot.metalLevel?.toFixed(1) ?? '-'} кг</span>
                                                        </div>
                                                    ))}
                                                </div>
                                            </div>

                                            {gIdx < mapData.groups.length - 1 && <div className="bmp-group-separator" />}
                                        </div>
                                    );
                                })}
                            </div>

                            {hoveredPot && (
                                <div 
                                    className="bmp-tooltip" 
                                    style={{ left: tooltipPos.x, top: tooltipPos.y - 80, transform: 'translateX(-50%)' }}
                                >
                                    <strong>Электролизёр {extractPotNumber(hoveredPot.name)}</strong>
                                    <p>Статус: {hoveredPot.state || 'Неизвестно'}</p>
                                    <p>Уровень металла: {hoveredPot.metalLevel?.toFixed(2) ?? '-'} кг</p>
                                </div>
                            )}
                        </>
                    ) : (
                        <div className="bmp-empty">Данные карты отсутствуют</div>
                    )}
                </div>

                <div className="bmp-footer">
                    <button className="bmp-btn-close" onClick={onClose}>Закрыть</button>
                </div>
            </div>
        </div>
    );
}

export default BuildingMapPopup;