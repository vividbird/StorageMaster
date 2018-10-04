using StorageMaster.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster.Entities.Vehicles
{
    public abstract class Vehicle
    {
        List<Product> trunk;

        public int Capacity { get; protected set; }
        public IReadOnlyCollection<Product> Trunk { get { return trunk; } }
        public bool IsFull { get { return Trunk.Sum(x => x.Weight) >= Capacity; } }
        public bool IsEmpty { get { return Trunk.Count == 0; } }

        public Vehicle(int capacity)
        {
            this.Capacity = capacity;
            trunk = new List<Product>();

        }
        public void LoadProduct(Product product)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Vehicle is full!");
            }
            trunk.Add(product);
        }
        public Product Unload()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }
            Product productToReturn = trunk.Last();
            trunk.Remove(productToReturn);

            return productToReturn;
        }

    }
}
