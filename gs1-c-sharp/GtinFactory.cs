using System;
using Kekos.Gs1.Gtin;
using Kekos.Gs1.Entity;
using Kekos.Gs1.Generator;

namespace Kekos.Gs1
{
    public class GtinFactory
    {
        public static Gtin.Gtin Get(string locale, AbstractEntity entity)
        {
            string classname = "Kekos.Gs1.Generator." + locale;
            Type generatorType = Type.GetType(classname);

            IGenerator generator = (IGenerator) Activator.CreateInstance(generatorType);
            return (Gtin.Gtin) generator.Generate(entity);
        }
    }
}
