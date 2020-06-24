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
    [Table(Name = "Status_order")]
    class StatusOrder
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id_status_order { get; set; }
        [Column]
        public string Name { get; set; }
    }
}
