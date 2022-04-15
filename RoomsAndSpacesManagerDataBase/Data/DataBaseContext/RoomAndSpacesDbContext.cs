using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDataBase.Data.DataBaseContext
{
    public class RoomAndSpacesDbContext : DbContext
    {
        public RoomAndSpacesDbContext()
        {
            this.Database.Connection.ConnectionString = @"Data Source=nt-db01.ukkalita.local;Initial Catalog=M1_Revit;integrated security=True;MultipleActiveResultSets=True";
        }

        public DbSet<ProjectDto> RaSM_Projects { get; set; }
        public DbSet<BuildingDto> RaSM_Buildings { get; set; }
        public DbSet<SubdivisionDto> RaSM_Subdivisions { get; set; }
        public DbSet<RoomDto> RaSM_Rooms { get; set; }
        public DbSet<CategoryDto> RaSM_RoomCategories { get; set; }
        public DbSet<SubCategoryDto> RaSM_RoomSubCategories { get; set; }
        public DbSet<RoomNameDto> RaSM_RoomNames { get; set; }
        public DbSet<RoomEquipmentDto> RaSM_RoomEquipments { get; set; }
        public DbSet<EquipmentDto> RaSM_Equipments { get; set; }
    }
}
