using Allure.NUnit;
using Allure.NUnit.Attributes;
using CalculatorTestAutomation.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CalculatorTestAutomation.CalculatorTests;

[AllureNUnit]
public class TestFIndAndClick : SetupTests
{
    [Test]
    [Description("Test Calculator Addition Results")]
    public void Addition()
    {
        Driver?.FindElementByName("Five").Click();
        Driver?.FindElementByName("Plus").Click();
        Driver?.FindElementByName("Seven").Click();
        Driver?.FindElementByName("Equals").Click();
        var calculatorResult = GetCalculatorResultText();
        ClassicAssert.AreEqual("12", calculatorResult);
    }
    [Test]
    [Description("Test Calculate Division Results")]
    public void Division()
    {
        Driver?.FindElementByAccessibilityId("num8Button").Click();
        Driver?.FindElementByAccessibilityId("num8Button").Click();
        Driver?.FindElementByAccessibilityId("divideButton").Click();
        Driver?.FindElementByAccessibilityId("num1Button").Click();
        Driver?.FindElementByAccessibilityId("num1Button").Click();
        Driver?.FindElementByAccessibilityId("equalButton").Click();
        ClassicAssert.AreEqual("8", GetCalculatorResultText());
    }
    [Test]
    [Description("Test Calculate Multiplication Results")]
    public void Multiplication()
    {
        Driver?.FindElementByXPath("//Button[@Name='Nine']").Click();
        Driver?.FindElementByXPath("//Button[@Name='Multiply by']").Click();
        Driver?.FindElementByXPath("//Button[@Name='Nine']").Click();
        Driver?.FindElementByXPath("//Button[@Name='Equals']").Click();
        ClassicAssert.AreEqual("81", GetCalculatorResultText());
    }
    [Test]
    [Description("Test Calculate Subtraction Results")]
    public void Subtraction()
    {
        Driver?.FindElementByXPath("//Button[@AutomationId='num9Button']").Click();
        Driver?.FindElementByXPath("//Button[@AutomationId='minusButton']").Click();
        Driver?.FindElementByXPath("//Button[@AutomationId='num1Button']").Click();
        Driver?.FindElementByXPath("//Button[@AutomationId='equalButton']").Click();
        ClassicAssert.AreEqual("8", GetCalculatorResultText());
    }
    [Test]
    [TestCase("One", "Plus", "Seven", "8")]
    [TestCase("Nine", "Minus", "One", "8")]
    [TestCase("Eight", "Divide by", "Eight", "1")]
    public void Templatized(string input1, string operation, string input2, string expectedResult)
    {
        Driver?.FindElementByName(input1).Click();
        Driver?.FindElementByName(operation).Click();
        Driver?.FindElementByName(input2).Click();
        Driver?.FindElementByName("Equals").Click();
        ClassicAssert.AreEqual(expectedResult, GetCalculatorResultText());
    }
    
    [AllureStep("GetCalculatorResultText")]
    private string? GetCalculatorResultText()
    {
        ScreenshotHelper.TakeScreenshot(Driver!, "CalculatorResults");
        return Driver?.FindElementByAccessibilityId("CalculatorResults")
            .Text.Replace("Display is", string.Empty).Trim();
    }
}