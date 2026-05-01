import pdfMake from 'pdfmake/build/pdfmake'
import pdfFonts from 'pdfmake/build/vfs_fonts'
import { getUserData } from './auth'

pdfMake.vfs = pdfFonts.vfs

export const exportTableToPDF = (data, corpusId) => {
    const { role } = getUserData()
    const isTechnologist = role === 'Technologist'
    
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
            const isDeviationOutOfRange = !isNaN(deviation) && deviation <= -5
            
            let deviationStyle = 'normalCell'
            let calculatedStyle = 'normalCell'
            let zprStyle = 'normalCell'
            
            if (isTechnologist) {
                if (isDeviationOutOfRange) {
                    deviationStyle = 'technologistBlue'
                    calculatedStyle = 'technologistBlue'
                    zprStyle = 'technologistBlue'
                }
            } else {
                if (isDeviationOutOfRange) {
                    deviationStyle = 'redCell'
                    calculatedStyle = 'redCell'
                    zprStyle = 'redCell'
                }
            }
            
            return [
                { text: row.potName || '-' },
                { text: (row.targetMetalLevel?.toFixed(1) ?? '-').toString() },
                { text: (row.actualMetalLevel?.toFixed(1) ?? '-').toString() },
                { text: (row.deviationValue?.toFixed(1) ?? '-').toString(), style: deviationStyle },
                { text: (row.amperage?.toFixed(0) ?? '-').toString() },
                { text: (row.avgAmperage?.toFixed(1) ?? '-').toString() },
                { text: (row.calculatedTask?.toFixed(0) ?? '-').toString(), style: calculatedStyle },
                { text: (row.roundCalculatedTask?.toFixed(0) ?? '-').toString(), style: zprStyle },
                { text: row.metalMarkName || '-' }
            ]
        })
    ]

    const docDefinition = {
        content: [
            { text: 'Таблица параметров', style: 'header' },
            { text: `Дата: ${new Date().toLocaleDateString()}`, margin: [0, 0, 0, 20] },
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
                margin: [0, 0, 0, 10],
                fillColor: '#fd8288',
                color: 'white'
            },
            technologistBlue: {
                fillColor: '#b6d7fd',
                color: 'black'
            },
            redCell: {
                fillColor: '#ffc7c7',
                color: 'black'
            },
            normalCell: {
                color: 'black'
            }
        },
        defaultStyle: {
            font: 'Roboto'
        }
    }

    pdfMake.createPdf(docDefinition).download(`parameters_${corpusId}_${new Date().toLocaleDateString()}.pdf`)
}