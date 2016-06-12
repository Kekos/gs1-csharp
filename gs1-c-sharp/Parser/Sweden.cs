using System;
using Kekos.Gs1.Entity;
using Kekos.Gs1.Gtin;

namespace Kekos.Gs1.Parser
{
    class Sweden : IParser
    {
        private static int[] weight_dividers = { 0, 0, 0, 1000, 100, 10 };
        private static int[] price_dividers = { 100, 10, 0 };

        public AbstractEntity Parse(Gtin.Gtin gtin)
        {
            AbstractEntity entity = null;
            string code = gtin.Code;

            if (gtin is Gtin13)
            {
                int mode = int.Parse(code[0].ToString());

                switch (mode)
                {
                    case 2:
                        // Weight or price
                        int mode2 = int.Parse(code[1].ToString());

                        if (mode2 >= 0 && mode2 <= 2)
                        {
                            int divider = price_dividers[mode2];
                            float price = int.Parse(code.Substring(8, 4));

                            entity = new PriceProduct(code.Substring(2, 6), (divider == 0 ? price : price / divider));
                        }
                        else if (mode2 >= 3 && mode2 <= 5)
                        {
                            int divider = weight_dividers[mode2];
                            float weight = int.Parse(code.Substring(8, 4));

                            entity = new WeightProduct(code.Substring(2, 6), weight / divider);
                        }

                        break;

                    case 7:
                        if (code.Substring(0, 4) == "7388")
                        {
                            // Publication
                            entity = new Publication(code.Substring(4, 4), float.Parse(code.Substring(8, 4)) / 10);
                        }

                        break;

                    case 9:
                        // Coupon
                        entity = new Coupon(code.Substring(2, 6), float.Parse(code.Substring(8, 4)) / 10);
                        break;

                    default:
                        entity = new Product(code.Substring(6, 6), code.Substring(0, 6)); 
                        break;
                }
            }
            else
            {
                entity = new Product(code.Substring(6, 1), code.Substring(0, 6));
            }

            return entity;
        }
    }
}
