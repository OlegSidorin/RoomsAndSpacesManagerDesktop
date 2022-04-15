using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RoomsAndSpacesManagerLib.Models.RevitModels;
using RoomsAndSpacesManagerLib.ViewModels;
using RoomsAndSpacesManagerLib.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerLib
{
    [Transaction(TransactionMode.Manual)]
    public class Cmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            Document doc = commandData.Application.ActiveUIDocument.Document;


            AddParametersIntoSpacies addParametersIntoSpacies = new AddParametersIntoSpacies();
            SelectRoomEventHendler evHendSelectRoom = new SelectRoomEventHendler();
            ExternalEvent ExEventSelectRoom = ExternalEvent.Create(evHendSelectRoom);
            ExternalEvent ExEventAddParameters = ExternalEvent.Create(addParametersIntoSpacies);

            

            MainWindow mainWindow = new MainWindow();
            MainWindowViewModel vm = new MainWindowViewModel();
            vm.ApplyEventGetRoomFromRvtModel = ExEventSelectRoom;
            vm.ApplyEventAddParametersIntoSpacies = ExEventAddParameters;
            mainWindow.DataContext = vm;
            mainWindow.Show();

            return Result.Succeeded;
        }
    }
}
