using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Tests.General.Utilities.Forms
{
    public static class FormDataAccount
    {
    	//TODO: Add one method for first and lastname (since both use TEST)
    	
    	public static string GenerateFirstName()
    	{
    		//TODO: Add enum parameter to switch; determine format of string to return
    		return "TESTfirstname";
    	}
    	
    	public static string GenerateLastName()
    	{
    		return "test";
    	}
    	
    	public static void GenerateUsernameEmail(out string username, out string email)
    	{
    		System.DateTime currentDateTime = System.DateTime.Now;    				
			//short date from current DateTime e.g. "2/20/2012". Uses current culture e.g. mm/dd/yyyy
			string shortDate = currentDateTime.ToShortDateString();
			//Format for use as valid e-mail address e.g. 2-20-2012
			shortDate = shortDate.Replace('/', '-');						
            //Fraction of the day that has elapsed since midnight
			TimeSpan timeSinceMidnight = currentDateTime.TimeOfDay;
			//Seconds since midnight e.g. 47003.9293606
            double secondsSinceMidnight = timeSinceMidnight.TotalSeconds;
            //Format lyndaqa-"shortDate"-"secondsSinceMidnight" to no decimal places.
            //e.g. lyndaqa-2-20-2012-47004
            username = String.Format("lyndaqa-{0}-{1:0.}", shortDate, secondsSinceMidnight);            
            email = String.Format("{0}@mailinator.com", username);  
    	}
    	
    	public static string GeneratePassword()
    	{
    		return "lynda1";
    	}

	}

}
