using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegStep1;

namespace Lynda.Test.ConsumerPages
{    	
	/// <summary>
	/// Represents account information section on consumer registration page 1
	/// </summary>
	public class AccountInfo
    {       
		/// <summary>
		/// Standard = Set account information to standard information.
		/// </summary>
		public enum DefaultInfo {Standard};
		
		/// <summary>
		/// Field in Account Information form.
		/// </summary>
		public enum Field {FirstName, LastName, Email, Username, Password, ConfirmPassword};
		
		private AccountInfoRepo accountInfoRepo = null;
		
		//Ctrl-A to select all text then release Ctrl. Used so all text can be selected first before being typed over.
        private const string pressKeysSelectAll = "{Control down}{Akey}{Control up}";
		
		private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string password;
        private string passwordconfirm;

    	private bool signMeUpNewsletters;
    	private bool signMeUpNewReleases;
    	private bool signMeUpSpecial;

    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.AccountInfo class without initializing any account information.
    	/// </summary>
    	public AccountInfo()
        {      
    		accountInfoRepo = new AccountInfoRepo();
        }
    	
    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.AccountInfo class.
    	/// </summary>
    	/// <param name="defaultInfo">Specifies how to initalize the account information.</param>
    	public AccountInfo(DefaultInfo defaultInfo) :
    		this ()
        {       	
        	if (defaultInfo < DefaultInfo.Standard || defaultInfo > DefaultInfo.Standard)
        	{
        		throw new ArgumentOutOfRangeException("defaultInfo", defaultInfo,
					  "Must be one of the following: Standard");

        	}
        	switch (defaultInfo)
        	{
        		case DefaultInfo.Standard:
        			{
        				//Form fields
        				FirstName="TESTfirstname";
        				LastName="test";        				
        				System.DateTime currentDateTime = System.DateTime.Now;    				
						//short date from current DateTime e.g. "2/20/2012". Uses current culture e.g. mm/dd/yyyy
						string shortDate = currentDateTime.ToShortDateString();
						//Format for use as valid e-mail address e.g. 2-20-2012
						shortDate = shortDate.Replace('/', '-');						
                        //Fraction of the day that has elapsed since midnight
						TimeSpan timeSinceMidnight = currentDateTime.TimeOfDay;
						//Seconds since midnight e.g. 47003.9293606
                        double secondsSinceMidnight = timeSinceMidnight.TotalSeconds;
                        //Format "shortDate"-"secondsSinceMidnight" to no decimal places. e.g. 2-20-2012-47004
                        Username = String.Format("{0}-{1:0.}", shortDate, secondsSinceMidnight);                       
                        Email = String.Format("{0}@mailinator.com", username);                        
                        Password = "lynda1";
                        PasswordConfirm = password;
                        
                        //No sign ups
                        SignMeUpNewReleases=false;
        				SignMeUpNewsletters=false;
        				SignMeUpSpecial=false;

        				break;
        			}
        		default:
        			throw new Exception(String.Format("Code not implemented yet: {0}", defaultInfo.ToString()));
        	}
        	
        }
        
        /// <summary>
        /// Gets Ranorex.Adpater values from the web page and store in this class instance's fields.
        /// </summary>
        internal void GetAccountInfo()
        {       	
        	FirstName = accountInfoRepo.DOM.FirstNameInput.Value;
        	LastName=accountInfoRepo.DOM.LastNameInput.Value;
        	Email=accountInfoRepo.DOM.EmailInput.Value;
        	Username=accountInfoRepo.DOM.UsernameInput.Value;
        	Password=accountInfoRepo.DOM.PasswordInput.Value;
        	PasswordConfirm=accountInfoRepo.DOM.PasswordConfirmInput.Value;
        	        	
        	SignMeUpNewsletters = Convert.ToBoolean(accountInfoRepo.DOM.NewslettersInput.Checked);
        	SignMeUpNewReleases = Convert.ToBoolean(accountInfoRepo.DOM.NewReleasesInput.Checked);
        	SignMeUpSpecial = Convert.ToBoolean(accountInfoRepo.DOM.SpecialInput.Checked);       	
        }
 
        /// <summary>
        /// Checks or unchecks monthly newsletters on web page.
        /// </summary>
        internal void CheckOrUncheckMonthlyNewsletters()
        {
        	bool newslettersChecked = Convert.ToBoolean(accountInfoRepo.DOM.NewslettersInput.Checked);
        	//If Newsletters currently checked on the page and should be unchecked
        	//or Newsletters currently unchecked and should be checked
        	if ((newslettersChecked && !signMeUpNewsletters) ||
        	    (!newslettersChecked && signMeUpNewsletters))
        	{
        		//Click the box on the page to check/uncheck it
        		accountInfoRepo.DOM.NewslettersInput.Click();
        	}
        }
        
        /// <summary>
        /// Checks or unchecks new releases notifications on web page.
        /// </summary>
        internal void CheckOrUncheckNewReleases()
        {
        	bool newReleasesChecked = Convert.ToBoolean(accountInfoRepo.DOM.NewReleasesInput.Checked);
        	if ((newReleasesChecked && !signMeUpNewReleases) ||
        	    (!newReleasesChecked && signMeUpNewReleases))
        	{
        		//Click the box on the page to check/uncheck it
        		accountInfoRepo.DOM.NewReleasesInput.Click();
        	}

        }
        
        /// <summary>
        /// Checks or unchecks special announcements on web page.
        /// </summary>
        internal void CheckOrUncheckSpecial()
        {
        	bool specialChecked = Convert.ToBoolean(accountInfoRepo.DOM.SpecialInput.Checked);
        	if ((specialChecked && !signMeUpSpecial) ||
        	    (!specialChecked && signMeUpSpecial))
        	{
        		//Click the box on the page to check/uncheck it
        		accountInfoRepo.DOM.SpecialInput.Click();
        	}
        }
        
        #region TypeIntoFields
        private void TypeFirstName()
        {
        	if (firstName!=null) {accountInfoRepo.DOM.FirstNameInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, firstName));}
        }
        
        private void TypeLastName()
        {
        	if (lastName!=null) {accountInfoRepo.DOM.LastNameInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, lastName));}
        }
        
        private void TypeEmail()
        {
        	if (email!=null) {accountInfoRepo.DOM.EmailInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, email));}
        }
        
        private void TypeUsername()
        {
        	if (username!=null) {accountInfoRepo.DOM.UsernameInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, username));}
        }
        
        private void TypePassword()
        {
        	if (password!=null) {accountInfoRepo.DOM.PasswordInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, password));}
        }
        
        private void TypeConfirmPassword()
        {
       		if (passwordconfirm!=null) {accountInfoRepo.DOM.PasswordConfirmInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, passwordconfirm));}
        }
        #endregion

        /// <summary>
        /// Enters value for field Lynda.Test.ConsumerPage.AccountInfo.Field from this AccountInfo instance
        /// into the Ranorex.Adpater field on the web page.
        /// </summary>
        internal void EnterAccountInfo(AccountInfo.Field formField, string fieldData)
        {
        	switch (formField)
        	{
        		case Field.FirstName:
        			{
        				FirstName=fieldData;
        				TypeFirstName();
        				break;
        			}
        		case Field.LastName:
        			{
        				LastName=fieldData;
        				TypeLastName();
        				break;
        			}
        		case Field.Email:
        			{
        				Email=fieldData;
        				TypeEmail();
        				break;
        			}
        		case Field.Username:
        			{
        				Username=fieldData;
        				TypeUsername();
        				break;
        			}
        		case Field.Password:
        			{
        				Password=fieldData;
        				TypePassword();
        				break;
        			}
        		case Field.ConfirmPassword:
        			{
        				PasswordConfirm=fieldData;
        				TypeConfirmPassword();
        				break;	
        			}
        		default:
        			throw new Exception(String.Format("Code not implemented yet: {0}", formField.ToString()));
        	}
        }

        /// <summary>
        /// Enters each field value from this AccountInfo instance into the corresponding Ranorex.Adapter on the web page.
        /// </summary>
        internal void EnterAccountInfo()
        {          
        	//Form fields       	
        	TypeFirstName();
        	TypeLastName();
        	TypeEmail();
        	TypeUsername();
        	TypePassword();
        	TypeConfirmPassword();       				
        	//Sign ups       	
        	CheckOrUncheckMonthlyNewsletters();
        	CheckOrUncheckNewReleases();
        	CheckOrUncheckSpecial();
        }  
        
        /// <summary>
        /// Gets or sets the account first name in this instance.
        /// </summary>
        public string FirstName
        {            
        	get { return firstName; }
            set
            {
            	const int MAXFIRSTNAMELENGTH = 20;
                if (value!=null && value.Length > MAXFIRSTNAMELENGTH)
                {
                    throw new ArgumentException(String.Format("Length cannot exceed {0} characters", value));
                }
                firstName = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the account last name in this instance.
        /// </summary>
        public string LastName
        {
        	get {return lastName;}
        	set {lastName = value;}
        }
        
        /// <summary>
        /// Gets or sets the account email in this instance.
        /// </summary>
        public string Email
        {
        	get {return email;}
        	set {email = value;}
        }
        
        /// <summary>
        /// Gets or sets the account username in this instance.
        /// </summary>
        public string Username
        {
        	get {return username;}
        	set {username = value;}        	
        }
        
        /// <summary>
        /// Gets or sets the account password in this instance.
        /// </summary>
        public string Password
        {
        	get {return password;}
        	set {password = value;}
        }
        
        /// <summary>
        /// Gets or sets the account password confirmation in this instance.
        /// </summary>
        public string PasswordConfirm
        {
        	get {return passwordconfirm;}
        	set {passwordconfirm = value;}
        }      
        
        /// <summary>
        /// Gets or sets the state of the "sign me up for monthly newsletters" check box in this instance.
        /// </summary>
        public bool SignMeUpNewReleases
        {
        	get {return signMeUpNewReleases;}
        	set {signMeUpNewReleases=value;}
        }
        
        /// <summary>
        /// Gets or sets the state of the "sign me up for new releases notification" check box in this instance.
        /// </summary>
        public bool SignMeUpNewsletters
        {
        	get {return signMeUpNewsletters;}
        	set {signMeUpNewsletters=value;}
        }
        
        /// <summary>
        /// Gets or sets the state of the "sign me up for special announcements and offers" check box in this instance.
        /// </summary>
        public bool SignMeUpSpecial
        {
        	get {return signMeUpSpecial;}
        	set {signMeUpSpecial=value;}
        }

        
    }      
}
