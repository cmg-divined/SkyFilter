using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Coflnet.Sky.Core;

namespace Coflnet.Sky.Filter
{
    public class UIdFilter : GeneralFilter
    {
        public override FilterType FilterType => FilterType.Equal | FilterType.RANGE;
        public override IEnumerable<object> Options => new object[] { "000000000000", "ffffffffffff" };

        public override Func<DBItem, bool> IsApplicable => item
                    => !(item?.Category.HasFlag(Category.BLOCKS) ?? false);


        public override Expression<Func<SaveAuction, bool>> GetExpression(FilterArgs args)
        {
            var key = NBT.Instance.GetKeyId("uid");
            var val = NBT.UidToLong(args.Get(this));
            return a => a.NBTLookup.Where(l => l.KeyId == key && l.Value == val).Any();
        }
    }
}
