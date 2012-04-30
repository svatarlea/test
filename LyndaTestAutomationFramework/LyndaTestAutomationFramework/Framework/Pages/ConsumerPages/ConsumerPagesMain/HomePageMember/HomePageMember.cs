using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Repository;

using ConsumerPagesMain.HomePageMember;
using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;

namespace Lynda.Test.ConsumerPages
{   
    /// <summary>
    /// Represents a lynda member home page (e.g. http://www.lynda.com/Member.aspx)
    /// </summary>
    public class HomePageMember : LyndaHeaderFooterPage1
    {
    	/// <summary>
        /// HomePageMemberRepo Ranorex repository instance for this member home page
        /// </summary>
        private HomePageMemberRepo homePageMemberRepo = null;

        /// <summary>
    	/// A tab in the Latest releases section of the page
    	/// </summary>
    	public enum ReleaseTabItem { None, All, ThreeD, Audio };

    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.HomePageMember class
    	/// and waits for the page to load.
    	/// </summary>
    	/// <param name="browserForPage">Browser instance containing the member home page.</param>
    	public HomePageMember(Browser browserForPage)// : base(browserForPage)
    	{
    		homePageMemberRepo = new HomePageMemberRepo();
    		browser=browserForPage;   		
    		WaitForLoad(); 
    	}
    	
    	/// <summary>
    	/// Waits for page to load.
    	/// </summary>
    	public void WaitForLoad()
    	{
    		browser.ClickTitleBar();
    		WaitForLoad(homePageMemberRepo.DOMbase.LatestReleaseTabs.Audio);
    	}

		/// <summary>
		/// Clicks a tab in the "Latest releases" section.
		/// </summary>
		/// <param name="releaseTabItem">Tab to click</param>
    	public void ClickLatestReleaseTab(ReleaseTabItem releaseTabItem)
        {
        	if (releaseTabItem < ReleaseTabItem.None || releaseTabItem > ReleaseTabItem.Audio)
            {
        		throw new ArgumentOutOfRangeException("releaseTabItem", releaseTabItem, "Must be one of the following: None, All, ThreeD, Audio");
            }

    		browser.ClickTitleBar();
        	switch (releaseTabItem)
        	{
        		case ReleaseTabItem.Audio:
        			Validate.Exists(homePageMemberRepo.DOMbase.LatestReleaseTabs.Audio);
        			homePageMemberRepo.DOMbase.LatestReleaseTabs.Audio.Click();
        			break;
        		default:
        			throw new Exception(String.Format("Code not implemented yet: {0}", releaseTabItem.ToString()));
        	}
        }

    }
}
