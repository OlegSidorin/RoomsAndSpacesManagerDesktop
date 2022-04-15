using Autodesk.Revit.DB;
using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerLib.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerLib.Models.RevitHelper
{
    class RevitHelperModel
    {
        public static ParameterDto SetProperty(string paramName, string dtoProp, Element item, RoomDto roomDto)
        {

            Parameter param = item.LookupParameter(paramName);
            if (param != null)
            {
                string storageType = param.StorageType.ToString();
                string exstring = param.Definition.Name;
                if (storageType.ToString() == "String")
                {
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();
                    if (param.AsString() != propValue)
                    {
                        ParameterDto parameterDto = new ParameterDto();
                        parameterDto.Name = paramName;
                        parameterDto.NewValue = propValue;
                        parameterDto.OldValue = param.AsString();
                        param?.Set(propValue);
                        return parameterDto;
                    }
                }
                if (storageType.ToString() == "Integer")
                {
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();

                    if (propValue == null)
                        return null;
                    int value;
                    if (!int.TryParse(propValue, out value))
                        return null;

                    if (param.AsInteger() != value)
                    {
                        ParameterDto parameterDto = new ParameterDto();
                        parameterDto.Name = paramName;
                        parameterDto.NewValue = propValue.ToString();
                        parameterDto.OldValue = param.AsInteger().ToString();
                        param?.Set(value);
                        return parameterDto;
                    }
                }
                if (storageType.ToString() == "Double")
                {
                    double value;
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();
                    if (propValue == null)
                        return null;
                    if (!double.TryParse(propValue, out value))
                        return null;
                    if (param.AsDouble() != value & param.AsDouble() != UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_SQUARE_METERS))
                    {
                        ParameterDto parameterDto = new ParameterDto();
                        parameterDto.Name = paramName;
                        parameterDto.NewValue = value.ToString();
                        parameterDto.OldValue = param.AsDouble().ToString();
                        if (param.DisplayUnitType == DisplayUnitType.DUT_SQUARE_METERS)
                            value = UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_SQUARE_METERS);
                        param?.Set(value);
                        return parameterDto;
                    }
                }
            }

            return null;
        }
    }
}
