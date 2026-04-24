import pdfMake from 'pdfmake/build/pdfmake'
import pdfFonts from 'pdfmake/build/vfs_fonts'

pdfMake.vfs = pdfFonts.vfs

export const exportTasksToPDF = (shiftData, totalData, corpusName, date) => {
    if (!shiftData || shiftData.length === 0) return

    const shiftHeaders = [
        'Смена 1, кг | Время',
        'Смена 2, кг | Время',
        'Смена 3, кг | Время',
        '№ Электролиза | № Ковша',
        'Fe, %',
        'Si, %',
        'Cu, %',
        'Mn, %',
        'Марка'
    ]

    const shiftBody = [
        shiftHeaders,
        ...shiftData.map(row => [
            `${row.shift1Kg || '-'} | ${row.shift1Time || '-'}`,
            `${row.shift2Kg || '-'} | ${row.shift2Time || '-'}`,
            `${row.shift3Kg || '-'} | ${row.shift3Time || '-'}`,
            `${row.electrolyzerNum || '-'} | ${row.bucketNum || '-'}`,
            row.fe || '-',
            row.si || '-',
            row.cu || '-',
            row.mn || '-',
            row.mark || '-'
        ])
    ]

    const totalHeaders = [
        'Задание на выливку, кг',
        'Марка'
    ]

    const totalBody = [
        totalHeaders,
        [totalData[0]?.task || '0', totalData[0]?.mark || '-']
    ]

    const docDefinition = {
        content: [
            { text: 'Таблица заданий', style: 'header' },
            { text: `Корпус: ${corpusName}`, margin: [0, 5, 0, 0] },
            { text: `Дата: ${new Date(date).toLocaleDateString()}`, margin: [0, 5, 0, 20] },
            { text: 'Задание по сменам', style: 'subheader' },
            {
                table: {
                    headerRows: 1,
                    widths: ['auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto'],
                    body: shiftBody
                },
                layout: {
                    fillColor: function(rowIndex) {
                        if (rowIndex === 0) return '#fd8288'
                        return (rowIndex % 2 === 0) ? '#f2f2f2' : null
                    }
                }
            },
            { text: 'Итоговое задание на смену', style: 'subheader', margin: [0, 20, 0, 10] },
            {
                table: {
                    headerRows: 1,
                    widths: ['auto', 'auto'],
                    body: totalBody
                },
                layout: {
                    fillColor: function(rowIndex) {
                        if (rowIndex === 0) return '#fd8288'
                        return null
                    }
                }
            }
        ],
        styles: {
            header: {
                fontSize: 18,
                bold: true,
                margin: [0, 0, 0, 10]
            },
            subheader: {
                fontSize: 14,
                bold: true,
                margin: [0, 0, 0, 10]
            }
        },
        defaultStyle: {
            font: 'Roboto'
        }
    }

    const fileName = `tasks_${corpusName}_${new Date(date).toLocaleDateString()}.pdf`
    pdfMake.createPdf(docDefinition).download(fileName)
}