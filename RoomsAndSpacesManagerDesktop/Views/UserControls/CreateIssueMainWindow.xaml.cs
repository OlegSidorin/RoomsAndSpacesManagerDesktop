﻿using RoomsAndSpacesManagerDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoomsAndSpacesManagerDesktop.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreateIssueMainWindow.xaml
    /// </summary>
    public partial class CreateIssueMainWindow : UserControl
    {
        public CreateIssueMainWindow()
        {
            InitializeComponent();
            //CreateIssueViewModel vm = (CreateIssueViewModel)this.DataContext;
            //MessageBox.Show("bybwbfkbpfwbz");
            CreateIssueViewModel.ThisWindow = this;
        }

        private void dgRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (dgRooms.SelectedCells != null)
            //{
            //    string str = dgRooms.SelectedCells.First().Column.Header.ToString();
            //    MessageBox.Show(str);
            //}
            //CreateIssueViewModel vm = (CreateIssueViewModel)this.DataContext;
            //vm.ThisWindow = this;

        }
    }
}
