using System;

namespace Mifdemo.Domain.Seed
{
    public abstract class BaseAuditModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}