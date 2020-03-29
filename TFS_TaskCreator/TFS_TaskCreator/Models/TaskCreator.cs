using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi;
using System.Collections.Generic;
using System;
using TFS_API = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace TFS_TaskCreator.Models
{
    /// <summary>
    /// Creates TFS Work Item based on specified values.
    /// TFS Fields Reference: https://docs.microsoft.com/en-us/azure/devops/reference/xml/reportable-fields-reference?view=azure-devops
    /// Sample: https://github.com/Microsoft/azure-devops-dotnet-samples/blob/master/ClientLibrary/Snippets/Microsoft.TeamServices.Samples.Client/WorkItemTracking/WorkItemsSample.cs
    /// Get ALL Available Reference Fields: https://{tfs_url/project}/_apis/wit/fields?api-version=4.1
    /// </summary>
    public class TaskCreator
    {
        private readonly Uri _uri;
        private readonly string _personalAccessToken;
        private readonly string _project;
        private readonly string _customDepartment;
        private readonly VssBasicCredential _credentials;

        public TaskCreator(Settings settings)
        {
            _uri = new Uri(settings.URI);
            _personalAccessToken = settings.PersonalAccessToken;
            _project = settings.ProjectName;
            _customDepartment = settings.CustomDepartment;
            _credentials = new VssBasicCredential("", settings.PersonalAccessToken);
        }

        /// <summary>
        /// Creates a TFS Work Item
        /// </summary>
        /// <param name="item">TFS Item with fields filled out.</param>
        /// <returns>The created Work Item</returns>
        public TFS_API.WorkItem CreateWorkItem(TFS_Item item)
        {
            JsonPatchDocument patchDocument = GeneratePatchDocument(item);
            VssConnection connection = new VssConnection(_uri, _credentials);
            WorkItemTrackingHttpClient workItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                string workItemType = item.WorkItemType == Enums.WorkItemType.UserStory ? "User Story" : item.WorkItemType.ToString();

                TFS_API.WorkItem result = workItemTrackingHttpClient.CreateWorkItemAsync(patchDocument, _project, workItemType).Result;
                Console.WriteLine($"{item.WorkItemType} Successfully Created: {item.WorkItemType} #{0}", result.Id);
                return result;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"Error creating {item.WorkItemType}: {ex.InnerException.Message}");
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private JsonPatchDocument GeneratePatchDocument(TFS_Item item)
        {
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                ["Microsoft.VSTS.Common.AcceptanceCriteria"] = item.AcceptanceCriteria,
                ["Microsoft.VSTS.Common.Activity"] = item.Activity,
                ["System.AreaPath"] = item.AreaPath,
                ["System.AssignedTo"] = item.AssignedTo,
                ["System.Description"] = item.Description,
                ["System.IterationPath"] = item.IterationPath,
                ["Microsoft.VSTS.Scheduling.OriginalEstimate"] = item.OriginalEstimate,
                ["Microsoft.VSTS.Scheduling.RemainingWork"] = item.OriginalEstimate,
                ["Microsoft.VSTS.Common.Priority"] = item.Priority,
                ["Microsoft.VSTS.Scheduling.StoryPoints"] = item.StoryPoints,
                ["System.Title"] = item.Title,
                ["Microsoft.VSTS.Common.ValueArea"] = item.ValueArea
            };

            if (!string.IsNullOrEmpty(_customDepartment))
            {
                // Todo: Make this dynamic. Currently only setup to handle my own company only.
                values.Add($"{_customDepartment}.MilestoneDeliverable", item.Milestone); // no
                values.Add($"{_customDepartment}.SprintPoints", item.SprintPoints); // Same as StoryPoints
                values.Add($"{_customDepartment}.StoryType", item.StoryType); // Development
                values.Add($"{_customDepartment}.Commitment", item.CurrentSprintExpectation); // Description of what you want to accomplish this sprint
                if (item.WorkItemType == Enums.WorkItemType.UserStory) { values.Add("System.Tags", "Ready for Sprint Planning"); }
            }

           JsonPatchDocument patchDocument = new JsonPatchDocument();
            values.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.Value))
                {
                    patchDocument.Add(new JsonPatchOperation
                    {
                        Operation = Operation.Add,
                        Path = $"/fields/{x.Key}",
                        Value = x.Value
                    });
                }
            });

            if (!string.IsNullOrEmpty(item.ParentID)) { patchDocument.Add(LinkParentOperation(item.ParentID)); }

            return patchDocument;
        }

        private JsonPatchOperation LinkParentOperation(string parentId)
        {
            // Url: https://stackoverflow.com/questions/48175492/using-vsconnection-workitemtrackinghttpclient-patch-to-add-parent-relation-via-v
            return new JsonPatchOperation()
            {
                Operation = Operation.Add,
                Path = "/relations/-",
                Value = new
                {
                    rel = "System.LinkTypes.Hierarchy-Reverse",
                    url = _uri.AbsoluteUri + _project + "/_apis/wit/workItems/" + parentId,
                    attributes = new
                    {
                        comment = "link parent WIT"
                    }
                }
            };
        }
    }
}
