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

        int NumberOfOptions = 6;

        JsonValue json;

        public Form1()
        {

            InitializeComponent();

        }
        async void Search(string query)
        {

            var httpClient = new HttpClient();
            ImageList = new ImageList();
            listView1.Items.Clear();
            SKUs.Clear();
            Names.Clear();

            httpClient.DefaultRequestHeaders.Add("x-algolia-agent", "Algolia for vanilla JavaScript 3.22.1");
            httpClient.DefaultRequestHeaders.Add("x-algolia-application-id", "XW7SBCT9V6");
            httpClient.DefaultRequestHeaders.Add("x-algolia-api-key", "6bfb5abee4dcd8cea8f0ca1ca085c2b3");

            var postData = "{\"params\":\"query=" + query + "&facets=*&page=0\"}";

            var content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await httpClient.PostAsync("https://xw7sbct9v6-dsn.algolia.net/1/indexes/products/query", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            ImageList.ImageSize = new Size(140, 100);

            json = JsonObject.Parse(responseJson);

            for (i = 0; i < NumberOfOptions; i++)
            {
                LoadPicture2(i);
            }

            int count = 0;
            listView1.LargeImageList = ImageList;
            for(i=0;i<NumberOfOptions && i<Names.Count;i++)
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = Names[i] + " (" + SKUs[i] + ")";
                lst.ImageIndex = count++;
                listView1.Items.Add(lst);
            }


        }

        private void LoadPicture(int i)
        {
            PictureBox pic = Pictures[i];
            String img;
            try
            {
                img = json["hits"][i]["thumbnail_url"];
            }

            catch
            {
                img = "";
            }

            if (img != "")
            {
                pic.Load(img);
                pic.SizeMode = PictureBoxSizeMode.Zoom;

                //textBox2.Text = goat_json["hits"][i].ToString();
                //textBox2.Text = json["hits"].ToString();

            }

            else
            {
                pic.Load("C:\\Users\\danie\\Documents\\Visual Studio 2015\\Projects\\SneakerTracker\\SneakerTracker\\bin\\notfound.png");
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                // panel.Controls.Add(pic);
                //textBox2.Text = json["hits"].ToString();
            }
        }

        private void LoadPicture2(int i)
        {
            String url;
            try
            {
                url = json["hits"][i]["thumbnail_url"];
            }

            catch
            {
                url = "";
                Bitmap DefaultImage = (Bitmap)Image.FromFile("..\\notfound.png");
                ImageList.Images.Add(DefaultImage);
                GatherProductInfo(i);
                return;
            }

            if(url == "")
            {
                Bitmap DefaultImage = (Bitmap)Image.FromFile("..\\notfound.png");
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
                Bitmap DefaultImage = (Bitmap)Image.FromFile("..\\notfound.png");
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(textBox1.Text);
        }

    }



}
