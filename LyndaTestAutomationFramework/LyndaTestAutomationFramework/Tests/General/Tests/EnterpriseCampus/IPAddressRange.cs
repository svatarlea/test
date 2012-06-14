/*
 * Created by Ranorex
 * User: jalex
 * Date: 6/6/2012
 * Time: 3:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using Ranorex;

namespace Tests.General.Tests.EnterpriseCampus
{
	/// <summary>
	/// Description of IPAddressRange.
	/// </summary>
	public class IPAddressRange
	{
		private System.Net.Sockets.AddressFamily addressFamily;
        private byte[] lowerBytes;
        private byte[] upperBytes;

        public IPAddressRange(string iplower, string ipupper)
        {

            IPAddress lower = null;
            IPAddress upper = null;
            try
            {
                lower = IPAddress.Parse(iplower);
                upper = IPAddress.Parse(ipupper);
            }
            catch (Exception e)
            {
            	Report.Log(ReportLevel.Error, e.Message);
            }
            
            // Validate that lower.AddressFamily == upper.AddressFamily 
            Validate.AreEqual(lower.AddressFamily, upper.AddressFamily);

            this.addressFamily = lower.AddressFamily;
            this.lowerBytes = lower.GetAddressBytes();
            this.upperBytes = upper.GetAddressBytes();
        }

        public bool IsInRange(string chkaddress)
        {
            IPAddress address = null;
            try
            {
                address = IPAddress.Parse(chkaddress);
                
            }
            catch (Exception e)
            {
            	Report.Log(ReportLevel.Error, e.Message);
            }

            if (address.AddressFamily != addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < this.lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == upperBytes[i]);
            }

            return true;
        }
        	
    }
	
}
