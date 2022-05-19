using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;


namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    public class ProjectsDbContext : MainDbContext
    {
        public void DB()
        {
            
        }

        #region Добавть данные
        /// <summary>
        /// Добавить новый проект в БД
        /// </summary>
        /// <param name="proj"></param>
        public void AddNewProjects(ProjectDto proj)
        {
            context.RaSM_Projects.Add(proj);
            context.SaveChanges();
        }

        /// <summary>
        /// Добавить новое здание в БД
        /// </summary>
        /// <param name="building"></param>
        public void AddNewBuilding(BuildingDto building)
        {
            context.RaSM_Buildings.Add(building);
            context.SaveChanges();
        }

        /// <summary>
        /// Добавить новое подразделение в БД
        /// </summary>
        /// <param name="subdivision"></param>
        public void AddNewSubdivision(SubdivisionDto subdivision)
        {
            context.RaSM_Subdivisions.Add(subdivision);
            context.SaveChanges();
        }

        /// <summary>
        /// Добавить новое помещение в БД
        /// </summary>
        /// <param name="rooms"></param>
        public void AddNewRooms(List<RoomDto> rooms)
        {
            context.RaSM_Rooms.AddRange(rooms.Where(x => x.Id == default).ToList());
            context.SaveChanges();
        }

        public void AddNewRoom(RoomDto room)
        {
            context.RaSM_Rooms.Add(room);
            context.SaveChanges();
        }

        public List<RoomDto> AddNewRoom(SubdivisionDto subdiv)
        {
            context.RaSM_Rooms.Add(new RoomDto()
            {
                SubdivisionId = subdiv.Id
            });
            context.SaveChanges();
            return context.RaSM_Rooms.Where(x => x.SubdivisionId == subdiv.Id).ToList();
        }

        public List<RoomDto> AddNewRoom(SubdivisionDto subdiv, RoomDto room, out RoomDto roomDtoNew)
        {
            roomDtoNew = new RoomDto()
            {
                SubdivisionId = subdiv.Id,
                RoomNameId = room.RoomName.Id,
                ShortName = room.ShortName,
                Min_area = room.RoomName.Min_area,
                Class_chistoti_GMP = room.RoomName.Class_chistoti_GMP,
                Class_chistoti_SanPin = room.RoomName.Class_chistoti_SanPin,
                Class_chistoti_SP_158 = room.RoomName.Class_chistoti_SP_158,
                T_calc = room.RoomName.T_calc,
                T_max = room.RoomName.T_max,
                T_min = room.RoomName.T_min,
                Pritok = room.RoomName.Pritok,
                Vityazhka = room.RoomName.Vityazhka,
                Discription_AR = room.RoomName.Discription_AR,
                Ot_vlazhnost = room.RoomName.Ot_vlazhnost,
                Discription_OV = room.RoomName.Discription_OV,
                Osveshennost_pro_obshem_osvech = room.RoomName.Osveshennost_pro_obshem_osvech,
                Group_el_bez = room.RoomName.Group_el_bez,
                Discription_EOM = room.RoomName.Discription_EOM,
                Equipment_VK = room.RoomName.Equipment_VK,
                Discription_SS = room.RoomName.Discription_SS,
                Discription_AK_ATH = room.RoomName.Discription_AK_ATH,
                Discription_GSV = room.RoomName.Discription_GSV,
                Discription_HS = room.RoomName.Discription_HS

            };
            context.RaSM_Rooms.Add(roomDtoNew);

            context.SaveChanges();
            return context.RaSM_Rooms.Where(x => x.SubdivisionId == subdiv.Id).ToList();
        }


        #endregion


        #region Получить данные
        /// <summary>
        /// Получить список проектов из БД
        /// </summary>
        /// <returns></returns>
        public List<ProjectDto> GetProjects()
        {
            return context.RaSM_Projects.ToList();
        }

        /// <summary>
        /// Получить список зданий из БД
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public List<BuildingDto> GetModels(ProjectDto project)
        {
            return context.RaSM_Buildings.Where(x => x.ProjectId == project.Id).ToList();
        }

        /// <summary>
        /// Получить список подразделений из БД
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public List<SubdivisionDto> GetSubdivisions(BuildingDto building)
        {
            return context.RaSM_Subdivisions.Where(x => x.BuildingId == building.Id).OrderBy(x => x.Order).ToList();

        }

        /// <summary>
        /// Получить список задний из БД
        /// </summary>
        /// <param name="subdivision"></param>
        /// <returns></returns>
        public List<RoomDto> GetRooms(SubdivisionDto subdivision)
        {
            if (subdivision != null)
                return context.RaSM_Rooms.Where(x => x.Subdivision.Id == subdivision.Id).ToList();
            else
                return null;
        }

        public List<RoomDto> GetAllRoomsByProject(ProjectDto project)
        {
            if (project != null)
            {
                List<int> subDivsIds = new List<int>();

                foreach (BuildingDto build in project.Buildings)
                {
                    foreach (SubdivisionDto subdiv in build.Subdivisions.OrderBy(x => x.Order))
                    {
                        subDivsIds.Add(subdiv.Id);
                    }
                }

                return context.RaSM_Rooms.Where(x => subDivsIds.Contains(x.SubdivisionId)).OrderBy(x => x.Subdivision.Order).ToList();
            }
            return null;
        }

        #endregion


        #region Удалить данные
        /// <summary>
        /// Удалить проект из БД. (С проектом удаляются все здания, подразделения и помещения из БД)
        /// </summary>
        /// <param name="projDto"></param>
        public void RemoveProject(ProjectDto projDto)
        {
            context.RaSM_Projects.Remove(projDto);
            context.SaveChanges();
        }

        /// <summary>
        /// Удалить здание из БД. (Со зданием удаляются все подразделения и помещения из БД)
        /// </summary>
        /// <param name="buildDto"></param>
        public void RemoveBuilding(BuildingDto buildDto)
        {
            context.RaSM_Buildings.Remove(buildDto);
            context.SaveChanges();
        }

        /// <summary>
        /// Удалить подразделения из БД. (С подразделение удаляются все помещения, которые соотвествуют подразделению, из БД)
        /// </summary>
        /// <param name="subdiv"></param>
        public void RemoveSubDivision(SubdivisionDto subdiv)
        {
            context.RaSM_Subdivisions.Remove(subdiv);
            context.SaveChanges();
        }

        /// <summary>
        /// Удалить помещение из БД
        /// </summary>
        /// <param name="room"></param>
        public void RemoveRoom(RoomDto room)
        {
            context.RaSM_Rooms.Remove(room);
            context.SaveChanges();
        } 
        #endregion


        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
