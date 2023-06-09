﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Entity;
using RideSharing.Service;

namespace RideSharing.API
{
    [Route("api/v1/driver-ratings")]
    [ApiController]
    public class DriverRatingController : BaseController<DriverRating>
    {
        public DriverRatingController(IDriverRatingService baseService) : base(baseService)
        {
        }
    }
}
