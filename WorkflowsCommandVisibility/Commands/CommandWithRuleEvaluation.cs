using System.Linq;
using Sitecore.Data;
using Sitecore.Shell.Framework.Commands;
using WorkflowsCommandVisibility.Helpers;

namespace WorkflowsCommandVisibility.Commands
{
    public class CommandWithRulesEvaluation : Sitecore.Shell.Framework.Commands.Workflow
    {
        private const string HideCommandFieldName = "Hide Command";
        private const string DisableCommandFieldName = "Disable Command";

        public override CommandState QueryState(CommandContext context)
        {
            var item = context.Items.FirstOrDefault();            

            if (item != null)
            {
                var commandID = context.Parameters["commandid"];
                if (commandID != null)
                {
                    var commandItem = Sitecore.Context.ContentDatabase.GetItem(new ID(commandID));

                    if (commandItem != null)
                    {
                        if (RulesHelper.EvaluateRule(HideCommandFieldName, commandItem, item))
                        {
                            return CommandState.Hidden;
                        }
                        if (RulesHelper.EvaluateRule(DisableCommandFieldName, commandItem, item))
                        {
                            return CommandState.Disabled;
                        }
                    }
                }
            }
            return base.QueryState(context);
        }
    }
}
