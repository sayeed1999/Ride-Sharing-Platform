﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Entity
{
    public static class AuthorizationPolicies
    {
        public const string AdminOnly = "AdminOnly";
    }

    public static class Roles
    {
        public const string Admin = "admin";
        public const string Moderator = "moderator";
        public const string Rider = "rider";
        public const string Customer = "customer";
    }
}
