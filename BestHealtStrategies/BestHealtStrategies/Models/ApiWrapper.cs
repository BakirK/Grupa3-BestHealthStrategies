using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public static class ApiWrapper
    {
        public static ApiCalls CurrentApi { get; set; } = new SpoonacularApi();
    }
}
