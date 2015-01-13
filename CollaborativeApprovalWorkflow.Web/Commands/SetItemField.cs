using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Web;
using Sitecore.Workflows.Simple;

namespace CollaborativeApprovalWorkflow.Commands
{
    public class SetItemFieldToMe
    {
        public void Process(WorkflowPipelineArgs args)
        {
            Item dataItem = args.DataItem;
            Item innerItem = args.ProcessorItem.InnerItem;
            System.Collections.Specialized.NameValueCollection parameters = WebUtil.ParseUrlParameters(innerItem["parameters"]);
            var fieldName = parameters["field"];

            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                using (new EditContext(dataItem))
                {
                    dataItem[fieldName] = Context.User.LocalName;
                }
            }
        }
    }
}