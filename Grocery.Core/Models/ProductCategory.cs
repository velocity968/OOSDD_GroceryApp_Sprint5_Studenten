using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Models
{
    public partial class ProductCategory : Category
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory(int id, int productId, int categoryId) : this(id, default!, productId, categoryId) { }
        public ProductCategory(int id, string name, int productId, int categoryId) : base(id,  name)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
}
