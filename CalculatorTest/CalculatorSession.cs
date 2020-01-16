using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;

namespace CalculatorTest
{
   public class CalculatorSession
    {

        //window app driver URL provided by WinAppDriver.exe
        //run WipAppDriver
        private const string WindowAppDriverURL = "http://127.0.0.1:4723";
        //ID could be any .exe file as well => C:/Program Files/AppName.Exe
        private const string CalculatorAppId = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";


        protected static WindowsDriver<WindowsElement> session;

        public static void SetUp(TestContext context)
        {
            //launch app if it isn't launched
            if (session == null)
            {
                //create new session
                //note: multiple instances share the same process id
                //DesiredCapabilities appCap = new DesiredCapabilities();
                //appCap.SetCapability("app", CalculatorAppId);
                //appCap.SetCapability("deviceName", "WindowsPC");

                //windowSession = new WindowsDriver<WindowsElement>(new Uri(WindowAppDriverURL), appCap);
                //session = new WindowsDriver<WindowsElement>(remoteAddress: new Uri(WindowAppDriverURL), AppiumOptions: appCap);


                //# Desired Capability is obsolete repleaced with AppiumOptions in Appium Package 4.0#//
                AppiumOptions options = new AppiumOptions();
                options.AddAdditionalCapability("deviceName", "WindowsPC");
                options.AddAdditionalCapability("platformName", "Windows");
                options.AddAdditionalCapability("app", CalculatorAppId);
         
                session = new WindowsDriver<WindowsElement>(new Uri(WindowAppDriverURL), options);

                //Set implicit timeout to 1 second to make element search to retry every 500 ms for at most 3 times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);


                Assert.IsNotNull(session);


            }

        }

        public static void TearDown()
        {
            if (session != null)
            {
                session.Quit();
                session = null; 
            }
        }
    }
}
