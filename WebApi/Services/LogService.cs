using BookStore.Api.Interfaces;
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
        private readonly IBookStoreDbContext _bookStoreDbContext;

        public LogService(IBookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public Log CreateLog(Log log)
        {
            _bookStoreDbContext.Logs.Add(log);
            _bookStoreDbContext.SaveChanges();
            return log;
        }
    }
}