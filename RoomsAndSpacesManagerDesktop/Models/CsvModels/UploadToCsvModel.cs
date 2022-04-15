using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsAndSpacesManagerDesktop.Models.CsvModels
{
    internal class UploadToCsvModel
    {
        RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();
        private ProjectDto Project { get; set; }
        private string path;
        private ExcelPackage excel { get; set; }
        private List<BuildingDto> BuildList { get; set; }
        private double Koef { get; set; }
        public UploadToCsvModel(ProjectDto project, List<BuildingDto> buildList, double koef)
        {
            this.Project = project;
            this.BuildList = buildList;
            this.Koef = koef;
        }
        public UploadToCsvModel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            excel = new ExcelPackage();
        }

        public void UploadToExcel()
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();
            path = openFileDialog.SelectedPath;

            if (File.Exists(path + @"\Программа.xlsx"))
                File.Delete(path + @"\Программа.xlsx");


            excel.Workbook.Worksheets.Add("Программа помещений");
            excel.Workbook.Worksheets.Add("Сводная");

            UploadRoomProgramToExcel(Project);
            UploadRoomSummaryToExcel(BuildList);

            FileInfo excelFile = new FileInfo(path + @"\Программа.xlsx");
            excel.SaveAs(excelFile);
        }


        public void UploadRoomProgramToExcel(ProjectDto project)
        {

            var worksheet = excel.Workbook.Worksheets["Программа помещений"];
            int rowCount = 1;
            int colCount = 1;

            worksheet.Cells[rowCount, colCount].Value = "№/№";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Площадь, м^2";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Примечание";
            colCount = 1;
            rowCount++;

            int i = 1;
            foreach (BuildingDto build in context.RaSM_Projects.FirstOrDefault(x => x.Id == project.Id).Buildings)
            {
                worksheet.Cells[rowCount, colCount].Value = i;
                colCount++;
                worksheet.Cells[rowCount, colCount].Value = build.Name;
                colCount = 1;
                rowCount++;
                int ii = 1;
                foreach (SubdivisionDto subdivision in build.Subdivisions.OrderBy(x => x.Order))
                {
                    string iis = i.ToString() + "." + ii.ToString();

                    worksheet.Cells[rowCount, colCount].Value = iis;
                    colCount++;
                    worksheet.Cells[rowCount, colCount].Value = subdivision.Name;
                    colCount = 1;
                    rowCount++;

                    int iii = 1;
                    foreach (RoomDto room in subdivision.Rooms)
                    {
                        string iiis = i.ToString() + "." + ii.ToString() + "." + iii.ToString();

                        worksheet.Cells[rowCount, colCount].Value = iiis;
                        colCount++;
                        worksheet.Cells[rowCount, colCount].Value = room.ShortName;
                        colCount++;
                        worksheet.Cells[rowCount, colCount].Value = room.Min_area;
                        colCount++;
                        worksheet.Cells[rowCount, colCount].Value = room.Notation;
                        colCount = 1;
                        rowCount++;
                        iii++;
                    }
                    ii++;
                }
                i++;
            }
        }


        public void UploadRoomSummaryToExcel(List<BuildingDto> buildList)
        {
            var worksheet = excel.Workbook.Worksheets["Сводная"];
            int rowCount = 1;
            int colCount = 1;

            worksheet.Cells[rowCount, colCount].Value = "№/№";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Подразделение";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Площадь расчётная, м^2";
            colCount++;
            worksheet.Cells[rowCount, colCount].Value = "Ориент. общая площадь, м^2";
            colCount = 1;
            rowCount++;




            int n1 = 1;

            double sumarea = 0;
            double Ksumarea = 0;
            foreach (BuildingDto build in buildList)
            {
                worksheet.Cells[rowCount, 1].Value = n1.ToString();
                worksheet.Cells[rowCount, 2].Value = build.Name;
                worksheet.Cells[rowCount, 3].Value = build.SunnuryArea;
                sumarea += Convert.ToDouble(build.SunnuryArea);
                Ksumarea += Convert.ToDouble(build.SunnuryArea) * Koef;
                worksheet.Cells[rowCount, 4].Value = build.SunnuryArea * Koef;
                rowCount++;



                int n2 = 1;
                foreach (SubdivisionDto subdiv in build.Subdivisions.OrderBy(x => x.Order))
                {
                    worksheet.Cells[rowCount, 1].Value = n1.ToString() + "." + n2.ToString();
                    worksheet.Cells[rowCount, 2].Value = subdiv.Name;
                    worksheet.Cells[rowCount, 3].Value = subdiv.SunnuryArea;
                    worksheet.Cells[rowCount, 4].Value = subdiv.SunnuryArea * Koef;
                    n2++;
                    rowCount++;
                }

                n1++;
            }

            worksheet.Cells[rowCount, 3].Value = sumarea;
            worksheet.Cells[rowCount, 4].Value = Ksumarea;
        }
    }
}