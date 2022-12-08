using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.Entities.Concrete;

namespace UserLoginApp.Business.Interfaces
{
    public interface IRequestTimeService : IGenericServiceMD<RequestTime>
    {
        IEnumerable<RequestTime> GetUsersRequestCompleteTimeByDate(string date);
    }
}
