using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure
{
    [Table("RaSM_RoomCategory")]
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        //[NotMapped]
        //public List<SubCategoryDto> subCategoryDtos { get; set; } = new List<SubCategoryDto>();



        public virtual ICollection<SubCategoryDto> SubCategories { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
