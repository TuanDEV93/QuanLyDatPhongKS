using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyDatPhongKS.DTO
{
    public class Account
    {
        private int UserID;
        public int USERID
        {
            get { return UserID; }
            set { UserID = value; }
        }
        private string UserName;
        public string Username
        {
            get { return UserName; }
            set { UserName = value; }

        }

        private string DisplayName;
        public string displayName

        {
            get { return DisplayName; }
            set { DisplayName = value; }
        }

        private string PassWord;
        public string passWord

        {
            get { return PassWord; }
            set { PassWord = value; }
        }

        private bool Role;
        public bool role
        {
            get { return Role; }
            set { Role = value; }
        }

        private string UserAddress;
        public string userADDRESS
        {
            get { return userADDRESS; }
            set { userADDRESS = value; }

        }
        private string Phone;
        public string PHONE
        {
            get { return Phone; }
            set { Phone = value; }
        }
    }
}
