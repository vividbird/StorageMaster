using StorageMaster.Entities.Products;
using StorageMaster.Entities.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster.Entities.Storage
{
    public abstract class Storage
    {
        protected List<Product> products;
        protected Vehicle[] vehicles;

        public string Name { get; protected set; }
        public int Capacity { get; protected set; }
        public int GarageSlots { get; protected set; }

        public IReadOnlyCollection<Product> Products { get { return products; } }
        public IReadOnlyCollection<Vehicle> Garage { get { return vehicles; } }

        public bool IsFull { get { return Products.Sum(x => x.Weight) >= Capacity; } }

        public Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
            this.vehicles = new Vehicle[garageSlots];

            this.products = new List<Product>();

            Array.Copy(vehicles.ToArray(), this.vehicles, vehicles.Count());

        }
        public Vehicle GetVehicle(int garageSlot)
        {
            if (garageSlot >= GarageSlots)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }
            if (vehicles[garageSlot] == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }
            return vehicles[garageSlot];
        }
        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            Vehicle vehicle = this.GetVehicle(garageSlot);

            int indexToBeDelivered = Array.IndexOf(deliveryLocation.vehicles, null);

            if (indexToBeDelivered == -1)
            {
                throw new InvalidOperationException("No room in garage!");
            }
            else
            {
                deliveryLocation.vehicles[indexToBeDelivered] = vehicle;
                this.vehicles[garageSlot] = null;
            }

            return indexToBeDelivered;
        }
        public int UnloadVehicle(int garageSlot)
        {
            if (this.IsFull)
            {
                throw new InvalidOperationException("Storage is full!");
            }
            Vehicle vehicle = this.GetVehicle(garageSlot);

            int counter = 0;

            while (!IsFull && !vehicle.IsEmpty)
            {
                products.Add(vehicle.Unload());
                counter++;
            }
            return counter;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            List<string> vehiclesOutput = new List<string>();

            var allProducts = products.GroupBy(x => x.GetType().Name).OrderByDescending(x => x.Count()).ThenBy(x => x.GetType().Name);

            var productLines = allProducts.Select(x => $"{x.Key} ({x.Count()})");

            foreach (var vehicle in vehicles)
            {
                var vehicleOutput = vehicle != null ? vehicle.GetType().Name : "empty";
                vehiclesOutput.Add(vehicleOutput);
            }

            sb.AppendLine($"Stock ({this.Products.Sum(x => x.Weight)}/{this.Capacity}): [{String.Join(", ", productLines)}]");
            sb.AppendLine($"Garage: [{string.Join("|", vehiclesOutput)}]");


            return sb.ToString().Trim();
        }
    }
}
