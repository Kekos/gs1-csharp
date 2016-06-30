using System;
using System.Globalization;
using Kekos.Gs1.Entity;
using Kekos.Gs1.Gtin;

namespace Kekos.Gs1.Generator
{
    class Sweden : IGenerator
    {
        private static int[] weight_modulators = { 0, 5, 4, 3 };
        private static int[] price_modulators = { 2, 1, 0 };

        public Gtin.Gtin Generate(AbstractEntity entity)
        {
            Gtin.Gtin gtin = null;
            IFormatProvider numberFormat = CultureInfo.InvariantCulture;

            if (entity is Product)
            {
                Product product = (Product)entity;

                try
                {
                    gtin = new Gtin8();
                    gtin.SetPart(6, product.CompanyPrefix);
                    gtin.SetPart(1, product.Sku);

                }
                catch (ArgumentException)
                {
                    gtin = new Gtin13();
                    gtin.SetPart(9, product.CompanyPrefix);
                    gtin.SetPart(3, product.Sku);
                }
            }
            else if (entity is WeightProduct)
            {
                gtin = new Gtin13();
                WeightProduct product = (WeightProduct)entity;
                decimal weight = (decimal)product.Weight;
                int decimals = GetDecimalsCount(weight);
                int modulator;
                string weight_str = SubstringMax(weight.ToString(numberFormat).Replace(".", ""), 0, 4);

                if (decimals == 0)
                {
                    decimals = 3;
                    weight_str += "000";
                }
                else
                {
                    decimals = Math.Max(1, Math.Min(3, decimals));
                }

                modulator = weight_modulators[decimals];

                gtin.SetPart(2, "2" + modulator);
                gtin.SetPart(6, product.Sku);
                gtin.SetPart(4, weight_str);
            }
            else if (entity is PriceProduct)
            {
                gtin = new Gtin13();
                PriceProduct product = (PriceProduct)entity;
                decimal price = (decimal)product.Price;
                int decimals = GetDecimalsCount(price);
                int modulator;
                string price_str = SubstringMax(price.ToString(numberFormat).Replace(".", ""), 0, 4);

                decimals = Math.Max(0, Math.Min(2, decimals));
                modulator = price_modulators[decimals];

                gtin.SetPart(2, "2" + modulator);
                gtin.SetPart(6, product.Sku);
                gtin.SetPart(4, price_str);
            }
            else if (entity is Publication)
            {
                gtin = new Gtin13();
                Publication product = (Publication)entity;
                decimal price = (decimal)product.Price;
                int decimals = GetDecimalsCount(price);
                string price_str = price.ToString(numberFormat).Replace(".", "");

                if (decimals == 0)
                {
                    price_str = SubstringMax(price_str, 0, 3) + "0";
                }
                else
                {
                    price_str = SubstringMax(price_str, 0, 4);
                }

                gtin.SetPart(4, "7388");
                gtin.SetPart(4, product.Sku);
                gtin.SetPart(4, price_str);
            }
            else if (entity is Coupon)
            {
                gtin = new Gtin13();
                Coupon coupon = (Coupon)entity;
                decimal value = (decimal)coupon.Value;
                int decimals = GetDecimalsCount(value);
                string value_str = value.ToString(numberFormat).Replace(".", "");

                if (decimals == 0)
                {
                    value_str = SubstringMax(value_str, 0, 3) + "0";
                }
                else
                {
                    value_str = SubstringMax(value_str, 0, 4);
                }

                gtin.SetPart(2, "99");
                gtin.SetPart(6, coupon.Id);
                gtin.SetPart(4, value_str);
            }

            return gtin;
        }

        private int GetDecimalsCount(decimal number)
        {
            return BitConverter.GetBytes(decimal.GetBits(number)[3])[2];
        }

        private string SubstringMax(string str, int startIndex, int length)
        {
            int trim_length = Math.Min(length, str.Length - startIndex);
            return str.Substring(startIndex, trim_length);
        }
    }
}
