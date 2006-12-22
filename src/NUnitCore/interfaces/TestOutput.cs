namespace NUnit.Core
{
	using System;

    /// <summary>
    /// The TestOutput class holds a unit of output from 
    /// a test to either stdOut or stdErr
    /// </summary>
	[Serializable]
	public class TestOutput
	{
		string text;
		TestOutputType type;

        /// <summary>
        /// Construct with text and an ouput destination type
        /// </summary>
        /// <param name="text">Text to be output</param>
        /// <param name="type">Destination of output</param>
		public TestOutput(string text, TestOutputType type)
		{
			this.text = text;
			this.type = type;
		}

        /// <summary>
        /// Return string representation of the object for debugging
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
			return type + ": " + text;
		}

        /// <summary>
        /// Get the text 
        /// </summary>
		public string Text
		{
			get
			{
				return this.text;
			}
		}

        /// <summary>
        /// Get the output type
        /// </summary>
		public TestOutputType Type
		{
			get
			{
				return this.type;
			}
		}
	}

    /// <summary>
    /// Enum representing the output destination
    /// </summary>
	public enum TestOutputType
	{
        /// <summary>
        /// Send output to stdOut
        /// </summary>
		Out, 
        
        /// <summary>
        /// Send output to stdErr
        /// </summary>
        Error,

		/// <summary>
		/// Send output to Trace
		/// </summary>
		Trace
	}
}
