using RoomsAndSpacesManagerDataBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Infrastructure.Repositories
{
    class EquipmentRep
    {
        private List<EquipmentDto> equipments = new List<EquipmentDto>();
        public List<EquipmentDto> Equipments 
        {
            get 
            {
                SortMandatoryByFirstPosition();
                return equipments;
            }
            private set 
            {
                value = equipments;
            }
            
        }




        public EquipmentRep()
        {

        }
        public EquipmentRep(List<EquipmentDto> equipmentDtos)
        {
            equipments = equipmentDtos;
            SortMandatoryByFirstPosition();
        }

        public int Count
        {
            get 
            { 
                return equipments.Count; 
            }
        }


        public void Add(EquipmentDto equipment)
        {
            equipments.Add(equipment);
            SortMandatoryByFirstPosition();
        }

        public void AddRange(List<EquipmentDto> equipments)
        {
            equipments.AddRange(equipments);
            SortMandatoryByFirstPosition();
        }

        public void Remove(EquipmentDto equipment)
        {
            equipments.Remove(equipment);
            SortMandatoryByFirstPosition();
        }

        public List<EquipmentDto> GetEquipments()
        {
            SortMandatoryByFirstPosition();
            return equipments;
        }

        private void SortMandatoryByFirstPosition()
        {
            equipments.Sort((x, y) => x.Number.CompareTo(y.Number));

            int activeNumber = default;

            foreach (EquipmentDto eq in equipments)
            {
                if (!activeNumber.Equals(eq.Number))
                {
                    activeNumber = eq.Number;
                    eq.Currently = true;
                }
            }
        }
    }
}