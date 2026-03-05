using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace CalculatorTestAutomation.Infrastructure;

public abstract class ScreenshotHelper
{
    [AllureStep("TakeScreenshot")]
    public static void TakeScreenshot(WindowsDriver<WindowsElement> driver,
        string testName)
    {
        ITakesScreenshot takesScreenshot = driver;
        var screenshot = takesScreenshot.GetScreenshot();

        var screenshotsDir = Path.Combine(
            Directory.GetCurrentDirectory(),
            "allure-results");
        
        var filePath = Path.Combine(
            screenshotsDir,
            $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

        screenshot.SaveAsFile(filePath);
    
        AllureApi.AddAttachment(
            testName,
            "image/png",
            File.ReadAllBytes(filePath));
    }
}