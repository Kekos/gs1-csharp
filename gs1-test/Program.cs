using System;
using System.Text;
using Kekos.Gs1;
using Kekos.Gs1.Entity;
using Kekos.Gs1.Gtin;

namespace Kekos.Gs1.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string mode;

            while (true)
            {
                Console.WriteLine("GS1 GTIN generator & parser");
                Console.WriteLine("===========================\n");

                Console.WriteLine("Select mode:");
                Console.WriteLine(" G - Generate");
                Console.WriteLine(" P - Parse");

                mode = Console.ReadLine();

                switch (mode)
                {
                    case "G":
                        Generate();
                        break;

                    case "P":
                        Parse();
                        break;

                    default:
                        Console.WriteLine("Invalid mode");
                        break;
                }
            }
        }

        static void Generate()
        {
            try
            {
                Console.WriteLine("GS1 GTIN generator");
                Console.WriteLine("==================\n");

                Console.WriteLine("Select entity type:");
                Console.WriteLine(" 0 - Weight");
                Console.WriteLine(" 1 - Price");
                Console.WriteLine(" 2 - Publication");
                Console.WriteLine(" 3 - Coupon");

                int choice = int.Parse(Console.ReadLine());
                string sku = "";
                string id = "";
                float price = 0;
                float weight = 0;
                string code = "";
                AbstractEntity product;

                if (choice < 3)
                {
                    Console.WriteLine("Enter SKU: ");
                    sku = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Enter ID: ");
                    id = Console.ReadLine();
                }

                if (choice > 0)
                {
                    Console.WriteLine("Enter Price: ");
                    price = float.Parse(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("Enter Weight: ");
                    weight = float.Parse(Console.ReadLine());
                }

                switch (choice)
                {
                    case 0:
                        product = new WeightProduct(sku, weight);
                        break;

                    case 1:
                        product = new PriceProduct(sku, price);
                        break;

                    case 2:
                        product = new Publication(sku, price);
                        break;

                    case 3:
                        product = new Coupon(id, price);
                        break;

                    default:
                        product = null;
                        break;
                }

                code = GtinFactory.Get("Sweden", product).ToString();

                Console.WriteLine("GTIN code: " + code);
                Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Fel in-värde");
            }
        }

        static void Parse()
        {
            try
            {
                Console.WriteLine("GS1 GTIN parser");
                Console.WriteLine("===============\n");

                Console.WriteLine("Enter GTIN (8 or 13 chars):");

                string code = Console.ReadLine();
                Gtin.Gtin gtin;

                if (code.Length == 8)
                {
                    gtin = new Gtin8(code);
                }
                else if (code.Length == 13)
                {
                    gtin = new Gtin13(code);
                }
                else
                {
                    throw new ArgumentException("GTIN must be 8 or 13 characters");
                }

                if (!gtin.IsValid())
                {
                    throw new ArgumentException("GTIN checksum not valid");
                }

                AbstractEntity entity = EntityFactory.Get("Sweden", gtin);

                if (entity is Product)
                {
                    Product product = (Product) entity;
                    Console.WriteLine("Company Prefix: " + product.CompanyPrefix);
                    Console.WriteLine("SKU: " + product.Sku);
                }
                else if (entity is WeightProduct)
                {
                    WeightProduct product = (WeightProduct)entity;
                    Console.WriteLine("SKU: " + product.Sku);
                    Console.WriteLine("Weight: " + product.Weight);
                }
                else if (entity is PriceProduct)
                {
                    PriceProduct product = (PriceProduct)entity;
                    Console.WriteLine("SKU: " + product.Sku);
                    Console.WriteLine("Price: " + product.Price);
                }
                else if (entity is Coupon)
                {
                    Coupon coupon = (Coupon)entity;
                    Console.WriteLine("ID (Coupon): " + coupon.Id);
                    Console.WriteLine("Discount: " + coupon.Value);
                }
                else if (entity is Publication)
                {
                    Publication publication = (Publication)entity;
                    Console.WriteLine("SKU (Publication): " + publication.Sku);
                    Console.WriteLine("Price: " + publication.Price);
                }

                Console.ReadLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Fel in-värde");
            }
        }
    }
}
