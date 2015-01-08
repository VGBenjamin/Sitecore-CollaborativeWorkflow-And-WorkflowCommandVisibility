using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace CollaborativeApprovalWorkflow.WorkflowCondition
{
    public class WhenFieldsCompareToMe<T> : StringOperatorCondition<T> where T : RuleContext
    {
        public string FieldName { get; set; }

        public string Value { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Item item = ruleContext.Item;
            if (item == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.FieldName))
            {
                return false;
            }
            return base.Compare(this.Value ?? string.Empty, Context.User.LocalName);
        }

    }
}