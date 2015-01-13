using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Shell.Framework.Commands;

namespace CollaborativeApprovalWorkflow
{
    public class CommandWithRulesEvaluation : Sitecore.Shell.Framework.Commands.Workflow
    {
        private const string HideCommandFieldName = "Hide Command";
        private const string DisableCommandFieldName = "Disable Command";

        public CommandWithRulesEvaluation()
        {
        }

        public override string GetClick(CommandContext context, string click)
        {
            return base.GetClick(context, click);
        }

        public override void Execute(CommandContext context)
        {
            base.Execute(context);
        }

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
