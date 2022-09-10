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

}
