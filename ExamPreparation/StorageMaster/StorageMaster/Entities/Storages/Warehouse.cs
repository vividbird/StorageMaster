using StorageMaster.Entities.Vehicles;
using System.Collections.Generic;

namespace StorageMaster.Entities.Storage
{
    public class Warehouse : Storage
    {
        public Warehouse(string name) :
            base(name, 10, 10, new List<Vehicle> { new Semi(), new Semi(), new Semi() })
        {

        }
    }
}
