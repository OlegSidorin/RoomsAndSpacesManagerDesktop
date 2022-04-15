using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RoomAndSpacesOV.Dto;
using RoomAndSpacesOV.Infrastructure;
using RoomAndSpacesOV.Models.DbModels;
using RoomAndSpacesOV.Models.RvtModels;
using RoomAndSpacesOV.ViewModels.Base;
using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace RoomAndSpacesOV.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        public static List<SpaceDto> SpaciesList { get; set; }
        public static List<RoomDto> UnfoundedRooms { get; set; }
        public ExternalEvent AddParamtersToModelExEvent { get; set; }
        public MainWindowViewModel()
        {
            Projects = MainDbModel.GetProjects();

            AddParametersCommand = new RelayCommand(OnAddParametersCommandExecuted, CanAddParametersCommandExecure);
            AddParametersToModelEvHend.ChangeUI += ViewChangedSpaciesList;


        }

        #region Список измененых помещений. Метод отображение измененных помезений
        private ICollectionView spacesList;
        public ICollectionView SpacesList
        {
            get => spacesList;
            set => Set(ref spacesList, value);
        }
        private void ViewChangedSpaciesList(object obj)
        {
            SpacesList = CollectionViewSource.GetDefaultView(SpaciesList);
            SpacesList.Refresh();

            if (UnfoundedRooms != null & UnfoundedRooms.Count != 0)
            {
                UnfoundedRoomsList = CollectionViewSource.GetDefaultView(UnfoundedRooms);
                UnfoundedRoomsList.Refresh();
                ListCount = UnfoundedRooms.Count;
            }
        }
        #endregion

        #region Список проектов. Выбранный проект

        public List<ProjectDto> Projects { get; set; }

        private ProjectDto selectedProject;
        public ProjectDto SelectedProject
        {
            get => selectedProject;
            set
            {
                selectedProject = value;
                if (SelectedProject != null)
                    Buildings = MainDbModel.GetBuildingsByProject(SelectedProject);
            }
        }

        #endregion

        #region Список зданий. Выбранное здание
        private List<BuildingDto> buildings;
        public List<BuildingDto> Buildings
        {
            get { return buildings; }
            set
            {
                Set(ref buildings, value);
            }
        }

        private BuildingDto selectedBuilding;

        public BuildingDto SelectedBuilding
        {
            get { return selectedBuilding; }
            set 
            { 
                selectedBuilding = value; 

            }
        }

        #endregion

        #region Комманд. Заполнить параметры

        public ICommand AddParametersCommand { get; set; }

        private void OnAddParametersCommandExecuted(object obj)
        {
            try
            {
                AddParametersToModelEvHend.RoomsDto = MainDbModel.GetRoomsByBuilding(SelectedBuilding);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            AddParamtersToModelExEvent.Raise();
        }
        private bool CanAddParametersCommandExecure(object obj)
        {
            if (SelectedBuilding == null)
                return false;

            return true;
        }
        #endregion


        #region Список отсутсвующиз помещений
        private ICollectionView unfoundedRoomsList;
        public ICollectionView UnfoundedRoomsList { get => unfoundedRoomsList; set => Set( ref unfoundedRoomsList, value); }

        private int listCount;

        public int ListCount
        {
            get { return listCount; }
            set { Set(ref listCount, value); }
        }

        #endregion
    }
}
