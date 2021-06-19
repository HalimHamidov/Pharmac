using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// using Tests.Selenium.Api.E2ETests.ModelAutoGenerating;
using Tests.Selenium.TestData;
using Tests.Selenium.TestData.Models;
// using Tests.Selenium.TestData.Models.CollegialBodyModel;
// using Tests.Selenium.TestData.Models.InstructionModel;
// using Tests.Selenium.TestData.Models.Enum;

namespace Tests.Selenium.TestData.Generator
{
    public class DataAutoGenerator
    {
        private readonly Seeds _seeds;
        private readonly string _hostPrefix;

        public DataGeneratingService GeneratingService; 
         
        //public MaterialNamesGenerator PathGenerator { get; }

        public DataAutoGenerator(string hostPrefix, Seeds seeds, string testId = "")
        {
            _hostPrefix = hostPrefix;
            _seeds = seeds;
           // PathGenerator = new MaterialNamesGenerator();
          //  InitializeTestIdForGenerator(testId);
        }

        // private static void InitializeTestIdForGenerator(string testId)
        // {
        //     InstructionBuilder.SetTestId(testId);
        //     MeetingsBuilder.SetTestId(testId);
        //     IssuesBuilder.SetTestId(testId);
        // }

        // public User LoginByRole(CollegialBody collegialBody, CollegialBodyMemberRole role)
        // {
        //     var user = _seeds
        //         .GetMember(collegialBody, role);
        //     Login(user.Login, user.Password);
        //     return user;
        // }

        public void Login(User user)
        {
            Login(user.Login, user.Password);
        }

        public void Login(string userLogin, string userPassword)
        {
            LoginToGeneratingService(userLogin, userPassword, _hostPrefix);
        }

        private void LoginToGeneratingService(string login, string password, string hostPrefix)
        {
            GeneratingService = new DataGeneratingService(login, password, hostPrefix);
        }

        // public void DeleteGeneratingObjects()
        // {
        //     //  GeneratingService.RollBackStatus(_meetingsId, _issuesId);
        //     GeneratingService.DeleteMeetings(_meetingsId);
        //     GeneratingService.DeleteIssue(_issuesId);
        // }

        // public void GenerateMeeting(IEnumerable<Meeting> meetings)
        // {
        //     var meetingsId = GeneratingService
        //         .GenerateMeetings(meetings);

        //     _meetingsId = _meetingsId != null
        //         ? meetingsId
        //             .Union(_meetingsId)
        //             .ToList()
        //    : meetingsId;
        // }

        // public IEnumerable<GuidId> GenerateIssue(IEnumerable<Issue> issues)
        // {
        //     var issuesId = GeneratingService
        //         .GenerateIssues(issues, PathGenerator);
            
        //     _issuesId = _issuesId != null
        //         ? issuesId
        //             .Union(_issuesId)
        //             .ToList()
        //         : issuesId;

        //     ICollection<GuidId> issuesGuidId = new List<GuidId>();
        //     foreach (var issueId in issuesId)
        //     {
        //         issuesGuidId.Add(new GuidId { Id = issueId });
        //     }
            
        //     return issuesGuidId;
        // }

        // public IEnumerable<GuidId> GenerateInstruction(IEnumerable<Instruction> instructions)
        // {
        //     var instructionsId = GeneratingService
        //         .GenerateInstructions(instructions, PathGenerator);

        //     _instructionsId = _instructionsId != null
        //         ? instructionsId
        //             .Union(_instructionsId)
        //             .ToList()
        //         : instructionsId;

        //     return instructionsId.Select(x => new GuidId { Id = x }).ToList();
        // }

        // public bool ChangeMeetingStatus(Meeting meeting, MeetingActions action)
        // {
        //     return GeneratingService.ChangeMeetingStatus(meeting.Id, action);
        // }

        // public bool ChangeIssueStatus(Issue issue, IssueStatus status, bool withAdjustment = false)
        // {
        //     return GeneratingService.ChangeIssueStatus(issue.Id, status, withAdjustment);
        // }

        // public bool SendIssueOnReview(Issue issue)
        // {
        //     return GeneratingService.SendIssueOnReview(issue);
        // }

        // public bool FinishIssueReview(Issue issue)
        // {
        //     return GeneratingService.FinishIssueReview(issue);
        // }

        // public bool SendIssueReviewDecision(Issue issue, User reviewer, IssueReviewDecision issueReviewDecision)
        // {
        //     return new DataGeneratingService(reviewer.Login, reviewer.Password, _hostPrefix)
        //         .SendIssueReviewDecision(issue, reviewer, issueReviewDecision);
        // }

        // public bool AcceptIssue(Issue issue)
        // {
        //     return GeneratingService.AcceptIssue(issue.Id, false);
        // }

        // public void MarkPresenceAllParticipants(Issue issue, Meeting meeting)
        // {
        //     GeneratingService.MarkPresenceAllParticipants(issue.VersionId, meeting.Id);
        // }

        // public void SendOnApproveFirstDecision(CollegialBody collegialBody, Meeting meeting, User approvingUser)
        // {
        //     var decisionId = GeneratingService.GetDecisionIds(meeting.Id).First();
        //     var approvingUserId = GeneratingService.GetMemberId(collegialBody.Id, approvingUser);

        //     GeneratingService.SendOnApproveDecision(decisionId, approvingUserId);
        // }

        // public void VoteIssue(CollegialBody collegialBody, Issue issue, User user, Vote vote, string comment = "")
        // {
        //     var votingId = GeneratingService.GetVotingId(issue);
        //     var memberId = GeneratingService.GetMemberId(collegialBody.Id, user);
        //     GeneratingService.VoteIssue(votingId, memberId, vote, comment);
        // }

        // public void FormWrittenOpinion(User user, Meeting meeting, params WrittenOpinion[] writtenOpinions)
        // {
        //     new DataGeneratingService(user.Login, user.Password, _hostPrefix)
        //         .FormWrittenOpinion(meeting.Id, writtenOpinions);
        // }

        // public void FormProtocol(Meeting meeting)
        // {
        //     GeneratingService.FormProtocol(meeting.Id, new Guid[] { });
        // }

        // public void SignProtocol(User user, Meeting meeting)
        // {
        //     new DataGeneratingService(user.Login, user.Password, _hostPrefix)
        //         .SignProtocol(meeting.Id);
        // }

        // public void ApproveIssueInReview(CollegialBody collegialBody, User user, Issue issue)
        // {
        //     new DataGeneratingService(user.Login, user.Password, _hostPrefix)
        //         .ApproveIssueInReview(issue, GeneratingService.GetMemberId(collegialBody.Id, user));
        // }

        // public IEnumerable<string> GetNotifications(User user, int count = 100)
        // {
        //     return new DataGeneratingService(user.Login, user.Password, _hostPrefix)
        //            .GetNotifications(count);
        // }
    }
}
