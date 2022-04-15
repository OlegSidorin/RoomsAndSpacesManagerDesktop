using Autodesk.Revit.UI;
using RoomsAndSpacesManagerDataBase;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerLib.Dto;
using RoomsAndSpacesManagerLib.Infrastructure;
using RoomsAndSpacesManagerLib.Models.DataBaseModels;
using RoomsAndSpacesManagerLib.Models.RevitModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace RoomsAndSpacesManagerLib.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public ExternalEvent ApplyEventGetRoomFromRvtModel;
        public ExternalEvent ApplyEventAddParametersIntoSpacies;

        public static string roomId;
        public static int rvtToomId;

        ProjectsDbContext projectsDbContext = new ProjectsDbContext();

        List<RoomDto> roomDtos;
        public MainWindowViewModel()
        {
            Projects = projectsDbContext.GetProjects();
            SelectRoomEventHendler.ChangeUI += OnSelectRevitRoomCommandExecutdeEvent;
            AddParametersIntoSpacies.ChangeUI += OpenChangedSpaciesList;
            SelectRevitRoomCommand = new RelayCommand(OnSelectRevitRoomCommandExecutde, CanSelectRevitRoomCommandExecute);
            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecuted, CanPushToDbCommandExecute);
            AddParametersCommand = new RelayCommand(OnAddParametersCommandExecuted, CanAddParametersCommandExecute);
        }

        #region СелектедИнекс ТабКонтрола
        private int selectedIndexTabControl;
        public int SelectedIndexTabControl
        {
            get => selectedIndexTabControl;
            set => Set(ref selectedIndexTabControl, value);
        }
        #endregion

        #region Список проектов. Выбранный проект
        private List<ProjectDto> projects;
        /// <summary> Список проектов. Из БД </summary>
        public List<ProjectDto> Projects
        {
            get => projects;
            set => Set(ref projects, value);
        }
        private ProjectDto selectedProject;

        public ProjectDto SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                if (SelectedProject != null)
                {
                    if (SelectedProject.Buildings != null)
                        Buildings = projectsDbContext.GetModels(SelectedProject);
                }
                else
                {
                    Buildings = new List<BuildingDto>();
                }

            }
        }
        #endregion

        #region Список зданий. Выбранное здание

        private List<BuildingDto> buildings;
        public List<BuildingDto> Buildings
        {
            get => buildings;
            set => Set(ref buildings, value);
        }

        private BuildingDto selectedBuilding;
        public BuildingDto SelectedBuilding
        {
            get { return selectedBuilding; }
            set
            {
                selectedBuilding = value;
                if (SelectedBuilding != null)
                {
                    roomDtos = projectsDbContext.GetRoomsByModels(SelectedBuilding);
                    Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                    Rooms.Refresh();
                }
                else
                    Rooms = null;
            }
        }

        #endregion

        #region Список помещений. Выбранное помещение
        private ICollectionView rooms;
        public ICollectionView Rooms
        {
            get => rooms;
            set => Set(ref rooms, value);
        }
        private static RoomDto selectedRoom;
    

        public static RoomDto SelectedRoom
        {
            get => selectedRoom;
            set
            {
                selectedRoom = value;
            }
        }
        #endregion

        #region Выбрать помещение из ревита
        public ICommand SelectRevitRoomCommand { get; set; }

        private void OnSelectRevitRoomCommandExecutde(object sander)
        {
            SelectRoomEventHendler.DbRommId = (sander as RoomDto).Id;
            ApplyEventGetRoomFromRvtModel.Raise();
        }

        private void OnSelectRevitRoomCommandExecutdeEvent(object sander)
        {
            SelectedRoom.RoomNumber = roomId.ToString();
            SelectedRoom.ArRoomId = rvtToomId;
            Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            Rooms.Refresh();
        }



        private bool CanSelectRevitRoomCommandExecute(object sander) => true;
        #endregion

        #region Пуш в БД
        public ICommand PushToDbCommand { get; set; }

        private void OnPushToDbCommandExecuted(object obj)
        {
            projectsDbContext.SaveChanges();
            MessageBox.Show("Изменения успешно внесены в БД!", "Статус");
        }

        private bool CanPushToDbCommandExecute(object obj) => true;
        #endregion

        #region Комманд. Заполнить параметры пространст
        public ICommand AddParametersCommand { get; set; }

        private void OnAddParametersCommandExecuted(object obj)
        {
            AddParametersIntoSpacies.Rooms = roomDtos;

            ApplyEventAddParametersIntoSpacies.Raise();
        }

        private bool CanAddParametersCommandExecute(object obj) => true;
        #endregion

        #region Список измененных помещений
        public static List<SpaceDto> Spacies { get; set; }
        private void OpenChangedSpaciesList(object obj)
        {
            SelectedIndexTabControl = 1;
            ChangeParametersList = CollectionViewSource.GetDefaultView(Spacies);
            ChangeParametersList.Refresh();
        }

        private ICollectionView changeParametersList;
        public ICollectionView ChangeParametersList { get => changeParametersList; set => Set(ref changeParametersList, value); }

        #endregion
    }
}
