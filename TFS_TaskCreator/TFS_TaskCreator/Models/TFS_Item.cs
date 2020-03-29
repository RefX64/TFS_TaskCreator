using Newtonsoft.Json;
using System;

namespace TFS_TaskCreator.Models
{
    [Serializable]
    public class TFS_Item
    {
        [JsonIgnore] public Enums.WorkItemType WorkItemType { get; set; } // Values: User Story, Task, Bug, etc.

        public string AcceptanceCriteria { get; set; }
        public string AreaPath { get; set; }
        public string AssignedTo { get; set; }
        public string CurrentSprintExpectation { get; set; }
        [JsonIgnore] public string Description { get; set; }
        public string IterationPath { get; set; }
        public string Milestone { get; set; }
        public string ParentID { get; set; }
        public string Priority { get; set; }
        [JsonIgnore] public string SprintPoints { get; set; }
        [JsonIgnore] public string StoryPoints { get; set; }
        [JsonIgnore] public string Title { get; set; }

        // User Story Only
        public string StoryType { get; set; }
        public string ValueArea { get; set; }

        // Tasks Only
        public string Activity { get; set; }
        [JsonIgnore] public string OriginalEstimate { get; set; } // Hours
    }
}