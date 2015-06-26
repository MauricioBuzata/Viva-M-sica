using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva_Música.Entity;

namespace Viva_Música.Banco
{
    class DataBase : DataContext
    {
        public static string DBConnectionString =
           "Data Source = 'isostore:vivaMusicaDB.sdf'";

        public DataBase()
            : base(DBConnectionString)
        {
        }

        public Table<Noticias> Noticia
        {
            get { return this.GetTable<Noticias>(); }
        }
    }
}
