using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerLib.Models.DataBaseModels
{
    public class ProjectsDbContext
    {
        RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();

        public List<ProjectDto> GetProjects()
        {
            return context.RaSM_Projects.ToList();
        }

        public List<BuildingDto> GetModels(ProjectDto projDto)
        {
            return context.RaSM_Buildings.Where(x => x.ProjectId == projDto.Id).ToList();
        }

        public List<RoomDto> GetRooms(SubdivisionDto subdiv)
        {
            if (subdiv != null)
                return context.RaSM_Rooms.Where(x => x.Subdivision.Id == subdiv.Id).ToList();
            else
                return null;
        }

        public List<RoomDto> GetRoomsByModels(BuildingDto building)
        {
            List<RoomDto> rooms = new List<RoomDto>();

            foreach (SubdivisionDto subdiv in context.RaSM_Subdivisions.Where(x => x.BuildingId == building.Id))
            {
                rooms.AddRange(context.RaSM_Rooms.Where(x => x.SubdivisionId == subdiv.Id));
            }

            return rooms;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
