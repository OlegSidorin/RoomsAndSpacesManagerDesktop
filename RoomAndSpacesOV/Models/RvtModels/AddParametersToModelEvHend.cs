using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using RoomAndSpacesOV.Dto;
using RoomAndSpacesOV.Models.RvtHelper;
using RoomAndSpacesOV.ViewModels;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesOV.Models.RvtModels
{
    class AddParametersToModelEvHend : IExternalEventHandler
    {
        /// <summary>
        /// Список помещений. Из Cmd
        /// </summary>
        public static List<Space> Spacies { get; set; }

        /// <summary>
        /// Список помещений из БД. Из vm
        /// </summary>
        public static List<RoomDto> RoomsDto { get; set; }

        MainModelRvtHelper mainModelRvtHelper = new MainModelRvtHelper();
        public static event Action<object> ChangeUI;
        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            List<SpaceDto> spacesDto = new List<SpaceDto>();
            using (Transaction transaction = new Transaction(doc, "Внесение значений параметров"))
            {
                transaction.Start();
                foreach (Space item in Spacies)
                {

                    var rvtRoomNumber = item.LookupParameter("Номер").AsString();

                    var roomDto = RoomsDto.FirstOrDefault(x => x.RoomNumber == rvtRoomNumber);

                    SpaceDto space = new SpaceDto()
                    {
                        Name = item.Name,
                        RoomNumber = rvtRoomNumber
                    };
                    if (roomDto != null)
                    {
                        space.Subdivision = roomDto.Subdivision.Name;
                    }

                    #region Внесение параметров
                    if (roomDto != null)
                    {
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Вытяжка кратность", nameof(roomDto.Vityazhka), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Группа по электробезопасности", nameof(roomDto.Group_el_bez), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Категория пожароопасности", nameof(roomDto.Categoty_pizharoopasnosti), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Класс чистоты по СанПиН", nameof(roomDto.Class_chistoti_SanPin), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Класс чистоты по СП 158", nameof(roomDto.Class_chistoti_SP_158), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Количество пациентов", nameof(roomDto.Kolichestvo_posetitelei), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Количество персонала", nameof(roomDto.Kolichestvo_personala), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Мощность_ТХ", nameof(roomDto.El_Nagruzka), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Освещенность_ТХ", nameof(roomDto.Osveshennost_pro_obshem_osvech), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Относительная влажность_Текст", nameof(roomDto.Ot_vlazhnost), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание АР", nameof(roomDto.Discription_AR), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ВК", nameof(roomDto.Equipment_VK), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание КР", nameof(roomDto.Nagruzki_na_perekririe), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание МГ", nameof(roomDto.Discription_GSV), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ОВ", nameof(roomDto.Discription_OV), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ТГ", nameof(roomDto.Discription_GSV), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ЭМ", nameof(roomDto.Discription_EOM), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание СС", nameof(roomDto.Discription_SS), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Примечание ХС", nameof(roomDto.Discription_HS), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Приток кратность", nameof(roomDto.Pritok), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Расчетная площадь", nameof(roomDto.Min_area), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура максимальная С", nameof(roomDto.T_max), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура минимальная С", nameof(roomDto.T_min), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Температура расчетная С", nameof(roomDto.T_calc), item, roomDto));
                        space.parameters.Add(mainModelRvtHelper.SetPropertt("М1_Подразделение", nameof(roomDto.Subdivision.Name), item, roomDto));
                        space.parameters.RemoveAll(x => x == null);
                    }
                    #endregion

                    RoomsDto.Remove(roomDto);

                    spacesDto.Add(space);
                }



                transaction.Commit();
                MainWindowViewModel.SpaciesList = spacesDto.Where(x => x.parameters.Count != 0).ToList();
                MainWindowViewModel.UnfoundedRooms = RoomsDto;
                ChangeUI?.Invoke(this);
            }
        }

        public string GetName() => nameof(AddParametersToModelEvHend);
    }
}
