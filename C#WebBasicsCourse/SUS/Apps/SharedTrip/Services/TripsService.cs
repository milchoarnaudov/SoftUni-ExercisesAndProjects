using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext context;

        public TripsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            var isUserInTrip = this.context.UserTrips.Any(x => x.UserId == userId && x.TripId == tripId);

            if (isUserInTrip)
            {
                return false;
            }

            var userTrip = new UserTrip
            {
                TripId = tripId,
                UserId = userId
            };

            this.context.UserTrips.Add(userTrip);
            this.context.SaveChanges();
            return true;
        }

        public bool AreThereAvailableSeats(string tripId)
        {
            var trip = this.context.Trips.Where(x => x.Id == tripId)
                .Select(x => new { x.Seats, TakenSeats = x.UserTrips.Count() })
                .FirstOrDefault();
            var availableSeats = trip.Seats - trip.TakenSeats;
            return availableSeats > 0;
        }

        public void Create(TripInputModel tripvm)
        {
            var trip = new Trip
            {
                StartPoint = tripvm.StartPoint,
                Seats = tripvm.Seats,
                EndPoint = tripvm.EndPoint,
                DepartureTime = DateTime.ParseExact(tripvm.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Description = tripvm.Description,
                ImagePath = tripvm.ImagePath,
            };

            this.context.Trips.Add(trip);
            this.context.SaveChanges();
        }

        public ICollection<TripViewModel> GetAll()
        {
            return this.context.Trips.Select(x => new TripViewModel
            {
                Id = x.Id,
                DepartureTime = x.DepartureTime,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                Seats = x.Seats,
                UsedSeats = x.UserTrips.Count()
            }).ToList();
        }

        public TripDetailsViewModel GetTripDetailsById(string tripId)
        {
            return this.context.Trips.Where(x => x.Id == tripId)
                .Select(x => new TripDetailsViewModel
                {
                    DepartureTime = x.DepartureTime,
                    Description = x.Description,
                    EndPoint = x.EndPoint,
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    Seats = x.Seats,
                    StartPoint = x.StartPoint,
                    UsedSeats = x.UserTrips.Count(),
                })
                .FirstOrDefault();
        }
    }
}
