﻿using EventEchosAPI.Entities.Common;
using EventEchosAPI.Entities.Roles;
using System.Diagnostics;

namespace EventEchosAPI.Entities.Users
{
    public class User : AuditableEntity
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? Email { get; set; }

        public int? UserRoleId { get; set; }

        public UserRole UserRole { get; set; }

        public List<Admin> Admins{ get; set; }
        public List<Coordinator> Coordinators{ get; set; }
        public List<Guest> Guests{ get; set; }
    }
}
