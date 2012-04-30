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
	/// Represents payment information section on consumer registration page 2
	/// </summary>
	public class PaymentInformation
    {
    	/// <summary>
    	/// Standard = Set payment information to standard information.
    	/// </summary>
		public enum DefaultInfo {Standard};
		
		public enum Field {PaymentType,CreditCardType,CreditCardNumber,NameOnCard,SecurityCode,ExpireMonth,ExpireYear};
    	
    	private string paymentType;
    	private string amount;
    	private string creditCardType;
    	private string creditCardNumber;
    	private string nameOnCard;
    	private string creditCardSecurity;
    	private string expirationMonth;
    	private string expirationYear;
    	
    	private static PaymentInformationRepo paymentInformationRepo = null;
    	
    	//Ctrl-A to select all text then release Ctrl. Used so all text can be selected first before being typed over.
        private const string pressKeysSelectAll = "{Control down}{Akey}{Control up}";
    	
    	/// <summary>
        /// Initializes a new instance of the Lynda.Test.ConsumerPages.PaymentInformation class without initializing any 
        /// payment information.
    	/// </summary>
    	public PaymentInformation()
        {   
    		paymentInformationRepo=new PaymentInformationRepo();
        }  
    	
    	/// <summary>
    	/// Initializes a new instance of the Lynda.Test.ConsumerPages.PaymentInformation class.
    	/// </summary>
    	/// <param name="defaultInfo">Specifies how to initialize the payment information.</param>
    	public PaymentInformation(DefaultInfo defaultInfo):
    		this()
    	{
    		if (defaultInfo<DefaultInfo.Standard || defaultInfo>DefaultInfo.Standard)
    		{
    			throw new ArgumentOutOfRangeException("defaultInfo", defaultInfo,
					  "Must be one of the following: Standard");
    		}
    		
    		switch (defaultInfo)
        	{        		
        		case DefaultInfo.Standard:
        			{        				
						PaymentType="credit card";
						CreditCardType="Visa";
						CreditCardNumber="4111111111111111";
						NameOnCard="Sue Axelband";
						CreditCardSecurity="411";
						ExpirationMonth="08 - August";
						ExpirationYear="2012";
						break;
        			}
        		default:
        			throw new Exception(String.Format("Code not implemented yet: {0}", defaultInfo.ToString()));
        	}     		
    	}
    	
    	/// <summary>
    	/// Gets Ranorex.Adpater values from the web page and stores in this class instance's fields.
    	/// </summary>
    	internal void GetPaymentInfo()
    	{
    		PaymentType = SelectTagUI.GetSelectTagCurrentText(paymentInformationRepo.DOM.PaymentTypeSelect);
    		Amount = paymentInformationRepo.DOM.AmountTD.InnerText;
    		CreditCardType = SelectTagUI.GetSelectTagCurrentText(paymentInformationRepo.DOM.CreditCardTypeSelect);
    		CreditCardNumber = paymentInformationRepo.DOM.CreditCardNumberInput.Value;
    		NameOnCard = paymentInformationRepo.DOM.NameOnCardInput.Value;
    		CreditCardSecurity = paymentInformationRepo.DOM.CreditCardSecurityInput.Value;
    		ExpirationMonth = SelectTagUI.GetSelectTagCurrentText(paymentInformationRepo.DOM.ExpirationMonthSelect);
    		ExpirationYear = SelectTagUI.GetSelectTagCurrentText(paymentInformationRepo.DOM.ExpirationYearSelect);
    	}
    	
    	#region TypeOrSelectFields
    	private void SelectPaymentType()
    	{
    		if (paymentType!=null) {SelectTagUI.ChooseSelectTagOption(paymentInformationRepo.DOM.BasePath.ToString(), paymentInformationRepo.DOM.PaymentTypeSelect, paymentType);}
    	}
    	
    	private void SelectCreditCardType()
    	{
    		if (creditCardType!=null) {SelectTagUI.ChooseSelectTagOption(paymentInformationRepo.DOM.BasePath.ToString(), paymentInformationRepo.DOM.CreditCardTypeSelect, creditCardType);}
    	}
    	
    	private void TypeCreditCardNumber()
    	{
    		if (creditCardNumber!=null) {paymentInformationRepo.DOM.CreditCardNumberInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, creditCardNumber));}
    	}
    	
    	private void TypeNameOnCard()
    	{
    		if (nameOnCard!=null) {paymentInformationRepo.DOM.NameOnCardInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, nameOnCard));}
    	}
    	
    	private void TypeSecurityCode()
    	{
    		if (creditCardSecurity!=null) {paymentInformationRepo.DOM.CreditCardSecurityInput.PressKeys(String.Format("{0}{1}", pressKeysSelectAll, creditCardSecurity));}
    	}
    	
    	private void SelectExpireMonth()
    	{
    		if (expirationMonth!=null) {SelectTagUI.ChooseSelectTagOption(paymentInformationRepo.DOM.BasePath.ToString(), paymentInformationRepo.DOM.ExpirationMonthSelect, expirationMonth);}
    	}
    	
    	private void SelectExpireYear()
    	{
    		if (expirationYear!=null) {SelectTagUI.ChooseSelectTagOption(paymentInformationRepo.DOM.BasePath.ToString(), paymentInformationRepo.DOM.ExpirationYearSelect, expirationYear);}
    	}

    	#endregion

    	internal void EnterPaymentInfo(PaymentInformation.Field formField, string fieldData)
    	{
    		switch (formField)
    		{
    			case Field.PaymentType:
    				{
    					PaymentType=fieldData;
    					SelectPaymentType();
    					break;
    				}
    			case Field.CreditCardType:
    				{
    					CreditCardType=fieldData;
    					SelectCreditCardType();
    					break;
    				}
    			case Field.CreditCardNumber:
    				{
    					CreditCardNumber=fieldData;
    					TypeCreditCardNumber();
    					break;
    				}
    			case Field.NameOnCard:
    				{
    					NameOnCard=fieldData;
    					TypeNameOnCard();
    					break;
    				}
    			case Field.SecurityCode:
    				{
    					CreditCardSecurity=fieldData;
    					TypeSecurityCode();
    					break;
    				}
    			case Field.ExpireMonth:
    				{
    					ExpirationMonth=fieldData;
    					SelectExpireMonth();
    					break;
    				}
    			case Field.ExpireYear:
    				{
    					ExpirationYear=fieldData;
    					SelectExpireYear();
    					break;
    				}
    			default:
        			throw new Exception(String.Format("Code not implemented yet: {0}", formField.ToString()));
    		}
    	}
    	                               	
    	/// <summary>
    	/// Enters each field value from this PaymentInformation instance into the corresponding Ranorex.Adapter on the web page.
    	/// </summary>
    	internal void EnterPaymentInfo()
    	{
    		SelectPaymentType();
    		SelectCreditCardType();
    		TypeCreditCardNumber();
    		TypeNameOnCard();
    		TypeSecurityCode();
    		SelectExpireMonth();
    		SelectExpireYear();      			
    	}
    	
    	/// <summary>
        /// Gets or sets the Payment type in this instance.
        /// </summary>
        public string PaymentType
        {
        	get {return paymentType;}
        	set {paymentType = value;}
        }

        /// <summary>
        /// Gets or sets the Amount in this instance.
        /// </summary>
        public string Amount
        {
        	get {return amount;}
        	set {amount = value;}
        }
    	
        /// <summary>
        /// Gets or sets the Credit card type in this instance.
        /// </summary>
        public string CreditCardType
        {
        	get {return creditCardType;}
        	set {creditCardType = value;}
        }
        
        /// <summary>
        /// Gets or sets the credit card number in this instance.
        /// </summary>
        public string CreditCardNumber
        {
        	get {return creditCardNumber;}
        	set {creditCardNumber = value;}
        }
        
        /// <summary>
        /// Gets or sets the name on card in this instance.
        /// </summary>
        public string NameOnCard
        {
        	get {return nameOnCard;}
        	set {nameOnCard = value;}
        }
        
        /// <summary>
        /// Gets or sets the credit card security code in this instance.
        /// </summary>
        public string CreditCardSecurity
        {
        	get {return creditCardSecurity;}
        	set {creditCardSecurity = value;}
        }
        
        /// <summary>
        /// Gets or sets the expiration month in this instance.
        /// </summary>
        public string ExpirationMonth
        {
        	get {return expirationMonth;}
        	set {expirationMonth = value;}
        }
        
        /// <summary>
        /// Gets or sets the expiration year in this instance.
        /// </summary>
        public string ExpirationYear
        {
        	get {return expirationYear;}
        	set {expirationYear = value;}
        }
    	
    }
	

}
