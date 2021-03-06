﻿using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Globalization;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.StartPoint))
            {
                return this.Error("Start point is required");
            }

            if (string.IsNullOrEmpty(input.EndPoint))
            {
                return this.Error("End point is required");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Error("Seats must be between 2 and 6");
            }

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 80)
            {
                return this.Error("Description must be shorter than 80");
            }

            if (!DateTime.TryParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            {
                return this.Error("Invalid Date");
            }

            this.tripsService.Create(input);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trip = this.tripsService.GetTripDetailsById(tripId);
            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.tripsService.AreThereAvailableSeats(tripId))
            {
                return this.Error("Not available seats");
            }

            var userId = this.GetUserId();
            if (!this.tripsService.AddUserToTrip(userId, tripId))
            {
                return this.Redirect("/Trips/Details?tripId=" + tripId);
            }

            return this.Redirect("/Trips/All");
        }
    }
}
