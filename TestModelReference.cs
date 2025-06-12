using TH_WEB.Models;

namespace TH_WEB
{
    public class TestModelReference
    {
        public void TestCarRentalModels()
        {
            var carRental = new CarRental();
            var carType = new CarType();
            var location = new Location();
            var booking = new CarRentalBooking();
            var extra = new CarRentalExtra();
        }
    }
}
