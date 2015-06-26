using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viva_Música.Entity
{
    [Table(Name = "Noticias")]
    public class Noticias
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public string Title { get; set; }

        [Column(CanBeNull = false)]
        public string PubDate { get; set; }

        [Column(CanBeNull = false)]
        public string Link { get; set; }

        [Column(CanBeNull = false)]
        public string Description { get; set; }
    }
}
