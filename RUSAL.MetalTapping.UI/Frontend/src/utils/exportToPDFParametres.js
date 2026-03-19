import pdfMake from 'pdfmake/build/pdfmake'
import pdfFonts from 'pdfmake/build/vfs_fonts'

pdfMake.vfs = pdfFonts.vfs

export const exportTableToPDF = (data, corpusId) => {
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
        ...data.map(row => [
            row.potName || '-',
            (row.targetMetalLevel?.toFixed(1) ?? '-').toString(),
            (row.actualMetalLevel?.toFixed(1) ?? '-').toString(),
            (row.deviationValue?.toFixed(1) ?? '-').toString(),
            (row.current?.toFixed(0) ?? '-').toString(),
            (row.efficiency?.toFixed(1) ?? '-').toString(),
            (row.calculatedTask?.toFixed(0) ?? '-').toString(),
            (row.roundCalculatedTask?.toFixed(0) ?? '-').toString(),
            row.metalMarkName || '-'
        ])
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
                margin: [0, 0, 0, 10]
            }
        },
        defaultStyle: {
            font: 'Roboto'
        }
    }

    pdfMake.createPdf(docDefinition).download(`parameters_${corpusId}_${new Date().toLocaleDateString()}.pdf`)
}