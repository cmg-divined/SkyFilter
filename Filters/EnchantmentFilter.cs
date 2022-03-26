using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Coflnet.Sky.Core;
using Microsoft.EntityFrameworkCore;

namespace Coflnet.Sky.Filter
{
    public class EnchantmentFilter : GeneralFilter
    {
        public override FilterType FilterType => FilterType.Equal;
        public override Func<DBItem, bool> IsApplicable =>
                EnchantLvlFilter.IsEnchantable();
        public override IEnumerable<object> Options => Enum.GetNames(typeof(Enchantment.EnchantmentType)).OrderBy(e => e);


        public override Expression<Func<SaveAuction, bool>> GetExpression(FilterArgs args)
        {
            var enchant = Enum.Parse<Enchantment.EnchantmentType>(args.Get(this), true);
            if (enchant == Enchantment.EnchantmentType.None)
                return a => a.Enchantments == null || a.Enchantments.Count == 0;
            return null;
        }
    }



    public class EnchantLvlFilter : NumberFilter
    {
        //public override FilterType FilterType => FilterType.Equal | FilterType.SIMPLE;
        public override IEnumerable<object> Options => new object[] { 1, 10 };
        public override Func<DBItem, bool> IsApplicable =>
                IsEnchantable();

        public static Func<DBItem, bool> IsEnchantable()
        {
            return item => item.Category == Category.WEAPON
                            || item.Category == Category.ARMOR
                            || item.Tag == "ENCHANTED_BOOK"
                            || item.Tag.Contains("_DRILL")
                            || item.Description.ToLower().Contains("axe")
                            || item.Description.ToLower().Contains("shovel")
                            || item.Description.ToLower().Contains("hoe");
        }

        public virtual string EnchantmentKey { get; set; } = "Enchantment";

        public override Expression<Func<SaveAuction, bool>> GetExpression(FilterArgs args)
        {
            if (!args.Filters.ContainsKey(EnchantmentKey))
                throw new CoflnetException("invalid_filter", "You need to select an enchantment and a lvl to filter for");
            var enchant = Enum.Parse<Enchantment.EnchantmentType>(args.Filters[EnchantmentKey]);
            if (enchant == Enchantment.EnchantmentType.None)
                return null;
            var filterValue = args.Get(this);
            if (!short.TryParse(args.Get(this), out short lvl))
                return base.GetExpression(args);
            if (!args.Filters.ContainsKey("ItemId"))
                return a => a.Enchantments != null && a.Enchantments.Where(e => e.Type == enchant && e.Level == lvl).Any();
            var itemid = int.Parse(args.Filters["ItemId"]);
            return a => a.Enchantments != null && a.Enchantments.Where(e => itemid == e.ItemType && e.Type == enchant && e.Level == lvl).Any();
        }

        public override Expression<Func<SaveAuction, long>> GetSelector(FilterArgs args)
        {
            var enchant = Enum.Parse<Enchantment.EnchantmentType>(args.Filters[EnchantmentKey]);
            if (enchant == Enchantment.EnchantmentType.None)
                return a => 1;
            return a => a.Enchantments.Where(e => e.Type == enchant).Select(e => (int)e.Level).FirstOrDefault();
        }
    }

    public class SecondEnchantmentFilter : EnchantmentFilter
    {

    }

    public class SecondEnchantLvlFilter : EnchantLvlFilter
    {
        public override string EnchantmentKey { get; set; } = "SecondEnchantment";
    }
}
