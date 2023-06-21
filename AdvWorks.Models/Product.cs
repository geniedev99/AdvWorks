using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace AdvWorks.Models
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string Name { get; set; }
        [JsonIgnore]
        public bool MakeFlag { get; set; }
        [JsonIgnore]
        public bool FinishedGoodsFlag { get; set; }
        [JsonIgnore]
        public string Color { get; set; }
        [JsonIgnore]
        public int SafetyStockLevel { get; set; }
  
        public decimal ListPrice { get; set; }
        [JsonIgnore]
        public string Size { get; set; }
        [JsonIgnore]
        public DateTime SellStartDate { get; set; }
        [JsonIgnore]
        public DateTime SellEndDate { get; set; }
        [JsonIgnore]
        public int ReorderPoint { get; set; }
        [JsonIgnore]
        public decimal StandardCost { get; set; }
        [JsonIgnore]
        public int DaysToManufacture { get; set; }
        [JsonIgnore]
        public Guid Rowguid { get; set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }

        public List<ProductListPriceHistory> ProductListPriceHistories { get; set; }
        public ProductSubCategory SubCategory { get; set; }

        public Product()
        {

            ProductListPriceHistories = new List<ProductListPriceHistory>();
            SubCategory = new ProductSubCategory();
        }
       

       
    }
    }

