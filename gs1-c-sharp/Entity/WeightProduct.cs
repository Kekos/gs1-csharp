using System;

namespace Kekos.Gs1.Entity
{
    public class WeightProduct : AbstractEntity
    {
        private string sku;
        private float weight;

        public WeightProduct()
        {
        }

        public WeightProduct(string _sku)
        {
            Sku = _sku;
        }

        public WeightProduct(string _sku, float _weight)
        {
            Sku = _sku;
            Weight = _weight;
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
                    throw new ArgumentException("WeightProduct: Sku was not numeric");
                }

                if (value.Length == 0)
                {
                    throw new ArgumentException("WeightProduct: Sku can not be empty");
                }

                if (value.Length > 6)
                {
                    throw new ArgumentException("WeightProduct: Sku can not be longer than 6 characters, " + value.Length + " given");
                }

                sku = value;
            }
        }

        public float Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }
    }
}
