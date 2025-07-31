using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyDatPhongKS.DAO;
using QuanLyDatPhongKS.DTO;
namespace QuanLyDatPhongKS.BUS
{
    public class BUS_BillHistory
    {
        DAO_BillHistory dao = new DAO_BillHistory();
        public void AddHistory(BillHistoryDTO history)
        {
            dao.InsertHistory(history);
        }
    }
}