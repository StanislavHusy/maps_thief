using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Drawing.Imaging;

namespace maps_thief
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        IWebDriver Browser;

        private static IWebElement getShadowRoot(IWebDriver driver, IWebElement shadowHost)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            return (IWebElement)js.ExecuteScript("return arguments[0].shadowRoot", shadowHost);
        }
        public static IWebElement getShadowElement(IWebDriver driver, IWebElement shadowHost, String cssOfShadowElement)
        {
            IWebElement shardowRoot = getShadowRoot(driver, shadowHost);
            return shardowRoot.FindElement(By.CssSelector(cssOfShadowElement));
        }
        private void Deleter(IWebDriver Browser)
        {
            ///

            var shadowHost = Browser.FindElement(By.CssSelector("body > styling-wizard-app"));
            var shadowRoot = shadowHost.GetShadowRoot();
            IWebElement shadowTreeElement1 = shadowRoot.FindElement(By.CssSelector("#controls-panel"));

            IWebElement shadowTreeElement2 = shadowRoot.FindElement(By.CssSelector("#google-logo"));
            IWebElement shadowTreeElement3 = shadowRoot.FindElement(By.CssSelector("#pac-input"));
            IWebElement shadowTreeElement4 = shadowRoot.FindElement(By.CssSelector("#main-map > div > div > div:nth-child(9) > button"));
            IWebElement shadowTreeElement5 = shadowRoot.FindElement(By.CssSelector("#location-control"));
            IWebElement shadowTreeElement6 = shadowRoot.FindElement(By.CssSelector("#main-map > div > div > div:nth-child(14) > div.gmnoprint.gm-bundled-control.gm-bundled-control-on-bottom > div:nth-child(2)"));
            IWebElement shadowTreeElement7 = shadowRoot.FindElement(By.CssSelector("#main-map > div > div > div:nth-child(4)"));
            IWebElement shadowTreeElement8 = shadowRoot.FindElement(By.CssSelector("#main-map > div > div > div:nth-child(17) > div"));
            IWebElement shadowTreeElement10 = shadowRoot.FindElement(By.CssSelector("#main-map > div"));
            IWebElement shadowTreeElement11 = shadowRoot.FindElement(By.CssSelector("#cloud-styling-bar"));


            IJavaScriptExecutor jse = Browser as IJavaScriptExecutor;
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement1);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement2);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement3);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement4);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement5);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement6);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement7);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement8);
            jse.ExecuteScript("arguments[0].remove();", shadowTreeElement11);


            jse.ExecuteScript("arguments[0].style.position = 'fixed';", shadowTreeElement10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Navigate().GoToUrl("https://mapstyle.withgoogle.com/");
            Browser.Manage().Window.Maximize();
            Thread.Sleep(10000);
            Deleter(Browser);

        }
        private void Maps_creator(int segment)
        {
            Bitmap[] All_screens = new Bitmap[segment * segment];
            int i = 0;
            int switcher = 1;
            for (int p = 0; p < segment; p++)
            {
                if (switcher == 1)
                {
                    for (int z = segment - 1; z > -1; z--)
                    {
                        All_screens[i] = (Bitmap)Image.FromFile(@"C:\projects\maps_thief\screens\scren" + p + z + ".jpeg");
                        i++;
                    }
                    switcher = -switcher;
                }
                else
                {
                    for (int z = 0; z < segment; z++)
                    {
                        All_screens[i] = (Bitmap)Image.FromFile(@"C:\projects\maps_thief\screens\scren" + p + z + ".jpeg");
                        i++;
                    }
                    switcher = -switcher;
                }
            }
            int wi = All_screens[0].Width;
            int he = All_screens[0].Height;
            i = 0;
            Bitmap result = new Bitmap(wi * segment, he * segment);
            for (int p = 0; p < segment; p++)
            {
                int y0 = p * he;
                for (int z = 0; z < segment; z++)
                {
                    int x0 = z * wi;
                    for (int y1 = 0; y1 < he; y1++)
                    {
                        for (int x1 = 0; x1 < wi; x1++)
                        {
                            result.SetPixel(x1 + x0, y1 + y0, All_screens[i].GetPixel(x1, y1));
                        }
                    }
                    i++;
                }
            }
            result.Save(@"C:\projects\maps_thief\screens\scren_unite");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread.Sleep(5000);
            var shadowHost = Browser.FindElement(By.CssSelector("body > styling-wizard-app"));
            var shadowRoot = shadowHost.GetShadowRoot();
            IWebElement canvas = shadowRoot.FindElement(By.CssSelector("#main-map > div"));

            int wind = 1;
            int segment = 5;
            string[] url_data = new string[segment * segment];
            for (int p = 0; p < segment; p++)
            {
                for (int z = 0; z < segment - 1; z++)
                {
                    Thread.Sleep(1500);
                    ((ITakesScreenshot)Browser).GetScreenshot().SaveAsFile(@"C:\projects\maps_thief\screens\scren" + p + z + ".jpeg");
                    url_data[p * segment + z] = Browser.Url;
                    for (int i = 0; i < 3; i++)
                    {
                        Actions builder = new Actions(Browser);
                        builder.ClickAndHold(canvas).MoveByOffset(-1 * wind, 50).
                                                     MoveByOffset(canvas.Size.Width / 3* wind, 0).Release().Perform();
                        Thread.Sleep(250);
                    }
                }
                Thread.Sleep(1500);
                ((ITakesScreenshot)Browser).GetScreenshot().SaveAsFile(@"C:\projects\maps_thief\screens\scren" + p + (segment - 1) + ".jpeg");
                url_data[p * segment + segment - 1] = Browser.Url;
                for (int i = 0; i < 3; i++)
                {
                    Actions builder = new Actions(Browser);
                    builder.ClickAndHold(canvas).MoveByOffset(50, 1).
                                                 MoveByOffset(0, -(canvas.Size.Height / 3)).Release().Perform(); 
                    Thread.Sleep(250);
                }
                wind = -wind;
            }
            Maps_creator(segment);

        }
    }
}
