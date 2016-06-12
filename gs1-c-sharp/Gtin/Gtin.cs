using System;

namespace Kekos.Gs1.Gtin
{
    public abstract class Gtin
    {
        private string code = "";

        public string Code
        {
            get
            {
                if (code.Length == (length - 1))
                {
                    code += "" + GetChecksum();
                }

                return code;
            }

            set
            {
                ValidateCode(value);
                code = value;
            }
        }

        protected int length;

        public Gtin()
        {
        }

        public Gtin(string code)
        {
            Code = code;
        }

        public bool IsValid()
        {
            if (code.Length == length)
            {
                return (GetChecksum() == int.Parse("" + code[length - 1]));
            }

            return false;
        }

        public int GetChecksum()
        {
            int sum = 0;
            bool weightflag = true;

            for (int i = (length - 2); i >= 0; i--)
            {
                sum += ((int)(code[i] - '0')) * (weightflag ? 3 : 1);
                weightflag = !weightflag;
            }

            return (10 - (sum % 10)) % 10;
        }

        protected void ValidateCode(string code)
        {
            long i;
            if (!long.TryParse(code, out i))
            {
                throw new ArgumentException("Gtin: code was not numeric");
            }
        }

        public void SetPart(ushort maxlength, string code_part)
        {
            if (code_part.Length > maxlength)
            {
                throw new ArgumentException("Gtin: code part was to long");
            }

            ValidateCode(code_part);
            code += code_part.PadLeft(maxlength, '0');
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
