using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Exceptions
{
    public class UserFriendlyExceptions : Exception
    {
        public UserFriendlyExceptions(string errorMessage):this(errorMessage,null)
        {

        }

        public UserFriendlyExceptions(string errorMessage, Exception ex) : base($"The following error occured \"{errorMessage}\"", ex)
        {

        }
}
}
