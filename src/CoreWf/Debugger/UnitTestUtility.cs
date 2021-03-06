// This file is part of Core WF which is licensed under the MIT license.
// See LICENSE file in the project root for full license information.

namespace CoreWf.Debugger
{
    using CoreWf.Internals;
    using CoreWf.Runtime;
    using System;
    using System.Runtime;

    internal static class UnitTestUtility
    {
        internal static Func<string, Exception> AssertionExceptionFactory
        {
            get;
            set;
        }

        internal static void TestInitialize(Func<string, Exception> createAssertionException)
        {
            UnitTestUtility.AssertionExceptionFactory = createAssertionException;
        }

        internal static void TestCleanup()
        {
            UnitTestUtility.AssertionExceptionFactory = null;
        }

        internal static void Assert(bool condition, string assertionMessage)
        {
            if (UnitTestUtility.AssertionExceptionFactory != null)
            {
                if (!condition)
                {
                    throw FxTrace.Exception.AsError(UnitTestUtility.AssertionExceptionFactory(assertionMessage));
                }
            }
            else
            {
                Fx.Assert(condition, assertionMessage);
            }
        }
    }
}
