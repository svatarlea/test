/*
 * Created by Ranorex
 * User: jalex
 * Date: 5/14/2012
 * Time: 3:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WinForms = System.Windows.Forms;
using General.Utilities;
using General.Utilities.Forms;
using Tests.General.Utilities;

using Lynda.Test.Advanced.Utilities.WebPages;
using Lynda.Test.Browsers;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Tests.AppConfig;


namespace Tests.General.Tests.BVT5
{
    /// <summary>
    /// Description of Admin_CS_NewAcct.
    /// </summary>
    [TestModule("ED3E942B-6E05-4C79-B999-BD5146EA35CC", ModuleType.UserCode, 1)]
    public class Admin_CS_NewAcct : ITestModule
    {
        private static Admin_CS_NewAcctRepository repo = Admin_CS_NewAcctRepository.Instance;
    	private Browser	browser;
    	
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Admin_CS_NewAcct()
        {
            // Do not delete - a parameterless constructor is required!
        }

        
        
        string _varPersona = "";
        [TestVariable("8C000255-B7B7-4331-B9A2-6FA9DA65472E")]
        public string varPersona
        {
        	get { return _varPersona; }
        	set { _varPersona = value; }
        }
        
        
        
        string _varAccountType = "";
        [TestVariable("D601E63D-3D8C-472F-A1C0-9BA208F002E6")]
        public string varAccountType
        {
        	get { return _varAccountType; }
        	set { _varAccountType = value; }
        }
        
        
        string _varUsername = "";
        [TestVariable("CBA471B1-1567-4EEA-BD18-685C68CB5715")]
        public string varUsername
        {
        	get { return _varUsername; }
        	set { _varUsername = value; }
        }
        
        
        string _varSalesRep1 = "";
        [TestVariable("9E760F6A-7E9B-4189-B1DB-F72DA11FDAF6")]
        public string varSalesRep1
        {
        	get { return _varSalesRep1; }
        	set { _varSalesRep1 = value; }
        }
        
        
        string _varSalesOperationType = "";
        [TestVariable("0E5282F7-833B-42C1-838E-4A10F36C4260")]
        public string varSalesOperationType
        {
        	get { return _varSalesOperationType; }
        	set { _varSalesOperationType = value; }
        }
        
         
        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
                     
            Mouse.DefaultMoveTime = 50;
            Keyboard.DefaultKeyPressTime = 50;
            Delay.SpeedFactor = 1.0;
             
            string strResultsFile = Directory.GetCurrentDirectory() + @"\Public_AcctAccessData.xlsx";
            const string navigateTo = "/Welcome.aspx";
            string username = "knvirtualuser7";
            string password = "lynda1";
            //TODO: Update rxrep to support all Browsers
            if(AppSettings.Browser != BrowserProduct.Firefox)
            {
            	Report.Error("Note: Currently only Firefox Supported; Please change the appsettings Key - Browser value to Firefox and retry.");
            	throw new Ranorex.RanorexException();
            }
            
            BrowserProduct browserProduct = AppSettings.Browser;
            string url = string.Format("{0}{1}{2}","https://admin.", AppSettings.Domain,navigateTo);
                        
            int intIndex = TestCase.Current.DataContext.CurrentRowIndex;
            varSalesRep1 = TestCase.Current.DataContext.CurrentRow["Rep1"];
            varSalesOperationType = TestCase.Current.DataContext.CurrentRow["OperationType"];
            
            
            // TODO : Add logic to run from any row in the data source
            
            if (intIndex == 1)
            {
            	//Open browser and navigate to url
            	browser = new Browser(browserProduct, url);
            	//Wait for page to load
            	try
            	{
            		if (Validate.Exists(repo.DOM.DivTagCtl00_UcHeaderAdminLogin.btnLoginInfo.AbsolutePath, repo.DOM.DivTagCtl00_UcHeaderAdminLogin.btnLoginInfo.SearchTimeout,"{0}",new Validate.Options(true,ReportLevel.Error)))
            		{
            			repo.DOM.DivTagCtl00_UcHeaderAdminLogin.btnLogin.Click();
            		}
            	}
            	catch(ValidationException ve)
            	{
            		Report.Log(ReportLevel.Warn,"ValidationException - " + ve.ToString());
            		HandleUntrustedConnection("https://admin."+AppSettings.Domain+navigateTo);
            	}
            	repo.DOM.DivTagCtl00_UcHeaderAdminLogin.txtAdminUserName.PressKeys(username);
            	repo.DOM.DivTagCtl00_UcHeaderAdminLogin.txtAdminPasswd.PressKeys(password);
            	repo.DOM.DivTagCtl00_UcHeaderAdminLogin.btnLogin.Click();
            }
            
            Validate.Exists(repo.DOM.SomeBodyTag.lnkCS);	
            
            repo.DOM.SomeBodyTag.lnkCS.Click();
                       
            try
            {
            	if (Validate.Exists(repo.DOM.SomeBodyTag.lnkNewAccountInfo.AbsolutePath, repo.DOM.SomeBodyTag.lnkNewAccountInfo.SearchTimeout,"{0}",new Validate.Options(false,ReportLevel.Info)))
            	{
            		
            		repo.DOM.SomeBodyTag.lnkNewAccount.Click();
            	}
            
            }
            catch(ValidationException e)
            {
            	Report.Log(ReportLevel.Error, e.ToString());
            }
    		
           	
            switch(varPersona)
            {
            	case "lyndaPro admin":
            		Validate.Exists(repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lProadminInfo);
            		repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lProadmin.Click();
            		break;
            	case "Educator":
            		Validate.Exists(repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_EducatorInfo);
            		repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_Educator.Click();
            		break;
            	case "Consumer":
            		Validate.Exists(repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_ConsumerInfo);
            		repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_Consumer.Click();
            		break;
            	case "lyndaKiosk admin":
            		Validate.Exists(repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lKisokadminInfo);
            		repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lKisokadmin.Click();
            		break;
            	case "lyndaEnterprise admin":
            		Validate.Exists(repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lEnterpriseadminInfo);
            		repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lEnterpriseadmin.Click();
            		break;
            	case "lyndaCampus admin":
            		Validate.Exists(repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lCampusadminInfo);
            		repo.DOM.TableTagCtl00_cphMain_rblPersona.rbPersona_lCampusadmin.Click();
            		break;
            	
            	default:
            	    Report.Log(ReportLevel.Error, "Persona could not be determined." );
            	    break;
            		
            }
            
            Report.Log(ReportLevel.Info,"Validate if Accountype Default is selected as Regular");
            Validate.Attribute(repo.DOM.SomeBodyTag.rbAccountType_RegularInfo,"checked", "True");
            
            
            //TODO : handle different AccountTypes 
            if (varAccountType == "Complimentary")
               repo.DOM.SomeBodyTag.rbAccountType_Compl.Click();
            
            
            repo.DOM.SomeBodyTag.btnContinue.MoveTo();
            repo.DOM.SomeBodyTag.btnContinue.Focus();
            repo.DOM.SomeBodyTag.btnContinue.Click();
            
            
            switch(varPersona)
            {
            		
            		case "lyndaPro admin":
            			EnterAccountInformation();
            		
            			Validate.Exists(repo.DOM.SomeBodyTag.txtNoOfLicenses);
            			repo.DOM.SomeBodyTag.txtNoOfLicenses.PressKeys("12");
            		
            			//TODO : Promotional Code 
            		
            			repo.DOM.SomeBodyTag.btnCalculate.Click();
            			//TODO :Validate Amount Per license and total Amount
            		
            			repo.DOM.SomeBodyTag.btn_Step2of4_Continue.Click();
            			
            			EnterBillingInformation();
            		            		
            			EnterSalesInformation();
            		
            			repo.DOM.SomeBodyTag.btn_Step3of4_Continue.Click();
            			Validate.Exists(repo.DOM.SomeBodyTag.btn_PurchaseInfo);
            			repo.DOM.SomeBodyTag.btn_Purchase.Click();
            			Validate.Exists(repo.DOM.SomeBodyTag.btn_Continue_toCustomerDetails);
            			repo.DOM.SomeBodyTag.btn_Continue_toCustomerDetails.Click();
            			
            			break;
            		case "Educator":
            			//TODO : Create Educator account
            			break;
            		case "Consumer":
            			//TODO : Create Consumer account
            			break;
            		
            		case "lyndaKiosk admin":
            		
            			EnterAccountInformation();
            		
            			Validate.Exists(repo.DOM.DivTagTable_format.lk_NoOfConcurrentLicenses);
            			repo.DOM.DivTagTable_format.lk_NoOfConcurrentLicenses.PressKeys("12");
            		    repo.DOM.DivTagTable_format.lk_Price.PressKeys("1200");
            		    //TODO :Validate Amount
            			repo.DOM.DivTagTable_format.lk_btn_Step2of4Continue.Click();
            			
            			EnterBillingInformation();
            		
            			EnterSalesInformation();
            			
            			repo.DOM.SomeBodyTag.btn_Step3of4_Continue.Click();
            			Validate.Exists(repo.DOM.SomeBodyTag.btn_Purchase);
            			repo.DOM.SomeBodyTag.btn_Purchase.Click();
            			repo.DOM.DivTagTable_format.lk_btnContinue_EnterIPAddrs.Click();
            			Validate.Exists(repo.DOM.DivTagTable_format.lk_btnContinue_IPAddrsPg);
            			
            			string strIPAddress1, strIPAddress2;
            			FormDataIPAddress.GenerateIPAddress(out strIPAddress1, out strIPAddress2);
            			repo.DOM.SomeTBodyTag.lk_txtFromIPAddress1.PressKeys(strIPAddress1);
            			repo.DOM.DivTagTable_format.lk_btnContinue_IPAddrsPg.Click();
            				            			
            			break;
            		case "lyndaEnterprise admin":
            		
            			EnterAccountInformation_lc();
            		
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_txtNoOfLicenses);
            			repo.DOM.DivTagTable_format.lc_txtNoOfLicenses.PressKeys("12");
            		
            			//TODO : Promotional Code
            		
            			repo.DOM.DivTagTable_format.lc_btnCalculate.Click();
            			
            			//TODO : Validate Amount
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_btn_Step2of4_ContinueInfo);
            			repo.DOM.DivTagTable_format.lc_btn_Step2of4_Continue.Click();;
            		
            			EnterBillingInformation_lc();
            		
            			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.DivTagTable_format.lc_cmbSalesRep1, varSalesRep1);
            		    SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.DivTagTable_format.lc_cmbOperationType, varSalesOperationType);
            		 
            		    Validate.Exists(repo.DOM.DivTagTable_format.lc_btnPurchaseInfo);
            			repo.DOM.DivTagTable_format.lc_btnPurchase.Click();
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_btnPurchaseInfo);
            			repo.DOM.DivTagTable_format.lc_btnPurchase.Click();
            			
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_btnCustomerDetails);
            			repo.DOM.DivTagTable_format.lc_btnCustomerDetails.Click();
            			
            			break;
            		case "lyndaCampus admin":
            		
            			EnterAccountInformation_lc();
            		
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_txtNoOfLicenses);
            			repo.DOM.DivTagTable_format.lc_txtNoOfLicenses.PressKeys("12");
            		
            			//TODO : Promotional Code
            		
            			repo.DOM.DivTagTable_format.lc_btnCalculate.Click();
            			
            			//TODO : Validate Amount
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_btn_Step2of4_ContinueInfo);
            			repo.DOM.DivTagTable_format.lc_btn_Step2of4_Continue.Click();
            		
            			EnterBillingInformation_lc();
            		
            			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.DivTagTable_format.lc_cmbSalesRep1, varSalesRep1);
            			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.DivTagTable_format.lc_cmbOperationType, varSalesOperationType);
            		  
            			repo.DOM.DivTagTable_format.lc_btnPurchase.Click();
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_btnPurchase);
            			repo.DOM.DivTagTable_format.lc_btnPurchase.Click();
            			Validate.Exists(repo.DOM.DivTagTable_format.lc_btnCustomerDetails);
            			repo.DOM.DivTagTable_format.lc_btnCustomerDetails.Click();
            			
            			break;
            		default:
            		    break;
            		
            }
        	
        	
        	
        	
        	Validate.Attribute(repo.DOM.SomeBodyTag.txtDetailsUserName, "TagValue", varUsername);
        	
        	ExcelData.Write(strResultsFile , intIndex+1, 2, varPersona, varUsername);
            
        	//TODO : Logout and close only when all iterations are completed.
        	repo.DOM.DivTagCtl00_UcHeaderAdminLogin.btnLogout.Click();
            Host.Local.CloseApplication(repo.FormCustomer_Details.Self, new Duration(0));
        	
        }
        
        
        void EnterAccountInformation()
        {
        	        string strAccountCountry, strTitle, strDept, strOrgname, strPhoneNumber;
            		FormDataAccount.GenerateAccountInfo(out strAccountCountry, out strTitle, out strDept, out strOrgname, out strPhoneNumber);
            		Validate.Exists(repo.DOM.SomeBodyTag.cmbAccountCountry);
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeBodyTag.cmbAccountCountry,strAccountCountry);	
            		
            		repo.DOM.SomeBodyTag.txtAdminFirstName.PressKeys(FormDataAccount.GenerateFirstName()+varPersona);
            		repo.DOM.SomeBodyTag.txtAdminLastName.PressKeys(FormDataAccount.GenerateLastName());
            		repo.DOM.SomeBodyTag.txtTitle.PressKeys(strTitle);
            		repo.DOM.SomeBodyTag.txtDept.PressKeys(strDept);
            		repo.DOM.SomeBodyTag.txtOrgName.PressKeys(strOrgname);
            		repo.DOM.SomeBodyTag.txtPhoneNumber.PressKeys(strPhoneNumber);
            		
            		string strUsername, strEmail;
            		FormDataAccount.GenerateUsernameEmail(out strUsername, out strEmail);
            		varUsername=strUsername;
            		
            		repo.DOM.SomeBodyTag.txtEmail.PressKeys(strEmail);
            		repo.DOM.SomeBodyTag.txtEmailConfirm.PressKeys(strEmail);
            		repo.DOM.SomeBodyTag.txtUsername.PressKeys(strUsername);
            		repo.DOM.SomeBodyTag.txtPassword.PressKeys(FormDataAccount.GeneratePassword());
            		repo.DOM.SomeBodyTag.txtPasswordConfirm.PressKeys(FormDataAccount.GeneratePassword());
            		
            		// TODO : StartDate and EndDate
            		            		
            		repo.DOM.SomeBodyTag.btn_Step1of4_Continue.Click();
            		
        }
        
        void EnterBillingInformation()
        {
        	   		string strcompanyName, straddress, straptSuite, strcity, strstate, strzip, strcountry, strphone;
        	   	    FormDataBilling.GenerateAddress(out strcompanyName, out straddress, out straptSuite,out strcity, out strstate, out strzip, out strcountry, out strphone);	
        	   			
        	 		Validate.Exists(repo.DOM.SomeBodyTag.txtAddress);
            		repo.DOM.SomeBodyTag.txtAddress.PressKeys(straddress);
            		repo.DOM.SomeBodyTag.txtCity.PressKeys(strcity);
            		
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeBodyTag.cmbBillingState,strstate);	
            		repo.DOM.SomeBodyTag.txtZip.PressKeys(strzip);
            		
            		string strpaymentType, strcardType, strcardNumber, strnameOnCard, strcardSecurityCode,  strexpireMonth, strexpireYear;
            		FormDataPayment.GenerateCreditCard(out strpaymentType, out strcardType, out strcardNumber, out strnameOnCard, out strcardSecurityCode, out strexpireMonth, out strexpireYear);
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeBodyTag.cmbCreditCardType, strcardType);
            		repo.DOM.SomeBodyTag.txtCreditCardNumber.PressKeys(strcardNumber);
            		repo.DOM.SomeBodyTag.txtCreditCardName.PressKeys(strnameOnCard);
            		repo.DOM.SomeBodyTag.txtCreditCardSecurityCode.PressKeys(strcardSecurityCode);
            		            		
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.SomeBodyTag.cmbExpirationDateMonth,strexpireMonth);
            		
            		Validate.Exists(repo.DOM.SomeBodyTag.cmbExpirationDateYearInfo);
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeBodyTag.cmbExpirationDateYear, strexpireYear);
            		
        }
        
        void EnterSalesInformation()
        {
        			Validate.Exists(repo.DOM.SomeBodyTag.cmbSalesRep1Info);
        			SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.SomeBodyTag.cmbSalesRep1,varSalesRep1);
        			Validate.Exists(repo.DOM.SomeBodyTag.cmbSalesOperationTypeInfo);
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(), repo.DOM.SomeBodyTag.cmbSalesOperationType, varSalesOperationType);
            		   
        }
        
        
        void EnterAccountInformation_lc()
        {
        			string strAccountCountry, strTitle, strDept, strOrgname, strPhoneNumber;
            		FormDataAccount.GenerateAccountInfo(out strAccountCountry, out strTitle, out strDept, out strOrgname, out strPhoneNumber);
            		
            		Validate.Exists(repo.DOM.SomeTableTag.lc_cmbAccountCountry);
            		
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeTableTag.lc_cmbAccountCountry,strAccountCountry);
            		
            		repo.DOM.SomeTableTag.lc_txtAdminFirstName.PressKeys(FormDataAccount.GenerateFirstName()+varPersona);
            		repo.DOM.SomeTableTag.lc_txtAdminLastName.PressKeys(FormDataAccount.GenerateLastName());
            		repo.DOM.SomeTableTag.lc_txtTitle.PressKeys(strTitle);
            		repo.DOM.SomeTableTag.lc_txtDept.PressKeys(strDept);
            		repo.DOM.SomeTableTag.lc_txtOrgName.PressKeys(strOrgname);
            		repo.DOM.SomeTableTag.lc_txtPhoneNumber.PressKeys(strPhoneNumber);
            		
            		string strUsername, strEmail;
            		FormDataAccount.GenerateUsernameEmail(out strUsername, out strEmail);
            		varUsername=strUsername;
            		repo.DOM.SomeTableTag.lc_txtEmail.PressKeys(strEmail);
            		repo.DOM.SomeTableTag.lc_txtEmailConfirm.PressKeys(strEmail);
            		repo.DOM.SomeTableTag.lc_txtUserName.PressKeys(strUsername);
            		repo.DOM.SomeTableTag.lc_txtPassword.PressKeys(FormDataAccount.GeneratePassword());
            		repo.DOM.SomeTableTag.lc_txtPasswordConfirm.PressKeys(FormDataAccount.GeneratePassword());
            		
            		// TODO : StartDate and EndDate
            		            		
            		repo.DOM.DivTagTable_format.lc_btn_Step1of4_Continue.Click();
        }
        
        void EnterBillingInformation_lc()
        {
        			string strcompanyName, straddress, straptSuite, strcity, strstate, strzip, strcountry, strphone;
        	   	    FormDataBilling.GenerateAddress(out strcompanyName, out straddress, out straptSuite,out strcity, out strstate, out strzip, out strcountry, out strphone);	
        	   	    
            		Validate.Exists(repo.DOM.SomeTableTag1.lc_txtAddress);
            		repo.DOM.SomeTableTag1.lc_txtAddress.PressKeys(straddress);
            		repo.DOM.SomeTableTag1.lc_txtCity.PressKeys(strcity);
            		            		
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeTableTag1.lc_cmbBillingState,strstate);
            		repo.DOM.SomeTableTag1.lc_txtZip.PressKeys(strzip);
            		
            		string strpaymentType, strcardType, strcardNumber, strnameOnCard, strcardSecurityCode,  strexpireMonth, strexpireYear;
            		FormDataPayment.GenerateCreditCard(out strpaymentType, out strcardType, out strcardNumber, out strnameOnCard, out strcardSecurityCode, out strexpireMonth, out strexpireYear);
            		
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeTableTag2.lc_cmbCreditCardType, strcardType);
            		
            		repo.DOM.SomeTableTag2.lc_txtCreditCardNumber.PressKeys(strcardNumber);
            		repo.DOM.SomeTableTag2.lc_txtCreditCardName.PressKeys(strnameOnCard);
            		repo.DOM.SomeTableTag2.lc_txtCreditCardSecurity.PressKeys(strcardSecurityCode);
            		
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeTableTag2.lc_cmbExpirationDateMonth, strexpireMonth);
            		
            		Validate.Exists(repo.DOM.SomeTableTag2.lc_cmbExpirationYearInfo);
            		SelectTagUI.ChooseSelectTagOption(repo.DOM.BasePath.ToString(),repo.DOM.SomeTableTag2.lc_cmbExpirationYear, strexpireYear);
            		
            		
        }
         
        void HandleUntrustedConnection(string url)
        {
        	Validate.Exists(repo.DOM.ConnectionUntrustedPage.ButtonTagGetMeOutOfHereButtonInfo);
        	repo.DOM.ConnectionUntrustedPage.DivTagTechnicalContent.Click();
        	Validate.Exists(repo.DOM.ConnectionUntrustedPage.PTagTechnicalContentTextInfo);
        	Regex errorMsg = new Regex("^.*(Error code: ssl_error_bad_cert_domain)*^");
        	try
        	{
        		Validate.AreEqual(repo.DOM.ConnectionUntrustedPage.PTagTechnicalContentText.InnerText, errorMsg);
        		repo.DOM.ConnectionUntrustedPage.DivTagExpertContent.Click();
        		Validate.Exists(repo.DOM.ConnectionUntrustedPage.ButtonTagExceptionDialogButtonInfo);
        		repo.DOM.ConnectionUntrustedPage.ButtonTagExceptionDialogButton.Click();
        		
        		Validate.Exists(repo.FormAdd_Security_Exception.TextLocation_Info);
        		if(repo.FormAdd_Security_Exception.TextLocation_.TextValue != url)
        			repo.FormAdd_Security_Exception.TextLocation_.PressKeys(url);
        		
        		repo.FormAdd_Security_Exception.ButtonConfirm_Security_Excepti.Click();
        		Validate.Exists(repo.DOM.DivTagCtl00_UcHeaderAdminLogin.btnLoginInfo);	                                                        
        		
        	}
        	catch(ValidationException ve)
        	{
        		Report.Log(ReportLevel.Error, "Unknown Error - Could not login : " + ve.ToString());
        		throw new Ranorex.RanorexException();	
        	}
        
        }
            
        
    }
}
