﻿namespace UserService.App.Dtos.Address
{
	public class AddressDto
	{
		public Guid Id { get; set; }
		public string Street { get; set; }
		public string Number { get; set; }
		public string Neighborhood { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public Guid UserId { get; set; }
	}
}