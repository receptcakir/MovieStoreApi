using MovieStoreApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.MailServices
{
    public class MailService : IMailService
    {
        public async Task<bool> SendEmailAsync(RecommendMovieEmailDto recommendMovieEmailDto)
        {
            return true;
        }
    }
}
