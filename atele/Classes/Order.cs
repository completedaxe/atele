using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace atele.Classes
{
    [Table(Name = "Order")]
    class Order
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id_order { get; set; }
        [Column]
        public long Id_client { get; set; }
        [Column]
        public long Id_service { get; set; }
        [Column]
        public long Id_user { get; set; }
        [Column]
        public DateTime Date { get; set; }
        [Column]
        public long Id_status_order { get; set; }
    }
}
