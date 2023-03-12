using MovieStoreApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.MailServices
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(RecommendMovieEmailDto recommendMovieEmailDto);
    }
}
