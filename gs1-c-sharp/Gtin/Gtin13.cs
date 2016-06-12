using System;

namespace Kekos.Gs1.Gtin
{
    public class Gtin13 : Gtin
    {
        public Gtin13()
        {
            length = 13;
        }

        public Gtin13(string code)
        {
            length = 13;
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
                if (value.Length > 13)
                {
                    throw new ArgumentException("Gtin13: Code can not be longer than 13 characters, " + value.Length + " given");
                }

                if (value.Length < 12)
                {
                    throw new ArgumentException("Gtin13: Code can not be shorter than 12 characters, " + value.Length + " given");
                }

                base.Code = value;
            }
        }
    }
}
