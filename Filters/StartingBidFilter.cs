using System;
using System.Linq.Expressions;
using hypixel;

namespace Coflnet.Sky.Filter
{

    public class StartingBidFilter : NumberFilter
    {
        public override Expression<Func<SaveAuction, long>> GetSelector(FilterArgs args)
        {
            return a => a.StartingBid;
        }
    }
}
