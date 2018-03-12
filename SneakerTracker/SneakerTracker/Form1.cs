using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Json;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SneakerEngine;
using System.IO;


namespace SneakerTracker
{
    public partial class Form1 : Form
    {
        int i = 0;
        SneakerCollection MySneakers = new SneakerCollection();
        List<PictureBox> Pictures = new List<PictureBox>();
        ImageList ImageList = new ImageList();
        List<String> Names = new List<String>();
        List<String> SKUs = new List<String>();
        List<int> jsonIndecies = new List<int>();

        int NumberOfOptions = 6;
        JsonValue json;


        public Form1()
        {

            InitializeComponent();
            ImageList.ImageSize = new Size(140, 100);


        }
        async void Search(string query)
        {

            var httpClient = new HttpClient();

            //setup post request
            httpClient.DefaultRequestHeaders.Add("x-algolia-agent", "Algolia for vanilla JavaScript 3.22.1");
            httpClient.DefaultRequestHeaders.Add("x-algolia-application-id", "XW7SBCT9V6");
            httpClient.DefaultRequestHeaders.Add("x-algolia-api-key", "6bfb5abee4dcd8cea8f0ca1ca085c2b3");

            var postData = "{\"params\":\"query=" + query + "&facets=*&page=0&hitsPerPage=50\"}";
            var content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync("https://xw7sbct9v6-dsn.algolia.net/1/indexes/products/query", content);
            response.EnsureSuccessStatusCode();

            //get json response
            var responseJson = await response.Content.ReadAsStringAsync();
            json = JsonObject.Parse(responseJson);

            //clear current view
            ClearView();

            int temp = NumberOfOptions;
            //load individual pictures to imagelist
            for (i = 0; i < NumberOfOptions && i < json["hits"].Count; i++)
            {
                //string s = json["hits"][i]["product_category"];
                if (json["hits"][i]["product_category"] == "sneakers")
                {
                    LoadPicture(i);
                    jsonIndecies.Add(i);
                }
                else
                {
                    NumberOfOptions++;
                }
            }

            //add imagelist to listview
            listView1.LargeImageList = ImageList;

            //set text attr for listview (name, sku)
            int count = 0;
            for (i = 0; i < NumberOfOptions && i < Names.Count; i++)
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = Names[i] + " (" + SKUs[i] + ")";
                lst.ImageIndex = count++;
                listView1.Items.Add(lst);
            }

            NumberOfOptions = temp;


        }

        void ClearView()
        {
            listView1.Items.Clear();
            SKUs.Clear();
            Names.Clear();
            ImageList.Dispose();
        }

        private void LoadPicture(int i)
        {
            String url;
            try
            {
                url = json["hits"][i]["thumbnail_url"];
            }

            catch
            {
                url = "";
                Bitmap DefaultImage = Properties.Resources.NotFound;
                ImageList.Images.Add(DefaultImage);
                GatherProductInfo(i);
                return;
            }

            if (url == "")
            {
                Bitmap DefaultImage = Properties.Resources.NotFound;
                ImageList.Images.Add(DefaultImage);
                GatherProductInfo(i);
                return;
            }

            //load image from url
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);
            try
            {
                System.Net.WebResponse resp = request.GetResponse();
                System.IO.Stream respStream = resp.GetResponseStream();
                Bitmap bmp = new Bitmap(respStream);
                respStream.Dispose();
                ImageList.Images.Add(bmp);
                GatherProductInfo(i);
            }

            //catch bad url
            catch
            {
                Bitmap DefaultImage = Properties.Resources.NotFound;
                ImageList.Images.Add(DefaultImage);
                GatherProductInfo(i);
                return;
            }

        }

        private void GatherProductInfo(int i)
        {
            String sku, name;
            sku = json["hits"][i]["style_id"];
            name = json["hits"][i]["name"];

            SKUs.Add(sku);
            Names.Add(name);
        }


        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            Search(SearchBox.Text);
        }

        private void Select(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count >= 1)
            {
                ListViewItem SelectedItem = this.listView1.SelectedItems[0];
                if (SKUs[SelectedItem.Index] != "TBA")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Add(
                            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                        var response = client.GetAsync("https://dtzwqfkqs0.execute-api.us-east-1.amazonaws.com/prod/scrapePrices?productId=" + SKUs[SelectedItem.Index]).Result;

                        string content = response.Content.ReadAsStringAsync().Result;
                        textBox2.Text = content;
                    }
                }
            }

        }

    }




}
