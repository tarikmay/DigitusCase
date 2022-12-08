using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.Business.Interfaces;
using UserLoginApp.DataAccess.Interfaces;
using UserLoginApp.Entities.Concrete;

namespace UserLoginApp.Business.Concrete
{
    public class RequestTimeManager: GenericManagerMD<RequestTime>, IRequestTimeService
    {
        private readonly IGenericRepositoryMD<RequestTime> _genericRepositoryMD;

        public RequestTimeManager(IGenericRepositoryMD<RequestTime> genericRepositoryMD) : base(genericRepositoryMD)
        {
            _genericRepositoryMD = genericRepositoryMD;
        }

        public IEnumerable<RequestTime> GetUsersRequestCompleteTimeByDate(string date)
        {
            return _genericRepositoryMD.GetAll().Where(x =>(ObjectId.Parse(x.Id).CreationTime).ToString("dd.MM.yyyy") == date);
        }
    }
}
