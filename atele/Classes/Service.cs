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
    [Table(Name = "Services")]
    class Service
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id_service { get; set; }
        [Column]
        public string Name_services { get; set; }
        [Column]
        public int Price_services { get; set; }
        [Column]
        public bool Status_services { get; set; }
    }
}
