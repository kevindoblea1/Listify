using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Listify.Modelos
{
    [Table("articulos")]
    public class Articulo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombre { get; set; } = string.Empty;

        public int Cantidad { get; set; } = 1;

        public bool Comprado { get; set; } = false;
    }
}
