using MassTransit;
using MovieStoreApi.Core.DTOs;
using MovieStoreApi.Service.MailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieStoreApi.Service.Consumers
{
    public class SendEmailConsumer : IConsumer<RecommendMovieEmailDto>
    {
        private readonly IMailService _mailService;

        public SendEmailConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<RecommendMovieEmailDto> context)
        {
                var result = await _mailService.SendEmailAsync(context.Message);
                if (result)
                {
                    await File.WriteAllTextAsync($"wwwroot/email-reports/succeded.txt", JsonSerializer.Serialize(context.Message));
                }
                else
                    await File.WriteAllTextAsync($"wwwroot/email-reports/errors.txt", JsonSerializer.Serialize(context.Message));
        }
    }
}
