using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace Lynda.Test.Browsers
{
	/// <summary>
	/// Represents members used to retrieve system information of an installed web browser
	///  such as IE, Firefox, Chrome or Safari.
	/// </summary>
	internal static class BrowserSystemInfo
	{
		static string programFilesX86Path = null;
		static string localAppDataPath = null;
				
		/// <summary>
		/// Initializes Lynda.Test.Browsers.BrowserSystemInfo class.
		/// </summary>
		static BrowserSystemInfo()
        {
        	try
        	{
        		programFilesX86Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        	}
        	catch (PlatformNotSupportedException e)
        	{
        		throw new Exception("Unable to get Program Files x86 path since this platform is not supported.",e);
        	}
        	if (programFilesX86Path == "")
        	{
        		throw new Exception("A Program Files x86 path does not exist on this computer.");
        	}
        	try
        	{
        		localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        	}
        	catch (PlatformNotSupportedException e)
        	{
        		throw new Exception("Unable to get Local Application Data path since this platform is not supported.",e);
        	}
        	if (localAppDataPath == "")
        	{
        		throw new Exception("A Local Application Data path does not exist on this computer.");
        	}
        }

		/// <summary>
		/// Verifies a browser exe exists in a specific directory path.
		/// </summary>
		/// <param name="expectedExePath">Full directory path of the browser exe to check for existence.</param>
		/// <returns>Full directory path of the browser exe if it exists, or null if it doesn't exist.</returns>
		internal static string GetInstalledExePath(string expectedExePath)
		{
			if (File.Exists(expectedExePath))
        	{
				return expectedExePath;
        	}
        	return null;        	
		}
        
        /// <summary>
        /// Major part of the file version of a browser exe.
        /// </summary>
        /// <param name="expectedExePath">Full directory path of the browser exe.</param>
        /// <returns>Major part of the file version, or 0 if expectedExePath doesn't exist.</returns>
		internal static int GetInstalledVersion(string expectedExePath)
		{        	
			string installedExePath = GetInstalledExePath(expectedExePath);
			if (installedExePath==null)
			{
				return 0;
			}
        	FileVersionInfo exeFileVersionInfo = null;
			exeFileVersionInfo = FileVersionInfo.GetVersionInfo(installedExePath);
        	if (exeFileVersionInfo.FileMajorPart == 0)
        	{
        		throw new Exception(string.Format("No version information found in {0}.",installedExePath));
        	}
	        return exeFileVersionInfo.FileMajorPart;	 
		}

		/// <summary>
		/// The Program Files x86 folder.
		/// </summary>
		internal static string ProgramFilesX86Path
		{
			get{return programFilesX86Path;}
		}			
		
		internal static string LocalAppDataPath
		{
			get{return localAppDataPath;}
		}
	}

}
