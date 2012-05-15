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
		static int mouseDefaultMoveTime;
		static int keyboardDefaultKeyPressTime;
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

			//Browser
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

            //Domain
            domain = GetAppSettingsKeyValue("Domain");
            if (string.IsNullOrEmpty(domain))
            {
            	throw new Exception(string.Format("Application settings \"Domain\" key's value is null or empty. Value:{0}, File:{1}.",domain,configFileName));
            }
            
            //MouseDefaultMoveTime
            string mouseDefaultMoveTimeToParse = GetAppSettingsKeyValue("MouseDefaultMoveTime");
            if (string.IsNullOrEmpty(mouseDefaultMoveTimeToParse))
            {
            	throw new Exception(string.Format("Application settings \"MouseDefaultMoveTime\" key's value is null or empty. Value:{0}, File:{1}.",mouseDefaultMoveTimeToParse,configFileName));
            }
            try
            {
            	mouseDefaultMoveTime = Int32.Parse(mouseDefaultMoveTimeToParse);
            }
            catch (FormatException e)
            {
            	throw new Exception(string.Format("Unable to convert application settings \"MouseDefaultMoveTime\" key's value to Int32. Value:{0}, File:{1}.",mouseDefaultMoveTimeToParse,configFileName), e);
            }
            catch (OverflowException e)
            {
            	throw new Exception(string.Format("Unable to convert application settings \"MouseDefaultMoveTime\" key's value to Int32; value must be between {0} and {1}. Value:{2}, File:{3}.",Int32.MinValue,Int32.MaxValue,mouseDefaultMoveTimeToParse,configFileName), e);
            }
            if (mouseDefaultMoveTime<1)
            {
            	throw new Exception(string.Format("Application settings \"MouseDefaultMoveTime\" key's value must be > 0. Value:{0}, File:{1}.",mouseDefaultMoveTime, configFileName));
            }
            
            //KeyboardDefaultKeyPressTime
            string keyboardDefaultKeyPressTimeToParse = GetAppSettingsKeyValue("KeyboardDefaultKeyPressTime");
            if (string.IsNullOrEmpty(keyboardDefaultKeyPressTimeToParse))
            {
            	throw new Exception(string.Format("Application settings \"KeyboardDefaultKeyPressTime\" key's value is null or empty. Value:{0}, File:{1}.",keyboardDefaultKeyPressTimeToParse,configFileName));
            }            
            try
            {
            	keyboardDefaultKeyPressTime = Int32.Parse(keyboardDefaultKeyPressTimeToParse);
            }           
            catch (FormatException e)
            {
            	throw new Exception(string.Format("Unable to convert application settings \"KeyboardDefaultKeyPressTime\" key's value to Int32. Value:{0}, File:{1}.",keyboardDefaultKeyPressTimeToParse,configFileName), e);
            }            
            catch (OverflowException e)
            {
            	throw new Exception(string.Format("Unable to convert application settings \"KeyboardDefaultKeyPressTime\" key's value to Int32; value must be between {0} and {1}. Value:{2}, File:{3}.",Int32.MinValue,Int32.MaxValue,keyboardDefaultKeyPressTimeToParse,configFileName), e);
            }            
            if (keyboardDefaultKeyPressTime<1)
            {
            	throw new Exception(string.Format("Application settings \"KeyboardDefaultKeyPressTime\" key's value must be > 0. Value:{0}, File:{1}.",keyboardDefaultKeyPressTime, configFileName));
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
		/// Gets the MouseDefaultMoveTime key's value from the app.config file for the Tests.exe assembly.
		/// </summary>
		internal static int MouseDefaultMoveTime
		{
			get{return mouseDefaultMoveTime;}
		}
		
		/// <summary>
		/// Gets the KeyboardDefaultKeyPressTime key's value from the app.config file for the Tests.exe assembly.
		/// </summary>
		internal static int  KeyboardDefaultKeyPressTime
		{
			get{return keyboardDefaultKeyPressTime;}
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
