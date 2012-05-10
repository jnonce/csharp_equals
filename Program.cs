using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace equals
{
    class Program
    {
        static void Main(string[] args)
        {
            var objects = new object[]
            {
                new BaseClass(1),
                new BaseClass(1),
                new BaseClass(2),
                new ClassA(1, 2),
                new ClassA(1, 2),
                new ClassA(1, 1),
                new ClassA(2, 2),
            };
            for (int i = 0; i < objects.Length; i++)
            {
                for (int j = 0; j < objects.Length; j++)
                {
                    TestEquals(objects[i], objects[j]);
                }
            }
        }

        private static void TestEquals(object x, object y)
        {
            bool xNull = ReferenceEquals(x, null);
            bool yNull = ReferenceEquals(y, null);
            if (xNull)
            {
                if (!yNull)
                {
                    TestEquals(y, x);
                }
            }
            else
            {
                Console.WriteLine("! {0} -- {1} !", x, y);
                foreach (var item in from method in x.GetType().GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                     let paras = method.GetParameters()
                                     where method.Name == "Equals" && paras.Length == 1 && method.ReturnType == typeof(bool)
                                     select new { Method = method, Param = paras[0] })
                {
                    if (yNull || item.Param.ParameterType.IsAssignableFrom(y.GetType()))
                    {
                        bool eq = (bool)item.Method.Invoke(x, new object[]{y});
                        Console.WriteLine("{0}, {1}: {2}", item.Method.DeclaringType, item.Param.ParameterType, eq);
                    }
                }
            }
        }
    }


}
