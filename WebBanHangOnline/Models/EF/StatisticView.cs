using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Models.EF
{
    public class StatisticView
    {
        public static string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static StatisticViewModel Statistic()
        {
            using(var connect = new SqlConnection(strConnection))
            {
                var item = connect.QueryFirstOrDefault<StatisticViewModel>("StatisticProduct", commandType:System.Data.CommandType.StoredProcedure);
                return item;
            }
        }
    }
}