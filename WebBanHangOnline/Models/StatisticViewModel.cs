using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class StatisticViewModel
    {
        public int HomNay{ set; get; }
        public int HomQua{ set; get; }
        public int TuanNay{ set; get; }
        public int TuanTruoc{ set; get; }
        public int ThangTruoc{ set; get; }
        public int ThangNay{ set; get; }
        public int TatCa { set; get; }
    }
    public class StatisticModel
    {
        public string HomNay { set; get; }
        public string HomQua { set; get; }
        public string TuanNay { set; get; }
        public string TuanTruoc { set; get; }
        public string ThangTruoc { set; get; }
        public string ThangNay { set; get; }
        public string TatCa { set; get; }
    }
}