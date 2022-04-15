using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure
{
    [Table("RaSM_RoomEquipments")]
    public class RoomEquipmentDto
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string ClassificationCode { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Mandatory { get; set; }
        public string Discription { get; set; }

        public string CalcCount { get; set; }

        public int RoomNameId { get; set; }

        public virtual RoomNameDto RoomName { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
