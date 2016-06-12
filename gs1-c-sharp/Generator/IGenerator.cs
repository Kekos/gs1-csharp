using System;

namespace Kekos.Gs1.Generator
{
    interface IGenerator
    {
        Gtin.Gtin Generate(Entity.AbstractEntity entity);
    }
}
