using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_Projects")]
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BuildingDto> Buildings { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
