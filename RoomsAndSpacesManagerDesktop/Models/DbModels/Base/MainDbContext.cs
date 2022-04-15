using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;

namespace RoomsAndSpacesManagerDesktop.Models.DbModels.Base
{
    public class MainDbContext
    {
        public RoomAndSpacesDbContext context;
        public MainDbContext()
        {
            context = new RoomAndSpacesDbContext();
        }
    }
}
