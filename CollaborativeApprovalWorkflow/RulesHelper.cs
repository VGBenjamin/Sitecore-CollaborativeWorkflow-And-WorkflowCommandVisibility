﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;
using Sitecore.Rules;

namespace CollaborativeApprovalWorkflow
{
    public static class RulesHelper
    {
        public static bool EvaluateRule(string fieldName, Item item)
        {
            var ruleContext = new RuleContext();
            foreach (Rule<RuleContext> rule in RuleFactory.GetRules<RuleContext>(new[] { item }, fieldName).Rules)
            {
                if (rule.Condition != null)
                {
                    var stack = new RuleStack();
                    rule.Condition.Evaluate(ruleContext, stack);

                    if (ruleContext.IsAborted)
                    {
                        continue;
                    }
                    if ((stack.Count != 0) && ((bool)stack.Pop()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }  
    }
}
