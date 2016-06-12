using System;

namespace Kekos.Gs1.Gtin
{
    public class Gtin8 : Gtin
    {
        public Gtin8()
        {
            length = 8;
        }

        public Gtin8(string code)
        {
            length = 8;
            Code = code;
        }

        public new string Code
        {
            get
            {
                return base.Code;
            }

            set
            {
                if (value.Length > 8)
                {
                    throw new ArgumentException("Gtin8: Code can not be longer than 8 characters, " + value.Length + " given");
                }

                if (value.Length < 7)
                {
                    throw new ArgumentException("Gtin8: Code can not be shorter than 7 characters, " + value.Length + " given");
                }

                base.Code = value;
            }
        }
    }
}
