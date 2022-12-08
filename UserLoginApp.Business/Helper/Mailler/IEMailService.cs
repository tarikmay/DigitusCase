using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.Business.Helper.Mailler.Model;

namespace UserLoginApp.Business.Helper.Mailler
{
    public interface IEMailService
    {
        Task SendEmailAsync(EMailModel mailModel);
    }
}
