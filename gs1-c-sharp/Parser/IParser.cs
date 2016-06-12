using System;

namespace Kekos.Gs1.Parser
{
    interface IParser
    {
        Entity.AbstractEntity Parse(Gtin.Gtin gtin);
    }
}
