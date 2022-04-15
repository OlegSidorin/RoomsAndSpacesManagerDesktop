using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using RoomAndSpacesOV.Dto;
using RoomAndSpacesOV.Models.RvtHelper;
using RoomAndSpacesOV.Models.RvtModels;
using RoomAndSpacesOV.ViewModels;
using RoomAndSpacesOV.Views.Windows;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoomAndSpacesOV
{
    [Transaction(TransactionMode.Manual)]
    public class Cmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MainModelRvtHelper mainModelRvtHelper = new MainModelRvtHelper();


            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<Element> spaces = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_MEPSpaces).ToElements().ToList();
            AddParametersToModelEvHend.Spacies = spaces.Select(x => x as Space).ToList();
            RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();

            
            List<SpaceDto> spacesDto = new List<SpaceDto>();

            MainWindowViewModel.SpaciesList = spacesDto.Where(x => x.parameters.Count != 0).ToList();
            ExternalEvent ExEventSelectRoom = ExternalEvent.Create(new AddParametersToModelEvHend());
            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = new MainWindowViewModel() { AddParamtersToModelExEvent = ExEventSelectRoom };
            mainWindow.Show();

           


            //int count = 0;
            //using (Transaction transaction = new Transaction(doc, "Внесение значений параметров"))
            //{
            //    transaction.Start();
            //    foreach (Element item in spaces)
            //    {
            //        count++;

            //        var rvtRoomNumber = item.LookupParameter("Номер").AsString();

            //        var roomDto = context.RaSM_Rooms.FirstOrDefault(x => x.RoomNumber == rvtRoomNumber);

            //        SpaceDto space = new SpaceDto()
            //        {
            //            Name = item.Name,
            //            RoomNumber = rvtRoomNumber
            //        };

            //        #region Внесение параметров

            //        if (roomDto != null)
            //        {
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Класс чистоты по СанПиН", "Class_chistoti_SanPin", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Класс чистоты по СП 158", "Class_chistoti_SP_158", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Нагрузка ЭОМ", "El_Nagruzka", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Относительная влажность_Текст", "Ot_vlazhnost", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание АР", "Discription_AR", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ВК", "Equipment_VK", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание КР", "Nagruzki_na_perekririe", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание МГ", "Discription_GSV", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ОВ", "Discription_OV", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ЭОМ", "Discription_EOM", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание СС", "Discription_SS", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ХС", "Discription_HS", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Приток кратность", "Pritok", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("M1_Вытяжка кратность", "Vityazhka", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Расчетная площадь", "Summary_Area", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура максимальная С", "T_max", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура минимальная С", "T_min", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура расчетная С", "T_calc", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Мощность_ТХ", "El_Nagruzka", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Освещенность_ТХ", "Osveshennost_pro_obshem_osvech", item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Количество пациентов", nameof(roomDto.Kolichestvo_personala), item, roomDto));
            //            space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Количество персонала", nameof(roomDto.Kolichestvo_posetitelei), item, roomDto));
            //            space.parameters.RemoveAll(x => x == null);
            //        }
                    




            //        if (roomDto?.Rab_mesta_posetiteli != null & roomDto?.Rab_mesta_posetiteli != "")
            //        { 
            //            var roomDtodeee = roomDto?.Rab_mesta_posetiteli.Split('/');

            //            int pac;
            //            int.TryParse(roomDtodeee[1], out pac);

            //            if (pac != default)
            //                item.LookupParameter("М1_Количество пациентов")?.Set(pac);



            //            int per;
            //            int.TryParse(roomDtodeee[0], out per);

            //            if (per != default)
            //                item.LookupParameter("М1_Количество персонала")?.Set(per);
            //        }

            //        #endregion
            //        spacesDto.Add(space);
            //    }
            //    transaction.Commit();
            //}

            //MainWindowViewModel.SpaciesList = spacesDto.Where(x => x.parameters.Count != 0).ToList();
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.DataContext = new MainWindowViewModel();
            //mainWindow.ShowDialog();

            return Result.Succeeded;
        }
    }
}
