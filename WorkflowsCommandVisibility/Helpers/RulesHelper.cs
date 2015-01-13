using Sitecore.Data.Items;
using Sitecore.Rules;

namespace WorkflowsCommandVisibility.Helpers
{
    public static class RulesHelper
    {
        public static bool EvaluateRule(string ruleFieldName, Item ruleItem, Item contentItem)
        {
            var ruleContext = new RuleContext();
            ruleContext.Item = contentItem;

            foreach (Rule<RuleContext> rule in RuleFactory.GetRules<RuleContext>(new[] { ruleItem }, ruleFieldName).Rules)
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
