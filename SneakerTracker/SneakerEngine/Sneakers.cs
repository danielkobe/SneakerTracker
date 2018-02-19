using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerEngine
{
    internal class Sneaker
    {
        public double m_RetailPrice;
        public double m_ResellPrice;
        public string m_Brand;
        public string m_Name;
        public string m_Colorway;
        public string m_SKU;


        public Sneaker(double RetailPrice, double ResellPrice, string Brand, string Name, string Colorway, string SKU)
        {
            m_RetailPrice = RetailPrice;
            m_ResellPrice = ResellPrice;
            m_Brand = Brand;
            m_Name = Name;
            m_Colorway = Colorway;
            m_SKU = SKU;
        }
    }


    public class SneakerCollection
    {
        private List<Sneaker> m_KickCollection = new List<Sneaker>();

        public int NumberOfKicks()
        {
            return m_KickCollection.Count;
        }

        public void AddSneaker(double RetailPrice, double ResellPrice, string Brand, string Name, string Colorway, string SKU)
        {
            Sneaker NewSneaker = new Sneaker(RetailPrice, ResellPrice, Brand, Name, Colorway, SKU);
            m_KickCollection.Add(NewSneaker);

        }


    }
}
