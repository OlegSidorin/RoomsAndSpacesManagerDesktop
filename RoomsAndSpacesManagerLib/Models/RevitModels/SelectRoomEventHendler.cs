using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using RoomsAndSpacesManagerLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoomsAndSpacesManagerLib.Models.RevitModels
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class SelectRoomEventHendler : IExternalEventHandler
    {
        public static event Action<object> ChangeUI;
        public static int DbRommId;

        
        public void Execute(UIApplication app)
        {
            Autodesk.Revit.UI.UIDocument uiDoc = app.ActiveUIDocument;
            Autodesk.Revit.UI.Selection.Selection selection = uiDoc.Selection;
            var selectedIds = uiDoc.Selection.GetElementIds();
            Element dd = uiDoc.Document.GetElement(selectedIds.First());
            MainWindowViewModel.roomId = (dd as Room).get_Parameter(BuiltInParameter.ROOM_NUMBER).AsString();
            MainWindowViewModel.rvtToomId = dd.Id.IntegerValue;
            using (Transaction trans = new Transaction(app.ActiveUIDocument.Document, "AddToFamilyParam"))
            {
                trans.Start();
                dd.LookupParameter("М1_ID_задания")?.Set(DbRommId);
                //dd.LookupParameter("Имя").Set(DbRommId);
                trans.Commit();
            }
            ChangeUI.Invoke(this);
        }
        public string GetName() => nameof(SelectRoomEventHendler);
    }
}
