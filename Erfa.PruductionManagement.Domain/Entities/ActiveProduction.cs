using Erfa.PruductionManagement.Domain.Common;

namespace Erfa.PruductionManagement.Domain.Entities

{
    public class ActiveProduction : AuditableEntity
    {    
        public Guid Id { get; set; } = new Guid();
        public List<ProductionGroup> ActivProductionList { get; set; } = new List<ProductionGroup>();

        public void AddNewComponent(ProductionGroup component)
        {
            ActivProductionList.Add(component);
        }

        public void RemoveNewComponent(ProductionGroup component)
        {
            ActivProductionList.Remove(component);
        }

        public void ChangePriority(ProductionGroup component, int priority)
        {
            ActivProductionList.Remove(component);
            ActivProductionList.Insert(priority, component);
        }

        public void PrioritizeComponentsToghether(List<ProductionGroup> componentList)
        {
            ProductionGroup commonPriority = new ProductionGroup();
            foreach (ProductionGroup component in componentList)
            {
                ActivProductionList.Remove(component);
             //   commonPriority.AddRangeProductionItem(component.ProductionItems);
            }
        }

        public void MergeComponents(List<ProductionGroup> componentList)
        {
            ProductionGroup commonPriority = new ProductionGroup();
            foreach (ProductionGroup component in componentList)
            {
                ActivProductionList.Remove(component);
               // commonPriority.AddRangeProductionItem(component.ProductionItems);
            }
        }

        public ProductionGroup MergeItems(List<ProductionItem> itemList)
        {
            ProductionItem firstItem = itemList.First();
            itemList.Remove(firstItem);
            ProductionItem mergedItem = new ProductionItem(firstItem);

            ProductionGroup mergedComponent = new ProductionGroup();


            foreach (ProductionItem item in itemList)
            {
                if (!item.EqualsForProductionGroup(firstItem))
                {
                    throw new ArgumentException("not equal items!");
                }
               // mergedComponent.AddOrderNumber(item.OrderNumber);
                mergedItem.Quantity += item.Quantity;
            }
         //   mergedComponent.AddProductionItem(mergedItem);
          //  mergedComponent.ProductionItems[0].OrderNumber = String.Join(",", mergedComponent.OrderNumbers.ToArray());
            return mergedComponent;
        }
    }
}
