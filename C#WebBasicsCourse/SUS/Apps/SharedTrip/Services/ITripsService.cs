using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        ICollection<TripViewModel> GetAll();

        void Create(TripInputModel trip);

        bool AreThereAvailableSeats(string tripId);
        TripDetailsViewModel GetTripDetailsById(string tripId);
        bool AddUserToTrip(string userId, string tripId);
    }
}
