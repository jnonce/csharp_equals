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
            var objects = new object[][]
            {
                new object[] { null },
                new object[] { new BaseClass(1), new BaseClass(1) },
                new object[] { new BaseClass(2) },
                new object[] { new ClassA(1, 2), new ClassA(1, 2) },
                new object[] { new ClassA(1, 1) },
                new object[] { new ClassA(2, 2) },
            };
            foreach (Tuple<int, int, object> item1 in Indices(objects))
            foreach (Tuple<int, int, object> item2 in Indices(objects))
            {
                bool shouldEqual = item1.Item1 == item2.Item1;
                TestEquals(item1.Item3, item2.Item3, shouldEqual);
            }
        }

        public static IEnumerable<Tuple<int, int, object>> Indices(object[][] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                object[] row = items[i];
                for (int j = 0; j < row.Length; j++)
                {
                    yield return new Tuple<int, int, object>(i, j, row[j]);
                }
            }
        }

        private static void TestEquals(object x, object y, bool shouldEqual)
        {
            bool xNull = ReferenceEquals(x, null);
            bool yNull = ReferenceEquals(y, null);
            if (xNull)
            {
                if (!yNull)
                {
                    TestEquals(y, x, shouldEqual);
                }
            }
            else
            {
                var printHeader = (OnceAction)delegate()
                {
                    Console.WriteLine("! {0} -- {1} !", x, y);
                };
                foreach (var item in from method in x.GetType().GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                     let paras = method.GetParameters()
                                     where method.Name == "Equals" && paras.Length == 1 && method.ReturnType == typeof(bool)
                                     select new { Method = method, Param = paras[0] })
                {
                    if (yNull || item.Param.ParameterType.IsAssignableFrom(y.GetType()))
                    {
                        bool eq = (bool)item.Method.Invoke(x, new object[]{y});
                        if (eq != shouldEqual)
                        {
                            printHeader.Act();
                            Console.WriteLine("{0}, {1}: {2}", item.Method.DeclaringType, item.Param.ParameterType, eq);
                        }
                    }
                }
            }
        }

        private class OnceAction
        {
            private Action action;

            private OnceAction(Action act)
            {
                action = act;
            }

            public static implicit operator OnceAction(Action act)
            {
                return new OnceAction(act);
            }

            public static explicit operator OnceAction(Delegate act)
            {
                var actor = act as Action;
                if (actor == null)
                {
                    actor = delegate() { act.DynamicInvoke(); };
                }
                return new OnceAction(actor);
            }

            public void Act()
            {
                if (action != null)
                {
                    action();
                    action = null;
                }
            }
        }
    }


}
