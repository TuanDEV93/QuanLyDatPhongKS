
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.BUS
{
    public class BUS_Customer
    {
        DAO_Customer daoCustomer = new DAO_Customer(); // Tạo đối tượng của DAO_Customer
        public List<CustomerDTO> GetCustomer()
        {
            return daoCustomer.GetCustomer();
        }
        public List<CustomerDTO> SearchCustomerByDisplayName(string displayName)
        {
            return daoCustomer.SearchCustomerByDisplayName(displayName);
        }

        public void UpdateCustomer(CustomerDTO customer)
        {
            daoCustomer.UpdateCustomer(customer);
        }
        public void DeleteCustomer(int customerID)
        {

            daoCustomer.DeleteCustomer(customerID);
        }
        public CustomerDTO GetCustomerById(int customerId)
        {
            return daoCustomer.GetCustomerById(customerId);
        }
        public bool AddCustomer(CustomerDTO customer)
        {

            // Thêm logic kiểm tra trước khi thêm
            if (string.IsNullOrEmpty(customer.displayName) || string.IsNullOrEmpty(customer.customerName) ||
                string.IsNullOrEmpty(customer.passWord))
            {
                return false;
            }
            return daoCustomer.AddCustomer(customer);
        }
        public bool IsCustomerNameExists(string customerName)
        {
            return daoCustomer.IsCustomerNameExists(customerName);
        }
    }
}