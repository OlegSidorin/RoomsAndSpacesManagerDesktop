using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using RoomsAndSpacesManagerDesktop.Models.ExcelModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class EquipmentsViewModel : ViewModel
    {
        public static RoomDto Room { get; set; }

        EquipmentDbContext context = new EquipmentDbContext();
        RoomsDbContext roomsContext = new RoomsDbContext();
        List<EquipmentDto> equipmentsList { get; set; }

        public EquipmentsViewModel()
        {
            equipmentsList = context.GetEquipments(Room);
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(equipmentsList);
            RoomEquipmentsList.Refresh();

            #region Комманды
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecuted, CanAddNewRowCommandExecute);
            SaveChangesCommand = new RelayCommand(OnSaveChangesCommandExecuted, CanSaveChangesCommandExecute);
            ClickCheckboxCommand = new RelayCommand(OnClickCheckboxCommandExecuted, CanClickCheckboxCommandExecute);
            DeleteEquipmentCommand = new RelayCommand(OnDeleteEquipmentCommandExecuted, CanDeleteEquipmentCommandExecute);
            SetDefaultEquipmentsCommand = new RelayCommand(OnSetDefaultEquipmentsCommandExecuted, CanSetDefaultEquipmentsCommandExecute);
            #endregion
        }

        #region КоллекшенВью для списка оборудования
        private ICollectionView roomEquipmentsList;
        public ICollectionView RoomEquipmentsList
        {
            get => roomEquipmentsList;
            set => Set(ref roomEquipmentsList, value);
        }
        #endregion

        #region Комманд. Добавить новую строку
        public ICommand AddNewRowCommand { get; set; }
        private void OnAddNewRowCommandExecuted(object obj)
        {
            context.AddNewEquipment(Room);
            equipmentsList = context.GetEquipments(Room);
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(equipmentsList);
            RoomEquipmentsList.Refresh();
        }
        private bool CanAddNewRowCommandExecute(object obj) => true;
        #endregion

        #region Комманд. Сохранить изменения
        public ICommand SaveChangesCommand { get; set; }
        private void OnSaveChangesCommandExecuted(object obj)
        {
            context.SaveChanges();
        }
        private bool CanSaveChangesCommandExecute(object obj) => true;
        #endregion

        #region Комманд. Удалить строку оборудования

        public ICommand DeleteEquipmentCommand { get; set; }

        private void OnDeleteEquipmentCommandExecuted(object obj)
        {
            context.RemoveEquipment(obj as EquipmentDto);
            equipmentsList = context.GetEquipments(Room);
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(equipmentsList);
            RoomEquipmentsList.Refresh();
        }
        private bool CanDeleteEquipmentCommandExecute(object obj) => true;
        #endregion

        #region Комманд. Обработчик нажайтий на Чекбокс внутри списка оборудования.
        public ICommand ClickCheckboxCommand { get; set; }

        private void OnClickCheckboxCommandExecuted(object obj)
        {
            EquipmentDto equipmentDto = obj as EquipmentDto;
            if (!equipmentDto.Currently)
            {
                if (equipmentsList.Where(x => x.Number == equipmentDto.Number).Count() > 1)
                {
                    if (equipmentsList.Where(x => x.Number == equipmentDto.Number).Where(y => y.Currently == true).Count() == 0)
                    {
                        equipmentDto.Currently = true;
                    }
                }
                else
                {
                    equipmentDto.Currently = true;
                }
            }
        }
        private bool CanClickCheckboxCommandExecute(object obj) => true;
        #endregion

        #region Комманд. Сделать список оборудование по умолчанию

        public ICommand SetDefaultEquipmentsCommand { get; set; }

        private void OnSetDefaultEquipmentsCommandExecuted(object obj)
        {
            context.RemoveAllEquipment(Room);
            equipmentsList = context.AddEquipmentsByRoomNameId(Room);
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(equipmentsList);
            RoomEquipmentsList.Refresh();
        }
        private bool CanSetDefaultEquipmentsCommandExecute(object obj)
        {
            return true;
        }

        #endregion

    }
}
