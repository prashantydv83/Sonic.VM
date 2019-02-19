using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sonic.VM.Web.Models
{
    public class Order
    {
        public int OrdrId;
        public int PaymtDtlId;
        public int ProdId;
        public int Qty;
        public double Amnt;
    }
}