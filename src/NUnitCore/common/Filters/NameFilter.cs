#region Copyright (c) 2002-2003, James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole, Philip A. Craig
/************************************************************************************
'
' Copyright � 2002-2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' Copyright � 2000-2003 Philip A. Craig
'
' This software is provided 'as-is', without any express or implied warranty. In no 
' event will the authors be held liable for any damages arising from the use of this 
' software.
' 
' Permission is granted to anyone to use this software for any purpose, including 
' commercial applications, and to alter it and redistribute it freely, subject to the 
' following restrictions:
'
' 1. The origin of this software must not be misrepresented; you must not claim that 
' you wrote the original software. If you use this software in a product, an 
' acknowledgment (see the following) in the product documentation is required.
'
' Portions Copyright � 2003 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole
' or Copyright � 2000-2003 Philip A. Craig
'
' 2. Altered source versions must be plainly marked as such, and must not be 
' misrepresented as being the original software.
'
' 3. This notice may not be removed or altered from any source distribution.
'
'***********************************************************************************/
#endregion

using System;
using System.Collections;

namespace NUnit.Core.Filters
{
	/// <summary>
	/// Summary description for NameFilter.
	/// </summary>
	/// 
	[Serializable]
	public class NameFilter : ITestFilter
	{
		private ArrayList testNames = new ArrayList();

		/// <summary>
		/// Construct an empty NameFilter
		/// </summary>
		public NameFilter() { }

		/// <summary>
		/// Construct a NameFilter for a single TestName
		/// </summary>
		/// <param name="testName"></param>
		public NameFilter( TestName testName )
		{
			testNames.Add( testName );
		}

		/// <summary>
		/// Add a TestName to a NameFilter
		/// </summary>
		/// <param name="testName"></param>
		public void Add( TestName testName )
		{
			testNames.Add( testName );
		}

		/// <summary>
		/// Test the filter on a given test node
		/// </summary>
		/// <param name="test"></param>
		/// <returns></returns>
		public bool Pass(ITest test)
        {
			foreach (TestName testName in testNames )
			{
				if ( test.TestName == testName )
					return true;

				if ( IsDescendantOf( testName, test ) )
					return true;

				if ( IsParentOf( testName, test ) )
					return true;
			}

			return false;
        }

		private bool IsDescendantOf( TestName testName, ITest test )
		{
			for( ITest parent = test.Parent; parent != null; parent = parent.Parent )
			{
				if ( parent.TestName == testName )
					return true;
			}

			return false;
		}

		private bool IsParentOf( TestName testName, ITest test )
		{
			if ( !test.IsSuite || test.Tests == null )
				return false;

			foreach( ITest child in test.Tests )
			{
				if ( child.TestName == testName )
					return true;
			}

			foreach( ITest child in test.Tests )
			{
				if ( IsParentOf( testName, child ) )
					return true;
			}

			return false;
		}
	}
}
