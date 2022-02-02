using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Manav_Otomasyonu.Repository
{
    public abstract class RepositoryBase
    {
        SqlConnection con_=null;
        public RepositoryBase()
        {
            con_ = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ConnectionString);
        }
        public SqlConnection con
        {
            get
            {
                return con_;
            }
        }
    }
}
