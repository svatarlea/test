using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Tests.General.Utilities.Forms
{
    public static class FormDataBilling
    {
    	//TODO: Add GenerateData method for multiple enums (e.g. address,city, zip etc).

    	public static void GenerateAddress(out string companyName, out string address, out string aptSuite,
    	                                   out string city, out string state, out string zip, out string country,
    	                                   out string phone)
    	{
    		companyName = "lynda.com";
    		address = "6410 via real";
    		aptSuite = string.Empty;
    		city = "Carpinteria";
    		state = "California";
    		zip = "93013";
    		country = "United States";
    		phone = "5555555555";
    	}
    	
    	public static string GenerateHowDidYouHear()
    	{
    		return "Radio";
    	}



	}

}
