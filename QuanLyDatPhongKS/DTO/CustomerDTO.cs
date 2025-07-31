using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace QuanLyDatPhongKS.DTO
{
    public class CustomerDTO
    {
        private int CustomerID;
        public int customerID
        {
            get { return CustomerID; }
            set { CustomerID = value; }
        }

        private string CustomerName;
        public string customerName
        {
            get { return CustomerName; }
            set { CustomerName = value; }
        }

        private string Password;
        public string passWord
        {
            get { return Password; }
            set { Password = value; }
        }

        private string DisplayName;
        public string displayName
        {
            get { return DisplayName; }
            set { DisplayName = value; }
        }

        private string Email;
        public string EMAIL
        {
            get { return Email; }
            set { Email = value; }
        }

        private string Phone;
        public string PHONE
        {
            get { return Phone; }
            set { Phone = value; }
        }

        private string Address;
        public string ADDRESS
        {
            get { return Address; }
            set { Address = value; }
        }
    }
}
