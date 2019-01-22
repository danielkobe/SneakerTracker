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


namespace SneakerTracker
{
    public partial class Form1 : Form
    {
        int i = 0;
        SneakerCollection MySneakers = new SneakerCollection();
        List<PictureBox> Pictures = new List<PictureBox>();
        ImageList ImageList = new ImageList();
        List<int> jsonIndecies = new List<int>();
        int NumberOfOptions = 6;

        Controller controller = new Controller();


        public Form1()
        {
            controller.ProductFound += FormPropertyChanged;
            InitializeComponent();
            ImageList.ImageSize = new Size(140, 100);
            ResultsListView.LargeImageList = ImageList;


            foreach (DataGridViewColumn column in PricesGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

        }

        //Event handler updating GUI based on found product
        private void FormPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "info")
            {
                Product p = (Product) sender;
                ListViewItem ListItem = new ListViewItem();
                ListItem.Text = p.Name + " (" + p.ProductNumber + ")";
                ListItem.ImageKey = p.Name;
                ImageList.Images.Add(p.Name, p.ProductImage);
                ResultsListView.Items.Add(ListItem);
            }
        }

        private void BeginSearch(string query)
        {

            //clear current view
            ClearView();

            controller.Search(query, NumberOfOptions);

        }

        void ClearView()
        {
            ResultsListView.Items.Clear();
            ImageList.Dispose();
        }

        /*
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
        */

        /*
        private void GatherProductInfo(int i)
        {
            String sku, name;
            sku = json["hits"][i]["style_id"];
            name = json["hits"][i]["name"];

            SKUs.Add(sku);
            Names.Add(name);
        }
        */


        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            BeginSearch(SearchTextBox.Text);
        }

        /*
        private void Select(object sender, EventArgs e)
        {

            if (ResultsListView.SelectedItems.Count >= 1)
            {
                ListViewItem SelectedItem = this.ResultsListView.SelectedItems[0];

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                    //build own endpoint eventually
                    var response = client.GetAsync("https://dtzwqfkqs0.execute-api.us-east-1.amazonaws.com/prod/scrapePrices?productId=" + SKUs[SelectedItem.Index]).Result;
                    string content = response.Content.ReadAsStringAsync().Result;
                    var prices = JsonObject.Parse(content);
                    var sizes = prices["sizes"];

                    PricesGridView.Rows.Clear();
                    PricesGridView.Rows.Add(sizes.Count);

                    for (int i = 1; i <= sizes.Count; i++)
                    {
                        PricesGridView.Rows[i - 1].HeaderCell.Value = sizes[i-1]["size"].ToString();
                        var row = this.PricesGridView.Rows[i-1];
                        
                        //////
                        //GOAT
                        //////
                        if (sizes[i - 1]["goat"].Count>0)
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value = (double) sizes[i - 1]["goat"]["lowestAsk"];
                            if (this.ResellCheckBox.Checked == true)
                            {
                                var Base = (double) row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value;
                                Base = Base - ((double)(0.095 * Base) + 5);
                                Base = (double) (0.971 * Base);
                                row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value = (double) Base;

                            }
                        }
                        else
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value = 0.0;
                        }

                        ////////
                        //STOCKX
                        ////////
                        if (sizes[i - 1]["stockX"].Count > 0)
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value = (double) sizes[i - 1]["stockX"]["lowestAsk"];

                            if (this.ResellCheckBox.Checked == true)
                            {
                                var Base = (double)row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value;
                                Base = (double) (Base * .88);
                                row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value = Base;
                            }
                        }
                        else
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value = (double) 0;
                        }

                        /////////////
                        //FLIGHT CLUB
                        /////////////
                        if (sizes[i - 1]["flightClub"]["selling"].Count > 0)
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value = (double)sizes[i - 1]["flightClub"]["selling"]["lowestAsk"];
                            if (this.ResellCheckBox.Checked == true)
                            {
                                var Base = (double)row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value;
                                Base = (double)(Base * .8);
                                row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value = Base;
                            }
                        }
                        else
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value = (double) 0;
                        }

                        ///////////////
                        //STADIUM GOODS
                        ///////////////
                        if (sizes[i - 1]["stadiumGoods"].Count > 0)
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value = (double) sizes[i - 1]["stadiumGoods"]["lowestAsk"];
                            if (this.ResellCheckBox.Checked == true)
                            {
                                var Base = (double)row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value;
                                Base = (double)(Base * .8);
                                row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value = Base;
                            }
                        }
                        else
                        {
                            row.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value = (double) 0;
                        }

                    }
                }

            }

        }
        
        private void resellCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(ResellCheckBox.Checked==true)
            {
                foreach(DataGridViewRow r in PricesGridView.Rows)
                {
                    var Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value;
                    Base = Base - ((double)(0.095 * Base) + 5);
                    Base = (double)(0.971 * Base);
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value = Base;

                    Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value;
                    Base = (double)(Base * .88);
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value = Base;

                    Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value;
                    Base = (double)(Base * .8);
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value = Base;


                    Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value;
                    Base = (double)(Base * .8);
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value = Base;
                }
            }

            else
            {
                //Select(this, new EventArgs());
                foreach (DataGridViewRow r in PricesGridView.Rows)
                {
                    var Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value;
                    Base = (double)((1/0.971) * Base);
                    //Base = Base + ((double)(0.095 * Base) + 5);
                    Base = ((Base + 5.0) / .905);
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn1"].Index].Value = Base;

                    Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value;
                    Base = (double)(Base * (1/.88));
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn2"].Index].Value = Base;

                    Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value;
                    Base = (double)(Base * (1/0.8));
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn3"].Index].Value = Base;


                    Base = (double)r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value;
                    Base = (double)(Base * (1 / 0.8));
                    r.Cells[PricesGridView.Columns["dataGridViewTextBoxColumn4"].Index].Value = Base;
                }
            }
        }
        */
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            NumberOfResultsComboBox.Text = NumberOfResultsComboBox.SelectedItem.ToString();
            NumberOfOptions = int.Parse(NumberOfResultsComboBox.SelectedItem.ToString());
        }
    }
   



}
