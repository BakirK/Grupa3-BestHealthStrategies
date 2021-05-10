using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestHealtStrategies.Models
{
    public interface ApiCalls
    {
        public List<DailyMealPlan> getWeeklyMealPlan(User user);
        public Meal getMeal(int id);
    }
}
