using FluentResults;
using GroceryStore.Application.Abstractions;
using GroceryStore.Domain;
using MediatR;

namespace GroceryStore.Application.Features.GetAvailableSlots;

public class Handler(IGroceryRepository repository) : IRequestHandler<Query, Result<IEnumerable<GetSlotsResponse>>>
{
    public async Task<Result<IEnumerable<GetSlotsResponse>>> Handle(
        Query query,
        CancellationToken cancellationToken)
    {
        if (await repository.AllProductsExistAsync(query.Ids, cancellationToken))
            return Result.Fail("not all products exists");

        var products = await repository
            .GetAllProductsAsync(query.Ids, cancellationToken);

        var allPossibleSlotsResult = Slot.GenerateSlots(DateOnly.FromDateTime(query.OrderDate));

        if (allPossibleSlotsResult.IsFailed) return Result.Fail(allPossibleSlotsResult.Errors);

        var productsList = products.ToList();
        var orderDate = DateOnly.FromDateTime(query.OrderDate);
        var orderTime = TimeOnly.FromDateTime(query.OrderDate);

        var validSlots = allPossibleSlotsResult.Value
            .AsQueryable()
            .ApplyFilter(productsList, orderDate, orderTime, FilterByExternalProductsAdvanceNotice)
            .ApplyFilter(productsList, orderDate, orderTime, FilterByInStockSameDayDelivery)
            .ApplyFilter(productsList, orderDate, orderTime, FilterByFreshFoodSameDayDelivery)
            .ApplyFilter(productsList, orderDate, orderTime, FilterByExternalProductsDayOfWeek);

        var sortedSlots = validSlots
            .OrderBy(s => s.Date)
            .ThenBy(s => !s.IsGreenDelivery)
            .ThenBy(s => s.Start)
            .Select(s => new GetSlotsResponse
            {
                Date = s.Date,
                Start = s.Start,
                IsGreenDelivery = s.IsGreenDelivery
            })
            .ToList();

        return Result.Ok(sortedSlots.AsEnumerable());
    }


    private static IQueryable<Slot> FilterByExternalProductsAdvanceNotice(
        IQueryable<Slot> slots,
        List<Product> products,
        DateOnly orderDate,
        TimeOnly orderTime)
    {
        if (products.All(p => p.Type != ProductType.External))
            return slots;

        var minDeliveryDate = orderDate.AddDays(3);

        return slots.Where(s => s.Date >= minDeliveryDate);
    }


    private static IQueryable<Slot> FilterByInStockSameDayDelivery(
        IQueryable<Slot> slots,
        List<Product> products,
        DateOnly orderDate,
        TimeOnly orderTime)
    {
        if (products.All(p => p.Type != ProductType.InStock) || products.Any(p => p.Type == ProductType.External))
            return slots;

        return orderTime >= TimeOnly.FromTimeSpan(TimeSpan.FromHours(18))
            ? slots.Where(s => s.Date > orderDate)
            : slots;
    }

    private static IQueryable<Slot> FilterByFreshFoodSameDayDelivery(
        IQueryable<Slot> slots,
        List<Product> products,
        DateOnly orderDate,
        TimeOnly orderTime)
    {
        if (products.All(p => p.Type != ProductType.FreshFood) || products.Any(p => p.Type == ProductType.External))
            return slots;

        return orderTime >= TimeOnly.FromTimeSpan(TimeSpan.FromHours(12))
            ? slots.Where(s => s.Date > orderDate)
            : slots;
    }

    private static IQueryable<Slot> FilterByExternalProductsDayOfWeek(
        IQueryable<Slot> slots,
        List<Product> products,
        DateOnly orderDate,
        TimeOnly orderTime)
    {
        if (products.All(p => p.Type != ProductType.External))
            return slots;

        return slots.Where(s =>
            s.Date.DayOfWeek >= DayOfWeek.Tuesday &&
            s.Date.DayOfWeek <= DayOfWeek.Friday);
    }
}