using System;
using System.Collections.Generic;
using System.Text;

namespace MixPanelHttpClient
{
    public class DataInputCleaning
    {
        public static void validateNoPrimitiveTypes<T>()
        {
            if (typeof(string).Equals(typeof(T)) ||
                typeof(int).Equals(typeof(T)) ||
                typeof(double).Equals(typeof(T)) ||
                typeof(float).Equals(typeof(T))
                ) throw new NotSupportedException("Type must be a");
        }

        public static void validateOnlyPrimitiveTypes<T>()
        {
            Console.WriteLine(typeof(T));
            if (!typeof(string).Equals(typeof(T)))
                return;
            if (!typeof(int).Equals(typeof(T)))
                return;
            if (!typeof(double).Equals(typeof(T)))
                return;
            if (!typeof(float).Equals(typeof(T)))
                return;

            throw new NotSupportedException("Type must be string, int, double or float only");
        }
    }
}
