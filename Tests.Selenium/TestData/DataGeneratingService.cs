// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using SBT.Senat.TestData.Models;
// using SBT.Senat.TestData.Generator.ApiClient;
// using SBT.Senat.TestData.Generator.ApiClient.Dto.Issue;
// using SBT.Senat.TestData.Generator.ApiClient.ResponseDto;
// using SBT.Senat.TestData.Generator.ApiClient.ResponseDto.IssueResponse;
// using SBT.Senat.TestData.Generator.ApiClient.ResponseDto.MeetingResponse;
// using Holding = SBT.Senat.TestData.Models.Holding;
// using Issue = SBT.Senat.TestData.Models.Issue;
// using SBT.Senat.TestData.Models.InstructionModel;
// using SBT.Senat.TestData.Models.Enum;
// using SBT.Senat.TestData.Generator.ApiClient.Dto.Member;
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
    public class DataGeneratingService
    {
        private readonly IRequestSender _requestSender;

        public DataGeneratingService(string startLogin, string startPassword, string hostPrefix)
        {
            _requestSender = new RequestSender(
                startLogin,
                startPassword,
                hostPrefix);
        }

        public void GenerateSeeds(Seeds seeds)
        {
            // GenerateCustomFields(seeds);
            //GenerateSeedsUsers(seeds);
            // GenerateSeedsStructs(seeds);
            //GenerateSeedsAddUsersToCollegialBodies(seeds);
        }

        // private IEnumerable<ApiClient.Dto.Issue.Material> CreateMaterials(Guid collegialBodyId, MaterialNamesGenerator generator, IEnumerable<MaterialCategory> materialsCategory)
        // {
        //     IList<ApiClient.Dto.Issue.Material> materials = new List<ApiClient.Dto.Issue.Material>();
        //     if (materialsCategory != null)
        //     {
        //         foreach (var materialCategory in materialsCategory)
        //         {
        //             var path = generator.GetPath(materialCategory);
        //             var name = generator.GenerateName(materialCategory);

        //             var materialId = _requestSender
        //                 .CreateMaterial(collegialBodyId, path, false, name);
        //             materials.Add(new ApiClient.Dto.Issue.Material
        //             {
        //                 Category = materialCategory,
        //                 MaterialId = new ApiClient.Dto.GuidId
        //                 { Id = materialId },
        //                 Name = name
        //             });
        //         }
        //     }
        //     return materials;
        // }
        // public IEnumerable<Guid> GenerateIssues(IEnumerable<Issue> issues, MaterialNamesGenerator generator)
        // {
        //     var issuesId = new List<Guid>();

        //     foreach (var issue in issues)
        //     {
        //         var issueReview = GetIssueReviewFromIssue(issue);
        //         var issueInitiator = GetIssueInitiatorFromIssue(issue);

        //         var materials = CreateMaterials(issue.CollegialBodyId, generator, issue.Materials.Select((Models.Material material) => { return material.Category; }));

        //         var issueCustomFieldDto = FormIssueCustomFieldDto(issue);
        //         var issueId = _requestSender.CreateIssue(
        //             Mapper.MapIssueToApiDto(issue, materials, issueReview, issueInitiator, issueCustomFieldDto));

        //         if (issue.Status != IssueStatus.Nothing)
        //         {
        //             _requestSender.ChangeStateIssue
        //                 (issueId, IssueStatus.ToPrepared, new IssueWorkflow());
        //         }
        //         //TODO: Составить последовательность и реализовать переключения на другие статусы
        //         issue.Id = issueId;
        //         issue.VersionId = _requestSender.GetIssueVersionId(issueId);
        //         issuesId.Add(issueId);
        //         issue.Materials = new List<Models.Material>(materials.Select(material => new Models.Material { Name = material.Name, Category = material.Category }));
        //     }
        //     return issuesId;
        // }

        // private IssueReview GetIssueReviewFromIssue(Issue issue)
        // {
        //     if (issue.Reviewer == null)
        //         return null;

        //     var steps = issue.Reviewer.Select(step =>
        //     {
        //         return step.Select(reviewer => new ReviewerItem()
        //         {
        //             Reviewer = new GuidId
        //             {
        //                 Id = GetReviewerId(issue.CollegialBodyId, reviewer)
        //             }
        //         }).ToList();
        //     }).ToList();

        //     return new IssueReview()
        //     {
        //         Steps = steps
        //     };
        // }

        // private IEnumerable<ApiClient.Dto.GuidId> GetIssueInitiatorFromIssue(Issue issue)
        // {
        //     if (issue.Initiator == null)
        //         return null;

        //     var step = issue.Initiator
        //         .Select(x => new ApiClient.Dto.GuidId
        //         {
        //             Id = GetEmployeeId(x)
        //         }
        //         ).ToList();

        //     return step;
        // }

        // private IEnumerable<IssueCustomFieldDto> FormIssueCustomFieldDto(Issue issue)
        // {
        //     if (issue.CustomField == null)
        //         return null;

        //     var customFieldsCB = _requestSender.GetCollegialBodyCustomField(issue.CollegialBodyId);
        //     var customFieldsDto = new List<IssueCustomFieldDto>();

        //     foreach (var customField in issue.CustomField)
        //     {
        //         var customFieldCB = customFieldsCB
        //             .First(x => x.CustomFieldSetting.FormLabel.Ru == customField.Key);

        //         customFieldsDto.Add(new IssueCustomFieldDto()
        //         {
        //             CustomFieldSettingId = customFieldCB.CustomFieldSetting.Id,
        //             Discriminator = GetCustomFieldDtoDiscriminator(customFieldCB.CustomFieldSetting.Type),
        //             Value = customField.Value
        //         });
        //     }

        //     return customFieldsDto;
        // }

        // private string GetCustomFieldDtoDiscriminator(string discriminator)
        // {
        //     switch (discriminator)
        //     {
        //         case "SingleLineText":
        //             return "StringValueCustomFieldInputDto";

        //         case "IntegerValue":
        //             return "IntegerValueCustomFieldInputDto";

        //         default:
        //             throw new Exception($"Doesn't have value for '{discriminator}'");
        //     }
        // }

        // public IEnumerable<Guid> GenerateMeetings(IEnumerable<Meeting> meetings)
        // {
        //     var meetingsId = new List<Guid>();
        //     foreach (var meeting in meetings)
        //     {
        //         Guid meetingId;
        //         if (meeting.MeetingType == MeetingType.Absentia)
        //             meetingId = _requestSender.CreateAbsentiaMeeting(Mapper.MapAbsentiaMeetingToApiDto(meeting));
        //         else
        //             meetingId = _requestSender.CreateMeeting(Mapper.MapMeetingToApiDto(meeting));

        //         meetingsId.Add(meetingId);
        //         meeting.Id = meetingId;
        //         if (meeting.Status != MeetingActions.Nothing)
        //         {
        //             _requestSender.SelectActionMeeting(meetingId, MeetingActions.ToPrepared);
        //         }
        //     }
        //     return meetingsId;
        // }

        // private void RollBackMeetingStatusToPrepared(Guid meetingId)
        // {
        //     var i = 10;
        //     var status = _requestSender.GetMeeting(meetingId).Status;
        //     while (status != MeetingStatusResponse.Draft &&
        //            status != MeetingStatusResponse.Prepared)
        //     {
        //         if (--i < 1)
        //         {
        //             throw new Exception("Meeting status couldn't Roll");
        //         }

        //         _requestSender.RollBackMeeting(meetingId);
        //         status = _requestSender.GetMeeting(meetingId).Status;
        //     }
        // }

        // public IEnumerable<Guid> GenerateInstructions(IEnumerable<Instruction> instructions, MaterialNamesGenerator generator)
        // {
        //     var instructionsIds = new List<Guid>();

        //     foreach (var instruction in instructions)
        //     {
        //         var materials = CreateInstructionMaterials(instruction.CollegialBodyId.Id, generator, instruction.Materials);

        //         var instructionDTO = Mapper.MapInstructionToApiDto(instruction, materials);

        //         var instructionId = _requestSender.CreateInstruction(instructionDTO);
        //         instruction.Id = instructionId;
        //         instructionsIds.Add(instructionId);
        //     }

        //     return instructionsIds;
        // }

        // private IEnumerable<ApiClient.Dto.GuidId> CreateInstructionMaterials(Guid collegialBodyId, MaterialNamesGenerator generator, IEnumerable<MaterialCategory> materialsCategory)
        // {
        //     var materialIds = new List<ApiClient.Dto.GuidId>();

        //     if (materialsCategory != null)
        //     {
        //         foreach (var materialCategory in materialsCategory)
        //         {
        //             var materialId = _requestSender
        //                 .CreateInstructionMaterial(collegialBodyId, generator.GetPath(materialCategory));

        //             materialIds.Add(new ApiClient.Dto.GuidId { Id = materialId });
        //         }
        //     }

        //     return materialIds;
        // }

        // public void DeleteMeetings(IEnumerable<Guid> meetingsId)
        // {
        //     if (meetingsId != null)
        //     {
        //         var issuesList = new IssuesList { Issues = new List<GuidId>() };
        //         foreach (var meetingId in meetingsId)
        //         {
        //             // RollBackMeetingStatusToPrepared(meetingId);
        //             _requestSender.ModifyIssuesFromMeeting(meetingId, issuesList);
        //             _requestSender.DeleteMeeting(meetingId);
        //         }
        //     }
        // }

        // public void DeleteIssue(IEnumerable<Guid> issuesId)
        // {
        //     if (issuesId != null)
        //     {
        //         foreach (var issueId in issuesId)
        //         {
        //             _requestSender.UnlockIssue(issueId);
        //             _requestSender.ChangeStateIssue
        //                 (issueId, IssueStatus.ToPrepared, new IssueWorkflow());
        //             _requestSender.DeleteIssue(issueId);
        //         }
        //     }
        // }

        // public bool ChangeMeetingStatus(Guid meetingId, MeetingActions status)
        // {
        //     return _requestSender.SelectActionMeeting(meetingId, status);
        // }

        // public bool ChangeIssueStatus(Guid issueId, IssueStatus status, bool withAdjustment = false)
        // {
        //     return _requestSender.ChangeStateIssue(issueId, status, new IssueWorkflow() { isVotingWithAdjustment = withAdjustment.ToString() });
        // }

        // public bool SendIssueOnReview(Issue issue)
        // {
        //     return _requestSender.SendIssueOnReview(issue.Id);
        // }

        // public bool FinishIssueReview(Issue issue)
        // {
        //     return _requestSender.FinishIssueReview(issue.Id);
        // }

        // public bool ApproveIssueInReview(Issue issue, Guid userId)
        // {
        //     return _requestSender.ApproveIssueInReview(issue.VersionId, userId, new ApiClient.Dto.Issue.RootCoordinationDto{ Decision = "Approved" });
        // }

        // public bool SendIssueReviewDecision(Issue issue, User reviewer, IssueReviewDecision issueReviewDecision)
        // {
        //     var reviewSteps = _requestSender.GetReviewSteps(issue.VersionId);

        //     var coordinationId = reviewSteps
        //         .SelectMany(x => x.Coordinations)
        //         .First(x => x.Reviewer.FirstName == reviewer.FirstName &&
        //             x.Reviewer.LastName == reviewer.Surname).RootCoordinationId;

        //     return _requestSender.SendReviewDecision(coordinationId, Mapper. MapIssuesToProtocolApiDto(issueReviewDecision));
        // }

        // public void MarkPresenceAllParticipants(Guid issueId, Guid meetingId)
        // {
        //     var members = _requestSender.GetMeetingParticipants(meetingId);

        //     var attendees = new Attendees
        //     {
        //         PresentEmployees = members.Select(x => new ApiClient.Dto.GuidId() { Id = x }).ToList()
        //     };

        //     _requestSender.MarkPresenceParticipant(issueId, attendees);
        // }

        // public IEnumerable<Guid> GetDecisionIds(Guid meetingId)
        // {
        //     return _requestSender.GetDecisionIdsByMeeting(meetingId);
        // }

        // public Guid GetMemberId(Guid collegialBodyId, User user)
        // {
        //     var members = _requestSender.GetCollegialBodyMembers(collegialBodyId);

        //     return members.First(x =>
        //         x.FirstName == user.FirstName &&
        //         x.LastName == user.Surname).Id;
        // }

        // public Guid GetReviewerId(Guid collegialBodyId, User user)
        // {
        //     var members = _requestSender.GetReviewers(collegialBodyId, user);

        //     return members.First(x =>
        //         x.FirstName == user.FirstName &&
        //         x.LastName == user.Surname).Id;
        // }

        // public Guid GetEmployeeId(User user)
        // {
        //     var members = _requestSender.GetEmployees(user.Surname);

        //     return members.First(x =>
        //         x.FirstName == user.FirstName &&
        //         x.LastName == user.Surname).Id;
        // }

        // public Guid GetVotingId(Issue issue)
        // {
        //     return _requestSender.GetVotingIdByIssueVersion(issue.VersionId);
        // }

        // public void SendOnApproveDecision(Guid decisionId, Guid approvingUserId)
        // {
        //     _requestSender.SendOnApproveDecision(decisionId, Mapper.MapApprovingUserIdToApiDto(approvingUserId));
        // }

        // public bool AcceptIssue(Guid issueId, bool withForce)
        // {
        //     return _requestSender.AcceptIssue(issueId, withForce);
        // }

        // public void VoteIssue(Guid votingId, Guid userId, Vote vote, string comment = "")
        // {
        //     _requestSender.VoteIssue(votingId, Mapper.MapVoteToApiDto(userId, vote, comment));
        // }

        // public void FormWrittenOpinion(Guid meetingId, WrittenOpinion[] writtenOpinions)
        // {
        //     _requestSender.FormWrittenOpinion(meetingId, Mapper.MapWrittenOpinionToApiDto(writtenOpinions));
        // }

        // public void FormProtocol(Guid meetingId, Guid[] issues)
        // {
        //     _requestSender.FormProtocol(meetingId, Mapper.MapIssuesToProtocolApiDto(issues));
        // }

        // public void SignProtocol(Guid meetingId)
        // {
        //     var meeting = _requestSender.GetMeeting(meetingId);

        //     _requestSender.SignProtocol(meetingId, meeting.Protocol.Id, meeting.Protocol.CurrentVersion.Version);
        // }

        // public IEnumerable<string> GetNotifications(int count)
        // {
        //     return _requestSender.GetNotifications(count).Select(x => x.Body).ToList();
        // }

        // private void GenerateSeedsAddUsersToCollegialBodies(Seeds seeds)
        // {
        //     foreach (var collegialBody in seeds.CollegialBodies)
        //     {
        //         foreach (var member in collegialBody.Members)
        //         {
        //             var user = seeds.Users.Single(x => x.Login == member.Login);
        //             var item = _requestSender.FetchUser(user.Login,
        //                 $"{user.Surname} {user.FirstName} {user.Patronymic}");
        //             _requestSender.CreateMember(Mapper.MapMemberToApiDto(item), collegialBody.Id);
        //             _requestSender.ModifyRoleMember(Mapper.MapRoleToApiDto(member.Role),
        //                 collegialBody.Id, item.Id);

        //             if (member.Alternates != null && member.Alternates.Any())
        //             {
        //                 var alternates = new List<AlternateDto>();
        //                 var rank = 1;

        //                 foreach (var alternate in member.Alternates)
        //                 {
        //                     var alterateUser = seeds.Users.Single(x => x.Login == alternate);
        //                     var alterateItem = _requestSender.FetchUser(alterateUser.Login,
        //                         $"{alterateUser.Surname} {alterateUser.FirstName} {alterateUser.Patronymic}");
        //                     alternates.Add(new AlternateDto()
        //                     {
        //                         Id = alterateItem.Id,
        //                         Rank = rank++
        //                     });
        //                 }

        //                 _requestSender.AddAlternates(collegialBody.Id, item.Id, new AlternatesDto { Alternates = alternates });
        //             }
        //         }
        //     }
        // }

        // private void SetCollegialBodyMaterialTemplate(Models.CollegialBodyModel.CollegialBody collegialBody)
        // {
        //     foreach (var materialTemplate in collegialBody.MaterialTemplate)
        //     {
        //         var templatePath = TemplatePath(materialTemplate);

        //         _requestSender
        //             .SetCollegialBodyMaterialTemplate(templatePath, materialTemplate, collegialBody.Id);
        //     }
        // }

        // private string TemplatePath(MaterialTemplateCategory template)
        // {
        //     var materialDirectory = Path.Combine(Path.GetDirectoryName
        //             (AppDomain.CurrentDomain.SetupInformation.ApplicationBase),
        //             "TemplateFiles");

        //     return Path.Combine(materialDirectory, $"{template}.docx");
        // }

        // private void GenerateCustomFields(Seeds seeds)
        // {
        //     var createdCustomFields = _requestSender.GetCustomField();

        //     foreach (var customField in seeds.CustomFields)
        //     {
        //         if (!createdCustomFields.Any(x => x.FormLabel.Ru == customField.Label))
        //         {
        //             _requestSender.CreateCustomField(Mapper.MapCustomFieldToApiDto(customField));
        //         }
        //     }
        // }

        // private void GenerateSeedsUsers(Seeds seeds)
        // {
        //     foreach (var user in seeds.Users)
        //     {
        //         Guid userId;
        //         var item = _requestSender.FetchUser(user.Login,
        //             user.Surname + ' ' + user.FirstName + ' ' + user.Patronymic);
        //         if (item == null)
        //         {
        //             userId = _requestSender
        //                 .CreateUser(Mapper.MapUserToApiDto(user));
        //             var loginIsAdd = _requestSender
        //                 .SetInternalLoginPassword(Mapper.MapLoginToApiDto(user),
        //                                     userId);

                    //TODO: when need search renamed user with static login
                    // if (!loginIsAdd)
                    // {
                    //      item = _requestSender.FetchUser(user.Login);
                    //     //Add Rename User
                    // }
                // }
                // else
                // {
                //     userId = item.Id;
                // }
                // _requestSender
                //     .SetUserInsiderFlags(Mapper
                //         .MapInsiderUserToApiDto(userId,
                //             user.IsBitlInsider, user.IsIssuerInsider));
        //     }
        // }

        // private void GenerateSeedsStructs(Seeds seeds)
        // {
        //     var companyId = CheckCollegialBodies(seeds);
        //     if (companyId == Guid.Empty)
        //         CreateCollegialBodyStructure(seeds);
        //     else
        //         CheckAndEditSeeds(seeds, companyId);
        // }

        // private Guid CheckCollegialBodies(Seeds seeds)
        // {
        //     foreach (var collegialBody in seeds.CollegialBodies)
        //     {
        //         var companyId = _requestSender.CheckCollegialBodyId(collegialBody.Id);
        //         if (!(companyId == Guid.Empty))
        //             return companyId;
        //     }
        //     return Guid.Empty;
        // }

        // private void CreateCollegialBodyStructure(Seeds seeds)
        // {
        //     var holdingId = _requestSender
        //         .CreateHolding(Mapper.MapHoldingToApiDto(seeds.HoldingName));

        //     var companyId = _requestSender
        //         .CreateCompany(Mapper.MapCompanyToApiDto(seeds.CompanyName, holdingId));

        //     CreateCollegialBody(companyId, seeds);
        // }

        // private void CreateCollegialBody(Guid companyId, Seeds seeds)
        // {
        //     foreach (var collegialBody in seeds.CollegialBodies)
        //     {
        //         _requestSender
        //             .CreateCollegialBody(Mapper
        //                 .MapCollegialBodyToApiDto(companyId, collegialBody));

        //         SetCollegialBodySettings(collegialBody);

        //         if (collegialBody.MaterialTemplate != null)
        //             SetCollegialBodyMaterialTemplate(collegialBody);
        //     }
        // }

        // private void CheckAndEditSeeds(Seeds seeds, Guid companyId)
        // {
        //     var holdingId = _requestSender.GetHoldingIdByCompanyId(companyId);

        //     if (!_requestSender.CheckHoldingNameById(holdingId, seeds.HoldingName))
        //         _requestSender.RenameHolding(holdingId, Mapper.MapHoldingToApiDto(seeds.HoldingName));

        //     if (!_requestSender.CheckCompanyNameById(seeds.CompanyName, companyId, holdingId))
        //         _requestSender.RenameCompany(companyId, Mapper.MapCompanyToApiDto(seeds.CompanyName, holdingId));

        //     foreach (var collegialBody in seeds.CollegialBodies)
        //     {
        //         if (!_requestSender.CheckCollegialBodyNameById(collegialBody.Id, collegialBody.Name))
        //         {
        //             _requestSender.RenameCollegialBody(Mapper
        //                 .MapCollegialBodyToApiDto(companyId, collegialBody));
        //         }

        //         SetCollegialBodySettings(collegialBody);

        //         if (collegialBody.MaterialTemplate != null)
        //             SetCollegialBodyMaterialTemplate(collegialBody);
        //     }
        // }

        // private void SetCollegialBodySettings(Models.CollegialBodyModel.CollegialBody collegialBody)
        // {
        //     if (collegialBody.Settings?.InsiderSettings != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Insider);

        //     if (collegialBody.Settings?.VotingSettings != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Voting);

        //     if (collegialBody.Settings?.SignatureSettings != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Signature);

        //     if (collegialBody.Settings?.DecisionFormalization != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.DecisionFormalization);

        //     if (collegialBody.Settings?.MeetingSettings != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Meeting);

        //     if (collegialBody.Settings?.IntegrationSettings != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Integration);

        //     if (collegialBody.CustomField != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.CustomFieldSettings);

        //     if (collegialBody.Settings?.NumerationSettings != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Numeration);

        //     if (collegialBody.Settings?.RegulationsSettings != null)
        //         RequestCollegialBodySettings(collegialBody,
        //             Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Regulations);
        // }

        // private void RequestCollegialBodySettings(Models.CollegialBodyModel.CollegialBody collegialBody,
        //     Models.CollegialBodyModel.Settings.CollegialBodySettingsType settingsType)
        // {
        //     Object settingsDto = null;

        //     switch (settingsType)
        //     {
        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Insider:
        //             settingsDto = Mapper.MapInsiderCollegialBodyToApiDto(collegialBody.Settings.InsiderSettings);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Voting:
        //             settingsDto = Mapper.MapCollegialBodyVotingSettingsToApiDto(collegialBody.Settings.VotingSettings);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Signature:
        //             settingsDto = Mapper.MapCollegialBodySignatureSettingsToApiDto(collegialBody.Settings.SignatureSettings);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.DecisionFormalization:
        //             settingsDto = Mapper.MapCollegialBodyDecisionFormalizationToApiDto(collegialBody.Settings.DecisionFormalization);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Meeting:
        //             settingsDto = Mapper.MapCollegialBodyMeetingSettingsToApiDto(collegialBody.Settings.MeetingSettings);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Integration:
        //             settingsDto = Mapper.MapCollegialBodyIntegrationSettingsToApiDto(collegialBody.Settings.IntegrationSettings);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Numeration:
        //             settingsDto = Mapper.MapCollegialBodyNumerationSettingsToApiDto(collegialBody.Settings.NumerationSettings);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.CustomFieldSettings:
        //             settingsDto = MapCollegialBodyCustomFieldsToApiDto(collegialBody.CustomField);
        //             break;

        //         case Models.CollegialBodyModel.Settings.CollegialBodySettingsType.Regulations:
        //             settingsDto = Mapper.MapCollegialBodyRegulationsToApiDto(collegialBody.Settings.RegulationsSettings);
        //             break;
        //     }

        //     _requestSender.SetCollegialBodySettings(settingsDto, collegialBody.Id, settingsType);
        // }

        // private ApiClient.Dto.CollegialBody.CollegialBodyCustomFieldSettings MapCollegialBodyCustomFieldsToApiDto(IEnumerable<Models.CollegialBodyModel.Settings.CollegialBodyCustomFieldSettings> customFields)
        // {
        //     var existingCustomFields = _requestSender.GetCustomField();
        //     var order = 1;

        //     var settings = customFields.Select(x => new ApiClient.Dto.CollegialBody.CollegialBodyCustomField()
        //     {
        //         CustomFieldSettingId = existingCustomFields.First(y => y.FormLabel.Ru == x.Label).Id.ToString(),
        //         IsRequired = x.IsRequired,
        //         Order = order++
        //     }).ToList();

            // return new ApiClient.Dto.CollegialBody.CollegialBodyCustomFieldSettings()
            // {
            //     Settings = settings
            // };
        //}
    }
}

