using System;

namespace Kekos.Gs1.Entity
{
    public class Coupon : AbstractEntity
    {
        private string id;
        private float coupon_value;

        public Coupon()
        {
        }

        public Coupon(string _id)
        {
            Id = _id;
        }

        public Coupon(string _id, float _value)
        {
            Id = _id;
            Value = _value;
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                int i;
                if (!int.TryParse(value, out i))
                {
                    throw new ArgumentException("Coupon: Id was not numeric");
                }

                if (value.Length == 0)
                {
                    throw new ArgumentException("Coupon: Id can not be empty");
                }

                if (value.Length > 6)
                {
                    throw new ArgumentException("Coupon: Id can not be longer than 6 characters, " + value.Length + " given");
                }

                id = value;
            }
        }

        public float Value
        {
            get
            {
                return coupon_value;
            }

            set
            {
                coupon_value = value;
            }
        }
    }
}
