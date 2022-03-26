using System;
using Coflnet.Sky.Core;

namespace Coflnet.Sky.Filter
{
    public class RaiderKillsFilter : NBTNumberFilter
    {
        public override Func<DBItem, bool> IsApplicable => a
            => a.Tag == "RAIDER_AXE";
        protected override string PropName => "raider_kills";
    }
}

