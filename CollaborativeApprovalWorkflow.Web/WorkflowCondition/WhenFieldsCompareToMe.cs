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

            string str2 = item[this.FieldName];

            return base.Compare(str2, Context.User.LocalName);
        }

    }
}