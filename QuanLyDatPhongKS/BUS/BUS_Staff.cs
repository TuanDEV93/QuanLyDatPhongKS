
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.BUS
{
    public class BUS_Staff
    {
        DAO_Staff daoStaff = new DAO_Staff(); // Tạo đối tượng của DAO_Staff
        public List<StaffDTO> GetStaffByRole()
        {
            
            return daoStaff.GetStaffByRole();
        }

        public void UpdateStaff(StaffDTO staff)
        {

             daoStaff.UpdateStaff(staff);
        }
        public void DeleteStaff(int userID)
        {

            daoStaff.DeleteStaff(userID);
        }
        public StaffDTO GetStaffById(int userId)
        {
            return daoStaff.GetStaffById(userId);
        }
        public bool AddStaff(StaffDTO staff)
        {

            // Thêm logic kiểm tra trước khi thêm
            if (string.IsNullOrEmpty(staff.DisplayName) || string.IsNullOrEmpty(staff.UserName) ||
                string.IsNullOrEmpty(staff.Password))
            {
                return false;
            }
            return daoStaff.AddStaff(staff);
        }
        public bool IsUserNameExists(string userName)
        {
            return daoStaff.IsUserNameExists(userName);
        }
    }
}