using System;
using System.Collections.Generic;
using System.Text;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Lynda.Test.AbstractGeneral
{	    
    
	/// <summary>
	/// Represents an abstract web page.
	/// </summary>
	public abstract class AbstractPage1
    {     
        /// <summary>
        /// Waits for page to load by validating that a Ranorex.WebElement exists.
        /// </summary>
        /// <param name="webElement">The Ranorex.Webelement to validate</param></param>
        protected void WaitForLoad(WebElement webElement)
        {
        	try
        	{
        		Validate.Exists(webElement);
        	}
        	catch (Ranorex.ValidationException e)
            {
                throw new Exception(String.Format("Failed to wait for page to load since failed to validate existence of {0}.", webElement.GetPath().ToString()), e);
            }
        }
    }
	


}
