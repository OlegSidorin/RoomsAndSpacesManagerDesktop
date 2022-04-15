using Microsoft.VisualBasic.FileIO;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesManagerConsole
{
    class CsvToDbRooms
    {
        RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();
        List<CategoryDto> categoryDtos;
        List<SubCategoryDto> subCategoryDtos;
        public void AddCats()
        {
            List<CategoryDto> CatList = new List<CategoryDto>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Категории-Подкатегрии.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (CatList.FirstOrDefault(x => x.Name == fields[0]) == null)
                    {
                        CatList.Add(new CategoryDto()
                        {
                            Key = fields[1],
                            Name = fields[0]
                        });
                    }
                }
                roomAndSpacesDbContext.RaSM_RoomCategories.AddRange(CatList);
                roomAndSpacesDbContext.SaveChanges();

            }
        }


        public void GetSubCatsCsv()
        {
            categoryDtos = roomAndSpacesDbContext.RaSM_RoomCategories.ToList();
            List<SubCategoryDto> SubCatList = new List<SubCategoryDto>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Категории-Подкатегрии.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    SubCatList.Add(new SubCategoryDto()
                    {
                        Key = fields[3],
                        Name = fields[2]
                    });
                }

            }
            foreach (SubCategoryDto _subCat in SubCatList)
            {
                var dd = _subCat.Key.Split('.')[1];
                int id = categoryDtos.FirstOrDefault(x => x.Key.Split('.')[1] == dd).Id;
                _subCat.CategotyId = id;
            }

            roomAndSpacesDbContext.RaSM_RoomSubCategories.AddRange(SubCatList);
            roomAndSpacesDbContext.SaveChanges();
        }


        public void AddRooms()
        {
            subCategoryDtos = roomAndSpacesDbContext.RaSM_RoomSubCategories.ToList();
            List<RoomNameDto> RoomsList = new List<RoomNameDto>();
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\ya.goreglyad\Desktop\Помещения.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    RoomsList.Add(new RoomNameDto()
                    {
                        Key = fields[0],
                        Name = fields[1],
                        Min_area = fields[2],
                        Class_chistoti_SanPin = fields[3],
                        Class_chistoti_SP_158 = fields[4],
                        Class_chistoti_GMP = fields[5],
                        T_calc = fields[6],
                        T_min = fields[7],
                        T_max = fields[8],
                        Pritok = fields[9],
                        Vityazhka = fields[10],
                        Ot_vlazhnost = fields[11],
                        KEO_est_osv = fields[12],
                        KEO_sovm_osv = fields[13],
                        Discription_OV = fields[14],
                        Osveshennost_pro_obshem_osvech = fields[15],
                        Group_el_bez = fields[16],
                        Discription_EOM = fields[17],
                        Discription_AR = fields[18],
                        Equipment_VK = fields[19],
                        Discription_SS = fields[20],
                        Discription_AK_ATH = fields[21],
                        Discription_GSV = fields[22],
                        Categoty_Chistoti_po_san_epid = fields[23],
                        Discription_HS = fields[24]
                    });

                }

                foreach (RoomNameDto room in RoomsList)
                {
                    string roomKey = room.Key;


                    var sdd = subCategoryDtos.FirstOrDefault(x => x.Key == roomKey);

                    if (sdd != null)
                    {
                        room.SubCategotyId = sdd.Id;
                    }
                }

                roomAndSpacesDbContext.RaSM_RoomNames.AddRange(RoomsList);
                roomAndSpacesDbContext.SaveChanges();
            }
        }

    }
}
