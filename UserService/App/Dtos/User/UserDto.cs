﻿namespace UserService.App.Dtos.User
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string? FirebaseId { get; set; }

	}
}