using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viva_Música.Banco;
using Viva_Música.Entity;

namespace Viva_Música.Repository
{
    public class NoticiasRepository
    {
        private static DataBase GetDataBase()
        {
            DataBase db = new DataBase();
            if (!db.DatabaseExists())
                db.CreateDatabase();

            return db;
        }

        public static List<Noticias> Get()
        {
            DataBase db = GetDataBase();
            var query = from noticia in db.Noticia orderby noticia.Title select noticia;
            //    if (query.AsEnumerable().Count()> 0)
            //  {
            List<Noticias> lista = new List<Noticias>(query.AsEnumerable());
            return lista;
            // }
            //    else
            // return null;
        }

        public static void Create(Noticias pNoticias)
        {
            DataBase db = GetDataBase();
            db.Noticia.InsertOnSubmit(pNoticias);
            db.SubmitChanges();

        }

        public static void Delete(Noticias pNoticias)
        {
            DataBase db = GetDataBase();
            var query = from c in db.Noticia
                        where c.Id == pNoticias.Id
                        select c;

            db.Noticia.DeleteOnSubmit(query.ToList()[0]);
            db.SubmitChanges();
        }
    }
}
