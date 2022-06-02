using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using RoomsAndSpacesManagerDesktop.Views.UserControls;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class RoomsPropertiesViewModel : ViewModel
    {
        private RoomsPropertiesuserControl _thisView;
        public RoomsPropertiesuserControl ThisView
        {
            get { return _thisView; }
            set
            {
                if (_thisView != value)
                {
                    _thisView = value;
                    OnPropertyChanged();
                }
            }
        }

        public static RoomsDbContext roomsContext = new RoomsDbContext();

        public RoomsPropertiesViewModel()
        {
            Categories = roomsContext.GetCategories();

            #region Комманды
            ClearCategoriesComboBoxSelectedItem = new RelayCommand(OnClearCategoriesComboBoxSelectedItem, CanClearCategoriesComboBoxSelectedItem);
            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecutde, CanPushToDbCommandExecute);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecutde, CanAddNewRowCommandExecute);
            DeleteRoomCommand = new RelayCommand(OnDeleteRoomCommandExecutde, CanDeleteRoomCommandExecute);
            GetRoomEquipments = new RelayCommand(OnGetRoomEquipmentsExecutde, CanGetRoomEquipmentsExecute);
            LoadedCommand = new RelayCommand(OnLoadedCommandExecutde, CanLoadedCommandExecute);
            AddNewCategoryCommand= new RelayCommand(OnAddNewCategoryCommandExecutde, CanAddNewCategoryCommandExecute);
            #endregion
        }

        public ICommand ClearCategoriesComboBoxSelectedItem { get; set; }
        private void OnClearCategoriesComboBoxSelectedItem(object p)
        {
            SelectedCategories = null;
            SelectedSubCategories = null;
            //ThisView.cbSubCategories.ItemsSource = null;
            ThisView.cbSubCategories.SelectedItem = null;
            roomNameFiltering = string.Empty;
            ThisView.tbFilter.Text = "";
            RoomsList = roomsContext.GetAllRoomNames();
            Rooms = CollectionViewSource.GetDefaultView(RoomsList);
            Rooms.Filter = delegate (object item)
            {
                RoomNameDto user = item as RoomNameDto;
                if (user != null && user.Name != null && user.Name.ToLower().StartsWith(RoomNameFiltering.ToLower())) return true;
                return false;
            };
            Rooms.Refresh();
        }
        private bool CanClearCategoriesComboBoxSelectedItem(object p) => true;


        /*MainWindow~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Комманд. Рендер окна

        public ICommand LoadedCommand { get; set; }
        private async void OnLoadedCommandExecutde(object obj)
        {
            if (RoomsList == null | RoomsList?.Count == 0)
            {
                RoomsList = roomsContext.GetAllRoomNames();
                Rooms = CollectionViewSource.GetDefaultView(RoomsList);
                Rooms.Filter = delegate (object item)
                {
                    RoomNameDto user = item as RoomNameDto;
                    if (user != null && user.Name != null && user.Name.ToLower().Contains(RoomNameFiltering.ToLower())) return true;
                    return false;
                };
                Rooms.Refresh();
            }
        }

        private bool CanLoadedCommandExecute(object obj) => true;

        #endregion

        /*Верхняя панель. Список категорий и подкатегорий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Combobox - Список категорий
        private List<CategoryDto> categories;
        public List<CategoryDto> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        private CategoryDto selectedCategoties;
        /// <summary>
        /// Выбранная категория помещений
        /// </summary>
        public CategoryDto SelectedCategories
        {
            get { return selectedCategoties; }
            set
            {
                Set(ref selectedCategoties, value);
                if (SelectedCategories != null)
                {
                    SubCategories = roomsContext.GetSubCategotyes(SelectedCategories);
                    RoomsList = new List<RoomNameDto>();
                    Rooms = CollectionViewSource.GetDefaultView(RoomsList);
                    Rooms.Refresh();
                }
            }
        }
        #endregion

        #region Combobox - список подкатегорий

        private List<SubCategoryDto> subCategories;
        public List<SubCategoryDto> SubCategories
        {
            get { return subCategories; }
            set
            {
                Set(ref subCategories, value);
            }
        }


        private SubCategoryDto selectedSubCategoties;
        /// <summary>
        /// Выбранная подкатегория помещений
        /// </summary>
        public SubCategoryDto SelectedSubCategories
        {
            get { return selectedSubCategoties; }
            set
            {
                selectedSubCategoties = value;
                if (SelectedSubCategories != null)
                {
                    RoomsDbContext newroomsDbContext = new RoomsDbContext();
                    RoomsList = newroomsDbContext.GetRoomNames(SelectedSubCategories);
                    Rooms = CollectionViewSource.GetDefaultView(RoomsList);
                    Rooms.Refresh();
                }

            }
        }

        #endregion

        #region Фильтер помещений по имени и ID

        private string roomNameFiltering = string.Empty;

        public string RoomNameFiltering
        {
            get { return roomNameFiltering; }
            set 
            { 
                if (RoomNameFiltering != "")
                {
                    //RoomsList = roomsContext.GetAllRoomNames();
                    Rooms = CollectionViewSource.GetDefaultView(RoomsList);
                    Rooms.Filter = delegate (object item)
                    {
                        RoomNameDto user = item as RoomNameDto;
                        if (user != null && user.Name != null && user.Name.ToLower().Contains(RoomNameFiltering.ToLower())) return true;
                        return false;
                    };
                    Rooms.Refresh();
                }
                roomNameFiltering = value;
                CollectionViewSource.GetDefaultView(RoomsList).Refresh();
            }
        }


        #endregion

        /*Верхняя панель. Новые картеории и подкатегрии~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Новая категория. Комманд на добавление и текстбокс с именем

        private string newCategoryName;

        public string NewCategoryName
        {
            get { return newCategoryName; }
            set { newCategoryName = value; }
        }


        public ICommand AddNewCategoryCommand { get; set; }

        private void OnAddNewCategoryCommandExecutde(object obj)
        {

        }

        private bool CanAddNewCategoryCommandExecute(object obj)
        {
            if (NewCategoryName != null && NewCategoryName != "") return true;
            return false;
        }

        #endregion

        /*Центральная панель. Список комнат~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список комнат

        private List<RoomNameDto> roomsList;
        public List<RoomNameDto> RoomsList
        {
            get { return roomsList; }
            set => Set(ref roomsList, value);
        }

        private ICollectionView rooms;
        public ICollectionView Rooms
        {
            get => rooms;
            set => Set(ref rooms, value);
        }

        #endregion

        /*Нижнаяя панель. Комманды взаимодвествия с БД~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Комманд. Удаление помещения
        public ICommand DeleteRoomCommand { get; set; }

        private void OnDeleteRoomCommandExecutde(object obj)
        {
            if ((obj as RoomNameDto).Id == 0)
            {
                RoomsList.Remove(obj as RoomNameDto);
                Rooms = CollectionViewSource.GetDefaultView(RoomsList);
                Rooms.Refresh();
            }


            else
            {
                roomsContext.RemoveRoom(obj as RoomNameDto);
                RoomsList = roomsContext.GetRoomNames(SelectedSubCategories);
                Rooms = CollectionViewSource.GetDefaultView(RoomsList);
                Rooms.Refresh();
            }
               

            

        }
        private bool CanDeleteRoomCommandExecute(object obj) => true;
        #endregion

        #region Комманд. Добавить строку
        public ICommand AddNewRowCommand { get; set; }

        private void OnAddNewRowCommandExecutde(object obj)
        {
            RoomsList.Add(new RoomNameDto() { SubCategotyId = SelectedSubCategories.Id });
            Rooms = CollectionViewSource.GetDefaultView(RoomsList);
            Rooms.Refresh();
        }

        private bool CanAddNewRowCommandExecute(object obj)
        {
            if (SelectedSubCategories != null) return true;
            else return false;
        }
        #endregion

        #region Комманд. Закинуть обновления пространств в БД
        public ICommand PushToDbCommand { get; set; }
        private void OnPushToDbCommandExecutde(object p)
        {
            roomsContext.SaveChanges();
            roomsContext.AddRooms(RoomsList.Where(x => x.Id == 0).ToList());
            RoomsList = roomsContext.GetRoomNames(SelectedSubCategories);
            Rooms = CollectionViewSource.GetDefaultView(RoomsList);
            Rooms.Refresh();
            MessageBox.Show("Данные успешно загруженны в базу данных", "Статус");
        }
        private bool CanPushToDbCommandExecute(object p) => true;
        #endregion

        #region Статус выполнения
        private string status;
        public string Status
        {
            get => status;
            set => Set(ref status, value);
        }
        #endregion

        #region Комманд. Список оборудования
        public ICommand GetRoomEquipments { get; set; }
        private void OnGetRoomEquipmentsExecutde(object p)
        {
            RoomEquipmentsViewModel.RoomName = p as RoomNameDto;
            RoomEquipmentsWindow roomEquipmentsWindow = new RoomEquipmentsWindow();
            RoomEquipmentsViewModel roomEquipmentsViewModel = new RoomEquipmentsViewModel();
            
            roomEquipmentsWindow.DataContext = roomEquipmentsViewModel;
            roomEquipmentsWindow.ShowDialog();

            Rooms = CollectionViewSource.GetDefaultView(RoomsList);
            Rooms.Refresh();

        } 
        private bool CanGetRoomEquipmentsExecute(object p) => true;
        #endregion
    }
}
