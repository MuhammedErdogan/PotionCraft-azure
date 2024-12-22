using System.Collections.Generic;

namespace Shared
{
    public class StoreItem
    {
        public string ItemId { get; set; }

        public Dictionary<string, int> Prices { get; set; }
        public Dictionary<string, int> Items { get; set; }

        public StoreItem(string itemId, string itemName, Dictionary<string, int> prices, Dictionary<string, int> items)
        {
            ItemId = itemId;
            Prices = prices;
            Items = items;
        }
    }
}