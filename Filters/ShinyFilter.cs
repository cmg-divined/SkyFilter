using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Coflnet.Sky.Core;

namespace Coflnet.Sky.Filter
{
    public class IsShinyFilter : GeneralFilter
    {
        public override FilterType FilterType => FilterType.BOOLEAN;

        public override IEnumerable<object> Options => new object[] { "true", "false" };

        public override Expression<Func<SaveAuction, bool>> GetExpression(FilterArgs args)
        {
            var key = NBT.Instance.GetKeyId("is_shiny");
            if (args.Get(this) == "true")
                return a => a.NBTLookup.Where(l => l.KeyId == key ).Any();
            return a => !a.NBTLookup.Where(l => l.KeyId == key ).Any();
        }
    }
}

