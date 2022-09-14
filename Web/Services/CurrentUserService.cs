using Application.Common.Interfaces;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            if (long.TryParse(GetHeaderValue(httpContextAccessor, "X-UserID"), out long userID))
            {
                UserID = UserID;
            }
        }

        public long? UserID { get;}

        private string GetHeaderValue(IHttpContextAccessor httpContextAccessor,string headerKey)
        {
            if(httpContextAccessor.HttpContext!=null && httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerKey,out StringValues headerValues))
            {
                return headerValues.First();
            }
            return String.Empty;
        }
    }
}
