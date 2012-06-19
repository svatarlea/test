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

using General.Utilities.Forms;

namespace Tests.General.Tests.ActivationKey
{
    /// <summary>
    /// Description of ActivationKey.
    /// </summary>
    [TestModule("57C5AEE8-98DE-431F-BC89-B48464BBC2BE", ModuleType.UserCode, 1)]
    public class ActivationKey : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public ActivationKey()
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
            
            const string domain = "release.lynda.com";
            const string navigateTo = "/";
            
            const BrowserProduct browserProduct = BrowserProduct.Firefox;

            //Open browser and navigate to url
            string url = string.Format("http://{0}{1}", domain, navigateTo.ToString());
            Browser browser = new Browser(browserProduct, url);
        }
    }
}
