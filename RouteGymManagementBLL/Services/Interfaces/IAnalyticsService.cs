using RouteGymManagementBLL.ViewModels.AnalyticsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Interfaces
{
    public interface IAnalyticsService
    {
        AnalyticsViewModel GetAnalyticsData();
    }
}
