using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Lynda.Test.Advanced.Utilities.WebPages
{
    /// <summary>
    /// Represents methods to handle a web page select tag.
    /// </summary>
	public static class SelectTagUI
    {
        /// <summary>
        /// Clicks or selects a web page select tag option.
        /// </summary>
        /// <param name="pathToDOM">Ranorex.Core.Repository.RepoGenBaseFolder.BaseBath of a web page DOM.</param>
        /// <param name="selecttag">Ranorex.SelectTag of a web page select tag.</param>
        /// <param name="option">option to select in select tag.</param>
		public static void ChooseSelectTagOption(string pathToDOM, Ranorex.SelectTag selecttag, string option)
		{
            WebDocument domWebDocument = pathToDOM;
            
	        //Handle InnerText that contains white space. e.g.:
			//	InnerText="credit card" or
			//	InnerText="      credit card  " (where there is white space before and/or after)
			//and option = "credit card"
			//Regular expression covers white space before or after the option string	
            string patternOptionWhiteSpace =  String.Format(@"^\s*{0}\s*$",option);
            
            switch (domWebDocument.BrowserName)
            {
            	case "Chrome":
            	case "Safari":
            		{
		            	//1. Find TagValue for requested option
			        	string tagValueToSelect = null;
			        	bool tagOptionInList=false;
			        	IList<OptionTag> tagOptions = selecttag.Options;
			        	foreach (OptionTag optionTag in tagOptions)
			        	{	        		
                    		if (Regex.IsMatch(optionTag.InnerText, patternOptionWhiteSpace))                    		
			        		{
			        			tagValueToSelect = optionTag.TagValue;
			        			tagOptionInList=true;
			        			break;
			        		}
			        	}
			        	if (!tagOptionInList)
			        	{
			        		throw new ArgumentException(string.Format("None of the SelectTag {0} OptionTags have InnerText of {1}", selecttag.Name, option), "option");
			        	}
			        	//2. Press Down key until current option is requested option
			        	selecttag.Focus();
			        	bool tagOptionSelected=false;
			        	while (!tagOptionSelected)
			        	{
			        		string currentTagValue = selecttag.TagValue;
			        		if (currentTagValue == tagValueToSelect)
			        		{
			        			tagOptionSelected=true;
			        		}
			        		else
			        		{
			        			Keyboard.Press("{DOWN}");
			        			//Did option change?
			        			if (selecttag.TagValue == currentTagValue)
			        			{
			        				//No, so on last option
			        				break;
			        			}
			        		}
			        	}
			        	if (!tagOptionSelected)
			        	{
			        		throw new Exception(string.Format("None of the selected SelectTag {0} options had a TagValue of {1}", selecttag.Name, tagValueToSelect));
			        	}
		            	break;
            		}
            	case "IE":
		            String itemToClickRxPath = String.Format("/container[@caption='selectbox']/listitem[@accessiblename='{0}']",option);
		        	selecttag.Focus();
		        	selecttag.Click();
		        	ListItem itemToClick = itemToClickRxPath;
		        	itemToClick.Click();            		
            		break;            	
            	case "Mozilla":
            		selecttag.Focus();
        			selecttag.Click();
        			String optionRxPath = String.Format(@"option[@InnerText~'{0}']",patternOptionWhiteSpace);
        			OptionTag optionToClick;
        			bool result = selecttag.TryFindSingle<OptionTag>(optionRxPath,out optionToClick);  
        			
        			if (!result)
        			{
        				throw new ArgumentException(string.Format("TryFindSingle failed to find option {0} to click", optionRxPath),"option");
        			}
        			optionToClick.Click();
            		break;
            	default:
            		break;                		      
            }

        }

        /// <summary>
        /// Gets the current text shown in a web page select tag.
        /// </summary>
        /// <param name="selectTag">Ranorex.SelectTag of a web page select tag.</param>
        /// <returns>The text shown in the select tag.</returns>
		public static string GetSelectTagCurrentText(Ranorex.SelectTag selectTag)
        {
        	//Select tag's current value, e.g. -1 = "select one...", 17 = "California"
        	String tagValue = selectTag.TagValue;   
        	//Option tags contained within the select tag
        	IList<OptionTag> tagOptions = selectTag.Options;
        	//Get InnerText of tag's currently selected option tag
        	foreach (OptionTag optionTag in tagOptions)
        	{
        		if (optionTag.Value == tagValue)
        		{
        			return optionTag.InnerText;
        		}
        	}
        	throw new ArgumentException("SelectTag's current value does not have a corresponding OptionTag","selectTag");
        }
		
	}

}
