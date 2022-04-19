using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.SqlModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Toolkit = Xceed.Wpf.Toolkit;

namespace RoomsAndSpacesManagerDesktop.Models.ExcelModels
{
    class MainExcelModel
    {
        public void AddToDbFromExcelEqupment(RoomNameDto roomName)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            System.Windows.MessageBox.Show(openFileDialog.FileName);

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage(new FileInfo(openFileDialog.FileName));
           
            List<RoomEquipmentDto> equipnets = new List<RoomEquipmentDto>();

            string idPom = "start";

            var workbook = excel.Workbook;
            var worksheet = workbook.Worksheets.First();

            int rowCount = 2;
            int colCount = 1;

            while (idPom != "")
            {
                if (worksheet.Cells[rowCount, colCount].Value == null)
                    break;

                RoomEquipmentDto roomEquipmentDto = new RoomEquipmentDto();

                roomEquipmentDto.RoomNameId = roomName.Id;

                if (worksheet.Cells[rowCount, 2].Value != null)
                    roomEquipmentDto. Number = Convert.ToInt32(worksheet.Cells[rowCount, 2].Value);

                if (worksheet.Cells[rowCount, 3].Value != null)
                    roomEquipmentDto.ClassificationCode = worksheet.Cells[rowCount, 3].Value.ToString();



                if (worksheet.Cells[rowCount, 4].Value != null)
                    roomEquipmentDto.TypeName = worksheet.Cells[rowCount, 4].Value.ToString();

                if (worksheet.Cells[rowCount, 5].Value != null)
                    roomEquipmentDto.Name = worksheet.Cells[rowCount, 5].Value.ToString();

                if (worksheet.Cells[rowCount, 6].Value != null)
                    roomEquipmentDto.Count = Convert.ToInt32(worksheet.Cells[rowCount, 6].Value);



                string ddd = worksheet.Cells[rowCount, 7].Value?.ToString();


                if (worksheet.Cells[rowCount, 7].Value != null)
                {
                    if (ddd == "True")
                        roomEquipmentDto.Mandatory = true;
                }

                equipnets.Add(roomEquipmentDto);

                rowCount++;
            }

            EquipmentDbContext context = new EquipmentDbContext();
            context.AddNewEquipments(equipnets);
        }

        public static bool UploadProgramToExcel (List<RoomDto> rooms)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();

            string path;
            path = openFileDialog.SelectedPath + "\\" + rooms.First().Subdivision.Name + ".xlsx";
            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path).Close();
                }
                catch (Exception ex)
                {
                    Toolkit.MessageBox.Show("Возможно, нет доступа к папке\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }

            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);

                    #region All
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("Сформированное задание");
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets["Сформированное задание"];

                    int rowCount = 1;
                    int colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Subdivision);
                    worksheet.Cells[rowCount, colCount].Value = "Подразделение";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;


                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Min_area);
                    worksheet.Cells[rowCount, colCount].Value = "Мин. площадь";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_personala);
                    worksheet.Cells[rowCount, colCount].Value = "Количество рабочих мест";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_posetitelei);
                    worksheet.Cells[rowCount, colCount].Value = "Количество посетителей";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
                    worksheet.Cells[rowCount, colCount].Value = "Категория пожароопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_AR);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание АР";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_calc);
                    worksheet.Cells[rowCount, colCount].Value = "Температура расчётная, °C";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_min);
                    worksheet.Cells[rowCount, colCount].Value = "Температура мин, °C";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_max);
                    worksheet.Cells[rowCount, colCount].Value = "Температура макс, °C";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Pritok);
                    worksheet.Cells[rowCount, colCount].Value = "Приток, крат";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Vityazhka);
                    worksheet.Cells[rowCount, colCount].Value = "Вытяжка, крат";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Ot_vlazhnost);
                    worksheet.Cells[rowCount, colCount].Value = "Относительная влажность, %";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_OV);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание ОВ";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Equipment_VK);
                    worksheet.Cells[rowCount, colCount].Value = "Оборудование ВК";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Osveshennost_pro_obshem_osvech);
                    worksheet.Cells[rowCount, colCount].Value = "Освещенность при общем освещении, лк";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.El_Nagruzka);
                    worksheet.Cells[rowCount, colCount].Value = "Электрическая нагрузка, кВт";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Group_el_bez);
                    worksheet.Cells[rowCount, colCount].Value = "Группа по электробезопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_EOM);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание ЭОМ";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_SS);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание СС";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_AK_ATH);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание АК, АТХ";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Nagruzki_na_perekririe);
                    worksheet.Cells[rowCount, colCount].Value = "Нагрузка на перекрытие";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_GSV);
                    worksheet.Cells[rowCount, colCount].Value = "Технологические газы";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_HS);
                    worksheet.Cells[rowCount, colCount].Value = "Холодоснабжение";
                    colCount++;

                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 15;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 8;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 20;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(6).Width = 12;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 8;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(10).Width = 8;
                    worksheet.Column(10).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(11).Width = 8;
                    worksheet.Column(11).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(12).Width = 8;
                    worksheet.Column(12).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(13).Width = 20;
                    worksheet.Column(13).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(14).Width = 8;
                    worksheet.Column(14).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(15).Width = 8;
                    worksheet.Column(15).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(16).Width = 8;
                    worksheet.Column(16).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(17).Width = 15;
                    worksheet.Column(17).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(18).Width = 15;
                    worksheet.Column(18).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(19).Width = 15;
                    worksheet.Column(19).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(20).Width = 20;
                    worksheet.Column(20).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(21).Width = 20;
                    worksheet.Column(21).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(22).Width = 15;
                    worksheet.Column(22).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(23).Width = 8;
                    worksheet.Column(23).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(24).Width = 15;
                    worksheet.Column(24).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(25).Width = 15;
                    worksheet.Column(25).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(26).Width = 20;
                    worksheet.Column(26).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(27).Width = 20;
                    worksheet.Column(27).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(28).Width = 20;
                    worksheet.Column(28).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(29).Width = 20;
                    worksheet.Column(29).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(30).Width = 20;
                    worksheet.Column(30).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:AD1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:AD1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    System.Drawing.Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:AD1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 6); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:AD{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);

                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Subdivision?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Min_area?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_personala?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_posetitelei?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_AR?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.T_calc?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.T_min?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.T_max?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Pritok?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Vityazhka?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Ot_vlazhnost?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_OV?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Equipment_VK?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Osveshennost_pro_obshem_osvech?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.El_Nagruzka?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Group_el_bez?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_EOM?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_SS?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_AK_ATH?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Nagruzki_na_perekririe?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_GSV?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_HS?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

                    worksheet.Cells[$"A{1}:AD{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region AR

                    // Формирование листа АР
                    excel.Workbook.Worksheets.Add("АР");
                    worksheet = excel.Workbook.Worksheets["АР"];

                    rowCount = 1;
                    colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
                    worksheet.Cells[rowCount, colCount].Value = "Категория пожароопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_AR);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание АР";
                    colCount++;

                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 8;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Width = 8;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:I1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:I1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 5); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);

                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_AR?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

                    worksheet.Cells[$"A{1}:I{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region VK
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("ВК");
                    worksheet = excel.Workbook.Worksheets["ВК"];

                    rowCount = 1;
                    colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_personala);
                    worksheet.Cells[rowCount, colCount].Value = "Количество рабочих мест";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_posetitelei);
                    worksheet.Cells[rowCount, colCount].Value = "Количество посетителей";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Equipment_VK);
                    worksheet.Cells[rowCount, colCount].Value = "Оборудование ВК";
                    colCount++;

                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 8;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Width = 8;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 8;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(10).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:J1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:j1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 5); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);


                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_personala?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_posetitelei?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Equipment_VK?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

                    worksheet.Cells[$"A{1}:J{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region MGTG
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("МГТГ");
                   worksheet = excel.Workbook.Worksheets["МГТГ"];

                   rowCount = 1;
                   colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
                    worksheet.Cells[rowCount, colCount].Value = "Категория пожароопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_GSV);
                    worksheet.Cells[rowCount, colCount].Value = "Технологические газы";
                   colCount++;


                   colCount = 1;
                   rowCount++;

                   worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 8;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Width = 8;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:I1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:I1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 5); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);

                    foreach (var item in rooms)
                   {
                       worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                       colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                       colCount++;

                       worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                       colCount++;

                       worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                       colCount++;

                       worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                       colCount++;

                       worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                       colCount++;

                       worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                       colCount++;

                       worksheet.Cells[rowCount, colCount].Value = item.Discription_GSV?.ToString();
                       colCount++;

                       colCount = 1;

                       rowCount++;
                   }

                    worksheet.Cells[$"A{1}:I{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region KR
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("КР");
                    worksheet = excel.Workbook.Worksheets["КР"];

                    rowCount = 1;
                    colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Nagruzki_na_perekririe);
                    worksheet.Cells[rowCount, colCount].Value = "Нагрузка на перекрытие";
                    colCount++;


                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 20;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:E1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:E1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 5); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:E{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);


                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Nagruzki_na_perekririe?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

                    worksheet.Cells[$"A{1}:E{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region OV
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("ОВ");
                    worksheet = excel.Workbook.Worksheets["ОВ"];

                    rowCount = 1;
                    colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_personala);
                    worksheet.Cells[rowCount, colCount].Value = "Количество рабочих мест";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_posetitelei);
                    worksheet.Cells[rowCount, colCount].Value = "Количество посетителей";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
                    worksheet.Cells[rowCount, colCount].Value = "Категория пожароопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_calc);
                    worksheet.Cells[rowCount, colCount].Value = "Температура расчётная, °C";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_min);
                    worksheet.Cells[rowCount, colCount].Value = "Температура мин, °C";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.T_max);
                    worksheet.Cells[rowCount, colCount].Value = "Температура макс, °C";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Pritok);
                    worksheet.Cells[rowCount, colCount].Value = "Приток, крат";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Vityazhka);
                    worksheet.Cells[rowCount, colCount].Value = "Вытяжка, крат";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Ot_vlazhnost);
                    worksheet.Cells[rowCount, colCount].Value = "Относительная влажность, %";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_OV);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание ОВ";
                    colCount++;


                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 8;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Width = 8;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 8;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(10).Width = 8;
                    worksheet.Column(10).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(11).Width = 8;
                    worksheet.Column(11).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(12).Width = 8;
                    worksheet.Column(12).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(13).Width = 8;
                    worksheet.Column(13).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(14).Width = 15;
                    worksheet.Column(14).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(15).Width = 15;
                    worksheet.Column(15).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(16).Width = 15;
                    worksheet.Column(16).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(17).Width = 20;
                    worksheet.Column(17).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:Q1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:Q1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:Q1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 6); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:Q{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);

                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_personala?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_posetitelei?.ToString();
                        colCount++;


                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.T_calc?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.T_min?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.T_max?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Pritok?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Vityazhka?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Ot_vlazhnost?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_OV?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

                    worksheet.Cells[$"A{1}:Q{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region EOM
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("ЭОМ");
                    worksheet = excel.Workbook.Worksheets["ЭОМ"];

                    rowCount = 1;
                    colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_personala);
                    worksheet.Cells[rowCount, colCount].Value = "Количество рабочих мест";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_posetitelei);
                    worksheet.Cells[rowCount, colCount].Value = "Количество посетителей";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
                    worksheet.Cells[rowCount, colCount].Value = "Категория пожароопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Osveshennost_pro_obshem_osvech);
                    worksheet.Cells[rowCount, colCount].Value = "Освещенность при общем освещении, лк";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.El_Nagruzka);
                    worksheet.Cells[rowCount, colCount].Value = "Электрическая нагрузка, кВт";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Group_el_bez);
                    worksheet.Cells[rowCount, colCount].Value = "Группа по электробезопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_EOM);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание ЭОМ";
                    colCount++;

                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 8;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Width = 8;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 8;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(10).Width = 8;
                    worksheet.Column(10).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(11).Width = 15;
                    worksheet.Column(11).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(12).Width = 8;
                    worksheet.Column(12).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(13).Width = 15;
                    worksheet.Column(13).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(14).Width = 20;
                    worksheet.Column(14).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:N1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:N1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:N1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 6); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:N{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);

                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_personala?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Kolichestvo_posetitelei?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Osveshennost_pro_obshem_osvech?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.El_Nagruzka?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Group_el_bez?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_EOM?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

                    worksheet.Cells[$"A{1}:N{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region SS
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("СС");
                    worksheet = excel.Workbook.Worksheets["СС"];

                    rowCount = 1;
                    colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
                    worksheet.Cells[rowCount, colCount].Value = "Категория пожароопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_SS);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание СС";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_AK_ATH);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание АК, АТХ";
                    colCount++;

                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 8;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Width = 8;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(10).Width = 20;
                    worksheet.Column(10).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:J1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:J1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 6); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:J{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);


                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_SS?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_AK_ATH?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }
                    worksheet.Cells[$"A{1}:J{rooms.Count}"].AutoFilter = true;

                    #endregion

                    #region HS
                    // Формирование общего листа
                    excel.Workbook.Worksheets.Add("ХС");
                    worksheet = excel.Workbook.Worksheets["ХС"];

                    rowCount = 1;
                    colCount = 1;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Id);
                    worksheet.Cells[rowCount, colCount].Value = "Id";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование (по СП 158)";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Categoty_pizharoopasnosti);
                    worksheet.Cells[rowCount, colCount].Value = "Категория пожароопасности";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SanPin);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СанПиН";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_SP_158);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по СП 158";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Class_chistoti_GMP);
                    worksheet.Cells[rowCount, colCount].Value = "Класс чистоты по GMP";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_HS);
                    worksheet.Cells[rowCount, colCount].Value = "Холодоснабжение";
                    colCount++;

                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        //worksheet.Column(i).AutoFit();
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 5;
                    worksheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(2).Width = 8;
                    worksheet.Column(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(3).Width = 20;
                    worksheet.Column(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(4).Width = 20;
                    worksheet.Column(4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Column(5).Width = 8;
                    worksheet.Column(5).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(6).Width = 8;
                    worksheet.Column(6).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(7).Width = 8;
                    worksheet.Column(7).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(8).Width = 8;
                    worksheet.Column(8).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Column(9).Width = 20;
                    worksheet.Column(9).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["A1:I1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:I1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    colFromHex = System.Drawing.ColorTranslator.FromHtml("#cef4c1");
                    worksheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                    worksheet.View.FreezePanes(2, 6); // worksheet.View.FreezePanes(2, 1);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Top.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Bottom.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Left.Color.SetColor(System.Drawing.Color.DarkBlue);
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A1:I{rooms.Count + 1}"].Style.Border.Right.Color.SetColor(System.Drawing.Color.DarkBlue);
                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_HS?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }
                    worksheet.Cells[$"A{1}:I{rooms.Count}"].AutoFilter = true;

                    #endregion


                    FileInfo excelFile = new FileInfo(path);
                    excel.SaveAs(excelFile);
                }
                catch (Exception ex)
                {
                    Toolkit.MessageBox.Show("Возможно, файл открыт, закройте и повторите выгрузку\n" + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
                
            return true;
        }

        /// <summary>
        /// Выгрузка стандарта оборудования в Excel по проекту
        /// </summary>
        /// <param name="project"></param>
        public static void UploadStandartEquipmentToExcel(SqlDataReader sqlDataReader, string projectName)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();
            string path;
            path = openFileDialog.SelectedPath + "\\" + "Стандарт оборудования по" + projectName + ".xlsx";

            if (File.Exists(path))
                File.Delete(path);

            excel.Workbook.Worksheets.Add("Оборудование");

            ExcelWorksheet worksheet = excel.Workbook.Worksheets["Оборудование"];
            int rowCount = 1;
            int colCount = 1;


            worksheet.Cells[rowCount, colCount].Value = "Проект";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Здание";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Подразделение";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Id помещения";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Имя помещения";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Id оборудования";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Номер";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Код по классификатору";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Имя оборудования";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Количество";
            colCount++;

            rowCount++;
            colCount = 1;

            while (sqlDataReader.Read())
            {
                string o1 = sqlDataReader.GetValue(10).ToString().ToLower();
                string o2 = sqlDataReader.GetValue(11).ToString().ToLower();

                if ((o1 == "true" && o2 == "true") | (o1 == "" && o2 == ""))
                {
                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(0);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(1);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(2);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(3);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(4);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(5);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(9);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(6);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(7);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(8);
                    colCount++;

                    colCount = 1;
                    rowCount++;
                }
                else
                {
                    continue;
                }
            }
            FileInfo excelFile = new FileInfo(path);
            excel.SaveAs(excelFile);
        }

        /// <summary>
        /// Выгрузка всего списка оборудования в Excel по проекту
        /// </summary>
        /// <param name="project"></param>
        public static void UploadAllEquipmentToExcel(SqlDataReader sqlDataReader, string projectName)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            openFileDialog.ShowDialog();
            string path;
            path = openFileDialog.SelectedPath + "\\" + "Все оборудование по" + projectName + ".xlsx";

            if (File.Exists(path))
                File.Delete(path);

            excel.Workbook.Worksheets.Add("Оборудование");

            ExcelWorksheet worksheet = excel.Workbook.Worksheets["Оборудование"];
            int rowCount = 1;
            int colCount = 1;


            worksheet.Cells[rowCount, colCount].Value = "Проект";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Здание";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Подразделение";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Id помещения";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Имя помещения";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Id оборудования";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Номер";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Код по классификатору";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Имя оборудования";
            colCount++;

            worksheet.Cells[rowCount, colCount].Value = "Количество";
            colCount++;

            rowCount++;
            colCount = 1;

            while (sqlDataReader.Read())
            {
                string o1 = sqlDataReader.GetValue(10).ToString().ToLower();
                string o2 = sqlDataReader.GetValue(11).ToString().ToLower();



                if ( o1 == "true" |  o1 == "")
                {
                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(0);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(1);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(2);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(3);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(4);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(5);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(9);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(6);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(7);
                    colCount++;

                    worksheet.Cells[rowCount, colCount].Value = sqlDataReader.GetValue(8);
                    colCount++;

                    colCount = 1;
                    rowCount++;
                }
                else
                {
                    continue;
                }
            }
            FileInfo excelFile = new FileInfo(path);
            excel.SaveAs(excelFile);
        }
    }
}