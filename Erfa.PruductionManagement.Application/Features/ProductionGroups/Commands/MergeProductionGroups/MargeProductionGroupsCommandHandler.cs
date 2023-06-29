using AutoMapper;
using Erfa.PruductionManagement.Application.Contracts.Persistance;
using Erfa.PruductionManagement.Application.Exceptions;
using Erfa.PruductionManagement.Application.Services;
using Erfa.PruductionManagement.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace Erfa.PruductionManagement.Application.Features.ProductionGroups.Commands.MergeProductionGroups
{
    public class MargeProductionGroupsCommandHandler : IRequestHandler<MargeProductionGroupsCommand, ProductionGroupVm>
    {
        private readonly IProductionGroupRepository _productionGroupRepository;
        private readonly IMapper _mapper;
        private readonly ProductionService _productionService;

        public MargeProductionGroupsCommandHandler(
                         IProductionGroupRepository productionGroupRepository,
                         IMapper mapper,
                         ProductionService productionService)
        {
            _productionGroupRepository = productionGroupRepository;
            _mapper = mapper;
            _productionService = productionService;
        }

        public async Task<ProductionGroupVm> Handle(MargeProductionGroupsCommand request, CancellationToken cancellationToken)
        {
            var user = "Magdalena";
            var validator = new MargeProductionGroupsCommandValidator();
            await ProductionService.ValidateRequest(request, validator);

            HashSet<Guid> uniqeGroups = new HashSet<Guid>(request.Groups);

            List<ProductionGroup> groups = await _productionGroupRepository.FindListOfGroupsByIds(uniqeGroups);

            if (groups.Count != uniqeGroups.Count)
            {
                string ids = String.Join(", or ", uniqeGroups.ToArray());
                throw new ResourceNotFoundException(nameof(ProductionGroup), ids);
            }

            List<ProductionItem> allProductionItems = new List<ProductionItem>();
            foreach (ProductionGroup group in groups)
            {
                allProductionItems.AddRange(group.ProductionItems);
            }

            if (!_productionService.EqalProductItems(allProductionItems))
            {
                throw new ValidationException(
                    new ValidationResult(
                        new List<ValidationFailure> {
                            new ValidationFailure(nameof(ProductionGroup), "Not all the Production Items are equal")
                            }
                        )
                    );
            }
     
            int priority = groups.First().Priority;
            ProductionItem productionItem = new ProductionItem();
            productionItem.Item = groups[0].ProductionItems.First().Item;
            productionItem.Quantity = allProductionItems.Sum(p => p.Quantity);
            productionItem.RalGalv = allProductionItems[0].RalGalv;

            HashSet<string> orders = new HashSet<string>();
            HashSet<string> comments = new HashSet<string>();
            foreach (ProductionItem item in allProductionItems)
            {
                orders.Add(item.OrderNumber);
                string comment = "O: " + item.OrderNumber + ": " + item.Comment;
                comments.Add(item.Comment);
            }
            productionItem.OrderNumber = String.Join(", ", orders);
            productionItem.Comment = String.Join(", ", comments);

            ProductionGroup result = new ProductionGroup();
            result.Priority = priority;
            result.ProductionItems.Add(productionItem);
            
            await _productionService.MergePriorities(result, groups, user);
                       
            return _mapper.Map<ProductionGroupVm>(result);
        }
    }
}
