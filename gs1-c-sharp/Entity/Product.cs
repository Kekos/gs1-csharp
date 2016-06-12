using System;

namespace Kekos.Gs1.Entity
{
    public class Product : AbstractEntity
    {
        private string sku;
        private string company_prefix;

        public Product()
        {
        }

        public Product(string _sku)
        {
            Sku = _sku;
        }

        public Product(string _sku, string _company_prefix)
        {
            Sku = _sku;
            CompanyPrefix = _company_prefix;
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
                    throw new ArgumentException("Product: Sku was not numeric");
                }

                if (value.Length < 1)
                {
                    throw new ArgumentException("Product: Sku can not be shorter than 3 characters, " + value.Length + " given");
                }

                if (value.Length > 6)
                {
                    throw new ArgumentException("Product: Sku can not be longer than 6 characters, " + value.Length + " given");
                }

                sku = value;
            }
        }

        public string CompanyPrefix
        {
            get
            {
                return company_prefix;
            }

            set
            {
                int i;
                if (!int.TryParse(value, out i))
                {
                    throw new ArgumentException("Product: CompanyPrefix was not numeric");
                }

                if (value.Length < 6)
                {
                    throw new ArgumentException("Product: CompanyPrefix can not be shorter than 6 characters, " + value.Length + " given");
                }

                if (value.Length > 9)
                {
                    throw new ArgumentException("Product: CompanyPrefix can not be longer than 9 characters, " + value.Length + " given");
                }

                company_prefix = value;
            }
        }
    }
}
