using EmailApp.DTOs;
using EmailApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailApp.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptionsDto userEmailOptions);
        //Task TestSendEmailHangfire();
        Task<List<MstUser>> CheckingEmailInactive();
        Task TestSendEmail();
    }
}
