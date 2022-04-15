using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_SubdivisionDto")]
    public class SubdivisionDto : ViewModel
    {

        public SubdivisionDto()
        {

        }

        public static RoomAndSpacesDbContext roomAndSpacesDbContext = new RoomAndSpacesDbContext();
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; } = false;
        [NotMapped]
        public int? SunnuryArea { get; set; }
        private bool isReadOnly = false;
        [NotMapped]
        public bool IsReadOnly { get => isReadOnly; set => Set(ref isReadOnly, value); }
        private int subdivisionForce;
        public int SubdivisionForce 
        { 
            get => subdivisionForce;
            set 
            {
                if (subdivisionForce != 0)
                {
                    subdivisionForce = value;
                    RecalculateCountEquipmnet();
                }
                subdivisionForce = value;
            }
        }
        public string SubdivisionType { get; set; }
        public int BuildingId { get; set; }
        public int Order { get; set; }
        public virtual BuildingDto Building { get; set; }
        public virtual ICollection<RoomDto> Rooms { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public SubdivisionDto(SubdivisionDto subdivision)
        {
            this.Name = subdivision.Name;
        }
        private void RecalculateCountEquipmnet()
        {
            var eq = roomAndSpacesDbContext.RaSM_Rooms.Select(x => x.Equipments.ToList()).ToList();
            var allEquipmentInSubdivision = roomAndSpacesDbContext.RaSM_Rooms.Select(x => x.Equipments.ToList()).SelectMany(a => a).ToList().Where(x => x.CalcCount != null && x.CalcCount != "");
            foreach (var item in allEquipmentInSubdivision)
            {
                double vla = Convert.ToDouble(new String(item.CalcCount.Where(x => Char.IsDigit(x))?.ToArray()));
                item.Count = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.SubdivisionForce) / vla));
            }
            roomAndSpacesDbContext.SaveChanges();
        }
    }
}