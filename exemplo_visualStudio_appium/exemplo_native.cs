using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using System.Diagnostics;

namespace exemplo_visualStudio_appium
{

    [TestClass]
    public class exemplo_native
    {
        //instanciando variavel drive e time para o Appium
        public AppiumDriver<AndroidElement> driver;
        Process ExternalProcess = new Process();
        public TimeSpan DefaultTimeout = TimeSpan.FromSeconds(120);

        [TestInitialize]
        public void Initialization()
        {
            ExternalProcess.StartInfo.FileName = @"""C:\Program Files (x86)\Appium\node.exe""";
            ExternalProcess.StartInfo.Arguments = @"""lib\server\main.js --address 127.0.0.1 --port 4723 --platform-name Android --platform-version 23 --automation-name Appium --log-no-color""";
            ExternalProcess.Start();
            ExternalProcess.WaitForExit(6000);

            //iniciando os capabilities
            DesiredCapabilities capacidades = new DesiredCapabilities();
            capacidades.SetCapability("deviceName", "lispda");
            capacidades.SetCapability("platformversion", "7.0");
            capacidades.SetCapability("platformName", "Android");
            capacidades.SetCapability(CapabilityType.Platform, "Windows");
            capacidades.SetCapability("appPackage", "br.com.xxx.android.xxxxx.launcher");
            capacidades.SetCapability("appActivity", "br.com.xxx.android.xxxxx.launcher.LauncherActivity");
            capacidades.SetCapability("noReset", "true");

            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capacidades, TimeSpan.FromSeconds(180));
        }

        [TestMethod]
        public void TestLis()
        {
            driver.FindElementById("br.com.xxx.android.xxxxx.launcher:id/imgIcon").Click();
        }

        [TestCleanup]
        public void AfterTest()
        {
            driver.Quit();
            ExternalProcess.Kill();
        }
    }
}
