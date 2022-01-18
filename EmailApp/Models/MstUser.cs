using System;
using System.Collections.Generic;

#nullable disable

namespace EmailApp.Models
{
    public partial class MstUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool? IsConfirmed { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string MobilePhone { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public int? TitleId { get; set; }
        public string FullName { get; set; }
        public int? IndustryId { get; set; }
        public string IndustryName { get; set; }
        public int? MinistryId { get; set; }
        public int? DepartmentId { get; set; }
        public string Position { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public int? City { get; set; }
        public int? State { get; set; }
        public string Postcode { get; set; }
        public int? Country { get; set; }
        public string CountryCode { get; set; }
        public DateTime? RegisterDateUtc { get; set; }
        public bool? IsActive { get; set; }
        public string IsApprove { get; set; }
        public byte[] Image { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastPasswordChanged { get; set; }
        public DateTime? LastLogin { get; set; }
        public string IsReject { get; set; }
    }
}
