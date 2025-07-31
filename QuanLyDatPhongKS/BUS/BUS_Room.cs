using QuanLyDatPhongKS.Admin;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.BUS
{
    public class BUS_Room
    {
       
        private DAO_Room dao = new DAO_Room();
        public List<RoomDTO> GetAvailableRooms(string roomType, DateTime checkin, DateTime checkout)
        {
            
            return dao.GetAvailableRooms(roomType, checkin, checkout);
        }
        

        public RoomDTO GetRoomById(int roomId)
        {
            return dao.GetRoomById(roomId);
        }
        public RoomDTO GetTypeRoomById(int typeRoomId)
        {
            return dao.GetTypeRoomById(typeRoomId);
        }
        public List<RoomDTO> GetAllRoomTypes()
        {
            return dao.GetAllRoomTypes(); 
        }
        public bool AddRoomType(RoomDTO typeRoom)
        {
            return dao.InsertTypeRoom(typeRoom);
        }
        public void DeleteRoomType(int typeRoomId)
        {
            dao.DeleteRoomType(typeRoomId);
        }

        public void UpdateRoomType(RoomDTO room)
        {
            dao.UpdateRoomType(room);
        }
        public void UpdateRoom(RoomDTO room)
        {
            dao.UpdateRoom(room);
        }
        //Admin
        public List<RoomDTO> GetRooms(DateTime checkin, DateTime checkout)
        {
            return dao.GetRooms(checkin, checkout);
        }
        public bool AddRoom(RoomDTO room)
        {
            if (dao.IsRoomNumberExists(room.RoomNumber))
            {
                // hàm kiểm tra trùng số phòng Nếu trùng thì không thêm
                return false;
            }

            return dao.InsertRoom(room);
        }

    }
}