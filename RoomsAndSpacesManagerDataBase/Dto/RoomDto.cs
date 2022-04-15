using RoomsAndSpacesManagerDataBase.Data.DataBaseContext;
using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomsAndSpacesManagerDataBase.Dto
{
    [Table("RaSM_Rooms")]
    public class RoomDto : ViewModel
    {
        static RoomAndSpacesDbContext context = new RoomAndSpacesDbContext();
        private int roomNameId;
        private string min_area;
        private string class_chistoti_SanPin;
        private string class_chistoti_SP_158;
        private string class_chistoti_GMP;
        private string t_calc;
        private string discription_HS;
        private string categoty_Chistoti_po_san_epid;
        private string discription_GSV;
        private string discription_AK_ATH;
        private string discription_SS;
        private string equipment_VK;
        private string discription_AR;
        private string discription_EOM;
        private string group_el_bez;
        private string osveshennost_pro_obshem_osvech;
        private string discription_OV;
        private string kEO_sovm_osv;
        private string kEO_est_osv;
        private string ot_vlazhnost;
        private string vityazhka;
        private string pritok;
        private string t_max;
        private string t_min;

        public RoomDto()
        {

        }

        public RoomDto(RoomDto oldRoom)
        {
            this.RoomNameId = oldRoom.RoomNameId;
            this.ShortName = oldRoom.ShortName;
            this.RoomNumber = oldRoom.RoomNumber;
            this.Min_area = oldRoom.Min_area;
            this.Class_chistoti_SanPin = oldRoom.Class_chistoti_SanPin;
            this.Class_chistoti_SP_158 = oldRoom.Class_chistoti_SP_158;
            this.Class_chistoti_GMP = oldRoom.Class_chistoti_GMP;
            this.T_calc = oldRoom.T_calc;
            this.T_min = oldRoom.T_min;
            this.T_max = oldRoom.T_max;
            this.Pritok = oldRoom.Pritok;
            this.Vityazhka = oldRoom.Vityazhka;
            this.Ot_vlazhnost = oldRoom.Ot_vlazhnost;
            this.KEO_est_osv = oldRoom.KEO_est_osv;
            this.KEO_sovm_osv = oldRoom.KEO_sovm_osv;
            this.Discription_OV = oldRoom.Discription_OV;
            this.Osveshennost_pro_obshem_osvech = oldRoom.Osveshennost_pro_obshem_osvech;
            this.Group_el_bez = oldRoom.Group_el_bez;
            this.Discription_EOM = oldRoom.Discription_EOM;
            this.Discription_AR = oldRoom.Discription_AR;
            this.Equipment_VK = oldRoom.Equipment_VK;
            this.Discription_SS = oldRoom.discription_SS;
            this.Discription_AK_ATH = oldRoom.Discription_AK_ATH;
            this.Discription_GSV = oldRoom.discription_GSV;
            this.Categoty_Chistoti_po_san_epid = oldRoom.Categoty_Chistoti_po_san_epid;
            this.Discription_HS = oldRoom.Discription_HS;
            this.Categoty_pizharoopasnosti = oldRoom.Categoty_pizharoopasnosti;
            this.Rab_mesta_posetiteli = oldRoom.Rab_mesta_posetiteli;
            this.Kolichestvo_personala = oldRoom.Kolichestvo_personala;
            this.Kolichestvo_posetitelei = oldRoom.Kolichestvo_posetitelei;
            this.Nagruzki_na_perekririe = oldRoom.Nagruzki_na_perekririe;
            this.El_Nagruzka = oldRoom.El_Nagruzka;
        }




        #region Поля для выгрузки
        public int Id { get; set; }

        private string name;

        [NotMapped]
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
        public int RoomNameId
        {
            get => roomNameId;
            set
            {
                roomNameId = value;
                if (RoomNameId != 0)
                {
                    RoomName = context.RaSM_RoomNames.FirstOrDefault(x => x.Id == RoomNameId);
                    Name = RoomName?.Name;
                }

            }
        }
        
        public string ShortName { get; set; }
        public string RoomNumber { get; set; }

        #region Исходные данные по помещениям
        public string Min_area
        {
            get => min_area;
            set
            {
                Set(ref min_area, value);
            }
        }

        public string Class_chistoti_SanPin
        {
            get => class_chistoti_SanPin;
            set
            {
                Set(ref class_chistoti_SanPin, value);
            }
        }
        public string Class_chistoti_SP_158
        {
            get => class_chistoti_SP_158;
            set
            {
                Set(ref class_chistoti_SP_158, value);
            }
        }
        public string Class_chistoti_GMP
        {
            get => class_chistoti_GMP;
            set
            {
                Set(ref class_chistoti_GMP, value);
            }
        }
        public string T_calc
        {
            get => t_calc;
            set
            {
                Set(ref t_calc, value);

            }
        }
        public string T_min
        {
            get => t_min;
            set
            {
                Set(ref t_min, value);

            }
        }
        public string T_max
        {
            get => t_max;
            set
            {
                Set(ref t_max, value);

            }
        }
        public string Pritok
        {
            get => pritok;
            set
            {
                Set(ref pritok, value);
            }
        }
        public string Vityazhka
        {
            get => vityazhka;
            set
            {
                Set(ref vityazhka, value);
            }
        }
        public string Ot_vlazhnost
        {
            get => ot_vlazhnost;
            set
            {
                Set(ref ot_vlazhnost, value);
            }
        }
        public string KEO_est_osv
        {
            get => kEO_est_osv;
            set
            {
                Set(ref kEO_est_osv, value);
            }
        }
        public string KEO_sovm_osv
        {
            get => kEO_sovm_osv;
            set
            {
                Set(ref kEO_sovm_osv, value);
            }
        }
        public string Discription_OV
        {
            get => discription_OV;
            set
            {
                Set(ref discription_OV, value);
            }
        }
        public string Osveshennost_pro_obshem_osvech
        {
            get => osveshennost_pro_obshem_osvech;
            set
            {
                Set(ref osveshennost_pro_obshem_osvech, value);
            }
        }
        public string Group_el_bez
        {
            get => group_el_bez;
            set
            {
                Set(ref group_el_bez, value);
            }
        }
        public string Discription_EOM
        {
            get => discription_EOM;
            set
            {
                Set(ref discription_EOM, value);
            }
        }
        public string Discription_AR
        {
            get => discription_AR;
            set
            {
                Set(ref discription_AR, value);
            }
        }
        public string Equipment_VK
        {
            get => equipment_VK;
            set
            {
                Set(ref equipment_VK, value);
            }
        }
        public string Discription_SS
        {
            get => discription_SS;
            set
            {
                Set(ref discription_SS, value);
            }
        }
        public string Discription_AK_ATH
        {
            get => discription_AK_ATH;
            set
            {
                Set(ref discription_AK_ATH, value);
            }
        }
        public string Discription_GSV
        {
            get => discription_GSV;
            set
            {
                Set(ref discription_GSV, value);
            }
        }
        public string Categoty_Chistoti_po_san_epid
        {
            get => categoty_Chistoti_po_san_epid;
            set
            {
                Set(ref categoty_Chistoti_po_san_epid, value);
            }
        }
        public string Discription_HS
        {
            get => discription_HS;
            set
            {
                Set(ref discription_HS, value);
            }
        }

        public string Categoty_pizharoopasnosti { get; set; }
        public string Rab_mesta_posetiteli { get; set; }
        public string Kolichestvo_personala { get; set; }
        public string Kolichestvo_posetitelei { get; set; }
        public string Nagruzki_na_perekririe { get; set; }
        public string El_Nagruzka { get; set; }

        public string Notation { get; set; }
        #endregion


        private int arRoomId;
        

        public int ArRoomId { get => arRoomId; set => Set(ref arRoomId, value); }


        #region SupClass
        private RoomNameDto roomName;
        [NotMapped]
        public RoomNameDto RoomName { get => roomName; set => Set(ref roomName, value); }









        #endregion

        public int SubdivisionId { get; set; }
        public virtual SubdivisionDto Subdivision { get; set; }

        public virtual ICollection<EquipmentDto> Equipments { get; set; }
        #endregion
    }
}
