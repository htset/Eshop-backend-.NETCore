using Eshop_API.Models;
using System.Collections.Generic;


namespace Eshop_API.Controllers
{
    public class ItemPayload
    {
        public List<Item> items { get; set; }
        public int count { get; set; }


        public  ItemPayload(List<Item> items, int count)
        {
            this.items = items;
            this.count = count;
        }
    }
}
