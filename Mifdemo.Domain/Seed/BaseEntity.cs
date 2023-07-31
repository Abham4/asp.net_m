using System;

namespace Mifdemo.Domain.Seed
{
    public abstract class BaseEntity<T> where T : BaseAuditModel
    {
        public int Id { get; protected set; }
        public DateTime CretedDate { get; set; }
        public abstract T MapToModel();
        public abstract T MapToModel(T t);
        public BaseEntity(T auditModel)
        {
            Id = auditModel.Id;
            CretedDate = auditModel.CreatedDate;
        }
        public BaseEntity()
        {
            
        }
    }
}