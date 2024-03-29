﻿using Erfa.PruductionManagement.Domain.Common;
using Erfa.PruductionManagement.Domain.Enums;

namespace Erfa.PruductionManagement.Domain.Entities.Production

{
    public class ProductionItem : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string RalGalv { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string Worker { get; set; } = string.Empty;
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
        public ProductionItem(ProductionItem productionItem)
        {
            Item = productionItem.Item;
            Quantity = productionItem.Quantity;
            OrderNumber = productionItem.OrderNumber;
            RalGalv = productionItem.RalGalv;
            Comment = productionItem.Comment;
            State = ProductionState.New;
        }

        public bool EqualsForProductionGroup(object? obj)
        {
            return obj is ProductionItem item &&
                   EqualityComparer<Item>.Default.Equals(Item, item.Item) &&
                   string.Equals(RalGalv, item.RalGalv);
        }

    }


}
