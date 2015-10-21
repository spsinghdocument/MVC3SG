using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresantationAccessLeyer.Models
{
    public class SimpleItem
    {
        public String Text1 { get; set; }
        public String Text2 { get; set; }

        public static List<SimpleItem> GetItemsList()
        {
            List<SimpleItem> items = new List<SimpleItem>();
            for (int i = 0; i < 100; i++)
            {
                items.Add(new SimpleItem { Text1 = "Item " + i, Text2 = "Text " + i });
            }
            return items;
        }
    }
}