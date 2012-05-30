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
        /// Clicks or sets a web page select tag option.
        /// </summary>
        /// <param name="pathToDOM">Ranorex.Core.Repository.RepoGenBaseFolder.BaseBath of a web page DOM.</param>
        /// <param name="selecttag">Ranorex.SelectTag of a web page select tag.</param>
        /// <param name="option">option to select in select tag.</param>
		public static void ChooseSelectTagOption(string pathToDOM, Ranorex.SelectTag selecttag, string option)
		{
            WebDocument domWebDocument = pathToDOM;
            
            switch (domWebDocument.BrowserName)
            {
            	case "Chrome":
            	case "Safari":
        			//Using Chrome and Safari select tag problematic, so set the tag value directly for now.
        			selecttag.TagValue = FindSelectTagValueForOption(selecttag, option);
	        		break;       		
            	case "IE":
            		//Click option if visible after clicking select tag.
            		String itemToClickRxPath = String.Format("/container[@caption='selectbox']/listitem[@accessiblename='{0}']",option);

            		selecttag.Focus();
		        	selecttag.Click();

		        	ListItem itemToClick;
		        	try
		        	{
		        		itemToClick = itemToClickRxPath;
		        	}
		        	catch (Exception e)
		        	{
		        		throw new ArgumentException(string.Format("Unable to find option {0} in SelectTag {1}. Exception:{2}", option, selecttag.Name,e), "option");
		        	}
		        	
		        	if (itemToClick.Visible)
		        	{
		        		itemToClick.Click(); 
		        		break;
		        	}
		        	//If not visible, set tag value directly for now.
		        	selecttag.TagValue = FindSelectTagValueForOption(selecttag, option);
            		break;     
            	case "Mozilla":
            		//OptionTag.Visible does not work for an option tag that is not visible, so set the tag value directly for now.
            		selecttag.TagValue = FindSelectTagValueForOption(selecttag, option);
		        	break;
            	default:
            		break;                		      
            }

        }

		/// <summary>
		/// Find a specified option in all the options contained within a Ranorex.SelectTag 
		///  and return that option's Ranorex.OptionTag.TagValue.
		/// </summary>
		/// <param name="selecttag">Ranorex.SelectTag containing options to search.</param>
		/// <param name="option">Option to find in the Ranorex.SelectTag.</param>
		/// <returns>If option is found, returns Ranorex.OptionTag.TagValue of the found option.
		/// If not found, throws a System.ArgumentException.ArgumentException.</returns>
		private static string FindSelectTagValueForOption(Ranorex.SelectTag selecttag, string option)
		{
			//Handle InnerText that contains white space. e.g.:
			//	InnerText="credit card" or
			//	InnerText="      credit card  " (where there is white space before and/or after)
			//and option = "credit card"
			//Regular expression covers white space before or after the option string	
			string patternOptionWhiteSpace =  String.Format(@"^\s*{0}\s*$",option);
			
			//Find TagValue for requested option
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
        	return tagValueToSelect;
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
