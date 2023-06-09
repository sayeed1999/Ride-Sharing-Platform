﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Entity;
using RideSharing.Service;

namespace RideSharing.API
{
    [Route("api/v1/trips")]
    [ApiController]
    public class TripController : BaseController<Trip>
    {
        public TripController(ITripService baseService) : base(baseService)
        {
        }
    }
}
