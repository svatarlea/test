using System;
using System.Collections.Generic;
using System.Collections;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;
using Ranorex.Controls;

using Browsers;
using Browsers.ChromeBrowser;

namespace Lynda.Test.Browsers
{
    /// <summary>
    /// Browser.
    /// </summary>
	public enum BrowserProduct { IE, Firefox, Chrome, Safari };

    /// <summary>
    /// Represents a web browser such as IE, Firefox, Chrome or Safari.
    /// </summary>
    public class Browser
    {
    	private IEBrowser ieBrowser = null;
    	private ChromeBrowser chromeBrowser = null;
    	private FirefoxBrowser firefoxBrowser = null;
    	private SafariBrowser safariBrowser = null;
    	
    	private BrowserProduct browserProduct;

        /// <summary>
        /// Initializes a new instance of the Lynda.Test.Browsers.Browser class,
        /// opens a new Internet Explorer window without killing any existing Internet Explorer windows first and leaves the url as the
        /// browser's default.
        /// </summary>
    	public Browser()
            : this(BrowserProduct.IE, String.Empty, false)
        {   
        }

        /// <summary>
        /// Initializes a new instance of the Lynda.Test.Browsers.Browser class
        /// and opens a new browser window with the specified uri without killing any existing browser windows first.
        /// </summary>
        /// <param name="product">The Lynda.Test.Browsers.Browser.BrowserProduct to open.</param>
        /// <param name="uri">The default uri for the opened browser. If String.Empty then uri is the default uri for that browser.</param>
        public Browser(BrowserProduct product, string uri)
            : this(product, uri, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Lynda.Test.Browsers.Browser class
        /// and opens a new browser window with the specified uri.
        /// </summary>
        /// <param name="product">The Lynda.Test.Browsers.Browser.BrowserProduct to open.</param>
        /// <param name="url">The default uri for the opened browser. If String.Empty then uri is the default uri for that browser.</param>
        /// <param name="killExisting">If TRUE, kills any open browser processes for the specified browser first.</param>
        public Browser(BrowserProduct product, string uri, bool killExisting)
        {
        	if (product < BrowserProduct.IE || product > BrowserProduct.Safari)
            {
                throw new ArgumentOutOfRangeException("product", product, "Must be one of the following: IE, Firefox, Chrome or Safari");
            }
            browserProduct = product;

        	switch (browserProduct)
        	{
        		case BrowserProduct.IE:
		    		{
                        ieBrowser = new IEBrowser(uri, killExisting);	                      
		        		break;
		    		}
        		case BrowserProduct.Chrome:
        			{
        				chromeBrowser = new ChromeBrowser(uri,killExisting);
        				break;
        			}
        		case BrowserProduct.Firefox:
        			{
        				firefoxBrowser = new FirefoxBrowser(uri, killExisting);
        				break;
        			}
        		case BrowserProduct.Safari:
        			{
        				safariBrowser = new SafariBrowser(uri, killExisting);
        				break;
        			}
        		default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", product.ToString()));
            }
        }

        /// <summary>
        /// Gets the full directory path of a Lynda.Test.Browsers.Browser.BrowserProduct
        ///  that is installed on the system.
        /// </summary>
        /// <param name="browserProduct">Browser to get the full directory path of.</param>
        /// <returns>Full directory path of the installed browser, or null if the browser is not installed.</returns>
        public static string GetInstalledExePath(BrowserProduct browserProduct)
        {
        	switch (browserProduct)
        	{
        		case BrowserProduct.IE:
        			{
        				return IEBrowser.InstalledExePath;
        			}
        		case BrowserProduct.Chrome:
        			{
        				return ChromeBrowser.InstalledExePath;        	
        			}
        		case BrowserProduct.Firefox:
        			{
        				return FirefoxBrowser.InstalledExePath;        			
        			}
        		case BrowserProduct.Safari:
        			{
        				return SafariBrowser.InstalledExePath;        		
        			}
 				default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));       			
        	}
        }
               
        /// <summary>
        /// Gets the version of a Lynda.Test.Browsers.Browser.BrowserProduct
        ///  that is installed on the sytem.
        /// </summary>
        /// <param name="browserProduct">Browser to get the version of.</param>
        /// <returns>Major part of the file version of the installed browser exe, or 0 if the browser is not installed.</returns>
        public static int GetInstalledVersion(BrowserProduct browserProduct)
        {
        	switch (browserProduct)
        	{
        		case BrowserProduct.IE:
        			{
        				return IEBrowser.InstalledVersion;
        			}
        		case BrowserProduct.Chrome:
        			{
        				return ChromeBrowser.InstalledVersion;        	
        			}
        		case BrowserProduct.Firefox:
        			{
        				return FirefoxBrowser.InstalledVersion;        			
        			}
        		case BrowserProduct.Safari:
        			{
        				return SafariBrowser.InstalledVersion;        		
        			}
 				default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));       			
        	}
        }
        
        public static int GetSupportedVersion(BrowserProduct browserProduct)
        {
        	switch (browserProduct)
        	{
        		case BrowserProduct.IE:
        			{
        				return IEBrowser.SupportedExeMajorVersion;
        			}
        		case BrowserProduct.Chrome:
        			{
        				return ChromeBrowser.SupportedExeMajorVersion;      	
        			}
        		case BrowserProduct.Firefox:
        			{
        				return FirefoxBrowser.SupportedExeMajorVersion;        			
        			}
        		case BrowserProduct.Safari:
        			{
        				return SafariBrowser.SupportedExeMajorVersion;        		
        			}
 				default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));       			
        	}
        }
        
        /// <summary>
        /// Navigate to a uri by typing the uri in the browser's navigation edit box and pressing the {ENTER} key.
        /// </summary>
        /// <param name="uri">uri to navigate to.</param>
        public void Navigate(string uri)
        {
        	switch (browserProduct)
        	{
        		case BrowserProduct.IE:
        			{
        				ieBrowser.Navigate(uri);
        				break;
        			}
        		case BrowserProduct.Chrome:
        			{
        				chromeBrowser.Navigate(uri);
        				break;
        			}
        		case BrowserProduct.Firefox:
        			{
        				firefoxBrowser.Navigate(uri);
        				break;
        			}
        		case BrowserProduct.Safari:
        			{
        				safariBrowser.Navigate(uri);
        				break;
        			}
 				default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));       			
        	}
        	
        }

        /// <summary>
        /// Gets the text from the browser's navigation edit box.
        /// </summary>
        public string CurrentUri
        {
        	get
        	{
	        	switch (browserProduct)
	        	{
	        		case BrowserProduct.IE:
	        			{
	        				return ieBrowser.CurrentUri;
	        			}
	        		case BrowserProduct.Chrome:
	        			{
	        				return chromeBrowser.CurrentUri;
	        			}
	        		case BrowserProduct.Firefox:
	        			{
	        				return firefoxBrowser.CurrentUri;
	        			}
	        		case BrowserProduct.Safari:
	        			{
	        				return safariBrowser.CurrentUri;
	        			}
	 				default:
	                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));       			
	        	}
        	}

        }

        /// <summary>
        /// Half the size of this browser window.
        /// </summary>
        public void HalfSize()
        {
            switch (browserProduct)
            {
                case BrowserProduct.IE:
                    {
                        ieBrowser.HalfSize();
                        break;
                    }
                case BrowserProduct.Chrome:
                    {
            			chromeBrowser.HalfSize();
            			break;
                    }
                case BrowserProduct.Firefox:
                    {
                        firefoxBrowser.HalfSize();
                        break;
                    }
                case BrowserProduct.Safari:
                    {
            			safariBrowser.HalfSize();
            			break;
                    }
                default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));
            }
        }

        /// <summary>
        /// Move this browser window.
        /// </summary>
        /// <param name="x">Adds this paramter to browser window's current screen location x-coordinate.</param>
        /// <param name="y">Adds this paramter to browser window's current screen location y-coordinate.</param>
        public void Move(int x, int y)
        {
            switch (browserProduct)
            {
                case BrowserProduct.IE:
                    {
                        ieBrowser.Move(x, y);
                        break;
                    }
                case BrowserProduct.Chrome:
                    {
            			chromeBrowser.Move(x,y);
            			break;
                    }
                case BrowserProduct.Firefox:
                    {
                        firefoxBrowser.Move(x, y);
                        break;
                    }
                case BrowserProduct.Safari:
                    {
            			safariBrowser.Move(x,y);
            			break;
                    }
                default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));
            }
        }

        /// <summary>
        /// Click the title bar of this browser window.
        /// </summary>
        public void ClickTitleBar()
        {
            switch (browserProduct)
            {
                case BrowserProduct.IE:
                    {
                        ieBrowser.ClickTitleBar();
                        break;
                    }
                case BrowserProduct.Chrome:
                    {
            			chromeBrowser.ClickTitleBar();
            			break;
                    }
                case BrowserProduct.Firefox:
                    {
                        firefoxBrowser.ClickTitleBar();
                        break;
                    }
                case BrowserProduct.Safari:
                    {
            			safariBrowser.ClickTitleBar();
            			break;
                    }
                default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));
            }
        }

        /// <summary>
        /// Included for API test stability purposes. Move this browser window down and up quickly four times.
        /// </summary>
        public void Fun()
        {
            switch (browserProduct)
            {
                case BrowserProduct.IE:
                    {
                        ieBrowser.Fun();
                        break;
                    }
                case BrowserProduct.Chrome:
                    {
            			chromeBrowser.Fun();
            			break;
                    }
                case BrowserProduct.Firefox:
                    {
                        firefoxBrowser.Fun();
                        break;
                    }
                case BrowserProduct.Safari:
                    {
            			safariBrowser.Fun();
            			break;
                    }
                default:
                    throw new Exception(String.Format("Code not implemented yet: {0}", browserProduct.ToString()));
            }
        }

        [Obsolete("Please use a Skynet API page instance's WaitForLoad() or Ranorex API Ranorex.Validate.Exists().", true)]
        public void WaitForLoad()
        {
        }

        [Obsolete("Please use a Skynet API page instance's WaitForLoad() or Ranorex API Ranorex.Validate.Exists().", true)]
        public void WaitForLoad(WebElement webElement)
        {         
        }

    }
	
}