using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System;

namespace CalculatorTest
{
    [TestClass]
    public class UnitTest1 : CalculatorSession
    {

        private static WindowsElement header;
        private static WindowsElement calcResult;
        [TestMethod]
        public void TestAddition()
        {
            // Use inspect.exe to find automation ID
            session.FindElementByName("One").Click();
            session.FindElementByName("Plus").Click();
            session.FindElementByName("Two").Click();
            session.FindElementByName("Equals").Click();
            Assert.AreEqual("3", GetCalculatorResultText());
        }

        [TestMethod]
        public void TestSubtraction()
        {
            // Find the buttons by their accessibility ids using XPath and click them in sequence to perform 5 - 3 = 2
            // Use inspect.exe to find automation ID
            session.FindElementByXPath("//Button[@AutomationId=\"num5Button\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"minusButton\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"num3Button\"]").Click();
            session.FindElementByXPath("//Button[@AutomationId=\"equalButton\"]").Click();
            Assert.AreEqual("2", GetCalculatorResultText());
        }
        [TestMethod]
        public void TestDivision()
        {
            // Find the buttons by their names using XPath and click them in sequence to perform 9 x 9 = 81
            // Use inspect.exe to find name
            session.FindElementByXPath("//Button[@Name='Seven']").Click();
            session.FindElementByXPath("//Button[@Name='Multiply by']").Click();
            session.FindElementByXPath("//Button[@Name='Eight']").Click();
            session.FindElementByXPath("//Button[@Name='Equals']").Click();
            Assert.AreEqual("56", GetCalculatorResultText());
        }

        [TestMethod]
        public void TestMultiplication()
        {
            // Find the buttons by their accessibility ids and click them in sequence to perform 88 / 11 = 8
            session.FindElementByAccessibilityId("num9Button").Click();
            session.FindElementByAccessibilityId("num4Button").Click();
            session.FindElementByAccessibilityId("multiplyButton").Click();
            session.FindElementByAccessibilityId("num5Button").Click();
            session.FindElementByAccessibilityId("num6Button").Click();
            session.FindElementByAccessibilityId("equalButton").Click();
            Assert.AreEqual("5,264", GetCalculatorResultText());
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            //create session 
            SetUp(context);
            try
            {
                header = session.FindElementByAccessibilityId("Header");
            } catch
            {
                header = session.FindElementByAccessibilityId("ContentPresenter");
            }

            //checks to see if in calculator is in standard mode; if not changes it to standrad;
            if (!header.Text.Equals("Standard",StringComparison.OrdinalIgnoreCase))
            {
                session.FindElementByAccessibilityId("TogglePaneButton").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var splitViewPane = session.FindElementByClassName("ListView");
                splitViewPane.FindElementByName("Standard Calculator").Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Assert.IsTrue(header.Text.Equals("Standard", StringComparison.OrdinalIgnoreCase));
            }

            calcResult = session.FindElementByAccessibilityId("CalculatorResults");
            Assert.IsNotNull(calcResult);
        }

        
        [ClassCleanup]
        public static void ClassCleanUp()
        {
            TearDown();
        }

      
        [TestInitialize]
        private string GetCalculatorResultText()
        {
             return calcResult.Text.Replace("Display is", string.Empty).Trim();
           // return session.FindElementByAccessibilityId("CalculatorResults").ToString();
        }
    }
}
