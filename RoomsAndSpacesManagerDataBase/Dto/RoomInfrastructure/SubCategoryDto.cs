using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure
{
    [Table("RaSM_SubRoomCategory")]
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public int CategotyId { get; set; }

        public virtual CategoryDto Category { get; set; }

        public virtual ICollection<RoomNameDto> RoomNames { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
