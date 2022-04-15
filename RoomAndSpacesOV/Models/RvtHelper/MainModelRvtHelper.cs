using Autodesk.Revit.DB;
using RoomAndSpacesOV.Dto;
using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAndSpacesOV.Models.RvtHelper
{
    class MainModelRvtHelper
    {
        public ParameterDto SetPropertt(string paramName, string dtoProp, Element item, RoomDto roomDto)
        {
            Parameter param = item.LookupParameter(paramName);
            if (param != null)
            {
                string storageType = param.StorageType.ToString();
                if (storageType.ToString() == "String")
                {
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();

                    if (propValue == null)
                        propValue = "";

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

                    

                    int value;
                    if (!int.TryParse(propValue, out value))
                        value = 0;

                    if (propValue == null | propValue == "")
                        value = 0;



                    if (param.AsInteger() == value)
                        return null;

                    ParameterDto parameterDto = new ParameterDto();
                    parameterDto.Name = paramName;
                    parameterDto.NewValue = value.ToString();
                    parameterDto.OldValue = param.AsInteger().ToString();
                    param?.Set(value);
                    return parameterDto;
                }


                if (storageType.ToString() == "Double")
                {
                    double value;
                    string propValue = roomDto.GetType().GetProperty(dtoProp)?.GetValue(roomDto)?.ToString();
                    
                    if (!double.TryParse(propValue, out value))
                        value = 0;


                    if (propValue == null | propValue == "")
                        value = 0;

                    if (param.AsDouble() != value && param.AsDouble() != UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_SQUARE_METERS) && param.AsDouble() != UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_CELSIUS))
                    {
                        ParameterDto parameterDto = new ParameterDto();
                        parameterDto.Name = paramName;
                        parameterDto.NewValue = value.ToString();
                        parameterDto.OldValue = param.AsDouble().ToString();
                        if (param.DisplayUnitType == DisplayUnitType.DUT_SQUARE_METERS)
                            value = UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_SQUARE_METERS);

                        if (param.DisplayUnitType == DisplayUnitType.DUT_CELSIUS)
                            value = UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_CELSIUS);
                        param?.Set(value);
                        return parameterDto;
                    }
                }
            }
            return null;
        }
    }
}

