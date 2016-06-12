# GS1 parser and generator for C#

Encodes and decodes GTIN numbers (EAN-8 and EAN-13) with support for
products having variable weight and price.

## Documentation

### Locales

Each member country of [GS1](http://www.gs1.org/) have their own specification for
products with variable weight and price, magazines and coupons. This is handled
by this library by defining encoding rules in "locales".

Currently only the `Sweden` locale is implemented.

### Generate GTIN

Start by creating an entity:

```C#
Kekos.Gs1.Entity.WeightProduct entity = new Kekos.Gs1.Entity.WeightProduct(sku, weight);
Kekos.Gs1.Entity.PriceProduct entity = new Kekos.Gs1.Entity.PriceProduct(sku, price);
Kekos.Gs1.Entity.Publication entity = new Kekos.Gs1.Entity.Publication(sku, price);
Kekos.Gs1.Entity.Coupon entity = new Kekos.Gs1.Entity.Coupon(id, discount);
```

Use `GtinFactory` class by specifying which locale to use:

```C#
Kekos.Gs1.Gtin.Gtin code = Kekos.Gs1.GtinFactory.Get("Sweden", entity);
Console.WriteLine(code);
```

### Parse GTIN

Create a GTIN entity:

```C#
Kekos.Gs1.Gtin.Gtin13 gtin = new Kekos.Gs1.Gtin.Gtin13(code);
Kekos.Gs1.Gtin.Gtin8 gtin = new Kekos.Gs1.Gtin.Gtin8(code);
```

Use `EntityFactory` class by specifying which locale to use:

```C#
Kekos.Gs1.Entity.AbstractEntity entity = Kekos.Gs1.EntityFactory.Get("Sweden", gtin);
if (entity is Kekos.Gs1.Entity.WeightProduct)
{
  Console.WriteLine("Weight: " + entity.Weight);
}
```

### Product entity

```C#
Kekos.Gs1.Entity.Product product = new Kekos.Gs1.Entity.Product(string sku, string company_prefix);
// Properties
product.Sku;
product.CompanyPrefix;
```

### Weight product entity

```C#
Kekos.Gs1.Entity.WeightProduct product = new Kekos.Gs1.Entity.WeightProduct(string sku, float weight);
// Properties
product.Sku;
product.Weight;
```

### Price product entity

```C#
Kekos.Gs1.Entity.PriceProduct product = new Kekos.Gs1.Entity.PriceProduct(string sku, float price);
// Properties
product.Sku;
product.Price;
```

### Coupon entity

```C#
Kekos.Gs1.Entity.Coupon coupon = new Kekos.Gs1.Entity.Coupon(string id, float value);
// Properties
coupon.Id;
coupon.Value;
```

### Publication entity

```C#
Kekos.Gs1.Entity.Publication publication = new Kekos.Gs1.Entity.Publication(string sku, float price);
// Properties
publication.Sku;
publication.Price;
```

### GTIN-13 entity (EAN-13) and GTIN-8 entity (EAN-8)

When setting the code you don't have to specify the checksum (digit 8 or 13).
The `Gtin` classes will add the checksum automatically if needed.

```C#
Kekos.Gs1.Gtin.Gtin8 gtin = new Kekos.Gs1.Gtin.Gtin8(string code);
// ...or
Kekos.Gs1.Gtin.Gtin13 gtin = new Kekos.Gs1.Gtin.Gtin13(string code);

// Properties
gtin.Code;

// Methods
gtin.IsValid(); // returns int
gtin.GetChecksum(); // returns bool
gtin.ToString();
```

## Bugs and improvements

Report bugs in GitHub issues or feel free to make a pull request :-)

## License

MIT
