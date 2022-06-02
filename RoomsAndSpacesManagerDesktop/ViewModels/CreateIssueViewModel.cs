using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Data.DataBaseContext;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Infrastructure.Repositories;
using RoomsAndSpacesManagerDesktop.Models.CsvModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using RoomsAndSpacesManagerDesktop.Models.ExcelModels;
using RoomsAndSpacesManagerDesktop.Models.SqlModel;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;
using Toolkit = Xceed.Wpf.Toolkit;

namespace RoomsAndSpacesManagerDesktop.ViewModels
{
    class CreateIssueViewModel : ViewModel
    {
        #region филды

        private static Views.UserControls.CreateIssueMainWindow _thisWindow;
        public static Views.UserControls.CreateIssueMainWindow ThisWindow
        {
            get { return _thisWindow; }
            set
            {
                if (_thisWindow != value)
                {
                    _thisWindow = value;
                }
            }
        }

        ProjectsDbContext projContext = new ProjectsDbContext();
        List<RoomDto> roomDtos;
        RoomsDbContext roomsContext = new RoomsDbContext();
        UploadToCsvModel uploadToCsvModel = new UploadToCsvModel();
        List<RoomNameDto> roomsNamesList;

        #endregion

        private string _buttonNameAddNewStrings;
        public string ButtonNameAddNewStrings
        {
            get { return _buttonNameAddNewStrings; }
            set
            {
                if (_buttonNameAddNewStrings != value)
                {
                    _buttonNameAddNewStrings =  value;
                    OnPropertyChanged();
                }
            }
        }

        private string _numberNewStrings;
        public string NumberNewStrings
        {
            get { return _numberNewStrings; }
            set
            {
                if (_numberNewStrings != value)
                {
                    _numberNewStrings = value;
                    if (value.EndsWith("1"))
                        ButtonNameAddNewStrings = $"Добавить {value} строку";
                    else if (value.EndsWith("2") || value.EndsWith("3") || value.EndsWith("4"))
                        ButtonNameAddNewStrings = $"Добавить {value} строки";
                    else 
                        ButtonNameAddNewStrings = $"Добавить {value} строк";
                    if (value == "11" || value == "12" || value == "13" || value == "14")
                        ButtonNameAddNewStrings = $"Добавить {value} строк";
                    OnPropertyChanged();
                }
            }
        }


        public CreateIssueViewModel()
        {
            allRoomNames = roomsContext.GetAllRoomNames();
            Projects = projContext.GetProjects();
            Categories = roomsContext.GetCategories();

            NumberNewStrings = 1.ToString();
            
            #region Команды

            PushToDbCommand = new RelayCommand(OnPushToDbCommandExecuted, CanPushToDbCommandExecute);
            UploadProgramToExcelCommand = new RelayCommand(OnUploadProgramToExcelCommandExecuted, CanUploadProgramToExcelCommandExecute);
            UploadStandartEquipmentToExcelCommand = new RelayCommand(OnUploadStandartEquipmentToExcelCommandExecuted, CanUploadStandartEquipmentToExcelCommandExecute);
            AddNewRowCommand = new RelayCommand(OnAddNewRowCommandExecuted, CanAddNewRowCommandExecute);
            AddNewProjectCommand = new RelayCommand(OnAddNewProjectCommandExecuted, CanAddNewProjectCommandExecute);
            AddNewBuildingCommand = new RelayCommand(OnAddNewBuildingCommandExecuted, CanAddNewBuildingCommandExecute);
            DeleteCommand = new RelayCommand(OnDeleteCommandExecuted, CanDeleteCommandExecute);
            DeleteIssueCommand = new RelayCommand(OnDeleteIssueCommandExecuted, CanDeleteIssueCommandExecute);
            DuplicateRowCommand = new RelayCommand(OnDuplicateRowCommandExecuted, CanDuplicateRowCommandExecute);
            SetDefaultValueCommand = new RelayCommand(OnSetDefaultValueCommandExecuted, CanSetDefaultValueCommandExecute);
            AddNewSubdivisionCommand = new RelayCommand(OnAddNewSubdivisionCommandExecuted, CanAddNewSubdivisionCommandExecute);
            RenderComboboxCommand = new RelayCommand(OnRenderComboboxCommandExecuted, CanRenderComboboxCommandExecute);
            LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);
            CopySubdivisionCommnd = new RelayCommand(OnCopySubdivisionCommndExecuted, CanCopySubdivisionCommndExecute);
            LoadedSummuryCommand = new RelayCommand(OnLoadedSummuryCommandExecuted, CanLoadedSummuryCommandExecute);
            UploadProgramToCsv = new RelayCommand(OnUploadProgramToCsvExecuted, CanUploadProgramToCsvExecute);
            ClearTextboxCommand = new RelayCommand(OnClearTextboxCommandExecuted, CanClearTextboxCommandExecute);
            GetEquipmentCommand = new RelayCommand(OnGetEquipmentCommandExecuted, CanGetEquipmentCommandExecute);
            PushToDbSaveChangesCommand = new RelayCommand(OnPushToDbSaveChangesCommandExecuted, CanPushToDbSaveChangesCommandExecute);
            UploadAllEquipmentToExcelCommand = new RelayCommand(OnUploadAllEquipmentToExcelCommandExecuted, CanUploadAllEquipmentToExcelCommandExecute);
            RenameSubdivisionCommand = new RelayCommand(OnRenameSubdivisionCommandExecuted);
            RenameBuildingCommand = new RelayCommand(OnRenameBuildingCommandExecuted);
            ProjectSettingsCommand = new RelayCommand(OnProjectSettingsCommandExecuted, CanProjectSettingsCommandExecute);
            CopyNameToShortNameCommand = new RelayCommand(OnCopyNameToShortNameCommandExecuted, CanCopyNameToShortNameCommandExecute);

            SetDefaultValue_MinAreaCommand = new RelayCommand(OnSetDefaultValue_MinAreaCommandExecuted, CanSetDefaultValue_MinAreaCommandExecute);

            SetDefaultValue_Class_chistoti_SanPinCommand = new RelayCommand(OnSetDefaultValue_Class_chistoti_SanPinCommandExecuted, CanSetDefaultValue_Class_chistoti_SanPinCommandExecute);
            SetDefaultValue_Class_chistoti_SP_158Command = new RelayCommand(OnSetDefaultValue_Class_chistoti_SP_158CommandExecuted, CanSetDefaultValue_Class_chistoti_SP_158CommandExecute);
            SetDefaultValue_Class_chistoti_GMPCommand = new RelayCommand(OnSetDefaultValue_Class_chistoti_GMPCommandExecuted, CanSetDefaultValue_Class_chistoti_GMPCommandExecute);

            SetDefaultValue_T_calcCommand = new RelayCommand(OnSetDefaultValue_T_calcCommandExecuted, CanSetDefaultValue_T_calcCommandExecute);
            SetDefaultValue_T_minCommand = new RelayCommand(OnSetDefaultValue_T_minCommandExecuted, CanSetDefaultValue_T_minCommandExecute);
            SetDefaultValue_T_maxCommand = new RelayCommand(OnSetDefaultValue_T_maxCommandExecuted, CanSetDefaultValue_T_maxCommandExecute);

            SetDefaultValue_VityazhkaCommand = new RelayCommand(OnSetDefaultValue_VityazhkaCommandExecuted, CanSetDefaultValue_VityazhkaCommandExecute);
            SetDefaultValue_PritokCommand = new RelayCommand(OnSetDefaultValue_PritokCommandExecuted, CanSetDefaultValue_PritokCommandExecute);

            SetDefaultValue_Discription_ARCommand = new RelayCommand(OnSetDefaultValue_Discription_ARCommandExecuted, CanSetDefaultValue_Discription_ARCommandExecute);
            SetDefaultValue_Ot_vlazhnostCommand = new RelayCommand(OnSetDefaultValue_Ot_vlazhnostCommandExecuted, CanSetDefaultValue_Ot_vlazhnostCommandExecute);
            
            SetDefaultValue_Discription_OVCommand = new RelayCommand(OnSetDefaultValue_Discription_OVCommandExecuted, CanSetDefaultValue_Discription_OVCommandExecute);
            SetDefaultValue_Equipment_VKCommand = new RelayCommand(OnSetDefaultValue_Equipment_VKCommandExecuted, CanSetDefaultValue_Equipment_VKCommandExecute);
            SetDefaultValue_Osveshennost_pro_obshem_osvechCommand = new RelayCommand(OnSetDefaultValue_Osveshennost_pro_obshem_osvechCommandExecuted, CanSetDefaultValue_Osveshennost_pro_obshem_osvechCommandExecute);

            SetDefaultValue_Group_el_bezCommand = new RelayCommand(OnSetDefaultValue_Group_el_bezCommandExecuted, CanSetDefaultValue_Group_el_bezCommandExecute);
            SetDefaultValue_Discription_EOMCommand = new RelayCommand(OnSetDefaultValue_Discription_EOMCommandExecuted, CanSetDefaultValue_Discription_EOMCommandExecute);
            SetDefaultValue_Discription_SSCommand = new RelayCommand(OnSetDefaultValue_Discription_SSCommandExecuted, CanSetDefaultValue_Discription_SSCommandExecute);

            SetDefaultValue_Discription_AK_ATHCommand = new RelayCommand(OnSetDefaultValue_Discription_AK_ATHCommandExecuted, CanSetDefaultValue_Discription_AK_ATHCommandExecute);
            SetDefaultValue_Discription_GSVCommand = new RelayCommand(OnSetDefaultValue_Discription_GSVCommandExecuted, CanSetDefaultValue_Discription_GSVCommandExecute);
            SetDefaultValue_Discription_HSCommand = new RelayCommand(OnSetDefaultValue_Discription_HSCommandExecuted, CanSetDefaultValue_Discription_HSCommandExecute);


            #endregion

        }

        /*MainWindow~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Команда рендера окна
        public ICommand LoadedCommand { get; set; }
        private void OnLoadedCommandExecuted(object obj)
        {
            if (SelectedSubdivision != null)
            {
                roomDtos = projContext.GetRooms(SelectedSubdivision);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }


        }

       

        private bool CanLoadedCommandExecute(object obj) => true;
        #endregion

        private void RefreshAndFocusToSelectedItem()
        {
            Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            Rooms.Refresh();
            
            if (ThisWindow.dgRooms.SelectedItem != null)
            {
                ThisWindow.dgRooms.Focus();
                DataGridCellInfo cellInfo = new DataGridCellInfo(ThisWindow.dgRooms.SelectedItem, ThisWindow.dgRooms.Columns[0]);
                ThisWindow.dgRooms.CurrentCell = cellInfo;
                ThisWindow.dgRooms.ScrollIntoView(ThisWindow.dgRooms.SelectedItem);
                ThisWindow.dgRooms.BeginEdit();
            }
            
        }
        private void RefreshAndFocusToSelectedIndex()
        {
            //ThisWindow.dgRooms.Focus();

            int selectIndex = ThisWindow.dgRooms.SelectedIndex;

            void tryfunc(int ind)
            {
                ThisWindow.dgRooms.ScrollIntoView(ThisWindow.dgRooms.Items.GetItemAt(ind));
                ThisWindow.dgRooms.SelectedItem = ThisWindow.dgRooms.Items.GetItemAt(ind);
                ThisWindow.dgRooms.Focus();
            }

            //bool success = false;

            //MessageBox.Show(selectIndex.ToString());
            //DataGridCellInfo cellInfo = new DataGridCellInfo(ThisWindow.dgRooms.SelectedItem, ThisWindow.dgRooms.Columns[0]);
            //ThisWindow.dgRooms.CurrentCell = cellInfo;

            //Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            Rooms.Refresh();

            //Thread.Sleep(msec);
            int i = 0;
            if (selectIndex > 0)
            {
                do
                {
                    try
                    {
                        tryfunc(selectIndex);
                        //success = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        //success = false;
                        Thread.Sleep(100);
                        i += 1;
                        //Toolkit.MessageBox.Show(i.ToString() + ": " + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                while (true);
            }
            

            //try
            //{
            //    if (selectIndex > 0)
            //    {
            //        ThisWindow.dgRooms.ScrollIntoView(ThisWindow.dgRooms.Items.GetItemAt(selectIndex));
            //        ThisWindow.dgRooms.SelectedItem = ThisWindow.dgRooms.Items.GetItemAt(selectIndex);
            //        ThisWindow.dgRooms.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Toolkit.MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

        }
        private async void RefreshAndFocusToLastItem()
        {
            await Task.Run(() =>
            {
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
                Thread.Sleep(1200);
            });

            //MessageBox.Show(ThisWindow.dgRooms.Items.Count.ToString());

            if (ThisWindow.dgRooms.Items.Count > 0)
            {
                if (VisualTreeHelper.GetChild(ThisWindow.dgRooms, 0) is Decorator border)
                {
                    var scroll = border.Child as ScrollViewer;
                    //MessageBox.Show("(scroll != null) " + (scroll != null).ToString());
                    if (scroll != null) scroll.ScrollToEnd();
                }
            }

        }

        /*Создание нового проекта и здания~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Имена новых проекта и здания
        private string newProjectName;
        public string NewProjectName
        {
            get => newProjectName;
            set => Set(ref newProjectName, value);
        }

        private string newBuildingName;
        public string NewBuildingName
        {
            get => newBuildingName;
            set => Set(ref newBuildingName, value);
        }

        private string newSubdivisionName;
        public string NewSubdivisionName
        {
            get => newSubdivisionName;
            set => Set(ref newSubdivisionName, value);
        }

        #endregion

        #region СелектедПроджект для добавления новых проектов
        private ProjectDto selectedProjectForAdd;

        public ProjectDto SelectedProjectForAdd
        {
            get { return selectedProjectForAdd; }
            set
            {
                selectedProjectForAdd = value;
            }
        }
        #endregion

        /*Верхняя панель. Список проектов и зданий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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

                    AllRooms = projContext.GetAllRoomsByProject(SelectedProject).OrderBy(x => x.Subdivision.BuildingId).ToList();
                }
                else
                {
                    Buildings = new List<BuildingDto>();
                }

                OnLoadedSummuryCommandExecuted("");

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
                selectedSubdivision = value;
                if (SelectedSubdivision != null)
                {
                    roomDtos = projContext.GetRooms(SelectedSubdivision);
                    Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                    Rooms.Refresh();
                }
                else
                    Rooms = null;
            }
        }
        #endregion

        #region Комманда. Создать новый проект
        public ICommand AddNewProjectCommand { get; set; }
        private void OnAddNewProjectCommandExecuted(object p)
        {
            projContext.AddNewProjects(new ProjectDto()
            {
                Name = NewProjectName
            });
            Projects = projContext.GetProjects();
            NewProjectName = string.Empty;
            if (Buildings != null && Buildings.Count != 0)
            {
                Buildings.Clear();
                OnPropertyChanged(nameof(Buildings));
            }

        }
        private bool CanAddNewProjectCommandExecute(object p) => true;
        #endregion

        #region Комманда. Создать новую модель
        public ICommand AddNewBuildingCommand { get; set; }
        private void OnAddNewBuildingCommandExecuted(object p)
        {
            if (p != null)
            {
                projContext.AddNewBuilding(new BuildingDto()
                {
                    ProjectId = (p as ProjectDto).Id,
                    Name = NewBuildingName
                });
                Buildings = projContext.GetModels(p as ProjectDto);
            }
            NewBuildingName = string.Empty;

        }
        private bool CanAddNewBuildingCommandExecute(object p)
        {
            if (p != null)
            {
                return true;
            }
            else { return false; }
        }
        #endregion

        #region Комманда. Создать новое подразделение

        public ICommand AddNewSubdivisionCommand { get; set; }
        private void OnAddNewSubdivisionCommandExecuted(object p)
        {
            if (p != null)
            {
                int nextOrder;
                if ((p as BuildingDto).Subdivisions != null && (p as BuildingDto).Subdivisions.Count !=  0)
                    nextOrder = (p as BuildingDto).Subdivisions.Select(x => x.Order).Max() + 1;
                else
                    nextOrder = 1;

                projContext.AddNewSubdivision(new SubdivisionDto()
                {
                    BuildingId = (p as BuildingDto).Id,
                    Name = NewSubdivisionName,
                    Order = nextOrder
                });
                Subdivisions = projContext.GetSubdivisions(p as BuildingDto);
            }
            NewBuildingName = string.Empty;
        }
        private bool CanAddNewSubdivisionCommandExecute(object p)
        {
            if (p != null)
            {
                return true;
            }
            else { return false; }
        }

        #endregion

        #region Комманда удаления проектов и зданий
        public ICommand DeleteCommand { get; set; }
        private void OnDeleteCommandExecuted(object p)
        {
            if (p is ProjectDto)
            {
                projContext.RemoveProject(p as ProjectDto);
                Projects = projContext.GetProjects();
            }
            if (p is BuildingDto)
            {
                projContext.RemoveBuilding(p as BuildingDto);
                Buildings = projContext.GetModels(SelectedProject);
            }
            if (p is SubdivisionDto)
            {
                projContext.RemoveSubDivision(p as SubdivisionDto);
                Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
            }

        }
        private bool CanDeleteCommandExecute(object p) => true;
        #endregion

        #region Комманд. Открыть окно настроек для проектов

        public ICommand ProjectSettingsCommand { get; set; }

        private void OnProjectSettingsCommandExecuted(object obj)
        {
            if (Subdivisions != null)
            {
                ProjectSettingsWindow projectSettingsWindow = new ProjectSettingsWindow();
                ProjectSettingsViewModel projectSettingsViewModel = new ProjectSettingsViewModel(Subdivisions, ref projContext, projectSettingsWindow);
                projectSettingsWindow.DataContext = projectSettingsViewModel;
                projectSettingsWindow.ShowDialog();

                if (SelectedBuilding != null)
                {
                    if (SelectedBuilding.Subdivisions != null)
                        Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
                }
                else
                    Subdivisions = null;
            }
        }

        private bool CanProjectSettingsCommandExecute(object obj) => true;

        #endregion

        #region Комманд. Переименвоание подразделений

        public ICommand RenameSubdivisionCommand { get; set; }
        private void OnRenameSubdivisionCommandExecuted(object obj)
        {
            var renamedSubdivision = obj as SubdivisionDto;
            if (renamedSubdivision.IsReadOnly)
            {
                projContext.SaveChanges();
            }


            if (renamedSubdivision.IsReadOnly)
                renamedSubdivision.IsReadOnly = false;
            else
                renamedSubdivision.IsReadOnly = true;
        }

        #endregion

        #region Комманд. Переименвоание зданий

        public ICommand RenameBuildingCommand { get; set; }
        private void OnRenameBuildingCommandExecuted(object obj)
        {
            var renamedSubdivision = obj as BuildingDto;
            if (renamedSubdivision.IsReadOnly)
            {
                projContext.SaveChanges();
            }


            if (renamedSubdivision.IsReadOnly)
                renamedSubdivision.IsReadOnly = false;
            else
                renamedSubdivision.IsReadOnly = true;
        }

        #endregion

        /*Верхняя панель. Список категорий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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
        public CategoryDto SelectedCategoties
        {
            get { return selectedCategoties; }
            set
            {
                Set(ref selectedCategoties, value);
                SubCategories = roomsContext.GetSubCategotyes(SelectedCategoties);
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
        public SubCategoryDto SelectedSubCategoties
        {
            get { return selectedSubCategoties; }
            set
            {
                selectedSubCategoties = value;



            }
        }

        #endregion

        #region Список исходных помещений

        List<RoomNameDto> allRoomNames { get; set; }

        private ICollectionView roomsNames;
        public ICollectionView RoomsNames
        {
            get => roomsNames;
            set => Set(ref roomsNames, value);
        }

        private RoomNameDto selectedRoomName;

        public RoomNameDto SelectedRoomName
        {
            get { return selectedRoomName; }
            set
            {
                selectedRoomName = value;
                AddRoomInfo();
                EquipmentDbContext equipmentDbContext = new EquipmentDbContext();

                if (SelectedRoom.Id != 0)
                {
                    equipmentDbContext.RemoveAllEquipment(SelectedRoom);
                    equipmentDbContext.CopyRoomNameEquipmentsToRoomIssue(SelectedRoomName, SelectedRoom);

                    //#region Сортировка списка. Чтобы были только первые позиции в списке
                    //List<EquipmentDto> equpmets = equipmentDbContext.GetEquipments(SelectedRoomName)
                    //    .Where(x => x.RoomNameId == SelectedRoomName.Id)
                    //    .Select(x => new EquipmentDto(x) { RoomId = SelectedRoom.Id, Mandatory = x.Mandatory, Currently = false })
                    //    .ToList();
                    //EquipmentRep equipments = new EquipmentRep(equpmets);
                    ////equpmets.Sort((x, y) => x.Number.CompareTo(y.Number));
                    ////int activeNumber = default;
                    ////foreach (EquipmentDto eq in equpmets)
                    ////{
                    ////    if (!activeNumber.Equals(eq.Number))
                    ////    {
                    ////        activeNumber = eq.Number;
                    ////        eq.Mandatory = true; 
                    ////    }
                    ////}
                    //#endregion
                    ////List<EquipmentDto> equipment = equipmentDbContext.GetEquipmentsWithSortItems(SelectedRoomName);
                    //var eee = equipments.GetEquipments();

                    //MessageBox.Show(SelectedRoom.Name + "\n" + SelectedRoomName.Name);

                    //if (string.IsNullOrEmpty(SelectedRoom.ShortName)) SelectedRoom.ShortName = SelectedRoomName.Name;
                    

                }
                //RoomsNames.Refresh();
                selectedRoomName = null;
                ThisWindow.dgRooms.CommitEdit();
                ThisWindow.dgRooms.CancelEdit();
                RefreshAndFocusToSelectedIndex();

            }
        }

        private void AddRoomInfo()
        {
            if (string.IsNullOrEmpty(SelectedRoom.ShortName)) SelectedRoom.ShortName = SelectedRoomName.Name;
            SelectedRoom.RoomNameId = SelectedRoomName.Id;
            SelectedRoom.Min_area = SelectedRoomName.Min_area;
            SelectedRoom.Class_chistoti_GMP = SelectedRoomName.Class_chistoti_GMP;
            SelectedRoom.Class_chistoti_SanPin = SelectedRoomName.Class_chistoti_SanPin;
            SelectedRoom.Class_chistoti_SP_158 = SelectedRoomName.Class_chistoti_SP_158;
            SelectedRoom.T_calc = SelectedRoomName.T_calc;
            SelectedRoom.T_max = SelectedRoomName.T_max;
            SelectedRoom.T_min = SelectedRoomName.T_min;
            SelectedRoom.Pritok = SelectedRoomName.Pritok;
            SelectedRoom.Vityazhka = SelectedRoomName.Vityazhka;
            SelectedRoom.Ot_vlazhnost = SelectedRoomName.Ot_vlazhnost;
            SelectedRoom.KEO_est_osv = SelectedRoomName.KEO_est_osv;
            SelectedRoom.KEO_sovm_osv = SelectedRoomName.KEO_sovm_osv;
            SelectedRoom.Discription_OV = SelectedRoomName.Discription_OV;
            SelectedRoom.Osveshennost_pro_obshem_osvech = SelectedRoomName.Osveshennost_pro_obshem_osvech;
            SelectedRoom.Group_el_bez = SelectedRoomName.Group_el_bez;
            SelectedRoom.Discription_EOM = SelectedRoomName.Discription_EOM;
            SelectedRoom.Discription_AR = SelectedRoomName.Discription_AR;
            SelectedRoom.Equipment_VK = SelectedRoomName.Equipment_VK;
            SelectedRoom.Discription_SS = SelectedRoomName.Discription_SS;
            SelectedRoom.Discription_AK_ATH = SelectedRoomName.Discription_AK_ATH;
            SelectedRoom.Discription_GSV = SelectedRoomName.Discription_GSV;
            SelectedRoom.Discription_HS = SelectedRoomName.Discription_HS;
        }

        #region Флаг разворачивания Comboboxa

        private bool isDropDownOpen;

        public bool IsDropDownOpen
        {
            get { return isDropDownOpen; }
            set
            {
                Set(ref isDropDownOpen, value);
            }
        }

        #endregion


        #endregion

        #region Комманда при отрисовке комбобокса

        private ICommand renderComboboxCommand;
        public ICommand RenderComboboxCommand
        {
            get => renderComboboxCommand;
            set => renderComboboxCommand = value;
        }

        private void OnRenderComboboxCommandExecuted(object p)
        {
            
            if (p != null)
            {
                //MessageBox.Show(p.GetType().ToString());
                //SubCategoryDto subcdto = p as SubCategoryDto;
                //SubCategoryDto subcdto = p as SubCategoryDto;
                //MessageBox.Show(subcdto.Id.ToString() + ":" + subcdto.Name.ToString());
                roomsNamesList = RoomsPropertiesViewModel.roomsContext.GetRoomNames(p as SubCategoryDto);
                RoomsNames = CollectionViewSource.GetDefaultView(roomsNamesList);
                RoomsNames.Refresh();
            }
            else
            {
                RoomsNames = CollectionViewSource.GetDefaultView(allRoomNames);
            }

            if (RoomNameFiltering != "")
            {
                
                IEqualityComparer<string> comparer = StringComparer.InvariantCultureIgnoreCase; //?
                RoomsNames.Filter = delegate (object item)
                {
                    RoomNameDto roomNameDto = item as RoomNameDto;
                    if (roomNameDto != null && roomNameDto.Name != null && roomNameDto.Name.ToLower().Contains(RoomNameFiltering.ToLower())) return true;
                    return false;
                };
                RoomsNames.Refresh();
            }
        }
        private bool CanRenderComboboxCommandExecute(object p) => true;

        #endregion

        #region Строки фильтрации помещений
        private string roomNameFiltering = "";
        public string RoomNameFiltering
        {
            get { return roomNameFiltering; }
            set
            {
                roomNameFiltering = value;
                if (RoomNameFiltering != "")
                {
                    IsDropDownOpen = true;
                }
                else
                {
                    IsDropDownOpen = false;
                }
                CollectionViewSource.GetDefaultView(allRoomNames).Refresh();
            }
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(RoomNameFiltering))
                return true;
            else
                return ((item as RoomNameDto).Name.IndexOf(RoomNameFiltering, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        #endregion

        #region Анлоадед текстбокс

        public ICommand ClearTextboxCommand { get; set; }
        private void OnClearTextboxCommandExecuted(object obj)
        {
            RoomNameFiltering = "";
        }
        private bool CanClearTextboxCommandExecute(object obj) => true;

        #endregion

        /*Центральная панель. Список проектов и зданий~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

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

        #region Филтер. По Id

        //private string idFilter;

        //public string IdFilter
        //{
        //    get { return idFilter; }
        //    set
        //    {
        //        idFilter = value;
        //    }
        //}


        #endregion

        #region Перенос помещения из одного подразделения в другое
        private SubdivisionDto selectedSubdivisionAction;

        public SubdivisionDto SelectedSubdivisionAction
        {
            get { return selectedSubdivisionAction; }
            set
            {
                selectedSubdivisionAction = value;
                if (SelectedSubdivisionAction != null)
                {
                    SelectedRoom.Subdivision = SelectedSubdivisionAction;
                    SelectedRoom.SubdivisionId = SelectedSubdivisionAction.Id;
                    projContext.SaveChanges();

                    roomDtos = projContext.GetRooms(SelectedSubdivision);
                    Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                    Rooms.Refresh();
                    SelectedSubdivisionAction = null;
                }
            }
        }

        #endregion

        #region Комманд. Копирoвание Имени в Kраткое имя
        public ICommand CopyNameToShortNameCommand { get; set; }
        private void OnCopyNameToShortNameCommandExecuted(object p)
        {
            try
            {
                RoomDto roomDto = (RoomDto)p;
                if (roomDto != null)
                {
                    roomDto.ShortName = roomDto.Name;
                    RefreshAndFocusToSelectedIndex();
                    //Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                    //Rooms.Refresh();
                    
                    //ThisWindow.dgRooms.Focus();
                    //DataGridCellInfo cellInfo = new DataGridCellInfo(ThisWindow.dgRooms.SelectedItem, ThisWindow.dgRooms.Columns[0]);
                    //ThisWindow.dgRooms.CurrentCell = cellInfo;
                    //ThisWindow.dgRooms.ScrollIntoView(ThisWindow.dgRooms.SelectedItem);
                    //ThisWindow.dgRooms.BeginEdit();
                    
                }

            }
            catch (Exception ex)
            {
                Toolkit.MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }
        private bool CanCopyNameToShortNameCommandExecute(object p) => true;

        #endregion

        #region Комманд. Удаление строк из списка
        public ICommand DeleteIssueCommand { get; set; }
        private void OnDeleteIssueCommandExecuted(object p)
        {
            if ((p as RoomDto).Id == default)
            {
                roomDtos.Remove(p as RoomDto);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
            else
            {
                projContext.RemoveRoom(p as RoomDto);

                roomDtos = projContext.GetRooms(SelectedSubdivision);
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                Rooms.Refresh();
            }
        }
        private bool CanDeleteIssueCommandExecute(object p) => true;

        #endregion

        #region Комманд. Дублирование строки
        public ICommand DuplicateRowCommand { get; set; }
        private void OnDuplicateRowCommandExecuted(object p)
        {
            if ((p as RoomDto).Id == default) // id == 0?
            {
                //roomDtos.Remove(p as RoomDto);
                //RoomDto roomDto = p as RoomDto;
                //MessageBox.Show(roomDto.Id.ToString());
                //Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                //Rooms.Refresh();
                RefreshAndFocusToLastItem();
            }
            else
            {
                //projContext.RemoveRoom(p as RoomDto);
                
                RoomDto room = p as RoomDto;
                if (SelectedBuilding != null)
                {
                    RoomDto roomDtoNew = new RoomDto();
                    roomDtos = projContext.AddNewRoom(SelectedSubdivision, room, out roomDtoNew);
                }

                //MessageBox.Show(roomDto.Id.ToString());
                //roomDtos = projContext.GetRooms(SelectedSubdivision);
                //Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                //Rooms.Refresh();
                RefreshAndFocusToLastItem();
            }
        }
        private bool CanDuplicateRowCommandExecute(object p) => true;

        #endregion

        #region Комманд. Получить значение по умолчанию по выбранной строке
        public ICommand SetDefaultValueCommand { get; set; }
        private void OnSetDefaultValueCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;

            if (room.Name != null)
            {
                room.RoomNameId = room.RoomName.Id;
                room.Min_area = room.RoomName.Min_area;
                room.Class_chistoti_GMP = room.RoomName.Class_chistoti_GMP;
                room.Class_chistoti_SanPin = room.RoomName.Class_chistoti_SanPin;
                room.Class_chistoti_SP_158 = room.RoomName.Class_chistoti_SP_158;
                room.T_calc = room.RoomName.T_calc;
                room.T_max = room.RoomName.T_max;
                room.T_min = room.RoomName.T_min;
                room.Pritok = room.RoomName.Pritok;
                room.Vityazhka = room.RoomName.Vityazhka;
                room.Discription_AR = room.RoomName.Discription_AR;
                room.Ot_vlazhnost = room.RoomName.Ot_vlazhnost;
                //room.KEO_est_osv = room.RoomName.KEO_est_osv;
                //room.KEO_sovm_osv = room.RoomName.KEO_sovm_osv;
                room.Discription_OV = room.RoomName.Discription_OV;
                room.Osveshennost_pro_obshem_osvech = room.RoomName.Osveshennost_pro_obshem_osvech;
                room.Group_el_bez = room.RoomName.Group_el_bez;
                room.Discription_EOM = room.RoomName.Discription_EOM; 
                room.Equipment_VK = room.RoomName.Equipment_VK;
                room.Discription_SS = room.RoomName.Discription_SS;
                room.Discription_AK_ATH = room.RoomName.Discription_AK_ATH;
                room.Discription_GSV = room.RoomName.Discription_GSV;
                room.Discription_HS = room.RoomName.Discription_HS;

                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValueCommandExecute(object p) => true;

        public ICommand SetDefaultValue_MinAreaCommand { get; set; }
        private void OnSetDefaultValue_MinAreaCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Min_area = room.RoomName.Min_area;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_MinAreaCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Class_chistoti_SanPinCommand { get; set; }
        private void OnSetDefaultValue_Class_chistoti_SanPinCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Class_chistoti_SanPin = room.RoomName.Class_chistoti_SanPin;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Class_chistoti_SanPinCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Class_chistoti_SP_158Command { get; set; }
        private void OnSetDefaultValue_Class_chistoti_SP_158CommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Class_chistoti_SP_158 = room.RoomName.Class_chistoti_SP_158;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Class_chistoti_SP_158CommandExecute(object p) => true;

        public ICommand SetDefaultValue_Class_chistoti_GMPCommand { get; set; }
        private void OnSetDefaultValue_Class_chistoti_GMPCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Class_chistoti_GMP = room.RoomName.Class_chistoti_GMP;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Class_chistoti_GMPCommandExecute(object p) => true;

        public ICommand SetDefaultValue_T_calcCommand { get; set; }
        private void OnSetDefaultValue_T_calcCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.T_calc = room.RoomName.T_calc;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_T_calcCommandExecute(object p) => true;

        public ICommand SetDefaultValue_T_minCommand { get; set; }
        private void OnSetDefaultValue_T_minCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.T_min = room.RoomName.T_min;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_T_minCommandExecute(object p) => true;

        public ICommand SetDefaultValue_T_maxCommand { get; set; }
        private void OnSetDefaultValue_T_maxCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.T_max = room.RoomName.T_max;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_T_maxCommandExecute(object p) => true;

        public ICommand SetDefaultValue_VityazhkaCommand { get; set; }
        private void OnSetDefaultValue_VityazhkaCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Vityazhka = room.RoomName.Vityazhka;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_VityazhkaCommandExecute(object p) => true;

        public ICommand SetDefaultValue_PritokCommand { get; set; }
        private void OnSetDefaultValue_PritokCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Pritok = room.RoomName.Pritok;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_PritokCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Discription_ARCommand { get; set; }
        private void OnSetDefaultValue_Discription_ARCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Discription_AR = room.RoomName.Discription_AR;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Discription_ARCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Ot_vlazhnostCommand { get; set; }
        private void OnSetDefaultValue_Ot_vlazhnostCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Ot_vlazhnost = room.RoomName.Ot_vlazhnost;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Ot_vlazhnostCommandExecute(object p) => true;


        public ICommand SetDefaultValue_Discription_OVCommand { get; set; }
        private void OnSetDefaultValue_Discription_OVCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Discription_OV = room.RoomName.Discription_OV;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Discription_OVCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Equipment_VKCommand { get; set; }
        private void OnSetDefaultValue_Equipment_VKCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Equipment_VK = room.RoomName.Equipment_VK;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Equipment_VKCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Osveshennost_pro_obshem_osvechCommand { get; set; }
        private void OnSetDefaultValue_Osveshennost_pro_obshem_osvechCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Osveshennost_pro_obshem_osvech = room.RoomName.Osveshennost_pro_obshem_osvech;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Osveshennost_pro_obshem_osvechCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Group_el_bezCommand { get; set; }
        private void OnSetDefaultValue_Group_el_bezCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Group_el_bez = room.RoomName.Group_el_bez;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Group_el_bezCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Discription_EOMCommand { get; set; }
        private void OnSetDefaultValue_Discription_EOMCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Discription_EOM = room.RoomName.Discription_EOM;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Discription_EOMCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Discription_SSCommand { get; set; }
        private void OnSetDefaultValue_Discription_SSCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Discription_SS = room.RoomName.Discription_SS;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Discription_SSCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Discription_AK_ATHCommand { get; set; }
        private void OnSetDefaultValue_Discription_AK_ATHCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Discription_AK_ATH = room.RoomName.Discription_AK_ATH;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Discription_AK_ATHCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Discription_GSVCommand { get; set; }
        private void OnSetDefaultValue_Discription_GSVCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Discription_GSV = room.RoomName.Discription_GSV;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Discription_GSVCommandExecute(object p) => true;

        public ICommand SetDefaultValue_Discription_HSCommand { get; set; }
        private void OnSetDefaultValue_Discription_HSCommandExecuted(object p)
        {

            RoomDto room = p as RoomDto;
            if (room.Name != null)
            {
                room.Discription_HS = room.RoomName.Discription_HS;
                Rooms = CollectionViewSource.GetDefaultView(roomDtos);
            }


            //Rooms.Refresh();
        }
        private bool CanSetDefaultValue_Discription_HSCommandExecute(object p) => true;


        #endregion

        #region Комманд. Открыть окно с оборудованием

        public ICommand GetEquipmentCommand { get; set; }

        private void OnGetEquipmentCommandExecuted(object p)
        {
            EquipmentsViewModel.Room = SelectedRoom;

            EquipmentsWindow equipmentsWindow = new EquipmentsWindow();
            EquipmentsViewModel vm = new EquipmentsViewModel();
            equipmentsWindow.DataContext = vm;

            equipmentsWindow.ShowDialog();
        }
        private bool CanGetEquipmentCommandExecute(object p) => true;

        #endregion

        /*Нижняя панель. Интерфейс добавления строки и синхронизации с БД~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Добавить новую строку комнаты с Айдишником здания
        public ICommand AddNewRowCommand { get; set; }
        /// <summary>
        /// Метод. Добавть новую строку
        /// </summary>
        /// <param name="p"></param>
        private void OnAddNewRowCommandExecuted(object p)
        {
            var success = int.TryParse(NumberNewStrings, out int num);
            //MessageBox.Show(NumberNewStrings.ToString());
            if (success)
            {
                for (int i = 1; i <= num; i++)
                {
                    if (SelectedBuilding != null)
                    {
                        //roomDtos.Add(new RoomDto()
                        //{
                        //    SubdivisionId = SelectedSubdivision.Id
                        //});
                        roomDtos = projContext.AddNewRoom(SelectedSubdivision);
                        //Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                        //Rooms.Refresh();
                        
                    }
                }
                RefreshAndFocusToLastItem();
            }
            
        }
        private bool CanAddNewRowCommandExecute(object p)
        {
            if (SelectedSubdivision == null) return false;
            else return true;
        }
        #endregion

        #region Копировать подразделение из другого здания
        public ICommand CopySubdivisionCommnd { get; set; }



        public static List<SubdivisionDto> CopySubdivisionList { get; set; }
        /// <summary>
        /// Метод. Добавть новую строку
        /// </summary>
        /// <param name="p"></param>
        private void OnCopySubdivisionCommndExecuted(object p)
        {
            CopySubDivisionViewModel.projContext = projContext;
            CopySubDivisionViewModel.selectedBuildingId = SelectedBuilding.Id;
            CopySubDivisionViewModel.Building = SelectedBuilding;
            CopySubdivisionWindow copySubdivisionWindow = new CopySubdivisionWindow();
            CopySubDivisionViewModel copySubDivisionViewModel = new CopySubDivisionViewModel();
            copySubDivisionViewModel.copySubdivisionWindow = copySubdivisionWindow;
            copySubdivisionWindow.DataContext = copySubDivisionViewModel;
            copySubdivisionWindow.ShowDialog();

            Subdivisions = projContext.GetSubdivisions(SelectedBuilding);
        }
        private bool CanCopySubdivisionCommndExecute(object p)
        {
            if (SelectedBuilding == null) return false;

            return true;
        }
        #endregion

        #region Комманд. Закинуть обновления пространств в БД
        public ICommand PushToDbCommand { get; set; }
        private void OnPushToDbCommandExecuted(object p)
        {
            if (roomDtos != null)
            {
                projContext.AddNewRooms(roomDtos);
                projContext.SaveChanges();
                roomDtos = projContext.GetRooms(SelectedSubdivision);
                //Rooms = CollectionViewSource.GetDefaultView(roomDtos);
                //Rooms.Refresh();
                RefreshAndFocusToSelectedIndex();
                Toolkit.MessageBox.Show("Данные успешно загруженны в базу данных", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                //MessageBox.Show("Данные успешно загруженны в базу данных", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                ThisWindow.dgRooms.Focus();
            }
            else
            {
                Toolkit.MessageBox.Show("Ошибка! Нет выбранных помещений", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
                //MessageBox.Show("Ошибка! Нет выбранных помещений", "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private bool CanPushToDbCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Комманд. Выгрузить программу в эксель
        public ICommand UploadProgramToExcelCommand { get; set; }
        private void OnUploadProgramToExcelCommandExecuted(object p)
        {
            MainExcelModel.UploadProgramToExcel(projContext.GetRooms(SelectedSubdivision));
        }
        private bool CanUploadProgramToExcelCommandExecute(object p)
        {
            if (SelectedSubdivision != null) return true;
            return false;
        }
        #endregion

        #region Комманд. Выгрузить стандарт список оборудвания в эксель

        public ICommand UploadStandartEquipmentToExcelCommand { get; set; }
        private void OnUploadStandartEquipmentToExcelCommandExecuted(object p)
        {
            //SqlMainModel.GetEqupmentByProjects(SelectedProject);
            SqlMainModel.GetStandartEquipmnetByProject(SelectedProject);
        }
        private bool CanUploadStandartEquipmentToExcelCommandExecute(object p)
        {
            if (SelectedProject != null) return true;
            return false;
        }

        #endregion

        #region Комманд. Выгрузить список оборудования целиком в эксель

        public ICommand UploadAllEquipmentToExcelCommand { get; set; }
        private void OnUploadAllEquipmentToExcelCommandExecuted(object obj)
        {
            SqlMainModel.GetAllEquipmnetByProject(SelectedProject);
        }
        private bool CanUploadAllEquipmentToExcelCommandExecute(object obj)
        {
            if (SelectedProject != null) return true;

            return false;
        }

        #endregion

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /*ViewModel для таблицы "Программа помещений"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список помещений для целого проекта с сортировкой. Помещения получаются при выборе проекта. 

        private List<RoomDto> allRooms;

        public List<RoomDto> AllRooms
        {
            get { return allRooms; }
            set { Set(ref allRooms, value); }
        }

        #endregion

        #region Комманд. Сохранить изменения в номерах помещений

        public ICommand PushToDbSaveChangesCommand { get; set; }

        private void OnPushToDbSaveChangesCommandExecuted(object obj)
        {
            projContext.SaveChanges();
        }
        private bool CanPushToDbSaveChangesCommandExecute(object obj)
        {
            if (AllRooms != null && AllRooms.Count != 0) return true;
            else return false;
        }

        #endregion

        /*Таблица "Сводная"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        #region Список всех помещений для проекта

        private List<BuildingDto> _summury;
        public List<BuildingDto> _Summury
        {
            get => _summury;
            set => _summury = value;
        }

        private ICollectionView summury;
        public ICollectionView Summury
        {
            get => summury;
            set => Set(ref summury, value);
        }

        #endregion

        #region Комманд. Загрузка окна Summury
        private List<BuildingDto> buildList;
        public ICommand LoadedSummuryCommand { get; set; }
        private void OnLoadedSummuryCommandExecuted(object obj)
        {
            int? sss = 0;
            if (SelectedProject != null)
            {
                buildList = projContext.GetModels(SelectedProject);
                foreach (var build in buildList)
                {
                    int? summAreaBuild = 0;
                    foreach (var subDiv in build.Subdivisions)
                    {
                        int? summAreaSubdiv = 0;
                        foreach (var room in subDiv.Rooms)
                        {
                            if (room.Min_area != null)
                            {
                                int i;
                                int.TryParse(room.Min_area, out i);
                                summAreaSubdiv += i;
                            }

                        }
                        subDiv.SunnuryArea = summAreaSubdiv;
                        summAreaBuild += summAreaSubdiv;
                    }
                    build.SunnuryArea = summAreaBuild;
                    sss += summAreaBuild;
                }
                Summury = CollectionViewSource.GetDefaultView(buildList);
                Summury.Refresh();
                SummuryArea = sss;
            }
        }
        private bool CanLoadedSummuryCommandExecute(object obj) => false;

        #endregion

        #region Итоговая площадь
        private int? summuryArea;
        public int? SummuryArea
        {
            get { return summuryArea; }
            set { Set(ref summuryArea, value); }
        }
        #endregion

        #region Комманд. Выгрузка в Excel

        public ICommand UploadProgramToCsv { get; set; }
        private void OnUploadProgramToCsvExecuted(object obj)
        {
            try
            {
                UploadToCsvModel UploadToCsvModel = new UploadToCsvModel(SelectedProject, buildList, Koef);
                UploadToCsvModel.UploadToExcel();
                //uploadToCsvModel.UploadRoomProgramToExcel(SelectedProject);
                //uploadToCsvModel.UploadRoomSummaryToExcel(buildList);
                Toolkit.MessageBox.Show("Выгрузка завершена", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Toolkit.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private bool CanUploadProgramToCsvExecute(object obj) => true;

        #endregion

        #region Коэффициент умножения площади

        private double koef = 2.5;

        public double Koef
        {
            get { return koef; }
            set { koef = value; }
        }


        #endregion

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
    }
}