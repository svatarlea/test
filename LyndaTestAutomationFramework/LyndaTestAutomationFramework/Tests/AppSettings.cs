using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;

using Lynda.Test.Browsers;

namespace Tests.AppConfig
{
	/// <summary>
	/// Represents specific keys and values from the appSettings section in the app.config file for the Tests.exe assembly.
	/// </summary>
	internal static class AppSettings
	{
		static BrowserProduct browser;
		static string domain;
		static NameValueCollection appSettings;
		static string configFileName;
		
		/// <summary>
		/// Initializes Tests.AppConfig.AppSettings class.
		/// Reads specific key values from the appSettings section in the app.config file for the Tests.exe assembly.
		/// </summary>
		static AppSettings()
		{
			AssemblyName thisAssembly = Assembly.GetEntryAssembly().GetName();
			configFileName = string.Format("{0}.exe.config", thisAssembly.Name);

			appSettings = null;
			try
			{
				appSettings = System.Configuration.ConfigurationManager.AppSettings;
			}
			catch (ConfigurationErrorsException e)
			{
				throw new Exception(
					string.Format("Failed to get application settings data from {0}.",configFileName),e);
			}

			string appSettingsBrowser = GetAppSettingsKeyValue("Browser");

            try
            {
            	browser = (BrowserProduct)Enum.Parse(typeof(BrowserProduct),appSettingsBrowser,true);
            }
            catch (ArgumentNullException e)
            {
            	throw new Exception(string.Format("Application settings \"Browser\" key's value is null in {0}.",configFileName),e);
            }
            catch (ArgumentException e)
            {
            	throw new Exception
            		(string.Format("Application settings \"Browser\" key's value is not IE, Firefox, Chrome or Safari. Or the value is either an empty string or only contains white space. File:{0}.",configFileName), e);	                                  
            }

            if (browser < BrowserProduct.IE || browser > BrowserProduct.Safari)
            {
                throw new Exception(string.Format("Application settings \"Browser\" key's value is not IE, Firefox, Chrome or Safari. File:{0}.",configFileName));
            }

            domain = GetAppSettingsKeyValue("Domain");
            if (string.IsNullOrEmpty(domain))
            {
            	throw new Exception(string.Format("Application settings \"Domain\" key's value is null or empty. Value:{0}, File:{1}.",domain,configFileName));
            }
		}
		
		/// <summary>
		/// Gets the Browser key's value from the app.config file for the Tests.exe assembly.
		/// </summary>
		internal static BrowserProduct Browser
		{
			get{return browser;}
		}
		
		/// <summary>
		/// Gets the Domain key's value from the app.config file for the Tests.exe assembly.
		/// </summary>
		internal static string Domain
		{
			get{return domain;}
		}
		
		/// <summary>
		/// Get a key's value from the appSettings section in the app.config for the Tests.exe assembly.
		/// </summary>
		/// <param name="keyName">Name of the key.</param>/param>
		/// <returns>The key's value.</returns>
		private static string GetAppSettingsKeyValue(string keyName)
		{
			string keyValue = null;
			keyValue = appSettings.Get(keyName);
            if (keyValue==null)
            {
            	throw new Exception(string.Format("The key \"{0}\" was not found in the application settings of {1}" +
            	                                  " or the key was found but its associated value was null.",
            	                                  keyName,configFileName));
            }
            return keyValue;
		}
		
		
	}
}
