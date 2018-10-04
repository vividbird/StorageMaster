using StorageMaster.Entities.Vehicles;
using System.Collections.Generic;

namespace StorageMaster.Entities.Storage
{
    public class DistributionCenter : Storage
    {
        public DistributionCenter(string name)
            : base(name, 2, 5, new List<Vehicle> { new Van(), new Van(), new Van() })
        {
        }
    }
}
