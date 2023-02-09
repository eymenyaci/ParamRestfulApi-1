using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace WebApi.Services
{
    public class LogService : ILogService
    {
        public async Task<Log> CreateLog(Log log)
        {
            using (var bookDbContext = new BookDbContext())
            {
                bookDbContext.Logs.Add(log);
                await bookDbContext.SaveChangesAsync();
                return log;
            }
        }
    }
}