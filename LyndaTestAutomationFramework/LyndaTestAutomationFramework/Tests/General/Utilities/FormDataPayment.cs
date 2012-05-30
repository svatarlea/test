using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Tests.General.Utilities.Forms
{
    public static class FormDataPayment
    {

    	public static void GenerateCreditCard(out string paymentType, out string cardType, out string cardNumber,
    	                                      out string nameOnCard, out string cardSecurityCode,
    	                                      out string expireMonth, out string expireYear)
    	{
    		paymentType = "credit card";
    		cardType = "Visa";
    		cardNumber = "4111111111111111";
    		nameOnCard = "Sue Axelband";
    		cardSecurityCode = "411";
    		expireMonth = "08 - August";
    		expireYear = "2012";
    	}

	}

}
