using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using RoomsAndSpacesManagerLib.ViewModels;
using System.Windows;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerLib.Models.RevitHelper;
using RoomsAndSpacesManagerLib.Dto;

namespace RoomsAndSpacesManagerLib.Models.RevitModels
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class AddParametersIntoSpacies : IExternalEventHandler
    {
        public static List<RoomDto> Rooms { get; set; }
        public static event Action<object> ChangeUI;

        public void Execute(UIApplication app)
        {
            List<SpaceDto> changedSpacies = new List<SpaceDto>();

            Document doc = app.ActiveUIDocument.Document;
            List<Room> elements = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .WhereElementIsNotElementType()
                .Select(x => x as Room)
                .Where(x => x.LookupParameter("Номер") != null & x.LookupParameter("Номер").AsString() != "")
                .ToList();
            int count = 0;
            int count2 = 0;
            using (Transaction trans = new Transaction(doc, "set parameters"))
            {
                trans.Start();
                foreach (RoomDto room in Rooms)
                {
                    Room rvtRoom = elements.FirstOrDefault(x => x.LookupParameter("Номер").AsString() == room.RoomNumber);
                    if (rvtRoom != null)
                    {
                        //Console.WriteLine("get number of room " + rvtRoom.Number);
                        SpaceDto spaceDto = new SpaceDto()
                        {
                            Name = rvtRoom.Name,
                            RoomNumber = rvtRoom.Number,
                        };

                        if (rvtRoom == null)
                            count++;
                        else
                            count2++;

                        spaceDto.parameters.Add(RevitHelperModel.SetProperty("М1_Категория пожароопасности", nameof(room.Categoty_pizharoopasnosti), rvtRoom, room));
                        spaceDto.parameters.Add(RevitHelperModel.SetProperty("М1_Класс чистоты по СанПиН", nameof(room.Class_chistoti_SanPin), rvtRoom, room));
                        spaceDto.parameters.Add(RevitHelperModel.SetProperty("М1_Класс чистоты по СП 158", nameof(room.Class_chistoti_SP_158), rvtRoom, room));
                        spaceDto.parameters.Add(RevitHelperModel.SetProperty("М1_Количество пациентов", nameof(room.Kolichestvo_posetitelei), rvtRoom, room));
                        spaceDto.parameters.Add(RevitHelperModel.SetProperty("М1_Количество персонала", nameof(room.Kolichestvo_personala), rvtRoom, room));
                        spaceDto.parameters.Add(RevitHelperModel.SetProperty("М1_Примечание АР", nameof(room.Discription_AR), rvtRoom, room));
                        spaceDto.parameters.Add(RevitHelperModel.SetProperty("М1_Расчетная площадь", nameof(room.Min_area), rvtRoom, room));

                        spaceDto.parameters.RemoveAll(x => x == null);
                        changedSpacies.Add(spaceDto);
                    }
                }
                
                trans.Commit();
            }
            MainWindowViewModel.Spacies = changedSpacies.Where(x => x.parameters.Count != 0).ToList();
            ChangeUI?.Invoke(this);
        }
        public string GetName() => nameof(AddParametersIntoSpacies);
    }
}
