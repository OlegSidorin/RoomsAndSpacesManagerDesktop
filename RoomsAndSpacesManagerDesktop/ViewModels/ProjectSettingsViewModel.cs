using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDesktop.Infrastructure.Commands;
using RoomsAndSpacesManagerDesktop.Models.DbModels;
using RoomsAndSpacesManagerDesktop.ViewModels.Base;
using RoomsAndSpacesManagerDesktop.Views.Windows;
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
    class ProjectSettingsViewModel : ViewModel
    {
        List<SubdivisionDto> subdivisionsList = new List<SubdivisionDto>();
        ProjectsDbContext context;
        ProjectSettingsWindow window;

        public ProjectSettingsViewModel()
        {
            
        }

        public ProjectSettingsViewModel(List<SubdivisionDto> _subdivisionsList, ref ProjectsDbContext _context, ProjectSettingsWindow _window)
        {
            SubdivisionTypes = new List<string>() 
            {
                 "Палатное", "Оперблок", "РИТ", "ДС", "Амбулаторное", "Иное"
            };

            ApplyChangesCommand = new RelayCommand(OnApplyChangesCommandExecuted, CanApplyChangesCommandExecute);
            UpCommand = new RelayCommand(OnUpCommandExecuted, CanUpCommandExecute);
            DownCommand = new RelayCommand(OnDownCommandExecuted, CanDownCommandExecute);
            subdivisionsList = _subdivisionsList;
            context = _context;
            window = _window;
            Subdivisions = CollectionViewSource.GetDefaultView(subdivisionsList);
            Subdivisions.Refresh();
        }

        #region КоллекшенВью. Список подразделений

        private ICollectionView subdivisions;

        public ICollectionView Subdivisions
        {
            get { return subdivisions; }
            set { Set(ref subdivisions, value); }
        }

        private SubdivisionDto selectedSubdivision;

        public SubdivisionDto SelectedSubdivision
        {
            get { return selectedSubdivision; }
            set { Set(ref selectedSubdivision, value); }
        }

        #endregion

        #region Комманд. Сохранить изменения

        public ICommand ApplyChangesCommand { get; set; }
        private void OnApplyChangesCommandExecuted(object obj)
        {
            context.SaveChanges();
            window.Close();
        }
        private bool CanApplyChangesCommandExecute(object obj) => true;

        #endregion

        #region Комманд. Переместить выбранное подразделение вверх

        public ICommand UpCommand { get; set; }

        private void OnUpCommandExecuted(object obj)
        {
            var subDiv = obj as SubdivisionDto;
            if (subDiv.Order > 1)
            {
                subdivisionsList.FirstOrDefault(x => x.Order == subDiv.Order - 1).Order = subDiv.Order;
                subDiv.Order = subDiv.Order - 1;
                context.SaveChanges();
                var sortList = subdivisionsList.OrderBy(x => x.Order).ToList();
                Subdivisions = CollectionViewSource.GetDefaultView(sortList);
                Subdivisions.Refresh();

            }

        }
        private bool CanUpCommandExecute(object obj) => true;

        #endregion

        #region Комманд. Переместить выбранное подразделение вниз

        public ICommand DownCommand { get; set; }

        private void OnDownCommandExecuted(object obj)
        {
            var subDiv = obj as SubdivisionDto;

            if (subDiv.Order < subdivisionsList.Count)
            {
                subdivisionsList.FirstOrDefault(x => x.Order == subDiv.Order + 1).Order = subDiv.Order;
                subDiv.Order = subDiv.Order + 1;
                context.SaveChanges();
                var sortList = subdivisionsList.OrderBy(x => x.Order).ToList();
                Subdivisions = CollectionViewSource.GetDefaultView(sortList);
                Subdivisions.Refresh();
            }
        }
        private bool CanDownCommandExecute(object obj) => true;

        #endregion

        #region Список типов подразделений

        public List<string> SubdivisionTypes { get; set; }

        #endregion
    }
}