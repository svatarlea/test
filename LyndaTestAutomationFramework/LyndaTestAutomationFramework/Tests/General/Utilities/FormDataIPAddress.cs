/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/15/2012
 * Time: 11:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Tests.General.Utilities
{
	
		/// <summary>
	/// Description of FormDataIPAddress.
	/// </summary>
	public static class FormDataIPAddress
	{
		
		// Method to generate unique IPAddresses
		public static void GenerateIPAddress(out string strIPAddress1, out string strIPAddress2)
    		{
    			System.DateTime currentDateTime = System.DateTime.Now;
            		int sumofHradMins = currentDateTime.Hour + currentDateTime.Minute;
            		strIPAddress1 = String.Format("{0}.{1}.{2}.{3}", currentDateTime.Month.ToString(), currentDateTime.Day.ToString(), sumofHradMins.ToString(), currentDateTime.Second.ToString());
            		strIPAddress2 = String.Format("{0}.{1}.{2}.{3}", currentDateTime.Month.ToString(), currentDateTime.Day.ToString(), (sumofHradMins+2).ToString(), currentDateTime.Second.ToString());
            
    		}
	}
		
}
