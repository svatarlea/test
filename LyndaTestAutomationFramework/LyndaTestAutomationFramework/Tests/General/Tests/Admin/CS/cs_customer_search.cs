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

namespace Tests.General.Tests.Admin.CS
{
    /// <summary>
    /// Description of cs_customer_search.
    /// </summary>
    [TestModule("4FFB794A-69E6-4683-92DD-3C199813DAC3", ModuleType.UserCode, 1)]
    public class cs_customer_search : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public cs_customer_search()
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
            string url = "/Welcome.aspx";
            
            Browser browser = new Browser(BrowserProduct.Firefox, domain, true);            
           browser.Navigate(string.Format("https://{0}{1}{2}",admin,domain,url));
            
            adminrepo repo = adminrepo.Instance;
            
            repo.WebDocumentWelcome.adminlogin.username.PressKeys("admintester");
            repo.WebDocumentWelcome.adminlogin.password.PressKeys("Lynda1");
            repo.WebDocumentWelcome.adminlogin.submitbtn.Click();
            
            
            //Load the CS search form
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.CS);
            repo.WebDocumentWelcome.HeaderAdmin.CS.Click();
            Validate.Exists(repo.WebDocumentWelcome.CS.email);
            
            //Enter email address
            string ConsumerEmail, ConsumerPersona, ConsumerPersonaStatus, ConsumerProduct;
           	FormDataAdmin.GenerateAdminConsumer(out ConsumerEmail, out ConsumerPersona, out ConsumerPersonaStatus, out ConsumerProduct);
           	repo.WebDocumentWelcome.CS.email.PressKeys(ConsumerEmail);
           	SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.CS.persona, ConsumerPersona);
           	SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.CS.personastatus, ConsumerPersonaStatus);
           	SelectTagUI.ChooseSelectTagOption(repo.WebDocumentWelcome.BasePath.ToString(), repo.WebDocumentWelcome.CS.product, ConsumerProduct);
            repo.WebDocumentWelcome.HeaderAdmin.submitbtn.Click();
            Validate.Exists(repo.WebDocumentWelcome.HeaderAdmin.recordsfound);
            
            //Click link to customer details page
            repo.WebDocumentWelcome.HeaderAdmin.linkcustomerdetails.Click();
            
            //Billing tab
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.custdetailsbilling);
            repo.WebDocumentWelcome.CustomerDetails.custdetailsbilling.Click();
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetailsGrid.tranid);
                
            //Order/Invoice/Quote                
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.custdetailsorderinvquote);
           	repo.WebDocumentWelcome.CustomerDetails.custdetailsorderinvquote.Click();
           	Validate.Exists(repo.WebDocumentWelcome.CustomerDetailsGrid.updatedby);
           	 
           	//Pending Payment                
           	Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.pendingpayment);
            repo.WebDocumentWelcome.CustomerDetails.pendingpayment.Click();
            //Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.
            
            //Notes
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.notes);              
            repo.WebDocumentWelcome.CustomerDetails.notes.Click();
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetailsGrid.notedate);
                            
            //Usage
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.usage);              
            repo.WebDocumentWelcome.CustomerDetails.usage.Click();
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetailsGrid1.titledd);
               
            //Login History
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.loginhistory);
            repo.WebDocumentWelcome.CustomerDetails.loginhistory.Click();
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetailsGrid1.browserdd);
                
            //Abuse Details                
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.abusedetails);
            repo.WebDocumentWelcome.CustomerDetails.abusedetails.Click();
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetailsGrid1.activitytype);
            
            //MGMT
            Validate.Exists(repo.WebDocumentWelcome.CustomerDetails.mgmt);
            repo.WebDocumentWelcome.CustomerDetails.mgmt.Click();
            
            
           	//Logout
            repo.WebDocumentWelcome.HeaderAdmin.Logout.Click();
        }
    }
}
