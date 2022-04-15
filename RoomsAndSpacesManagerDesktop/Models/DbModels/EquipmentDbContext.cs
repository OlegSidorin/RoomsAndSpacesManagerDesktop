using RoomsAndSpacesManagerDataBase.Dto;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using RoomsAndSpacesManagerDesktop.Infrastructure.Repositories;
using RoomsAndSpacesManagerDesktop.Models.DbModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsAndSpacesManagerDesktop.Models.DbModels
{
    class EquipmentDbContext : MainDbContext
    {
        #region RoomEq
        public List<RoomEquipmentDto> GetAllEquipments()
        {
            return context.RaSM_RoomEquipments.ToList();
        }

        public List<RoomEquipmentDto> GetEquipments(RoomNameDto roomName)
        {
            return context.RaSM_RoomEquipments.Where(x => x.RoomNameId == roomName.Id).ToList();
        }

        public List<EquipmentDto> GetEquipmentsWithSortItems(RoomNameDto roomName, RoomDto room)
        {
            List<EquipmentDto> equpmets = context.RaSM_RoomEquipments
                .Where(x => x.RoomNameId == roomName.Id)
                .Select(x => new EquipmentDto(x, 0) { RoomId = room.Id, Mandatory = false })
                .ToList();
            equpmets.Sort((x, y) => x.Number.CompareTo(y.Number));

            int activeNumber = default;

            foreach (EquipmentDto eq in equpmets)
            {
                if (!activeNumber.Equals(eq.Number))
                {
                    activeNumber = eq.Number;
                    eq.Mandatory = true;
                }
            }
            return equpmets;
        }

        public void AddNewEquipment(RoomNameDto roomName)
        {
            context.RaSM_RoomEquipments.Add(new RoomEquipmentDto() { RoomNameId = roomName.Id });
            context.SaveChanges();
        }
        public void AddNewEquipments(List<RoomEquipmentDto> equipments)
        {
            context.RaSM_RoomEquipments.AddRange(equipments);
            context.SaveChanges();
        }
        public void RemoveEquipment(RoomEquipmentDto equipment)
        {
            context.RaSM_RoomEquipments.Remove(equipment);
            context.SaveChanges();
        }
        #endregion



        #region Eq
        public List<EquipmentDto> GetEquipments(RoomDto room)
        {
            return context.RaSM_Equipments.Where(x => x.RoomId == room.Id).ToList();
        }

        public List<EquipmentDto> AddEquipmentsByRoomNameId(RoomDto room)
        {
            var roomEqupments = context.RaSM_RoomEquipments.Where(x => x.RoomNameId == room.RoomNameId).ToList();
            var equpments = roomEqupments.Select(x => new EquipmentDto(x, room.Subdivision?.SubdivisionForce)
            {
                RoomId = room.Id,
                Currently = false
            }).ToList();

            EquipmentRep equipments = new EquipmentRep(equpments);
            context.RaSM_Equipments.AddRange(equipments.Equipments);

            context.SaveChanges();

            return equipments.Equipments;
        }


        public void CopyRoomNameEquipmentsToRoomIssue(RoomNameDto roomName, RoomDto room)
        {
            //context.RaSM_Equipments.RemoveRange(context.RaSM_Equipments.Where(x => x.RoomId == room.Id));
            List<EquipmentDto> equpmets = context.RaSM_RoomEquipments
                        .Where(x => x.RoomNameId == roomName.Id).ToList()
                        .Select(x => new EquipmentDto(x, room.Subdivision?.SubdivisionForce) { RoomId = room.Id, Mandatory = x.Mandatory, Currently = false })
                        .ToList();

            EquipmentRep equipments = new EquipmentRep(equpmets);


            context.RaSM_Equipments.AddRange(equpmets);
            context.SaveChanges();
        }

        public void CopyEquipmentBetweenRoomIssue(RoomDto defaultRoom, RoomDto currentRoom)
        {
            var equipments = context.RaSM_Equipments
                .Where(x => x.RoomId == defaultRoom.Id).ToList()
                .Select(x => new EquipmentDto(x, currentRoom.Id, currentRoom.Subdivision?.SubdivisionForce)).ToList();

            context.RaSM_Equipments.AddRange(equipments);
            context.SaveChanges();
        }


        public void AddNewEquipment(RoomDto room)
        {
            context.RaSM_Equipments.Add(new EquipmentDto() { RoomId = room.Id });
            context.SaveChanges();
        }

        public void AddNewEquipment(EquipmentDto eq)
        {
            context.RaSM_Equipments.Add(eq);
            context.SaveChanges();
        }

        public void RemoveEquipment(EquipmentDto equipment)
        {
            context.RaSM_Equipments.Remove(equipment);
            context.SaveChanges();
        }

        public void RemoveAllEquipment(RoomDto room)
        {
            context.RaSM_Equipments.RemoveRange(context.RaSM_Equipments.Where(x => x.RoomId == room.Id));
            context.SaveChanges();
        }

        #endregion

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}