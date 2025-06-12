namespace GroceryStore.Domain;

internal static class Constants
{
    internal const int MaxDeliverySchedulingDays = 14;
    internal const int DeliveryStartTime = 8;
    internal const int DeliveryEndTime = 22;
    internal const int OffPeakHoursStart = 13;
    internal const int OffPeakHoursEnd = 15;
    internal const int ExternalProductMinimumDaysNotice = 3;
    internal const int FreshFoodSameDayDeliveryCutoff = 12;
    internal const int InStockSameDayDeliveryCutoff = 18;
}