using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace General.Utilities.Forms
{
    public static class AdminReportViewer
    {

    	public static void ReportViewer(out string selectYear, out string selectNumYrPeriod, out string selectMoYrPeriod, out string selectAuthor)    		
    	{
    		selectYear = "2010";
    		selectNumYrPeriod = "12/2011";
    		selectMoYrPeriod = "April 2010";
    		selectAuthor = "David Rivers";
    	}
	}

}