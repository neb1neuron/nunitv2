using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Tests
{
	[TestFixture]
	public class AssertThrowsTests : MessageChecker
	{
		[Test]
		public void CorrectExceptionThrown()
		{
#if NET_2_0
            Assert.Throws(typeof(ArgumentException), TestDelegates.ThrowsArgumentException);
            Assert.Throws(typeof(ArgumentException),
                delegate { throw new ArgumentException(); });

            Assert.Throws<ArgumentException>(
                delegate { throw new ArgumentException(); });
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsArgumentException);
#else
			Assert.Throws(typeof(ArgumentException),
				new TestDelegate( TestDelegates.ThrowsArgumentException ) );
#endif
        }

		[Test]
		public void CorrectExceptionIsReturnedToMethod()
		{
			ArgumentException ex = Assert.Throws(typeof(ArgumentException),
                new TestDelegate(TestDelegates.ThrowsArgumentException)) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));

#if NET_2_0
            ex = Assert.Throws<ArgumentException>(
                delegate { throw new ArgumentException("myMessage", "myParam"); }) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));

			ex = Assert.Throws(typeof(ArgumentException), 
                delegate { throw new ArgumentException("myMessage", "myParam"); } ) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));

            ex = Assert.Throws<ArgumentException>(TestDelegates.ThrowsArgumentException) as ArgumentException;

            Assert.IsNotNull(ex, "No ArgumentException thrown");
            Assert.That(ex.Message, StartsWith("myMessage"));
            Assert.That(ex.ParamName, Is.EqualTo("myParam"));
#endif
		}

		[Test, ExpectedException(typeof(AssertionException))]
		public void NoExceptionThrown()
		{
			expectedMessage =
				"  Expected: <System.ArgumentException>" + Environment.NewLine +
				"  But was:  null" + Environment.NewLine;
#if NET_2_0
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsNothing);
#else
			Assert.Throws( typeof(ArgumentException),
				new TestDelegate( TestDelegates.ThrowsNothing ) );
#endif
		}

        [Test, ExpectedException(typeof(AssertionException))]
        public void UnrelatedExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.ArgumentException>" + Environment.NewLine +
                "  But was:  <System.ApplicationException>" + Environment.NewLine;
#if NET_2_0
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsApplicationException);
#else
			Assert.Throws( typeof(ArgumentException),
				new TestDelegate(TestDelegates.ThrowsApplicationException) );
#endif
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void BaseExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.ArgumentException>" + Environment.NewLine +
                "  But was:  <System.Exception>" + Environment.NewLine;
#if NET_2_0
            Assert.Throws<ArgumentException>(TestDelegates.ThrowsSystemException);
#else
            Assert.Throws( typeof(ArgumentException),
                new TestDelegate( TestDelegates.ThrowsSystemException) );
#endif
        }

        [Test,ExpectedException(typeof(AssertionException))]
        public void DerivedExceptionThrown()
        {
            expectedMessage =
                "  Expected: <System.Exception>" + Environment.NewLine +
                "  But was:  <System.ArgumentException>" + Environment.NewLine;
#if NET_2_0
            Assert.Throws<Exception>(TestDelegates.ThrowsArgumentException);
#else
            Assert.Throws( typeof(Exception),
				new TestDelegate( TestDelegates.ThrowsArgumentException) );
#endif
        }

        [Test]
        public void DoesNotThrowSuceeds()
        {
#if NET_2_0
            Assert.DoesNotThrow(TestDelegates.ThrowsNothing);
#else
            Assert.DoesNotThrow( new TestDelegate( TestDelegates.ThrowsNothing ) );

			Assert.That( new TestDelegate( TestDelegates.ThrowsNothing ), Throws.Nothing );
#endif
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void DoesNotThrowFails()
        {
#if NET_2_0
            Assert.DoesNotThrow(TestDelegates.ThrowsArgumentException);
#else
            Assert.DoesNotThrow( new TestDelegate( TestDelegates.ThrowsArgumentException ) );
#endif
        }
    }
}
