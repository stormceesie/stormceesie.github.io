Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Reflection
Imports Excel = Microsoft.Office.Interop.Excel
Imports SolidEdgeAssembly
Imports SolidEdgeFramework

Module Program
    Sub Main()
        ' Nieuwe instantie van Solid Edge starten.
        Dim application As SolidEdgeFramework.Application = New SolidEdgeFramework.Application()

        ' Solid Edge-document openen.
        Dim assemblyDocument As SolidEdgeAssembly.AssemblyDocument = application.ActiveDocument

        ' Lijst van de eigenschappen van de assembly.
        Dim properties As SolidEdgeFramework.Properties = assemblyDocument.Properties

        ' Excel-document maken.
        Dim excel As Excel.Application = New Excel.Application()
        Dim workbook As Excel.Workbook = excel.Workbooks.Add()
        Dim worksheet As Excel.Worksheet = workbook.Sheets(1)

        ' Koppen van de kolommen in Excel-document instellen.
        worksheet.Cells(1, 1) = "Property Name"
        worksheet.Cells(1, 2) = "Property Value"

        ' Eigenschappen van de assembly in Excel-document schrijven.
        Dim row As Integer = 2
        For Each [property] As SolidEdgeFramework.Property In properties
            worksheet.Cells(row, 1) = [property].Name
            worksheet.Cells(row, 2) = [property].Value
            row += 1
        Next

        ' Excel-document opslaan.
        Dim fileName As String = Path.Combine(Path.GetTempPath(), "SolidEdgeAssemblyProperties.xlsx")
        workbook.SaveAs(fileName)

        ' Excel-document afsluiten.
        workbook.Close()
        excel.Quit()

        ' Solid Edge afsluiten.
        application.Quit()

        Console.WriteLine($"Assembly properties are exported to Excel file: {fileName}")
    End Sub
End Module