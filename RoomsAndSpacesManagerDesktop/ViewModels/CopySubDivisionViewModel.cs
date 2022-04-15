using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Infrastructure.Repositories;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class CopySubDivisionViewModel : ViewModel
    {
        public CopySubdivisionWindow copySubdivisionWindow;
        public static ProjectsDbContext projContext;
        EquipmentDbContext equipmentDbContext = new EquipmentDbContext();
        public static int selectedBuildingId;
        public static BuildingDto Building;
        public CopySubDivisionViewModel()
        {
            Projects = projContext.GetProjects();
            CopySubdivisionCommand = new RelayCommand(OnCopySubdivisionCommandExecuted, CanCopySubdivisionCommandExecute);
        }

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
                        Buildings = projContext.GetModels(SelectedProject);
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
        /// <summary> Список проектов. Из БД </summary>
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
                    if (SelectedBuilding.Subdivisions != null)
                        Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
                }
                else
                    Subdivisions = null;
            }
        }

        #endregion

        #region Список подразделений. Выбранное подразделение

        private List<SubdivisionDto> subdivisions;
        /// <summary> Список проектов. Из БД </summary>
        public List<SubdivisionDto> Subdivisions
        {
            get => subdivisions;
            set => Set(ref subdivisions, value);
        }

        private SubdivisionDto selectedSubdivision;

        public SubdivisionDto SelectedSubdivision
        {
            get { return selectedSubdivision; }
            set
            {
                Set(ref selectedSubdivision, value);
            }
        }

        #endregion

        #region Комманд. Копирование

        public ICommand CopySubdivisionCommand { get; set; }
        private void OnCopySubdivisionCommandExecuted(object obj)
        {
            EquipmentDbContext eqContext = new EquipmentDbContext();
            List<SubdivisionDto> newSubDivivsions = Subdivisions.Where(x => x.IsChecked).Select(x => new SubdivisionDto(x) { BuildingId = selectedBuildingId }).ToList();
            Subdivisions.Where(x => x.IsChecked).Select(x => projContext.GetRooms(x)).ToList();

            int nextOrder;
            if (Building.Subdivisions != null && Building.Subdivisions.Count != 0)
                nextOrder = Building.Subdivisions.Select(x => x.Order).Max() + 1;
            else
                nextOrder = 1;

            foreach (SubdivisionDto subdivision in Subdivisions.Where(x => x.IsChecked))
            {
                SubdivisionDto newSubdivision = new SubdivisionDto(subdivision) { BuildingId = selectedBuildingId, Order = nextOrder };
                nextOrder++;

                projContext.AddNewSubdivision(newSubdivision);

                List<RoomDto> newRooms = new List<RoomDto>();

                foreach (RoomDto room in projContext.GetRooms(subdivision))
                {
                    var newRom = new RoomDto(room) { SubdivisionId = newSubdivision.Id };
                    projContext.AddNewRoom(newRom);

                    if (room.Equipments != null & room.Equipments.Count != 0)
                        eqContext.CopyEquipmentBetweenRoomIssue(room, newRom);

                    //foreach (var currnetEquipment in room.Equipments)
                    //{
                    //    equipmentDbContext.AddNewEquipment(new EquipmentDto(currnetEquipment, newRom.Id));
                    //}
                    //var newEq = equipmentDbContext.GetEquipments(newRom);
                    //EquipmentRep equipment = new EquipmentRep(newEq);

                }
                projContext.AddNewRooms(newRooms);

            }
            copySubdivisionWindow.Close();
        }

        private bool CanCopySubdivisionCommandExecute(object obj) => true;

        #endregion
    }
}