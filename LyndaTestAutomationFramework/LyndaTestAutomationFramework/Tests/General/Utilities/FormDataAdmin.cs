using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace General.Utilities.Forms
{
    public static class FormDataAdmin
    {

    	public static void GenerateAdminLogin(out string AdminUsername, out string AdminPassword)    		
    	{
    		//AdminUsername = "mjordan@lynda.com";
    		AdminUsername = "admintester";
    		AdminPassword = "Lynda1";
    	}
    	    	
    	public static void GenerateAdminConsumer(out string ConsumerEmail, out string ConsumerPersona, out string ConsumerPersonaStatus, out string ConsumerProduct)    		
    	{
    		ConsumerEmail = "mjordan@lynda.com";
    		ConsumerPersona = "Consumer";
    		ConsumerPersonaStatus = "Active";
    		ConsumerProduct = "OTL Premium Annual";
    	}
	}

}