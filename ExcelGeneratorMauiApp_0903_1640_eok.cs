// 代码生成时间: 2025-09-03 16:40:56
 * Maintains code maintainability and extensibility.
 */

using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.Office.Interop.Excel;

namespace ExcelGeneratorMauiApp
{
    public class ExcelGeneratorApp : Application
    {
        public ExcelGeneratorApp()
        {
            MainPage = new AppShell();
        }
    }

    public class AppShell : Shell
    {
        public AppShell()
        {
            AddContentPage("Home", "HomePage");
        }
    }

    public class HomePage : ContentPage
    {
        private Button generateButton;
        private Button saveButton;
        private string excelFilePath;
        private Application excelApp;
        private Workbook workbook;
        private Worksheet worksheet;

        public HomePage()
        {
            // Initialize UI components
            generateButton = new Button { Text = "Generate Excel" };
            saveButton = new Button { Text = "Save Excel" };
            excelFilePath = "GeneratedExcel.xlsx";

            generateButton.Clicked += GenerateExcel;
            saveButton.Clicked += SaveExcel;

            // Layout and styling
            StackLayout mainLayout = new StackLayout
            {
                Children =
                {
                    generateButton,
                    saveButton
                },
                Padding = new Thickness(20),
                Spacing = 20
            };

            Content = mainLayout;
        }

        private async void GenerateExcel(object sender, EventArgs e)
        {
            try
            {
                // Initialize Excel application
                excelApp = new Application();
                excelApp.Visible = true;

                // Create a new workbook
                workbook = excelApp.Workbooks.Add(Type.Missing);
                worksheet = workbook.ActiveSheet;
                // Add headers and data
                worksheet.Cells[1, 1] = "Header1";
                worksheet.Cells[1, 2] = "Header2";
                worksheet.Cells[2, 1] = "Data1";
                worksheet.Cells[2, 2] = "Data2";

                // Code to add more data or formatting can be added here
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void SaveExcel(object sender, EventArgs e)
        {
            try
            {
                // Save the workbook to a file
                workbook.SaveAs(excelFilePath);
                workbook.Close(false);
                excelApp.Quit();

                await DisplayAlert("Success", "Excel file saved successfully!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                // Cleanup
                workbook = null;
                excelApp = null;
            }
        }
    }
}

// Usage: To run this application, build and deploy the MAUI project to a device or emulator.
// Click the 'Generate Excel' button to create a new Excel file with sample data.
// Click the 'Save Excel' button to save the generated Excel file to the device.
