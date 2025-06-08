using MediatR;
using FluentResults;
using GroceryStore.Application.Abstractions;
using GroceryStore.Domain.Entities;
using GroceryStore.Domain.Services;

namespace GroceryStore.Application.Features.GetAvailableSlots;

public class Handler(
    IGroceryRepository groceryRepository,
    IDeliveryPolicyDomainService deliveryPolicyDomainService)
    : IRequestHandler<Query, Result<IEnumerable<GetSlotsResponse>>>
{
    public async Task<Result<IEnumerable<GetSlotsResponse>>> Handle(
        Query query,
        CancellationToken cancellationToken)
    {
        if (!await groceryRepository.AllProductsExistAsync(query.Ids, cancellationToken))
            return Result.Fail("not all products exists");

        var products = await groceryRepository
            .GetAllProductsAsync(query.Ids, cancellationToken);

        var allPossibleSlotsResult = Slot.GenerateSlots(DateOnly.FromDateTime(query.OrderDate));

        if (allPossibleSlotsResult.IsFailed) return Result.Fail(allPossibleSlotsResult.Errors);
        
        var orderDate = DateOnly.FromDateTime(query.OrderDate);
        var orderTime = TimeOnly.FromDateTime(query.OrderDate);
        
        var filteredSlots = deliveryPolicyDomainService.FilterAvailableSlots(
            allPossibleSlotsResult.Value, products, orderDate, orderTime);

        var sorted = filteredSlots
            .OrderBy(s => s.Date)
            .ThenBy(s => !s.IsGreenDelivery)
            .ThenBy(s => s.Start)
            .Select(s => new GetSlotsResponse
            {
                Date = s.Date,
                Start = s.Start,
                IsGreenDelivery = s.IsGreenDelivery
            });

        return Result.Ok(sorted);
    }
}