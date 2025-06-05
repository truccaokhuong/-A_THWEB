namespace TH_WEB.Models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed,
        NoShow,
        Refunded
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        PartiallyPaid,
        Failed,
        Refunded,
        Cancelled
    }

    public enum PassengerType
    {
        Adult,
        Child,
        Infant,
        Senior
    }

    public enum PackageType
    {
        HotelOnly,
        HotelCar,
        Complete,
        Custom
    }

    public enum RoomType
    {
        Standard,
        Superior,
        Deluxe,
        Suite,
        Villa,
        Apartment
    }

    public enum MealPlan
    {
        RoomOnly,
        Breakfast,
        HalfBoard,
        FullBoard,
        AllInclusive
    }

    public enum CancellationType
    {
        NonRefundable,
        Strict,
        Moderate,
        Flexible,
        SuperFlexible
    }

    public enum PackageStatus
    {
        Draft,
        Active,
        Inactive,
        Archived,
        SoldOut
    }

    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        PayPal,
        BankTransfer,
        Cash,
        Installment
    }

    public enum ImageType
    {
        General,
        Hotel,
        Destination,
        Activity,
        Food,
        Transport
    }

    public enum TravelerType
    {
        Solo,
        Couple,
        Family,
        Friends,
        Business
    }

    public enum ExtraType
    {
        Service,
        Activity,
        Transport,
        Insurance,
        Equipment,
        Food
    }

    public enum PricingType
    {
        PerPerson,
        PerRoom,
        PerGroup,
        PerItem,
        PerDay
    }

    public enum TagType
    {
        Feature,
        Activity,
        Amenity,
        Location,
        Special
    }
} 