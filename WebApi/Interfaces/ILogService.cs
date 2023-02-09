using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Entity;

namespace WebApi.Interfaces
{
    public interface ILogService
    {
        Task<Log> CreateLog(Log log);
    }
}