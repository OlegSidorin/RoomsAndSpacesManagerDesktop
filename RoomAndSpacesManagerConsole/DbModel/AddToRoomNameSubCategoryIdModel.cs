using OfficeOpenXml;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesManagerConsole.DbModel
{
    class AddToRoomNameSubCategoryIdModel
    {

        public void AddOrderCountToSubdivision()
        {
            RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();

            foreach (BuildingDto build in roomAndSpacesDbContext.RaSM_Buildings)
            {
                int orderCount = 1;
                foreach (SubdivisionDto subdiv in build.Subdivisions)
                {
                    subdiv.Order = orderCount;
                    orderCount++;
                }
                orderCount = 1;
            }
            roomAndSpacesDbContext.SaveChanges();
        }

        public void AddToRoomNameSubCategoryIdModelMain()
        {
            RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();

            ExcelPackage excel = new ExcelPackage(new FileInfo(@"C:\Users\ya.goreglyad\Desktop\ПомещенияВДб.xlsx"));
            var workbook = excel.Workbook;
            var worksheet = workbook.Worksheets.First();

            int rowCount = 1;
            int count = 1;
            List<string> roomNamesIsNotExcist = new List<string>();
            while (rowCount < 112)
            {
                int subCatId = Convert.ToInt32(worksheet.Cells[rowCount, 1].Value);
                string roomName = worksheet.Cells[rowCount, 3].Value.ToString();
                if (roomAndSpacesDbContext.RaSM_RoomNames.FirstOrDefault(x => x.Name == roomName) != null)
                {
                    roomAndSpacesDbContext.RaSM_RoomNames.FirstOrDefault(x => x.Name == roomName).SubCategotyId = subCatId;
                }
                else
                {

                }
                rowCount++;
            }
            roomAndSpacesDbContext.SaveChanges();

        }

        

        public void Change_1_Field()
        {
            RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();
            //var dfdf = roomAndSpacesDbContext.RaSM_RoomNames.Where(x => x.SubCategotyId == 0).ToList();
            roomAndSpacesDbContext.RaSM_RoomNames.RemoveRange(roomAndSpacesDbContext.RaSM_RoomNames.Where(x => x.SubCategotyId == 0));
            roomAndSpacesDbContext.SaveChanges();

        }

        public void SwapPersonalPosetiteli()
        {
            RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();
            foreach (RoomDto room in roomAndSpacesDbContext.RaSM_Rooms.Where(x => x.Rab_mesta_posetiteli != null))
            {
                var s = room.Rab_mesta_posetiteli.Split('/');
                string personal = s[0];
                string posetiteli = s[1];
                room.Kolichestvo_personala = personal;
                room.Kolichestvo_posetitelei = posetiteli;
            }

            roomAndSpacesDbContext.SaveChanges();

        }
    }
}
