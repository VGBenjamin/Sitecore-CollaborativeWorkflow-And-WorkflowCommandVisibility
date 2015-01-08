using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Shell.Framework.Commands;

namespace CollaborativeApprovalWorkflow
{
    public class CommandWithRulesEvaluation : Sitecore.Shell.Framework.Commands.Workflow
    {
        private const string HideCommandFieldName = "Hide Command";
        private const string DisableCommandFieldName = "Disable Command";

        public override CommandState QueryState(CommandContext context)
        {
            if (RulesHelper.EvaluateRule(HideCommandFieldName, context.Folder))
            {
                return CommandState.Hidden;
            }
            if (RulesHelper.EvaluateRule(DisableCommandFieldName, context.Folder))
            {
                return CommandState.Disabled;
            }
            return base.QueryState(context);
        }
    }
}
