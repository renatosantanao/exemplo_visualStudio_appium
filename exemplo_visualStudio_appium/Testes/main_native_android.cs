using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace exemplo_visualStudio_appium.Testes
{
    [TestClass]
    public class main_native_android
    {
        public AndroidDriver<AndroidElement> driver;
        public TimeSpan DefaultTimeout = TimeSpan.FromSeconds(120);

        public AppiumLocalService appiumServico;
        public String appiumServicoUrl;
        public DesiredCapabilities capacidade;

        [TestInitialize]
        public void inicializar()
        {
            appiumServico = AppiumLocalService.BuildDefaultService();
            appiumServico.Start();
            appiumServicoUrl = appiumServico.GetType().ToString();
            Console.WriteLine("Endereço do servidor Appium : - " + appiumServicoUrl);

            // alterar parametros referentes as capacidades na biblioteca automatizaMobile
            // capacidade (indicando plataforma, dispositivo e aplicação)
            capacidade = new DesiredCapabilities();
            capacidade.SetCapability(MobileCapabilityType.PlatformName, "Android");
            capacidade.SetCapability(MobileCapabilityType.DeviceName, "k10 Power");
            capacidade.SetCapability(CapabilityType.Platform, "Windows");
            capacidade.SetCapability("appPackage", "br.com.mc1.android.energisa.launcher");
            capacidade.SetCapability("appActivity", "br.com.mc1.android.energisa.launcher.LauncherActivity");
            capacidade.SetCapability("noReset", "true");
            capacidade.SetCapability("fullReset", "false");
            capacidade.SetCapability("dontStopAppOnReset", "true");

            // abre a conexão com o servidor (url) e executa um dispositivo (descrito na capacidade)
            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capacidade, TimeSpan.FromSeconds(180));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));

        }

        [TestMethod]
        public void leitura()
        {

            WebDriverWait aguardandoElmento = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //interagindo com os componentes
            driver.FindElementById("br.com.mc1.android.energisa.launcher:id/imgIcon").Click();

            //aguarda até que o elemento exista
            aguardandoElmento.Until(ExpectedConditions.ElementExists(By.Id("br.com.mc1.android.energisa.lis:id/btnLogin")));

            //verifica o nome do botão, mas a ideia da variável seria para retorno ou exibição de mensagem
            String mensagemAtual = driver.FindElementById("br.com.mc1.android.energisa.lis:id/btnLogin").Text;
            Assert.AreEqual("ENTRAR", mensagemAtual);

        }

        [TestCleanup]
        public void finalizar()
        {
            driver.Quit();
            appiumServico.Dispose();
        }
    }
}
