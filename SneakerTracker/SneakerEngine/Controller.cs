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
using SneakerEngine;
using System.IO;

namespace SneakerEngine
{
    public class Product
    {
        public string Name;
        public string ProductNumber;
        public string ImageURL;
        public Bitmap ProductImage;

        public Product(string Name, string ProductNumber, string ImageURL)
        {
            this.Name = Name;
            this.ProductNumber = ProductNumber;
            this.ImageURL = ImageURL;
        }
        
    }
    public class Controller
    {
        JsonValue json;
        List<int> jsonIndecies = new List<int>();
        ImageList ImageList = new ImageList();

        List<String> Names = new List<String>();
        List<String> SKUs = new List<String>();
        public event PropertyChangedEventHandler ProductFound = delegate { };


        int NumberOfOptions;

        async public void Search(String query, int NumberOfOptions)
        {
            var httpClient = new HttpClient();
            this.NumberOfOptions = NumberOfOptions;
           
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
            ParseJSON();

            //
            //do image stuff later
            //
            
            /*
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
            */

        }

        private void ParseJSON()
        {
            int TempNumberOfOptions = NumberOfOptions;
            //load individual pictures to imagelist
            for (int i = 0; i < TempNumberOfOptions && i < json["hits"].Count; i++)
            {
                if (json["hits"][i]["product_category"] == "sneakers")
                {
                    String ProductNumber, Name, ImageURL;
                    ProductNumber = json["hits"][i]["style_id"];
                    Name = json["hits"][i]["name"];
                    try { ImageURL = json["hits"][i]["thumbnail_url"]; }
                    catch { ImageURL = ""; }

                    Product product = new Product(Name, ProductNumber, ImageURL);
                    product.ProductImage = LoadPicture(product.ImageURL);

                    ProductFound(product, new PropertyChangedEventArgs("info"));

                    //jsonIndecies.Add(i);
                }
                else
                {
                    TempNumberOfOptions++;
                }
            }
        }


        private Bitmap LoadPicture(string URL)
        {
            Bitmap ProductImage;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (URL == "")
            {
                ProductImage = Properties.Resources.NotFound;
            }

            
            else
            {
                //load image from url
                try
                {
                    WebClient _web = new WebClient();
                    byte[] _data = _web.DownloadData(URL);
                    MemoryStream _ms = new MemoryStream(_data);
                    ProductImage = new Bitmap(_ms);
                }

                //catch bad url
                catch
                {
                    ProductImage = Properties.Resources.NotFound;
                }
            }

            return ProductImage;
        }   
    }
}
