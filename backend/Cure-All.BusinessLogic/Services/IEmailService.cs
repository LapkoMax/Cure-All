using Cure_All.BusinessLogic.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.Services
{
    public interface IEmailService
    {
        Task SendConfirmEmail(MessageOptions message);

        Task SendResetPasswordEmail(MessageOptions message);
    }
}
