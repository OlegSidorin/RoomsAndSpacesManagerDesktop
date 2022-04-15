using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_Buildings")]
    public class BuildingDto : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int ProjectId { get; set; }

        [NotMapped]
        public int? SunnuryArea { get; set; }

        private bool isReadOnly = false;
        [NotMapped]
        public bool IsReadOnly { get => isReadOnly; set => Set(ref isReadOnly, value); }


        public virtual ProjectDto Project { get; set; }

        public virtual ICollection<SubdivisionDto> Subdivisions { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
