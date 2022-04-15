using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesOV.Models.DbModels
{
    public static class MainDbModel
    {
        private static RoomAndSpacesDbContext context;
        static MainDbModel()
        {
            context = new RoomAndSpacesDbContext();
        }

        public static List<ProjectDto> GetProjects()
        {
            return context.RaSM_Projects.ToList();
        }

        public static List<BuildingDto> GetBuildingsByProject(ProjectDto project)
        {
            return context.RaSM_Buildings.Where(x => x.ProjectId == project.Id).ToList();
        }

        public static List<RoomDto> GetRoomsByBuilding(BuildingDto building)
        {
            List<RoomDto> rooms = new List<RoomDto>();
            
            foreach (SubdivisionDto subdiv in building.Subdivisions)
            {
                rooms.AddRange(subdiv.Rooms);
            }
            return rooms;
        }
    }
}
