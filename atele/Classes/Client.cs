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
    [Table(Name = "Client")]
    class Client
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id_client { get; set; }
        [Column]
        public string Fio_client { get; set; }
        [Column]
        public string Phone_client { get; set; }
        [Column]
        public bool Status_client { get; set; }
    }
}
