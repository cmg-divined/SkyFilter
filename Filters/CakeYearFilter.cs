using System;
using System.Collections.Generic;
using System.Linq;
using hypixel;

namespace Coflnet.Sky.Filter
{
    public class CakeYearFilter : GeneralFilter
    {
        public override FilterType FilterType => FilterType.Equal | FilterType.NUMERICAL | FilterType.RANGE;

        public override IEnumerable<object> Options => new object[] { 1, CurrentMinecraftYear() +1 };

        public override Func<DBItem, bool> IsApplicable => item => item.Tag == "NEW_YEAR_CAKE";

        private static int CurrentMinecraftYear()
        {
            return (int)((DateTime.Now - new DateTime(2019, 6, 13)).TotalDays / (TimeSpan.FromDays(5) + TimeSpan.FromHours(4)).TotalDays + 1);
        }

        public override IQueryable<SaveAuction> AddQuery(IQueryable<SaveAuction> query, FilterArgs args)
        {
            var key = NBT.GetLookupKey("new_years_cake");
            var val = args.GetAsLong(this);
            return query.Where(a => a.NBTLookup.Where(l => l.KeyId == key && l.Value == val).Any());
        }
    }
}

