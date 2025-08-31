// 代码生成时间: 2025-08-31 23:42:13
using System;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MauiExcelGenerator
{
    /// <summary>
    /// A service that generates Excel files programmatically.
    /// </summary>
    public class ExcelGeneratorService
    {
        /// <summary>
        /// Generates an Excel file based on the provided data.
        /// </summary>
        /// <param name="filePath">The path where the Excel file will be saved.</param>
        /// <param name="sheetName">The name of the sheet in the Excel file.</param>
        /// <param name="data">The data to be written to the Excel file.</param>
        public void GenerateExcelFile(string filePath, string sheetName, object[,] data)
        {
            try
            {
                // Check if the file path is valid
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    throw new ArgumentException("File path cannot be empty.");
                }

                // Check if the data is valid
                if (data == null || data.GetLength(0) == 0)
                {
                    throw new ArgumentException("Data cannot be null or empty.");
                }

                // Create the Excel package
                using (SpreadsheetDocument package = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {
                    // Add a new workbook part
                    WorkbookPart workbookPart = package.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    // Add a new worksheet part
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                    // Add the sheet to the workbook
                    Sheets sheets = workbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                    sheets.AppendChild(new Sheet()
                    {
                        Id = workbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = sheetName
                    });

                    // Write data to the sheet
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    int rowCount = data.GetLength(0);
                    int colCount = data.GetLength(1);
                    for (int row = 0; row < rowCount; row++)
                    {
                        for (int col = 0; col < colCount; col++)
                        {
                            // Create a new row
                            if (row == 0)
                            {
                                Row newCell = new Row();
                                sheetData.AppendChild(newCell);
                            }

                            // Create a new cell
                            Cell newCell = new Cell() {
                                CellReference = $"{(char)('A' + col)}{row + 1}"
                            };
                            newCell.CellValue = new CellValue(data[row, col].ToString());
                            newCell.DataType = CellValues.String;

                            // Append the cell to the row
                            if (row == 0)
                            {
                                sheetData.GetFirstChild<Row>().AppendChild(newCell);
                            }
                            else
                            {
                                Row newRow = new Row();
                                newRow.AppendChild(newCell);
                                sheetData.AppendChild(newRow);
                            }
                        }
                    }

                    // Save changes
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}
