﻿using ProductBO.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBO
{
    public class ProductBase
    {

        private double Multi = 1.3;
        private double _Price;

        public int Id { get; set; }
        public string SKU { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public double Price {
            get
            {
                return _Price * Multi;
            }
            set
            {
                _Price = value;
            }
        }
        public int Unit { get; set; }

        public ProductBase()
        {

        }

        public ProductBase(string sku, string type, string name, string description, string img)
        {
            SKU = sku;
            Type = type;
            Name = name;
            Description = description;
            Img = img;
        }

        public virtual string BarCode()
        {
            return $"EAN-13:5998888{Id.ToString().PadLeft(4, '0')}12";
        }

        public List<ProductBase> GetProducts()
        {
            ProductDAO dao = new ProductDAO();
            return dao.Read(this);
        }

        public bool AddProduct()
        {
            ProductDAO dao = new ProductDAO();
            return dao.Create(this);
        }

        public bool UpdateProduct()
        {
            ProductDAO dao = new ProductDAO();
            return dao.Update(this);
        }

        public bool RemoveProduct()
        {
            ProductDAO dao = new ProductDAO();
            return dao.Delete(this.Id);
        }
    }
}
