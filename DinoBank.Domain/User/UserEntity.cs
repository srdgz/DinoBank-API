﻿namespace DinoBank.Domain.User
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Type { get; set; }
    }
}
