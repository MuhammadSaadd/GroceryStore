using FluentResults;

namespace GroceryStore.Domain;


public class Slot
{
    private Slot()
    {
        Date = default;
        Start = default;
        IsGreenDelivery = false;
    }

    private Slot(
        DateOnly date,
        TimeOnly start,
        bool isGreenDelivery) : this()
    {
        Date = date;
        Start = start;
        IsGreenDelivery = isGreenDelivery;
    }

    public DateOnly Date { get; private set; }
    public TimeOnly Start { get; private set; }
    public bool IsGreenDelivery { get; private set; }

    public static Result<Slot> Create(
        DateOnly date,
        TimeOnly start)
    {
        if (IsStartOutRange(start)) return Result.Fail($"Start Time: {start} is out of range");

        var isGreenDelivery = IsGreenDeliverySlot(start);

        var slot = new Slot(
            date,
            start,
            isGreenDelivery);

        return slot;
    }

    public static Result<List<Slot>> GenerateSlots(DateOnly orderDate,
        int days = Constants.MaxDeliverySchedulingDays)
    {
        var slots = new List<Slot>();
        
        for (var day = 0; day < days; day++)
        {
            var currentDate = orderDate.AddDays(day);

            for (var hour = Constants.DeliveryStartTime; hour <= Constants.DeliveryEndTime - 1; hour++)
            {
                var start = new TimeOnly(hour, 0);

                var slotResult = Create(
                    currentDate,
                    start);
                
                if(slotResult.IsFailed) return Result.Fail(slotResult.Errors);
                
                slots.Add(slotResult.Value);
            }
        }
        
        return Result.Ok(slots);
    }

    private static bool IsStartOutRange(TimeOnly start) =>
        start.Hour is < Constants.DeliveryStartTime or >= Constants.DeliveryEndTime;

    private static bool IsGreenDeliverySlot(TimeOnly start)
        => start.Hour is >= Constants.OffPeakHoursStart and < Constants.OffPeakHoursEnd;
}