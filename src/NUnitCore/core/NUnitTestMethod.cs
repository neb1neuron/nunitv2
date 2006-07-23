using System;
using System.Reflection;

namespace NUnit.Core
{
	/// <summary>
	/// Class to implement an NUnit test method
	/// </summary>
	public class NUnitTestMethod : TestMethod
	{
		#region Constructor
		public NUnitTestMethod(MethodInfo method) : base(method) { }
		#endregion

		#region TestMethod Overrides
		/// <summary>
		/// Run a test returning the result. Overrides TestMethod
		/// to count assertions.
		/// </summary>
		/// <param name="testResult"></param>
		public override void Run(TestCaseResult testResult)
		{
			base.Run(testResult);

			testResult.AssertCount = NUnitFramework.GetAssertCount();
		}

		/// <summary>
		/// Determine if an exception is an NUnit AssertionException
		/// </summary>
		/// <param name="ex">The exception to be examined</param>
		/// <returns>True if it's an NUnit AssertionException</returns>
		protected override bool IsAssertException(Exception ex)
		{
			return NUnitFramework.IsAssertException( ex );
		}

		/// <summary>
		/// Determine if an exception is an NUnit IgnoreException
		/// </summary>
		/// <param name="ex">The exception to be examined</param>
		/// <returns>True if it's an NUnit IgnoreException</returns>
		protected override bool IsIgnoreException(Exception ex)
		{
			return NUnitFramework.IsIgnoreException( ex );
		}
		#endregion
	}
}