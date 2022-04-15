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
    class RoomEquipmentsViewModel : ViewModel
    {
        public static RoomNameDto RoomName { get; set; }

        EquipmentDbContext context = new EquipmentDbContext();

        MainDbContext roomContext = new MainDbContext();
        public RoomEquipmentsViewModel()
        {
            if (RoomName != null)
            {
                RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetEquipments(RoomName));
                RoomEquipmentsList.Refresh();
            }
            else
            {
                RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetAllEquipments());
                RoomEquipmentsList.Refresh();
            }
            
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecuted, CanAddNewRowCommandExecute);
            SaveChangesCommand = new RelayCommand(OnSaveChangesCommandExecuted, CanSaveChangesCommandExecute);
            ChangeDataFromExcelCommand = new RelayCommand(OnChangeDataFromExcelCommandExecuted, CanChangeDataFromExcelCommandExecute);
            DeleteEquipmentCommand = new RelayCommand(OnDeleteEquipmentCommandExecuted, CanDeleteEquipmentCommandExecute);
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
            context.AddNewEquipment(RoomName);
            RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetEquipments(RoomName));
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

        #region Комманд. Заменить данные на данные из Excel

        public ICommand ChangeDataFromExcelCommand { get; set; }
        private void OnChangeDataFromExcelCommandExecuted(object obj)
        {

            MainExcelModel mainExcelModel = new MainExcelModel();
            mainExcelModel.AddToDbFromExcelEqupment(RoomName);

            RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetEquipments(RoomName));
            RoomEquipmentsList.Refresh();

        }
        private bool CanChangeDataFromExcelCommandExecute(object obj) => true;

        #endregion

        #region Комманд. Удалить строку оборудования

        public ICommand DeleteEquipmentCommand { get; set; }

        private void OnDeleteEquipmentCommandExecuted(object obj)
        {
            context.RemoveEquipment(obj as RoomEquipmentDto);

            RoomEquipmentsList = CollectionViewSource.GetDefaultView(context.GetEquipments(RoomName));
            RoomEquipmentsList.Refresh();
        }
        private bool CanDeleteEquipmentCommandExecute(object obj) => true;


        #endregion

    }
}
