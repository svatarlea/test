/*
 * Created by Ranorex
 * User: mperry
 * Date: 3/15/2012
 * Time: 10:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Reporting;
using Ranorex.Core.Testing;

namespace Tests
{
    class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {                    	
        	const string supportedRanorexVersion = "3.3.0.17843";
        	if (string.Compare(Ranorex.Host.Local.RanorexVersion, supportedRanorexVersion)!=0)
        	{
        		throw new Exception(string.Format("Ranorex version {0} not supported. Please use version {1}.",
        		                                  Ranorex.Host.Local.RanorexVersion, supportedRanorexVersion));
        	}
        	Keyboard.Enabled = false;
        	Mouse.Enabled = false;
        	Keyboard.AbortKey = System.Windows.Forms.Keys.Pause;
            int error = 0;

            try
            {
                error = TestSuiteRunner.Run(typeof(Program), Environment.CommandLine);
            }
            catch (Exception e)
            {
                Report.Error("Unexpected exception occurred: " + e.ToString());
                error = -1;
            }
            return error;
        }
    }
}
