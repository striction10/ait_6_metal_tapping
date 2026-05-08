import pdfMake from 'pdfmake/build/pdfmake'
import pdfFonts from 'pdfmake/build/vfs_fonts'
import { getUserData } from './auth'

pdfMake.vfs = pdfFonts.vfs

export const exportTableToPDF = (data, corpusId, corpusName, reglamentName, selectedDate) => {
    const { role } = getUserData()
    const isTechnologist = role === 'Technologist'
    
    const formattedDate = selectedDate ? new Date(selectedDate).toLocaleDateString() : new Date().toLocaleDateString()
    
    const headers = [
        '№ Электролиза',
        'Уровень металла, цель',
        'Уровень металла, факт',
        'Отклонение, см',
        'Сила тока, кА',
        'Выход по току, %',
        'Расчетное задание, кг',
        'ЗПР, кг',
        'Марка'
    ]

    const body = [
        headers,
        ...data.map(row => {
            const deviation = parseFloat(row.deviationValue)
            
            const minDeviation = row.minDeviation ?? -5
            const maxDeviation = row.maxDeviation ?? 5
            
            const isDeviationOutOfRange = !isNaN(deviation) && (deviation < minDeviation || deviation > maxDeviation)
            
            const getCellColor = () => {
                if (isTechnologist) {
                    if (isDeviationOutOfRange) {
                        return '#b6d7fd'
                    }
                } else {
                    if (isDeviationOutOfRange) {
                        return '#ffc7c7'
                    }
                }
                return null
            }
            
            const cellColor = getCellColor()
            
            return [
                { text: row.potName || '-' },
                { text: (row.targetMetalLevel?.toFixed(1) ?? '-').toString() },
                { text: (row.actualMetalLevel?.toFixed(1) ?? '-').toString() },
                { text: (row.deviationValue?.toFixed(1) ?? '-').toString(), fillColor: cellColor },
                { text: (row.amperage?.toFixed(0) ?? '-').toString() },
                { text: (row.avgAmperage?.toFixed(1) ?? '-').toString() },
                { text: (row.calculatedTask?.toFixed(0) ?? '-').toString(), fillColor: cellColor },
                { text: (row.roundCalculatedTask?.toFixed(0) ?? '-').toString(), fillColor: cellColor },
                { text: row.metalMarkName || '-' }
            ]
        })
    ]

    const docDefinition = {
        content: [
            { text: 'Таблица параметров', style: 'header' },
            { text: `Корпус: ${corpusName}`, margin: [0, 5, 0, 0] },
            { text: `Регламент: ${reglamentName}`, margin: [0, 5, 0, 10] },
            { text: `Дата: ${formattedDate}`, margin: [0, 0, 0, 20] },
            {
                table: {
                    headerRows: 1,
                    widths: ['auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto'],
                    body: body
                },
                layout: {
                    fillColor: function(rowIndex) {
                        if (rowIndex === 0) return '#fd8288'
                        return (rowIndex % 2 === 0) ? '#f2f2f2' : null
                    }
                }
            }
        ],
        styles: {
            header: {
                fontSize: 18,
                bold: true,
                margin: [0, 0, 0, 10]
            }
        },
        defaultStyle: {
            font: 'Roboto'
        }
    }

    const fileName = `Параметры_${corpusName}_${reglamentName}_${formattedDate.replace(/\//g, '-')}.pdf`
    pdfMake.createPdf(docDefinition).download(fileName)
}