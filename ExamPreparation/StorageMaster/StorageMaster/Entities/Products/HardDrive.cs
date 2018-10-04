namespace StorageMaster.Entities.Products
{
    public class HardDrive : Product
    {
        private const double hdWeight = 1;

        public HardDrive(double price) : base(price, hdWeight)
        {
        }
    }
}
