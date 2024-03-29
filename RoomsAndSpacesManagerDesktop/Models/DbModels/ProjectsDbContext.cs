﻿using System.Collections.Generic;
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
            return context.RaSM_Rooms.Where(x => x.SubdivisionId == subdiv.Id).OrderBy(r => r.RowNumber).ThenBy(r => r.Id).ToList();
        }
        public List<RoomDto> AddNewRoom(SubdivisionDto subdiv, int roomscount)
        {
            context.RaSM_Rooms.Add(new RoomDto()
            {
                SubdivisionId = subdiv.Id,
                RowNumber = roomscount + 1,
                RoomNumber = string.Empty,
                RoomNameId = 0,
                ShortName = string.Empty,
                Discription_HS = string.Empty,
                Discription_GSV = string.Empty,
                Discription_AK_ATH = string.Empty,
                Discription_SS = string.Empty,
                Equipment_VK = string.Empty,
                Discription_EOM = string.Empty,
                Group_el_bez = string.Empty,
                Osveshennost_pro_obshem_osvech = string.Empty,
                Discription_OV = string.Empty,
                Ot_vlazhnost = string.Empty,
                Discription_AR = string.Empty,
                Vityazhka = string.Empty,
                Pritok = string.Empty,
                T_min = string.Empty,
                T_max = string.Empty,
                T_calc = string.Empty,
                Class_chistoti_SP_158 = string.Empty,
                Class_chistoti_SanPin = string.Empty,
                Class_chistoti_GMP = string.Empty,
                Min_area = string.Empty,
                Notation = string.Empty,
                Categoty_Chistoti_po_san_epid = string.Empty,
                Nagruzki_na_perekririe = string.Empty,
                Categoty_pizharoopasnosti = string.Empty,
                El_Nagruzka = string.Empty,
                ArRoomId = 0,
                Kolichestvo_posetitelei = string.Empty,
                Kolichestvo_personala = string.Empty,
                Rab_mesta_posetiteli = string.Empty,
                KEO_est_osv = string.Empty,
                KEO_sovm_osv = string.Empty
            });
            context.SaveChanges();
            return context.RaSM_Rooms.Where(x => x.SubdivisionId == subdiv.Id).OrderBy(r => r.RowNumber).ThenBy(r => r.Id).ToList();
        }

        public List<RoomDto> AddNewRoom(SubdivisionDto subdiv, RoomDto room, out RoomDto roomDtoNew)
        {
            if (room.RoomName != null)
            {
                roomDtoNew = new RoomDto()
                {
                    SubdivisionId = subdiv.Id,
                    RowNumber = room.RowNumber,
                    RoomNumber = room.RoomNumber,
                    RoomNameId = room.RoomName.Id,
                    ShortName = room.ShortName,
                };

                if (room.Discription_HS != null) roomDtoNew.Discription_HS = room.Discription_HS; else room.Discription_HS = "";
                if (room.Discription_GSV != null) roomDtoNew.Discription_GSV = room.Discription_GSV; else room.Discription_GSV = "";
                if (room.Discription_AK_ATH != null) roomDtoNew.Discription_AK_ATH = room.Discription_AK_ATH; else room.Discription_AK_ATH = "";
                if (room.Discription_SS != null) roomDtoNew.Discription_SS = room.Discription_SS; else room.Discription_SS = "";
                if (room.Equipment_VK != null) roomDtoNew.Equipment_VK = room.Equipment_VK; else room.Equipment_VK = "";
                if (room.Discription_EOM != null) roomDtoNew.Discription_EOM = room.Discription_EOM; else room.Discription_EOM = "";
                if (room.Group_el_bez != null) roomDtoNew.Group_el_bez = room.Group_el_bez; else room.Group_el_bez = "";
                if (room.Osveshennost_pro_obshem_osvech != null) roomDtoNew.Osveshennost_pro_obshem_osvech = room.Osveshennost_pro_obshem_osvech; else room.Osveshennost_pro_obshem_osvech = "";
                if (room.Discription_OV != null) roomDtoNew.Discription_OV = room.Discription_OV; else room.Discription_OV = "";
                if (room.Ot_vlazhnost != null) roomDtoNew.Ot_vlazhnost = room.Ot_vlazhnost; else room.Ot_vlazhnost = "";
                if (room.Discription_AR != null) roomDtoNew.Discription_AR = room.Discription_AR; else room.Discription_AR = "";
                if (room.Vityazhka != null) roomDtoNew.Vityazhka = room.Vityazhka; else room.Vityazhka = "";
                if (room.Pritok != null) roomDtoNew.Pritok = room.Pritok; else room.Pritok = "";
                if (room.T_min != null) roomDtoNew.T_min = room.T_min; else room.T_min = "";
                if (room.T_max != null) roomDtoNew.T_max = room.T_max; else room.T_max = "";
                if (room.T_calc != null) roomDtoNew.T_calc = room.T_calc; else room.T_calc = "";
                if (room.Class_chistoti_SP_158 != null) roomDtoNew.Class_chistoti_SP_158 = room.Class_chistoti_SP_158; else room.Class_chistoti_SP_158 = "";
                if (room.Class_chistoti_SanPin != null) roomDtoNew.Class_chistoti_SanPin = room.Class_chistoti_SanPin; else room.Class_chistoti_SanPin = "";
                if (room.Class_chistoti_GMP != null) roomDtoNew.Class_chistoti_GMP = room.Class_chistoti_GMP; else room.Class_chistoti_GMP = "";
                if (room.Min_area != null) roomDtoNew.Min_area = room.Min_area; else room.Min_area = "";
                if (room.Notation != null) roomDtoNew.Notation = room.Notation; else room.Notation = "";
                //if (room.Equipments != null) roomDtoNew.Equipments = room.Equipments; else room.Equipments = "";
                if (room.Categoty_Chistoti_po_san_epid != null) roomDtoNew.Categoty_Chistoti_po_san_epid = room.Categoty_Chistoti_po_san_epid; else room.Categoty_Chistoti_po_san_epid = "";
                if (room.Nagruzki_na_perekririe != null) roomDtoNew.Nagruzki_na_perekririe = room.Nagruzki_na_perekririe; else room.Nagruzki_na_perekririe = "";
                if (room.Categoty_pizharoopasnosti != null) roomDtoNew.Categoty_pizharoopasnosti = room.Categoty_pizharoopasnosti; else room.Categoty_pizharoopasnosti = "";
                if (room.El_Nagruzka != null) roomDtoNew.El_Nagruzka = room.El_Nagruzka; else room.El_Nagruzka = "";
                if (room.ArRoomId != null) roomDtoNew.ArRoomId = room.ArRoomId;
                if (room.Kolichestvo_posetitelei != null) roomDtoNew.Kolichestvo_posetitelei = room.Kolichestvo_posetitelei; else room.Kolichestvo_posetitelei = "";
                if (room.Kolichestvo_personala != null) roomDtoNew.Kolichestvo_personala = room.Kolichestvo_personala; else room.Kolichestvo_personala = "";
                if (room.Rab_mesta_posetiteli != null) roomDtoNew.Rab_mesta_posetiteli = room.Rab_mesta_posetiteli; else room.Rab_mesta_posetiteli = "";

                context.RaSM_Rooms.Add(roomDtoNew);

                context.SaveChanges();
            }
            else
            {
                roomDtoNew = null;
            }
            
            return context.RaSM_Rooms.Where(x => x.SubdivisionId == subdiv.Id).OrderBy(r => r.RowNumber).ThenBy(r => r.Id).ToList();
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
                return context.RaSM_Rooms.Where(x => x.Subdivision.Id == subdivision.Id).OrderBy(r => r.RowNumber).ThenBy(r => r.Id).ToList();
            else
                return null;
        }
        public List<RoomDto> GetRooms(SubdivisionDto subdivision, string roomShortName)
        {
            if (subdivision != null)
                return context.RaSM_Rooms.Where(x => x.Subdivision.Id == subdivision.Id && x.ShortName.Contains(roomShortName)).OrderBy(r => r.RowNumber).ThenBy(r => r.Id).ToList();
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
        public List<RoomDto> GetAllRoomsByProject(ProjectDto project, BuildingDto buildingDto)
        {
            if (project != null)
            {
                List<int> subDivsIds = new List<int>();

                foreach (BuildingDto build in project.Buildings)
                {
                    if (build.Name.Equals(buildingDto.Name))
                    {
                        foreach (SubdivisionDto subdiv in build.Subdivisions.OrderBy(x => x.Order))
                        {
                            subDivsIds.Add(subdiv.Id);
                        }
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
