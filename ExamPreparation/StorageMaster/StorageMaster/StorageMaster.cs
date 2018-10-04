using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    public class StorageMaster
    {
        Dictionary<string, Stack<Product>> products;
        Dictionary<string, Storage> storages;
        Vehicle currentVehicle;

        public StorageMaster()
        {
            this.products = new Dictionary<string, Stack<Product>>();
            this.storages = new Dictionary<string, Storage>();
            this.currentVehicle = null;
        }
        public string AddProduct(string type, double price)
        {

            if (products.ContainsKey(type))
            {
                products[type].Push(this.GetProduct(type, price));
            }
            else
            {
                products.Add(type, new Stack<Product>(new List<Product>() { this.GetProduct(type, price) }));

            }
            return $"Added {type} to pool";

        }

        public string RegisterStorage(string type, string name)

        {

            Storage storageToAdd = this.GetStorage(type, name);

            storages.Add(name, storageToAdd);

            return $"Registered {name}";
        }



        public string SelectVehicle(string storageName, int garageSlot)
        {
            Vehicle vehicle = storages[storageName].GetVehicle(garageSlot);
            this.currentVehicle = vehicle;

            return $"Selected {currentVehicle.GetType().Name}";
        }


        public string LoadVehicle(IEnumerable<string> productNames)

        {

            int counter = 0;

            foreach (var productName in productNames)
            {
                if (!currentVehicle.IsFull && products.ContainsKey(productName) && products[productName].Count > 0)
                {
                    var productToAdd = products[productName].Pop();

                    this.currentVehicle.LoadProduct(productToAdd);

                    counter++;
                }
                else if (!products.ContainsKey(productName))
                {
                    throw new InvalidOperationException($"{productName} is out of stock!");
                }


            }
            return $"Loaded {counter}/{productNames.Count()} products into {currentVehicle.GetType().Name}";
        }



        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {

            if (!storages.ContainsKey(sourceName))
            {
                throw new InvalidOperationException("Invalid source storage!");
            }
            if (!storages.ContainsKey(destinationName))
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }
            var vehicle = storages[sourceName].GetVehicle(sourceGarageSlot);
            int destinationGarageSlot = storages[sourceName].SendVehicleTo(sourceGarageSlot, storages[destinationName]);

            return $"Sent {vehicle.GetType().Name} to {destinationName} (slot {destinationGarageSlot})";
        }



        public string UnloadVehicle(string storageName, int garageSlot)

        {
            var vehicleToUnload = storages[storageName].GetVehicle(garageSlot);

            int productsInVehicle = vehicleToUnload.Trunk.Count;
            int unloadedProductsCount = storages[storageName].UnloadVehicle(garageSlot);

            return $"Unloaded {unloadedProductsCount}/{productsInVehicle} products at {storageName}";

        }



        public string GetStorageStatus(string storageName)

        {

            return storages[storageName].ToString();

        }

        public string GetSummary()
        {

            StringBuilder sb = new StringBuilder();

            foreach (var storage in storages.OrderByDescending(x => x.Value.Products.Sum(p => p.Price)))
            {
                var totalMoney = storage.Value.Products.Sum(x => x.Price);
                sb.AppendLine($"{storage.Key}:");
                sb.AppendLine($"Storage worth: ${totalMoney:F2}");
            }
            return sb.ToString().Trim();
        }

        private Product GetProduct(string type, double price)
        {
            switch (type)
            {
                case "Gpu":
                    return new Gpu(price);
                case "HardDrive":
                    return new HardDrive(price);
                case "Ram":
                    return new Ram(price);
                case "SolidStateDrive":
                    return new SolidStateDrive(price);
                default:
                    throw new InvalidOperationException("Invalid product type!");
            }
        }
        private Storage GetStorage(string type, string name)
        {
            switch (type)
            {
                case "AutomatedWarehouse":
                    return new AutomatedWarehouse(name);
                case "DistributionCenter":
                    return new DistributionCenter(name);
                case "Warehouse":
                    return new Warehouse(name);
                default:
                    throw new InvalidOperationException("Invalid storage type!");
            }
        }
    }
}
