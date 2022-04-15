using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerLib.Dto
{
    class SpaceDto
    {
        public string RoomNumber { get; set; }
        public string Name { get; set; }
        public List<ParameterDto> parameters { get; set; } = new List<ParameterDto>();
    }
}
