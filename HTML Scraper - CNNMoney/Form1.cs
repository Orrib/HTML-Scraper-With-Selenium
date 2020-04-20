using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

// Author: Orrett Coke
// Date: 04-19-2020

namespace Graphics_and_Networking___Scraper
{
    public partial class Form1 : Form
    {
        public IWebDriver driver;
        public Form1()
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Find_Data();
        }
        private string siteUrl = "https://money.cnn.com/data/hotstocks/index.html";

        private void Find_Data()
        {
            IList<IWebElement> searchElements = driver.FindElements(By.TagName("tbody"));
            foreach (IWebElement i in searchElements)
            {
                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                var text = i.GetAttribute("innerHTML");

                htmlDocument.LoadHtml(text);
                var inputs = htmlDocument.DocumentNode.Descendants("tr").ToList();
                foreach (var items in inputs)
                {
                    HtmlAgilityPack.HtmlDocument htmlDocument1 = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument1.LoadHtml(items.InnerHtml);

                    var tds = htmlDocument1.DocumentNode.Descendants("td").ToList();

                    if (tds.Count != 0)
                        richTextBox1.AppendText(tds[0].InnerText + " " + tds[1].InnerText + " " + tds[2].InnerText + " " + tds[3].InnerText + "\t\r");
                }
                richTextBox1.AppendText("\t\r");
            }
        }

        public void Open_Browser()
        {

            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArgument("headless");
            driver = new ChromeDriver(driverService, options);


            try
            {
                driver.Navigate().GoToUrl(siteUrl);
            }
            catch
            {
                button2.Text = "Error Opening Browser";
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = "Opening Browser (Please Wait)...";
            Open_Browser();
            button2.Text = "Browser Is Open";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Text = "Exiting...";
            if (driver != null)
                driver.Quit();
            button3.Text = "3. Exit";

            button2.Text = "1. Open Browser";
        }
    }
}
