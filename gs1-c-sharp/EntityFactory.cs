using System;
using Kekos.Gs1.Entity;
using Kekos.Gs1.Parser;

namespace Kekos.Gs1
{
    public class EntityFactory
    {
        public static AbstractEntity Get(string locale, Gtin.Gtin gtin)
        {
            string classname = "Kekos.Gs1.Parser." + locale;
            Type parserType = Type.GetType(classname);

            IParser parser = (IParser)Activator.CreateInstance(parserType);
            return (AbstractEntity)parser.Parse(gtin);
        }
    }
}
