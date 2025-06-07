namespace GroceryStore.Domain;

//TODO: add exception handling using fluent results
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

    public static Slot Create(
        DateOnly date,
        TimeOnly start)
    {
        if (IsStartOutRange(start)) throw new ArgumentException("Start Time: {0} is out of range", start.ToString());

        var isGreenDelivery = IsGreenDeliverySlot(start);

        var slot = new Slot(
            date,
            start,
            isGreenDelivery);

        return slot;
    }

    public static IEnumerable<Slot> GenerateSlots(DateOnly orderDate, int days = Constants.MaxDeliverySchedulingDays)
    {
        for (var day = 0; day < days; day++)
        {
            var currentDate = orderDate.AddDays(day);

            for (var hour = Constants.DeliveryStartTime; hour <= Constants.DeliveryEndTime - 1; hour++)
            {
                var start = new TimeOnly(hour, 0);

                yield return Create(
                    currentDate,
                    start);
            }
        }
    }

    private static bool IsStartOutRange(TimeOnly start) => start.Hour is < Constants.DeliveryStartTime or >= Constants.DeliveryEndTime;

    private static bool IsGreenDeliverySlot(TimeOnly start)
        => start.Hour is >= Constants.OffPeakHoursStart and < Constants.OffPeakHoursEnd;
}