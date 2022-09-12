using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Entities.Interfaces
{
    public interface IAudited
    {
    }

    public interface IHasCreationTime : IAudited
    {
        public DateTime CreateTime { get; set; }
    }

    public interface ICreationAudited:IHasCreationTime
    {
        public long? CreatedUserID { get; set; }
    }
    
    public interface IHasModificationTime:IAudited
    {
        public DateTime? LastModifiedTime { get; set; }
    }

    public interface IModificationAudited:IHasModificationTime
    {
        public long? LastModifiedUserID { get; set; }
    }

    public interface IHasDeletionTime : IAudited
    {
        public DateTime? DeletedDate { get; set; }
    }

    public interface IDeletionAudited : IHasDeletionTime
    {
        public long? DeletedUserID { get; set; }
    }
}
