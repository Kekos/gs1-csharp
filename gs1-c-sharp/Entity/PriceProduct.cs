using System;

namespace Kekos.Gs1.Entity
{
    public class PriceProduct : AbstractEntity
    {
        private string sku;
        private float price;

        public PriceProduct()
        {
        }

        public PriceProduct(string _sku)
        {
            Sku = _sku;
        }

        public PriceProduct(string _sku, float _price)
        {
            Sku = _sku;
            Price = _price;
        }

        public string Sku
        {
            get
            {
                return sku;
            }

            set
            {
                int i;
                if (!int.TryParse(value, out i))
                {
                    throw new ArgumentException("PriceProduct: Sku was not numeric");
                }

                if (value.Length == 0)
                {
                    throw new ArgumentException("PriceProduct: Sku can not be empty");
                }

                if (value.Length > 6)
                {
                    throw new ArgumentException("PriceProduct: Sku can not be longer than 6 characters, " + value.Length + " given");
                }

                sku = value;
            }
        }

        public float Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }
    }
}
