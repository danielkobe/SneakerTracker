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

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {

                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }


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

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                    var response = client.GetAsync("https://dtzwqfkqs0.execute-api.us-east-1.amazonaws.com/prod/scrapePrices?productId=" + SKUs[SelectedItem.Index]).Result;

                    string content = response.Content.ReadAsStringAsync().Result;
                    var prices = JsonObject.Parse(content);
                    var sizes = prices["sizes"];

                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(sizes.Count);

                    for (int i = 1; i <= sizes.Count; i++)
                    {
                        dataGridView1.Rows[i - 1].HeaderCell.Value = sizes[i-1]["size"].ToString();
                        var row = this.dataGridView1.Rows[i-1];

                        //goat
                        if (sizes[i - 1]["goat"].Count>0)
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value = (double) sizes[i - 1]["goat"]["lowestAsk"];
                            if (this.resellCheckBox.Checked == true)
                            {
                                var Base = (double) row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value;
                                Base = Base - ((double)(0.095 * Base) + 5);
                                Base = (double) (0.971 * Base);
                                row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value = (double) Base;

                            }
                        }
                        else
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value = 0.0;
                        }

                        //stockx
                        if (sizes[i - 1]["stockX"].Count > 0)
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value = (double) sizes[i - 1]["stockX"]["lowestAsk"];

                            if (this.resellCheckBox.Checked == true)
                            {
                                var Base = (double)row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value;
                                Base = (double) (Base * .88);
                                row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value = Base;
                            }
                        }
                        else
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value = (double) 0;
                        }

                        //flight club
                        if (sizes[i - 1]["flightClub"]["selling"].Count > 0)
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value = (double)sizes[i - 1]["flightClub"]["selling"]["lowestAsk"];
                            if (this.resellCheckBox.Checked == true)
                            {
                                var Base = (double)row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value;
                                Base = (double)(Base * .8);
                                row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value = Base;
                            }
                        }
                        else
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value = (double) 0;
                        }

                        //stadium goods
                        if (sizes[i - 1]["stadiumGoods"].Count > 0)
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value = (double) sizes[i - 1]["stadiumGoods"]["lowestAsk"];
                            if (this.resellCheckBox.Checked == true)
                            {
                                var Base = (double)row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value;
                                Base = (double)(Base * .8);
                                row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value = Base;
                            }
                        }
                        else
                        {
                            row.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value = (double) 0;
                        }


                    }
                }

            }

        }

        private void resellCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(resellCheckBox.Checked==true)
            {
                foreach(DataGridViewRow r in dataGridView1.Rows)
                {
                    var Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value;
                    Base = Base - ((double)(0.095 * Base) + 5);
                    Base = (double)(0.971 * Base);
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value = Base;

                    Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value;
                    Base = (double)(Base * .88);
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value = Base;

                    Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value;
                    Base = (double)(Base * .8);
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value = Base;


                    Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value;
                    Base = (double)(Base * .8);
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value = Base;
                }
            }

            else
            {
                //Select(this, new EventArgs());
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    var Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value;
                    Base = (double)((1/0.971) * Base);
                    //Base = Base + ((double)(0.095 * Base) + 5);
                    Base = ((Base + 5.0) / .905);
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value = Base;

                    Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value;
                    Base = (double)(Base * (1/.88));
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value = Base;

                    Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value;
                    Base = (double)(Base * (1/0.8));
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value = Base;


                    Base = (double)r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value;
                    Base = (double)(Base * (1 / 0.8));
                    r.Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value = Base;
                }
            }
        }
    }




}
