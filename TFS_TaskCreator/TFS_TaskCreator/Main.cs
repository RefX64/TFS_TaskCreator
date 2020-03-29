using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TFS_TaskCreator.Models;
using static TFS_TaskCreator.Models.Enums;

namespace TFS_TaskCreator
{
    /*
        Help url: https://docs.microsoft.com/en-us/azure/devops/integrate/quickstarts/create-bug-quickstart?view=azure-devops
    */

    public partial class Main : Form
    {
        private Settings _settings;

        public Main()
        {
            InitializeComponent();
            ReloadSettings();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void StartCreateBtn_Click(object sender, EventArgs e)
        {
            // Format: 0-Title|1-Mini-Disc(Acceptrance Criteria)|2-Story Points|3-Hours|4-Descriptions
            TaskCreator taskCreator = new TaskCreator(_settings);
            string clippedTasks = Clipboard.GetText();
            List<string> tasks = Regex.Split(clippedTasks, @"\r\n").ToList();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, env) =>
            {
                tasks.ForEach(x =>
                {
                    x = x.Trim();
                    if (!string.IsNullOrEmpty(x) && x.Contains('|'))
                    {
                        string[] createStringArr = x.Split('|');
                        if (createStringArr.Length < 5)
                        {
                            MessageBox.Show($"An error has occured: The create-string is in invalid format. create-string: {x}", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception("Invalid create string format!"); // Something is wrong, decided to end the entire app.
                        }

                        CreateUserStoryAndTask(taskCreator, createStringArr);
                    }
                });
            };
            worker.RunWorkerCompleted += (s, env) => { SetStatus("Completed work..."); };
            worker.RunWorkerAsync();            
        }

        private void CreateUserStoryAndTask(TaskCreator taskCreator, string[] createStringArr)
        {
            TFS_Item userStoryItem = Utilities.DeepClone(_settings.TFSDefaults);

            userStoryItem.WorkItemType = WorkItemType.UserStory;
            ExtractCommonFields(userStoryItem, createStringArr);
            CleanTaskItem(userStoryItem, WorkItemType.UserStory);

            string workItemTitle = userStoryItem.Title.Length > 30 ? userStoryItem.Title.Substring(0, 30) : userStoryItem.Title;
            SetStatus($"Creating User Story: {workItemTitle}...");

            var userStoryItemResult = taskCreator.CreateWorkItem(userStoryItem);

            if (userStoryItemResult.Id.GetValueOrDefault() > 0)
            {
                TFS_Item taskItem = Utilities.DeepClone(_settings.TFSDefaults);
                taskItem.WorkItemType = WorkItemType.Task;
                taskItem.ParentID = userStoryItemResult.Id.Value.ToString();
                ExtractCommonFields(taskItem, createStringArr);
                CleanTaskItem(taskItem, WorkItemType.Task);
                taskItem.OriginalEstimate = createStringArr[3]; // Hours for the task, used for Original Estimate and Remaining Hours

                SetStatus($"Creating Task: {workItemTitle}...");

                var taskItemResult = taskCreator.CreateWorkItem(taskItem);
                if (userStoryItemResult.Id.GetValueOrDefault() <= 0)
                {
                    MessageBox.Show($"Failed to create task: {taskItem.Title}", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SetStatus(string statusText)
        {
            if (StatusLbl.InvokeRequired)
            {
                StatusLbl.Invoke(new Action(() => { StatusLbl.Text = statusText; }));
                return;
            }
            StatusLbl.Text = statusText;
        }

        private static void ExtractCommonFields(TFS_Item item, string[] createStringArr)
        {
            item.Title = createStringArr[0];
            item.AcceptanceCriteria = createStringArr[1] + "<br>" + item.AcceptanceCriteria;
            item.StoryPoints = createStringArr[2];
            item.SprintPoints = item.StoryPoints;
            item.Description = createStringArr[4];
        }

        private void CleanTaskItem(TFS_Item item, WorkItemType type)
        {
            if (type == WorkItemType.UserStory)
            {
                item.Activity = null;
            }
            else if (type == WorkItemType.Task)
            {
                item.StoryType = null;
                item.ValueArea = null;
                item.CurrentSprintExpectation = null;
            }
        }

        private void ReloadSettings()
        {
            _settings = Settings.LoadSettings();
            if (_settings == null)
            {
                _settings = new Settings();
                _settings.TFSDefaults = new TFS_Item();
                _settings.WriteCurrentSettings();
            }
        }

        private void TestCreate()
        {
            TFS_Item itemToCreate = CreateTestTFSItem(WorkItemType.Task);
            TaskCreator taskCreator = new TaskCreator(_settings);
            var result = taskCreator.CreateWorkItem(itemToCreate);
            Console.WriteLine(result.Id);
        }

        private TFS_Item CreateTestTFSItem(WorkItemType workItemType)
        {
            return new TFS_Item
            {
                AcceptanceCriteria = "Checked into TFS.",
                Activity = workItemType == WorkItemType.Task ? "Development" : string.Empty, // Task only
                AreaPath = "All_Devs",
                AssignedTo = "Ref Chowdhury",
                Description = "My first created test TFS task",
                IterationPath = "All_Devs\\Release 1\\Sprint 1",
                Milestone = "no",
                OriginalEstimate = workItemType == WorkItemType.Task ? "3" : string.Empty, // Task only
                ParentID = "6",
                Priority = "2",
                SprintPoints = "1",
                StoryPoints = "1", 
                StoryType = workItemType == WorkItemType.UserStory ? "Development" : string.Empty, // User Story only
                Title = "Test US Created by TaskCreator",
                ValueArea = workItemType == WorkItemType.UserStory ? "Business" : string.Empty, // User Story only
                WorkItemType = workItemType
            };
        }

        private void openSettingsLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists("settings.json"))
            {
                Process.Start("settings.json");
                return;
            }

            MessageBox.Show("settings.json was not found... Please restart the program.", "An error has occured:", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void reloadSettingsLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReloadSettings();
            SetStatus("Settings has been reloaded...");
        }
    }
}
