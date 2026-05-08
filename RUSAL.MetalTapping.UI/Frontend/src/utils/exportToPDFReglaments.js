import pdfMake from 'pdfmake/build/pdfmake'
import pdfFonts from 'pdfmake/build/vfs_fonts'

pdfMake.vfs = pdfFonts.vfs

export const exportReglamentsToPDF = (data, corpusName, reglamentName, selectedDate) => {
    if (!data || data.length === 0) return

    const firstItem = data[0]
    const ratioKeys = Object.keys(firstItem.castingRatio || {})
        .sort((a, b) => Number(a) - Number(b))

    const headers = [
        '№ Электролиза',
        ...ratioKeys.map(key => `${key}`)
    ]

    const body = [
        headers,
        ...data.map(row => [
            row.name || row.id || '-',
            ...ratioKeys.map(key => (row.castingRatio?.[key] ?? '-').toString())
        ])
    ]

    const formattedDate = selectedDate ? new Date(selectedDate).toLocaleDateString() : new Date().toLocaleDateString()

    const docDefinition = {
        content: [
            { text: 'Таблица регламентов', style: 'header' },
            { text: `Корпус: ${corpusName}`, margin: [0, 5, 0, 0] },
            { text: `Регламент: ${reglamentName}`, margin: [0, 5, 0, 10] },
            { text: `Дата: ${formattedDate}`, margin: [0, 0, 0, 20] },
            {
                table: {
                    headerRows: 1,
                    widths: Array(headers.length).fill('auto'),
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

    const fileName = `Регламенты_${corpusName}_${reglamentName}_${formattedDate.replace(/\//g, '-')}.pdf`
    pdfMake.createPdf(docDefinition).download(fileName)
}