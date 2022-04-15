using Microsoft.VisualBasic.FileIO;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesManagerConsole
{
    class AddInfoToDb
    {
        RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();
        public void ProjectInfoToDB(string ProjectName, string BuildingName)
        {
            //Добавление проекта
            roomAndSpacesDbContext.RaSM_Projects.Add(new ProjectDto()
            {
                Name = ProjectName
            });
            roomAndSpacesDbContext.SaveChanges();

            //Добавление здания
            roomAndSpacesDbContext.RaSM_Buildings.Add(new BuildingDto()
            {
                Name = BuildingName,
                ProjectId = roomAndSpacesDbContext.RaSM_Projects.FirstOrDefault(x => x.Name == ProjectName).Id
            });
            roomAndSpacesDbContext.SaveChanges();

            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\ЕКА-ЦКЛ Задание ТХ - Общий.csv"))
            {
                List<RoomDto> roomDtos = new List<RoomDto>();
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    var straintname = fields[2];
                    var id = roomAndSpacesDbContext.RaSM_RoomNames.FirstOrDefault(x => x.Name == straintname)?.Id;
                    var ddd = new RoomDto()
                    {
                        //BuildingId = roomAndSpacesDbContext.RaSM_Buildings.FirstOrDefault(x => x.Name == BuildingName).Id,
                        RoomNumber = fields[1],
                        //Name = fields[2],
                        ShortName = fields[3],
                        Min_area = fields[4],
                        //Count = Convert.ToInt32(fields[5]),
                        //Summary_Area = Convert.ToInt32(fields[6]),
                        Rab_mesta_posetiteli = fields[7],
                        Categoty_pizharoopasnosti = fields[8],
                        Class_chistoti_SanPin = fields[9],
                        Class_chistoti_SP_158 = fields[10],
                        Class_chistoti_GMP = fields[11],
                        Discription_AR = fields[12],
                        T_calc = fields[13],
                        T_min = fields[14],
                        T_max = fields[15],
                        Pritok = fields[16],
                        Vityazhka = fields[17],
                        Ot_vlazhnost = fields[18],
                        Discription_OV = fields[19],
                        Equipment_VK = fields[20],
                        KEO_est_osv = fields[21],
                        KEO_sovm_osv = fields[22],
                        Osveshennost_pro_obshem_osvech = fields[23],
                        El_Nagruzka = fields[24],
                        Group_el_bez = fields[25],
                        Discription_EOM = fields[26],
                        Discription_SS = fields[27],
                        Discription_AK_ATH = fields[28],
                        Nagruzki_na_perekririe = fields[29],
                        Discription_GSV = fields[30],
                        Discription_HS = fields[31],
                    };

                    if (id != null)
                        ddd.RoomNameId = Convert.ToInt32(id);

                    roomDtos.Add(ddd);

                }
                roomAndSpacesDbContext.RaSM_Rooms.AddRange(roomDtos.Where(x => x.Name != null));
                roomAndSpacesDbContext.SaveChanges();
            }

            



        }
    }
}
