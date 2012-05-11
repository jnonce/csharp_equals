using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        // Iterate over a arrays within arrays.  Return all the 2D indices and items
        // within the arrays.
        private static IEnumerable<Tuple<int, int, object>> Indices(object[][] items)
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
                // Run tests
                IEnumerable<TestResult> failures = from result in TestEquals(x, y)
                                                   where (result.Error != null) || (result.Result != shouldEqual)
                                                   select result;

                // Check result
                using (IEnumerator<TestResult> en = failures.GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        Console.WriteLine("! {0} -- {1} !", x, y);
                        do
                        {
                            Console.WriteLine(en.Current);
                        }
                        while (en.MoveNext());
                    }
                }
            }
        }

        private class TestResult
        {
            public MethodInfo TestedMethod { get; set; }
            public bool Result { get; set; }
            public Exception Error { get; set; }
        }

        private static IEnumerable<TestResult> TestEquals(object x, object y)
        {
            if (ReferenceEquals(x, null))
            {
                throw new ArgumentNullException("x");
            }

            Type yType = Object.ReferenceEquals(y, null) ? null : y.GetType();

            foreach (var item in from method in x.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                    let paras = method.GetParameters()
                                    where method.Name == "Equals" && paras.Length == 1 && method.ReturnType == typeof(bool)
                                    select new { Method = method, Param = paras[0] })
            {
                if ((yType == null) || item.Param.ParameterType.IsAssignableFrom(yType))
                {
                    var testResult = new TestResult { TestedMethod = item.Method };
                    try
                    {
                        testResult.Result = (bool)item.Method.Invoke(x, new object[] { y });
                    }
                    catch (Exception ex)
                    {
                        testResult.Error = ex;
                    }
                    yield return testResult;
                }
            }
        }
    }


}
