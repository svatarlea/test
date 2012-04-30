using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

using ConsumerPagesMain.RegStep2;
using Lynda.Test.Advanced.Utilities.WebPages;

namespace Lynda.Test.ConsumerPages
{   	
	/// <summary>
	/// Represents billing information section on consumer registration page 2
	/// </summary>
	public class BillingInformation
    {       
		/// <summary>
		/// Standard = Set billing information to standard information.
		/// </summary>
		public enum DefaultInfo {Standard};
		
		/// <summary>
		/// Field in Billing Information form.
		/// </summary>
		public enum Field {Firstname,Lastname,Company,Address,Apt,City,State,Zip,Country,Phone,BillingEmail,HowHear};
		
		private BillingInformationRepo billingInformationRepo = null;
		
		//Ctrl-A to select all text then release Ctrl. Used so all text can be selected first before being typed over.
        private const string pressKeysSelectAll = "{Control down}{Akey}{Control up}";
		
		private string firstName;
        private string lastName;
        private string company;
        private string address;
        private string apt;
        private string city;
        private string state;
        private string zip;
        private string country;
        private string phone;
        private string billingEmail;
        private string howHear;

        /// <summary>
        /// Initializes a new instance of the Lynda.Test.ConsumerPages.BillingInformation class without initializing any 
        /// billing information.
        /// </summary>
        public BillingInformation()
        {   	
        	billingInformationRepo = new BillingInformationRepo();
        }        
        
        /// <summary>
        /// Initializes a new instance of the Lynda.Test.ConsumerPages.BillingInformation class.
        /// </summary>
        /// <param name="defaultInfo">Specifies how to initalize the billing information.</param>
        public BillingInformation(DefaultInfo defaultInfo):
        	this()
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
        				FirstName="TESTfirstname";
        				LastName="test";
        				Company="lynda.com";
        				Address="6410 via real";
        				Apt=null;
        				City="Carpinteria";
        				State="California";
        				Zip="93013";
        				Country="United States";
        				Phone="5555555555";      				
        				BillingEmail=null;
        				HowHear="Other";
        				break;
        			}
        		default:
        			throw new Exception(String.Format("Code not implemented yet: {0}", defaultInfo.ToString()));
        	}      	
        }
        
        /// <summary>
        /// Gets Ranorex.Adpater values from the web page and stores in this class instance's fields.
        /// </summary>
        internal void GetBillingInfo()
        {       				
        	FirstName=billingInformationRepo.DOM.FirstNameInput.Value;
        	LastName=billingInformationRepo.DOM.LastNameInput.Value;
        	Company=billingInformationRepo.DOM.CompanyInput.Value;
        	Address=billingInformationRepo.DOM.AddressInput.Value;
        	Apt=billingInformationRepo.DOM.AptInput.Value;
        	City=billingInformationRepo.DOM.CityInput.Value;      	
        	State = SelectTagUI.GetSelectTagCurrentText(billingInformationRepo.DOM.StateSelect);         	
        	Zip=billingInformationRepo.DOM.ZipInput.Value;
        	Country=SelectTagUI.GetSelectTagCurrentText(billingInformationRepo.DOM.CountrySelect);
        	Phone=billingInformationRepo.DOM.PhoneInput.Value;
        	BillingEmail=billingInformationRepo.DOM.BillingEmailInput.Value;
        	HowHear=SelectTagUI.GetSelectTagCurrentText(billingInformationRepo.DOM.HowHearSelect);        	
        }      
        
        #region TypeOrSelectFields
        private void TypeFirstName()
        {
        	if (firstName!=null) {billingInformationRepo.DOM.FirstNameInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, firstName));}
        }
        
        private void TypeLastName()
        {
        	if (lastName!=null) {billingInformationRepo.DOM.LastNameInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, lastName));}
        }
        
        private void TypeCompany()
        {
        	if (company!=null) {billingInformationRepo.DOM.CompanyInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, company));}
        }
        
        private void TypeAddress()
        {
        	if (address!=null) {billingInformationRepo.DOM.AddressInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, address));}
        }
        
        private void TypeApt()
        {
        	if (apt!=null) {billingInformationRepo.DOM.AptInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, apt));}
        }
        
        private void TypeCity()
        {
        	if (city!=null) {billingInformationRepo.DOM.CityInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, city));}
        }
        
        private void SelectState()
        {
        	if (state!=null) {SelectTagUI.ChooseSelectTagOption(billingInformationRepo.DOM.BasePath.ToString(), billingInformationRepo.DOM.StateSelect, state);}
        }
        
        private void TypeZip()
        {
        	if (zip!=null) {billingInformationRepo.DOM.ZipInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, zip));}
        }
        
        private void SelectCountry()
        {
        	if (country!=null) {SelectTagUI.ChooseSelectTagOption(billingInformationRepo.DOM.BasePath.ToString(), billingInformationRepo.DOM.CountrySelect, country);}
        }
        
        private void TypePhone()
        {
        	if (phone!=null) {billingInformationRepo.DOM.PhoneInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, phone));}
        }
        
        private void TypeBilling()
        {
        	if (billingEmail!=null) {billingInformationRepo.DOM.BillingEmailInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, billingEmail));}
        }
        
        private void SelectHowHear()
        {
        	if (howHear!=null) {SelectTagUI.ChooseSelectTagOption(billingInformationRepo.DOM.BasePath.ToString(), billingInformationRepo.DOM.HowHearSelect, howHear);}
        }
        #endregion

        internal void EnterBillingInfo(BillingInformation.Field formField, string fieldData)
        {
        	switch (formField)
        	{
        		case Field.Firstname:
        			{
        				FirstName=fieldData;
        				TypeFirstName();
        				break;
        			}
        		case Field.Lastname:
        			{
        				LastName=fieldData;
        				TypeLastName();
        				break;
        			}
        		case Field.Company:
        			{
        				Company=fieldData;
        				TypeCompany();
        				break;
        			}
        		case Field.Address:
        			{
        				Address=fieldData;
        				TypeAddress();
        				break;
        			}
        		case Field.Apt:
        			{
        				Apt=fieldData;
        				TypeApt();
        				break;
        			}
        		case Field.City:
					{
        				City=fieldData;
        				TypeCity();
        				break;
					}
        		case Field.State:
        			{
        				State=fieldData;
        				SelectState();
        				break;
        			}
        		case Field.Zip:
        			{
        				Zip=fieldData;
        				TypeZip();
        				break;
        			}
        		case Field.Country:
        			{
        				Country=fieldData;
        				SelectCountry();
        				break;
        			}
        		case Field.Phone:
        			{
        				Phone=fieldData;
        				TypePhone();
        				break;
        			}
        		case Field.BillingEmail:
        			{
        				BillingEmail=fieldData;
        				TypeBilling();
        				break;
        			}
        		case Field.HowHear:
        			{
        				HowHear=fieldData;
        				SelectHowHear();
        				break;
        			}
        		default:
        			throw new Exception(String.Format("Code not implemented yet: {0}", formField.ToString()));
        	}
        }
        
        /// <summary>
        /// Enters each field value from this BillingInformation instance into the corresponding Ranorex.Adapter on the web page.
        /// </summary>
        internal void EnterBillingInfo()
        {
        	TypeFirstName();
        	TypeLastName();
        	TypeCompany();
        	TypeAddress();
        	TypeApt();
        	TypeCity();
        	SelectState();
        	TypeZip();
        	SelectCountry();
        	TypePhone();
        	TypeBilling();
        	SelectHowHear();
		}
       
        /// <summary>
        /// Gets or sets the billing first name in this instance.
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
        /// Gets or sets the billing last name in this instance.
        /// </summary>
        public string LastName
        {
        	get {return lastName;}
        	set {lastName = value;}
        }
               
        /// <summary>
        /// Gets or sets the company name in this instance.
        /// </summary>
        public string Company
        {
        	get {return company;}
        	set {company = value;}
        }
        
        /// <summary>
        /// Gets or sets the address in this instance.
        /// </summary>
        public string Address
        {
        	get {return address;}
        	set {address = value;}
        }
        
        /// <summary>
        /// Gets or sets the Apt in this instance.
        /// </summary>
        public string Apt
        {
        	get {return apt;}
        	set {apt = value;}
        }
        
        /// <summary>
        /// Gets or sets the City in this instance.
        /// </summary>
        public string City
        {
        	get {return city;}
        	set {city = value;}
        }
        
        /// <summary>
        /// Gets or sets the State in this instance.
        /// </summary>
        public string State
        {
        	get {return state;}
        	set {state = value;}
        }
        
        /// <summary>
        /// Gets or sets the Zip in this instance.
        /// </summary>
        public string Zip
        {
        	get {return zip;}
        	set {zip = value;}
        }
        
        /// <summary>
        /// Gets or sets the Country in this instance.
        /// </summary>
        public string Country
        {
        	get {return country;}
        	set {country = value;}
        }
        
        /// <summary>
        /// Gets or sets the Phone in this instance.
        /// </summary>
        public string Phone
        {
        	get {return phone;}
        	set {phone = value;}
        }
        
        /// <summary>
        /// Gets or sets the Billing email in this instance.
        /// </summary>
        public string BillingEmail
        {
        	get {return billingEmail;}
        	set {billingEmail = value;}
        }
        
        /// <summary>
        /// Gets or sets the "How did you hear about us?" in this instance.
        /// </summary>
        public string HowHear
        {
        	get {return howHear;}
        	set {howHear = value;}
        }
       
	}


}
