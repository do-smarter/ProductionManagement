using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Enums;

namespace Erfa.PruductionManagement.Domain.Entities

{
    public class ProductionItem : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public ProductionState State { get; set; } = ProductionState.New;

        public ProductionItem()
        {
        }
        public ProductionItem(Item item, int quantity, string orderNumber)
        {
            Item = item;
            Quantity = quantity;
            OrderNumber = orderNumber;
        }

        public ProductionItem(Item item, int quantity, string orderNumber, string ralGalv)
        {
            Item = item;
            Quantity = quantity;
            OrderNumber = orderNumber;
            RalGalv = ralGalv.ToUpper();
        }

        public ProductionItem(ProductionItem prodItem)
        {
            Item = prodItem.Item;
            RalGalv = prodItem.RalGalv.ToUpper();
        }

        public bool ProdEquals(object? obj)
        {
            return obj is ProductionItem item &&
                   EqualityComparer<Item>.Default.Equals(Item, item.Item) &&
                   string.Equals(RalGalv, item.RalGalv);
        }

    }


}
