using System;

namespace StorageMaster.Entities.Products
{
    public abstract class Product
    {
        /*/
         * Price – реално число (double)
Ако се въведе отрицателна цена, хвърлете InvalidOperationException със съобщението “Price cannot be negative!”.
Weight – реално число (double)
/*/

        private double price;

        public double Price
        {
            get { return price; }

            protected set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("Price cannot be negative!");
                }
                price = value;
            }
        }
        public double Weight { get; protected set; }

        public Product(double price, double weight)
        {
            this.Price = price;
            this.Weight = weight;
        }

    }
}
