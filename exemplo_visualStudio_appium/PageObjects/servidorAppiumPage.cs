using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Enums;

namespace exemplo_visualStudio_appium.PageObjects
{
    [TestClass]
    public class servidorAppiumPage
    {
        public AndroidDriver<AndroidElement> driver;
        public TimeSpan DefaultTimeout = TimeSpan.FromSeconds(120);

        public AppiumLocalService appiumServico;
        public String appiumServicoUrl;
        public DesiredCapabilities capacidade;

        public void inicializar()
        {
            appiumServico = AppiumLocalService.BuildDefaultService();
            appiumServico.Start();
            appiumServicoUrl = appiumServico.GetType().ToString();
            Console.WriteLine("Endereço do servidor Appium : - " + appiumServicoUrl);

            // capacidade (indicando plataforma, dispositivo e aplicação)
            capacidade = new DesiredCapabilities();
            capacidade.SetCapability(MobileCapabilityType.PlatformName, "Android");
            capacidade.SetCapability(MobileCapabilityType.DeviceName, "k10 Power");
            capacidade.SetCapability(CapabilityType.Platform,"Windows");
            capacidade.SetCapability("appPackage", "br.com.mc1.android.energisa.launcher");
            capacidade.SetCapability("appActivity", "br.com.mc1.android.energisa.launcher.LauncherActivity");
            capacidade.SetCapability("noReset", "true");
            capacidade.SetCapability("fullReset", "false");
            capacidade.SetCapability("dontStopAppOnReset", "true");

            // abre a conexão com o servidor (url) e executa um dispositivo (descrito na capacidade)
            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capacidade, TimeSpan.FromSeconds(180));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));

        }

        public void finalizar()
        {
            driver.Quit();
            appiumServico.Dispose();
        }

    }
}
