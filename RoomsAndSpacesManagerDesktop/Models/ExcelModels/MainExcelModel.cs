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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Min_area);
                    worksheet.Cells[rowCount, colCount].Value = "Мин. площадь";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Rab_mesta_posetiteli);
                    worksheet.Cells[rowCount, colCount].Value = "Рабочих мест/посетителей";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Kolichestvo_personala);
                    worksheet.Cells[rowCount, colCount].Value = "Количество персонала";
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
                    worksheet.Cells[rowCount, colCount].Value = "Приток";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Vityazhka);
                    worksheet.Cells[rowCount, colCount].Value = "Вытяжка";
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.KEO_est_osv);
                    worksheet.Cells[rowCount, colCount].Value = "КЕО при бок. ест. осв.";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.KEO_sovm_osv);
                    worksheet.Cells[rowCount, colCount].Value = "КЕО при бок. совм. осв.";
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Nagruzki_na_perekririe);
                    worksheet.Cells[rowCount, colCount].Value = "Нагрузка на перекрытие";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Discription_AK_ATH);
                    worksheet.Cells[rowCount, colCount].Value = "Примечание АК, АТХ";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 3 && i != 4)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(3).Width = 35;
                    worksheet.Column(4).Width = 25;
                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Subdivision?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Min_area?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Rab_mesta_posetiteli?.ToString();
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

                        worksheet.Cells[rowCount, colCount].Value = item.KEO_est_osv?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.KEO_sovm_osv?.ToString();
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

                        worksheet.Cells[rowCount, colCount].Value = item.Nagruzki_na_perekririe?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_AK_ATH?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_GSV?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Discription_HS?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;

                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Rab_mesta_posetiteli);
                    worksheet.Cells[rowCount, colCount].Value = "Рабочих мест/посетителей";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;
                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Rab_mesta_posetiteli?.ToString();
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;
                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;

                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Nagruzki_na_perekririe?.ToString();
                        colCount++;

                        colCount = 1;

                        rowCount++;
                    }

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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Rab_mesta_posetiteli);
                    worksheet.Cells[rowCount, colCount].Value = "Рабочих мест/посетителей";
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
                    worksheet.Cells[rowCount, colCount].Value = "Приток";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Vityazhka);
                    worksheet.Cells[rowCount, colCount].Value = "Вытяжка";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;
                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Rab_mesta_posetiteli?.ToString();
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Rab_mesta_posetiteli);
                    worksheet.Cells[rowCount, colCount].Value = "Рабочих мест/посетителей";
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.KEO_est_osv);
                    worksheet.Cells[rowCount, colCount].Value = "КЕО при бок. ест. осв.";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.KEO_sovm_osv);
                    worksheet.Cells[rowCount, colCount].Value = "КЕО при бок. совм. осв.";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;
                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Rab_mesta_posetiteli?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Categoty_pizharoopasnosti?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SanPin?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_SP_158?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Class_chistoti_GMP?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.KEO_est_osv?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.KEO_sovm_osv?.ToString();
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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
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

                    colCount = 1;
                    rowCount++;

                    worksheet.Cells["A1:AZ1"].Style.Font.Bold = true;

                    for (int i = 1; i < 35; i++)
                    {
                        worksheet.Column(i).Style.WrapText = true;
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;

                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
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

                        colCount = 1;

                        rowCount++;
                    }

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

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.Name);
                    worksheet.Cells[rowCount, colCount].Value = "Наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.ShortName);
                    worksheet.Cells[rowCount, colCount].Value = "Краткое наименование";
                    colCount++;

                    //worksheet.Cells[rowCount, colCount].Value = nameof(RoomDto.RoomNumber);
                    worksheet.Cells[rowCount, colCount].Value = "Номер помещения";
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
                        worksheet.Column(i).Width = 20;
                        //worksheet.Column(i).AutoFit();
                        if (i != 2 && i != 3)
                        {
                            worksheet.Column(i).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Column(i).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 35;
                    worksheet.Column(3).Width = 25;
                    foreach (var item in rooms)
                    {
                        worksheet.Cells[rowCount, colCount].Value = item.Id.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.Name?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.ShortName?.ToString();
                        colCount++;

                        worksheet.Cells[rowCount, colCount].Value = item.RoomNumber?.ToString();
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