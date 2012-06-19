using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;

using Tests.AppConfig;

using General.Utilities.Forms;

namespace Tests.General.Tests.Admin
{
    /// <summary>
    /// Description of admin_reports_sales.
    /// </summary>
    [TestModule("44F4B18A-A0EA-4EE4-BCD7-14D441D3FA95", ModuleType.UserCode, 1)]
    public class admin_reports_sales : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public admin_reports_sales()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            const string admin = "admin.";
            string domain = AppSettings.Domain;
            string url = "admin.lynda.com";
            
            Browser browser = new Browser(BrowserProduct.Firefox, domain, true);            
            browser.Navigate("https://admin.release.lynda.com/Welcome.aspx");
            
            adminrepo repo = adminrepo.Instance;
            
            repo.WebDocumentWelcome.adminlogin.username.PressKeys("admintester");
            repo.WebDocumentWelcome.adminlogin.password.PressKeys("Lynda1");
            repo.WebDocumentWelcome.adminlogin.submitbtn.Click();
            
      		//Reports - Sales - 1. Revenue Report by Sales Rep
    		//Reports - Sales - 2. IP Report
    		//Reports - Sales - 3. IP Reports for Accounts with Shibboleth
    		//Reports - Sales - 4. Open Invoice Report
    		//Reports - Sales - 5. IP Concurrency Summary
    		//Reports - Sales - 6. Discount Report by Product and Month
    		//Reports - Sales - 7. Discount Report by Discount Reason
    		//Reports - Sales - 8. Discount Report - Detail
    		//Reports - Sales - 9. Sales Actuals By Account Manager
    		//Reports - Sales - 10. Sales Actuals Summary
    		//Reports - Sales - 11. Sales Actuals Summary History
    		//Reports - Sales - 12. NCVI Commissions
    		//Reports - Sales - 13. SalesRep - Variance
    		//Reports - Sales - 14. SalesRep - Variance History
    		//Reports - Sales - 15. Missing Account Manager
    		//Reports - Sales - 16. Reassign Reps For Goals
    		//Reports - Sales - 17. Assign New Sales Representative
    		
    		
            //Reports - Sales - 1. Revenue Report by Sales Rep
    	    url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/Admin+MMUS+Revenue";
			browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
			Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);
    		
    		
            //Reports - Sales - 2. IP Report
    		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/IP+Program";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
    		Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);
    		
    		
    		 //Reports - Sales - 3. IP Reports for Accounts with Shibboleth * 
    		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/IP+Program";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
    		
    		
    		//Reports - Sales - 4. Open Invoice Report
    		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/Open+Invoice+Report";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
    		
    		
    		//Reports - Sales - 5. IP Concurrency Summary
    		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=%2fWebReports%2fIP+Concurrency+Summary";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
    		repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn.Click();
    		Validate.Exists(repo.WebDocumentWelcome.IPSummary.datetimemaxconcurrency);
    		
    		
    		//Reports - Sales - 6. Discount Report by Product and Month
    		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/Discount+Report+by+Product+and+Month";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
    		Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);

    		
    		 //Reports - Sales - 7. Discount Report by Discount Reason
      		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/Discount+Report+by+Discount+Reason";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));  		 
      		Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);  		 
    		 
    		 
    		//Reports - Sales - 8. Discount Report - Detail
     		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/Discount+Report+-+Detail";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   		
            Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);  			
    		
    		
    		//Reports - Sales - 9. Sales Actuals By Account Manager
     		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/SalesRep+-+Actual+By+AccountManager";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   		
            Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);     		
    		
            
    		//Reports - Sales - 10. Sales Actuals Summary
     		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/SalesRep+-+Actual+Summary";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);     			
    		
           
            //Reports - Sales - 11. Sales Actuals Summary History
            url = " /Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/SalesRep+-+Actual+Summary+History";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);     			
    		
            
    		//Reports - Sales - 12. NCVI Commissions
    		url = "https://admin.release.lynda.com/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/SalesRep+-+NCVI+Commissions";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);   
            repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn.Click();
    		Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh); 
            
    		
    		//Reports - Sales - 13. SalesRep - Variance
    		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=/WebReports/SalesRep+-+Variance";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);     			
    		
            
    		//Reports - Sales - 14. SalesRep - Variance History
    		url = "/Reports/ReportsViews/ViewSales.aspx?lpk46=%2fWebReports%2fSalesRep+-+Variance+History";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn);   
            repo.WebDocumentWelcome.HeaderAdmin.viewreportbtn.Click();
    		Validate.Exists(repo.WebDocumentWelcome.IPSummary.refresh);    			
    		
            
    		//Reports - Sales - 15. Missing Account Manager
    		url = "/Reports/ReportsViews/ViewMissingAccountManager.aspx";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.RoyaltyStatements.selectproduct);     			
    		
            
    		//Reports - Sales - 16. Reassign Reps For Goals     		
    		url = "/Reports/ReportsViews/ViewReassignRepsForGoals.aspx";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.RoyaltyStatements.selectproduct);      			
    		
            
            
    		//Reports - Sales - 17. Assign New Sales Representative
    		url = "/Reports/ReportsViews/ViewAssignNewSalesRepresentative.aspx";
    		browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));   			
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.selectrep);     			
    		
            
            	
            	
            
            
            //Logout
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();

        }
    }
}
