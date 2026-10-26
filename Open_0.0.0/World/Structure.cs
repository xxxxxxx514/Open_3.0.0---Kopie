using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_0._0._0.World
{
    class Structure
    {
        public StructureName StructureName { get; set; }
        public Tile[,] Field {  get; set; }
        public Structure() { }
        public Structure(StructureName structureName, Tile[,] field)
        {
            StructureName = structureName;
            Field = field;
        }
    }
}
