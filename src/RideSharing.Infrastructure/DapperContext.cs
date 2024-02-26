﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace RideSharing.Infrastructure
{
	public class DapperContext
	{
		private readonly IConfiguration configuration;
		private readonly string _connectionString;

		public DapperContext(IConfiguration configuration)
		{
			this.configuration = configuration;
			_connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
		}

		public IDbConnection CreateConnection()
			=> new NpgsqlConnection(_connectionString);
	}
}
