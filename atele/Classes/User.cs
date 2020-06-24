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
    [Table(Name = "User")]
    class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public long Id_user { get; set; }
        [Column]
        public string Name_user { get; set; }
        [Column]
        public bool Status_user { get; set; }
        [Column]
        public string Login { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public string Role { get; set; }
    }
}
