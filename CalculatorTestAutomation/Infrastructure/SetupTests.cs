using Allure.NUnit.Attributes;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;

namespace CalculatorTestAutomation.Infrastructure;

[TestFixture]
public class SetupTests
{
    protected WindowsDriver<WindowsElement>? Driver;
    private WinAppDriverManager _manager = null!;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _manager = new WinAppDriverManager();
        _manager.StartWinAppDriver(); // only process
    }

    [AllureBefore]
    [SetUp]
    public void Setup()
    {
        Driver = _manager.CreateCalculatorSession(); // new app session per test
    }

    [AllureAfter]
    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.FailCount > 0 && Driver != null)
        {
            ScreenshotHelper.TakeScreenshot(
                Driver,
                TestContext.CurrentContext.Test.Name);
        }

        Driver?.Quit();
        _manager.CloseCalculatorProcess();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _manager.StopWinAppDriver();
    }
}