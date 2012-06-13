///////////////////////////////////////////////////////////////////////////////
//
// This file was automatically generated by RANOREX.
// DO NOT MODIFY THIS FILE! It is regenerated by the designer.
// All your modifications will be lost!
// http://www.ranorex.com
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Repository;
using Ranorex.Core.Testing;

namespace ConsumerPagesMain.RegStep2
{
    /// <summary>
    /// The class representing the BillingInformationRepo element repository.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "3.3.0")]
    [RepositoryFolder("d11d1e90-a524-406b-aa69-266b05cccaf4")]
    public partial class BillingInformationRepo : RepoGenBaseFolder
    {
        static BillingInformationRepo instance = new BillingInformationRepo();
        BillingInformationRepoFolders.DOMAppFolder _dom;
        BillingInformationRepoFolders.StateContainerAppFolder _statecontainer;

        /// <summary>
        /// Gets the singleton class instance representing the BillingInformationRepo element repository.
        /// </summary>
        [RepositoryFolder("d11d1e90-a524-406b-aa69-266b05cccaf4")]
        public static BillingInformationRepo Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Repository class constructor.
        /// </summary>
        public BillingInformationRepo() 
            : base("BillingInformationRepo", "", null, 30000, false, "d11d1e90-a524-406b-aa69-266b05cccaf4", "./RepositoryImages\\BillingInformationRepod11d1e90.rximgres")
        {
            _dom = new BillingInformationRepoFolders.DOMAppFolder(this);
            _statecontainer = new BillingInformationRepoFolders.StateContainerAppFolder(this);
        }

#region Variables

#endregion

        /// <summary>
        /// The DOM folder.
        /// </summary>
        [RepositoryFolder("8e83c3dd-c9a3-42fe-84b2-0e8afeca1ba0")]
        public virtual BillingInformationRepoFolders.DOMAppFolder DOM
        {
            get { return _dom; }
        }

        /// <summary>
        /// The StateContainer folder.
        /// </summary>
        [RepositoryFolder("0c689279-1d54-4cd2-afe0-f504a90ad766")]
        public virtual BillingInformationRepoFolders.StateContainerAppFolder StateContainer
        {
            get { return _statecontainer; }
        }
    }

    /// <summary>
    /// Inner folder classes.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCode("Ranorex", "3.3.0")]
    public partial class BillingInformationRepoFolders
    {
        /// <summary>
        /// The DOMAppFolder folder.
        /// </summary>
        [RepositoryFolder("8e83c3dd-c9a3-42fe-84b2-0e8afeca1ba0")]
        public partial class DOMAppFolder : RepoGenBaseFolder
        {
            RepoItemInfo _selfInfo;
            RepoItemInfo _firstnameinputInfo;
            RepoItemInfo _lastnameinputInfo;
            RepoItemInfo _companyinputInfo;
            RepoItemInfo _addressinputInfo;
            RepoItemInfo _aptinputInfo;
            RepoItemInfo _cityinputInfo;
            RepoItemInfo _stateselectInfo;
            RepoItemInfo _zipinputInfo;
            RepoItemInfo _countryselectInfo;
            RepoItemInfo _phoneinputInfo;
            RepoItemInfo _billingemailinputInfo;
            RepoItemInfo _howhearselectInfo;

            /// <summary>
            /// Creates a new DOM  folder.
            /// </summary>
            public DOMAppFolder(RepoGenBaseFolder parentFolder) :
                    base("DOM", "/dom", parentFolder, 30000, false, "8e83c3dd-c9a3-42fe-84b2-0e8afeca1ba0", "")
            {
                _selfInfo = new RepoItemInfo(this, "Self", "", 0, null, "8e83c3dd-c9a3-42fe-84b2-0e8afeca1ba0");
                _firstnameinputInfo = new RepoItemInfo(this, "FirstNameInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[1]/td[@innertext=' *']/input[@id='txtFirstName']", 30000, null, "d5f7a92d-12b5-4074-a8db-8fcdeef766d4");
                _lastnameinputInfo = new RepoItemInfo(this, "LastNameInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[2]/td[@innertext=' *']/input[@id='txtLastName']", 30000, null, "755d4fb5-9598-42b2-9bd8-b861abf2d799");
                _companyinputInfo = new RepoItemInfo(this, "CompanyInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[3]/td[2]/input[@id='txtOrgName']", 30000, null, "4c79313a-2038-4b6b-8fe6-775c6f28f72e");
                _addressinputInfo = new RepoItemInfo(this, "AddressInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[4]/td[@innertext=' *']/input[@id='txtAddress1']", 30000, null, "86a8d0a4-5af6-4324-950b-0b4d6ca6b53d");
                _aptinputInfo = new RepoItemInfo(this, "AptInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[5]/td[2]/input[@id='txtAddress2']", 30000, null, "4ef1279d-68a2-4a76-ad5d-a8d291d9279e");
                _cityinputInfo = new RepoItemInfo(this, "CityInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[6]/td[@innertext=' *']/input[@id='txtCity']", 30000, null, "98ddb7e5-ce75-468b-9d90-75ba6ca6eb58");
                _stateselectInfo = new RepoItemInfo(this, "StateSelect", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[7]/td[2]/select[@id='drpState']", 30000, null, "a749f6b9-9f97-4fd3-bbd4-0b2e514c5e1d");
                _zipinputInfo = new RepoItemInfo(this, "ZipInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[8]/td[2]/input[@id='txtZIP']", 30000, null, "374b17c5-745e-4c16-9dd5-fc3a926ff804");
                _countryselectInfo = new RepoItemInfo(this, "CountrySelect", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[9]/td[2]/select[@id='drpCountry']", 30000, null, "80b0b8bb-b5ef-4696-9ed9-b693944ed6d8");
                _phoneinputInfo = new RepoItemInfo(this, "PhoneInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[10]/td[@innertext=' *']/input[@id='txtPhone']", 30000, null, "b46ad68e-0e54-4adf-870e-fd2836d35e10");
                _billingemailinputInfo = new RepoItemInfo(this, "BillingEmailInput", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[11]/td[@innertext=' *']/input[@id='txtEmail']", 30000, null, "a855235b-7cc0-49aa-876b-1ecd8610ed98");
                _howhearselectInfo = new RepoItemInfo(this, "HowHearSelect", "body/div[1]/div[2]/form/fieldset[1]/div[1]/table/tbody/tr[12]/td[2]/select[@id='drpHearAboutUs']", 30000, null, "5192a3ad-0ce4-4336-a04e-b6986dd77409");
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("8e83c3dd-c9a3-42fe-84b2-0e8afeca1ba0")]
            public virtual Ranorex.WebDocument Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.WebDocument>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("8e83c3dd-c9a3-42fe-84b2-0e8afeca1ba0")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The FirstNameInput item.
            /// </summary>
            [RepositoryItem("d5f7a92d-12b5-4074-a8db-8fcdeef766d4")]
            public virtual Ranorex.InputTag FirstNameInput
            {
                get
                {
                    return _firstnameinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The FirstNameInput item info.
            /// </summary>
            [RepositoryItemInfo("d5f7a92d-12b5-4074-a8db-8fcdeef766d4")]
            public virtual RepoItemInfo FirstNameInputInfo
            {
                get
                {
                    return _firstnameinputInfo;
                }
            }

            /// <summary>
            /// The LastNameInput item.
            /// </summary>
            [RepositoryItem("755d4fb5-9598-42b2-9bd8-b861abf2d799")]
            public virtual Ranorex.InputTag LastNameInput
            {
                get
                {
                    return _lastnameinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The LastNameInput item info.
            /// </summary>
            [RepositoryItemInfo("755d4fb5-9598-42b2-9bd8-b861abf2d799")]
            public virtual RepoItemInfo LastNameInputInfo
            {
                get
                {
                    return _lastnameinputInfo;
                }
            }

            /// <summary>
            /// The CompanyInput item.
            /// </summary>
            [RepositoryItem("4c79313a-2038-4b6b-8fe6-775c6f28f72e")]
            public virtual Ranorex.InputTag CompanyInput
            {
                get
                {
                    return _companyinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The CompanyInput item info.
            /// </summary>
            [RepositoryItemInfo("4c79313a-2038-4b6b-8fe6-775c6f28f72e")]
            public virtual RepoItemInfo CompanyInputInfo
            {
                get
                {
                    return _companyinputInfo;
                }
            }

            /// <summary>
            /// The AddressInput item.
            /// </summary>
            [RepositoryItem("86a8d0a4-5af6-4324-950b-0b4d6ca6b53d")]
            public virtual Ranorex.InputTag AddressInput
            {
                get
                {
                    return _addressinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The AddressInput item info.
            /// </summary>
            [RepositoryItemInfo("86a8d0a4-5af6-4324-950b-0b4d6ca6b53d")]
            public virtual RepoItemInfo AddressInputInfo
            {
                get
                {
                    return _addressinputInfo;
                }
            }

            /// <summary>
            /// The AptInput item.
            /// </summary>
            [RepositoryItem("4ef1279d-68a2-4a76-ad5d-a8d291d9279e")]
            public virtual Ranorex.InputTag AptInput
            {
                get
                {
                    return _aptinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The AptInput item info.
            /// </summary>
            [RepositoryItemInfo("4ef1279d-68a2-4a76-ad5d-a8d291d9279e")]
            public virtual RepoItemInfo AptInputInfo
            {
                get
                {
                    return _aptinputInfo;
                }
            }

            /// <summary>
            /// The CityInput item.
            /// </summary>
            [RepositoryItem("98ddb7e5-ce75-468b-9d90-75ba6ca6eb58")]
            public virtual Ranorex.InputTag CityInput
            {
                get
                {
                    return _cityinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The CityInput item info.
            /// </summary>
            [RepositoryItemInfo("98ddb7e5-ce75-468b-9d90-75ba6ca6eb58")]
            public virtual RepoItemInfo CityInputInfo
            {
                get
                {
                    return _cityinputInfo;
                }
            }

            /// <summary>
            /// The StateSelect item.
            /// </summary>
            [RepositoryItem("a749f6b9-9f97-4fd3-bbd4-0b2e514c5e1d")]
            public virtual Ranorex.SelectTag StateSelect
            {
                get
                {
                    return _stateselectInfo.CreateAdapter<Ranorex.SelectTag>(true);
                }
            }

            /// <summary>
            /// The StateSelect item info.
            /// </summary>
            [RepositoryItemInfo("a749f6b9-9f97-4fd3-bbd4-0b2e514c5e1d")]
            public virtual RepoItemInfo StateSelectInfo
            {
                get
                {
                    return _stateselectInfo;
                }
            }

            /// <summary>
            /// The ZipInput item.
            /// </summary>
            [RepositoryItem("374b17c5-745e-4c16-9dd5-fc3a926ff804")]
            public virtual Ranorex.InputTag ZipInput
            {
                get
                {
                    return _zipinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The ZipInput item info.
            /// </summary>
            [RepositoryItemInfo("374b17c5-745e-4c16-9dd5-fc3a926ff804")]
            public virtual RepoItemInfo ZipInputInfo
            {
                get
                {
                    return _zipinputInfo;
                }
            }

            /// <summary>
            /// The CountrySelect item.
            /// </summary>
            [RepositoryItem("80b0b8bb-b5ef-4696-9ed9-b693944ed6d8")]
            public virtual Ranorex.SelectTag CountrySelect
            {
                get
                {
                    return _countryselectInfo.CreateAdapter<Ranorex.SelectTag>(true);
                }
            }

            /// <summary>
            /// The CountrySelect item info.
            /// </summary>
            [RepositoryItemInfo("80b0b8bb-b5ef-4696-9ed9-b693944ed6d8")]
            public virtual RepoItemInfo CountrySelectInfo
            {
                get
                {
                    return _countryselectInfo;
                }
            }

            /// <summary>
            /// The PhoneInput item.
            /// </summary>
            [RepositoryItem("b46ad68e-0e54-4adf-870e-fd2836d35e10")]
            public virtual Ranorex.InputTag PhoneInput
            {
                get
                {
                    return _phoneinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The PhoneInput item info.
            /// </summary>
            [RepositoryItemInfo("b46ad68e-0e54-4adf-870e-fd2836d35e10")]
            public virtual RepoItemInfo PhoneInputInfo
            {
                get
                {
                    return _phoneinputInfo;
                }
            }

            /// <summary>
            /// The BillingEmailInput item.
            /// </summary>
            [RepositoryItem("a855235b-7cc0-49aa-876b-1ecd8610ed98")]
            public virtual Ranorex.InputTag BillingEmailInput
            {
                get
                {
                    return _billingemailinputInfo.CreateAdapter<Ranorex.InputTag>(true);
                }
            }

            /// <summary>
            /// The BillingEmailInput item info.
            /// </summary>
            [RepositoryItemInfo("a855235b-7cc0-49aa-876b-1ecd8610ed98")]
            public virtual RepoItemInfo BillingEmailInputInfo
            {
                get
                {
                    return _billingemailinputInfo;
                }
            }

            /// <summary>
            /// The HowHearSelect item.
            /// </summary>
            [RepositoryItem("5192a3ad-0ce4-4336-a04e-b6986dd77409")]
            public virtual Ranorex.SelectTag HowHearSelect
            {
                get
                {
                    return _howhearselectInfo.CreateAdapter<Ranorex.SelectTag>(true);
                }
            }

            /// <summary>
            /// The HowHearSelect item info.
            /// </summary>
            [RepositoryItemInfo("5192a3ad-0ce4-4336-a04e-b6986dd77409")]
            public virtual RepoItemInfo HowHearSelectInfo
            {
                get
                {
                    return _howhearselectInfo;
                }
            }
        }

        /// <summary>
        /// The StateContainerAppFolder folder.
        /// </summary>
        [RepositoryFolder("0c689279-1d54-4cd2-afe0-f504a90ad766")]
        public partial class StateContainerAppFolder : RepoGenBaseFolder
        {
            RepoItemInfo _selfInfo;
            RepoItemInfo _statelistitemInfo;

            /// <summary>
            /// Creates a new StateContainer  folder.
            /// </summary>
            public StateContainerAppFolder(RepoGenBaseFolder parentFolder) :
                    base("StateContainer", "/container[@caption='selectbox']", parentFolder, 30000, true, "0c689279-1d54-4cd2-afe0-f504a90ad766", "")
            {
                _selfInfo = new RepoItemInfo(this, "Self", "", 0, null, "0c689279-1d54-4cd2-afe0-f504a90ad766");
                _statelistitemInfo = new RepoItemInfo(this, "StateListitem", "listitem", 30000, null, "8446ba18-d980-4558-81d7-ebe9092bc1fa");
            }

            /// <summary>
            /// The Self item.
            /// </summary>
            [RepositoryItem("0c689279-1d54-4cd2-afe0-f504a90ad766")]
            public virtual Ranorex.Container Self
            {
                get
                {
                    return _selfInfo.CreateAdapter<Ranorex.Container>(true);
                }
            }

            /// <summary>
            /// The Self item info.
            /// </summary>
            [RepositoryItemInfo("0c689279-1d54-4cd2-afe0-f504a90ad766")]
            public virtual RepoItemInfo SelfInfo
            {
                get
                {
                    return _selfInfo;
                }
            }

            /// <summary>
            /// The StateListitem item.
            /// </summary>
            [RepositoryItem("8446ba18-d980-4558-81d7-ebe9092bc1fa")]
            public virtual Ranorex.ListItem StateListitem
            {
                get
                {
                    return _statelistitemInfo.CreateAdapter<Ranorex.ListItem>(true);
                }
            }

            /// <summary>
            /// The StateListitem item info.
            /// </summary>
            [RepositoryItemInfo("8446ba18-d980-4558-81d7-ebe9092bc1fa")]
            public virtual RepoItemInfo StateListitemInfo
            {
                get
                {
                    return _statelistitemInfo;
                }
            }
        }

    }
}