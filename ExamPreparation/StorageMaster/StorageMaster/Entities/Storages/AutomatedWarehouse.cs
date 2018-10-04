using StorageMaster.Entities.Vehicles;
using System.Collections.Generic;

namespace StorageMaster.Entities.Storage
{
    public class AutomatedWarehouse : Storage
    {

        public AutomatedWarehouse(string name) : base(name, 1, 2, new List<Vehicle> { new Truck() })
        {

        }
    }
}
