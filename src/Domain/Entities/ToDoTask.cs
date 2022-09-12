using Domain.Common.Entities;
using Domain.Common.Entities.Interface;
using Domain.Common.Entities.Interfaces;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ToDoTask : BaseEntity<int>, ICreationAudited, IModificationAudited, IDeletionAudited, ISoftDelete
    {
        /// ICreationAudited
        public long? CreatedUserID { get; set; }


        public DateTime CreateTime { get; set; }

        /// IModificationAudited
        public long? LastModifiedUserID { get; set; }
        public DateTime? LastModifiedTime { get; set; }

        /// IDeletionAudited
        public long? DeletedUserID { get; set; }
        public DateTime? DeletedDate { get; set; }

        // ISoftDelete
        public bool IsDeleted { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public string? Name { get; set; }

        public bool IsCompleted { get; set; }

        public TaskPriority Priority { get; set; } 
        
        public TaskState State { get; set; }  

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Navigation Properties
        
        public int AssignedPersonId { get; set; }

        public Person Person { get; set; }
    }
}
