using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelevisonStore.Models
{
    class ProductModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Brand_Id { get; set; }
        public string Design_Id { get; set; }
        public string Color_Id { get; set; }
        public string Screen_Id { get; set; }
        public string Size_Id { get; set; }
        public string Country_Id { get; set; }
        public string PriceIn { get; set; }
        public string PriceOut { get; set; }
        public string Warranty_Time { get; set; }
    }
}
