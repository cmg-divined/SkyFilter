using System.Linq;
using Microsoft.EntityFrameworkCore;
using Coflnet.Sky.Core;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Coflnet.Sky.Items.Client.Model;

namespace Coflnet.Sky.Filter
{
    public class SkinFilter : NBTItemFilter
    {
        protected override string PropName => "skin";

        public override IEnumerable<object> OptionsGet(OptionValues options)
        {
            return options.Options[PropName].Where(o => !o.ToString().Contains("http") && o.ToString().Length != 64).Prepend("any").Append("none");
        }
    }
}
