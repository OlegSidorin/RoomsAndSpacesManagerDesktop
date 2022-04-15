using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    public class RoomsDbContext : MainDbContext
    {
        #region Добавление в БД
        /// <summary>
        /// Добавить категории в БД (не актуален)
        /// </summary>
        /// <param name="subCategoryDtos"></param>
        public void AddCategoties(List<CategoryDto> subCategoryDtos)
        {
            context.RaSM_RoomCategories.AddRange(subCategoryDtos);
            context.SaveChanges();
        }

        /// <summary>
        /// Добавить подкатегории в БД (не актуален)
        /// </summary>
        /// <param name="subCategoryDtos"></param>
        public void AddSubCategoties(List<SubCategoryDto> subCategoryDtos)
        {
            context.RaSM_RoomSubCategories.AddRange(subCategoryDtos);
            context.SaveChanges();
        }

        /// <summary>
        /// Добавить имена комнат в БД (не актуален)
        /// </summary>
        /// <param name="subCategoryDtos"></param>
        public void AddRooms(List<RoomNameDto> roomNameDtos)
        {
            context.RaSM_RoomNames.AddRange(roomNameDtos);
            context.SaveChanges();
        }

        /// <summary>
        /// Удалитить комнаты из исходжного списка помещений
        /// </summary>
        /// <param name="roomNameDto"></param>
        public void RemoveRoom(RoomNameDto roomNameDto)
        {
            context.RaSM_RoomNames.Remove(context.RaSM_RoomNames.FirstOrDefault(x => x.Id == roomNameDto.Id));
            context.SaveChanges();
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }
        #endregion

        #region Получение из БД
        /// <summary>
        /// Получить список категорий
        /// </summary>
        /// <returns></returns>
        public List<CategoryDto> GetCategories()
        {
            return context.RaSM_RoomCategories.ToList();
        }

        /// <summary>
        /// Получить список подкатегорий
        /// </summary>
        /// <returns></returns>
        public List<SubCategoryDto> GetSubCategotyes(CategoryDto cat)
        {
            return context.RaSM_RoomSubCategories.Where(x => x.CategotyId == cat.Id).ToList();
        }

        /// <summary>
        /// Получить список имен комнат по подкатегрии
        /// </summary>
        /// <returns></returns>
        public List<RoomNameDto> GetRoomNames(SubCategoryDto subCat)
        {
            return context.RaSM_RoomNames.Where(x => x.SubCategotyId == subCat.Id).ToList();
        }


        /// <summary>
        /// Получить весь список помещений асинхронно
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoomNameDto>> GetAllRoomNamesAsync()
        {
            return await Task.Run(() => context.RaSM_RoomNames.ToList());
        }

        /// <summary>
        /// Получить весь список помещений
        /// </summary>
        /// <returns></returns>
        public List<RoomNameDto> GetAllRoomNames()
        {
            return context.RaSM_RoomNames.ToList();
        }

        /// <summary>
        /// Получить список помещений по категории
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<RoomNameDto> GetAllRoomNamesByCategoty(CategoryDto category)
        {
            return context.RaSM_RoomNames.ToList();
        }

        #endregion
    }
}
