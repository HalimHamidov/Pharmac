using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using System.Net;
using System.Web;
using Newtonsoft.Json;
// using Tests.Selenium.TestData.Generator.ApiClient.ResponseDto;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.Company;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.Holding;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.Member;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.User;
// using Tests.Selenium.TestData.Generator.ApiClient.ResponseDto.IssueResponse;
// using Tests.Selenium.TestData.Generator.ApiClient.ResponseDto.MeetingResponse;
// using Tests.Selenium.TestData.Generator.ApiClient.ResponseDto.UserResponse;
using Tests.Selenium.TestData.Models;
// using CollegialBody = Tests.Selenium.TestData.Generator.ApiClient.Dto.CollegialBody.CollegialBody;
// using Company = Tests.Selenium.TestData.Models.Company;
using GuidId = Tests.Selenium.TestData.GuidId;
// using HoldingId = Tests.Selenium.TestData.Generator.ApiClient.ResponseDto.HoldingId;
using System.Linq;
using Tests.Selenium.TestData;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.Issue;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.Decision;
// using Tests.Selenium.TestData.Models.Enum;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.Voting;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.WrittenOpinion;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.CustomField;
// using Tests.Selenium.TestData.Generator.ApiClient.Dto.Meeting;

namespace Tests.Selenium.TestData
{
    public class RequestSender : IRequestSender, IDisposable
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _url;
        private bool _isLogined = false;
        private readonly RestClient _client;
        public delegate void Logger(string text);
        private readonly Logger _logger;
        private bool _disposed = false;

        public RequestSender(string username, string password, string url, Logger logger = null)
        {
            _logger = logger;
            _username = username;
            _password = password;
            _url = url;
            _client = new RestClient(url)
            {
                CookieContainer = new CookieContainer()
            };
        }

        private void Log(string text)
        {
            _logger?.Invoke(text);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                Logout();
            }
            _disposed = true;
        }

        private void Login(string username, string password, bool rememberMe)
        {
            const bool needSendLogin = true;
            var loginInformation = new Person
            {
                Username = username,
                Password = password,
                RememberMe = rememberMe
            };
            var response = SendRequest("api/account/login", Method.POST, loginInformation, needSendLogin);
            Log($"Login. OK? - ");
            _isLogined = ColorResponse(response.IsSuccessful);
            if (!_isLogined)
                ProgramTermination(response.ErrorMessage);
        }

        private void ProgramTermination(string errorMessage)
        {
            Console.Read();
            Logout();
            throw new Exception(errorMessage);
            //Environment.Exit(1);
        }

        private void Logout()
        {
            var request = CreateRequest("api/account/logout", Method.POST);
            var response = _client.Execute(request);
            Log($"Logouted. OK? - ");
            ColorResponse(response.IsSuccessful);
            _isLogined = false;
        }

        private bool ColorResponse(bool response)
        {
            Console.ForegroundColor = response ? ConsoleColor.Green : ConsoleColor.Red;
            Log($"{response}");
            Console.ResetColor();
            return response;
        }

        private bool ColorResponseCode(string statusCode)
        {
            var isTrue = statusCode.Contains("OK");
            ColorResponse(isTrue);
            return isTrue;
        }

        private void AddJsonHeader(RestRequest request)
        {
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
        }

        private RestRequest CreateRequest(string uri, Method method = Method.GET, object post = null)
        {
            var request = new RestRequest(uri, method);
            if (post != null)
            {
                AddJsonHeader(request);
                var serializedPost = JsonConvert.SerializeObject(post);
                request.AddJsonBody(serializedPost);
                Log($"send json {serializedPost}");
                Log($"Request on |{request.Resource}| - Send.");
            }
            return request;
        }

        private IRestResponse<T> SendRequest<T>(string uri, Method method = Method.GET, object post = null)
        {
            var request = CreateRequest(uri, method, post);
            if (!_isLogined)
                Login(_username, _password, false);
            var response = _client.Execute<T>(request);
            return response;
        }

        private IRestResponse SendRequest(string uri, Method method = Method.GET, object post = null, bool needSendLogin = false)
        {
            var request = CreateRequest(uri, method, post);
            if (!_isLogined && !needSendLogin)
                Login(_username, _password, false);
            var response = _client.Execute(request);
            return response;
        }

        private IRestResponse SendRequestFile(string uri, string path, Method method = Method.GET, string fileName=null, object post = null)
        {
            if (!_isLogined)
                Login(_username, _password, false);
            var request = new RestRequest(uri, method);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "multipart/form-data");

            if (fileName != null)
                request.AddFile("attachment", File.ReadAllBytes(path), fileName);
            else
                request.AddFile("attachment", path);

            var response = _client.Execute(request);
            return response;
        }

        public void LogoutForTestLogin()
        {
            Logout();
        }

        // public Guid CreateMaterial(Guid collegialBodyId, string path, bool encrypt = false, string fileName=null)
        // {
        //     var material = SendRequestFile
        //         ($"api/v2.0/issues/drafts?collegialBodyId={collegialBodyId}&path=/&encrypt={encrypt}", path, Method.POST, fileName);
        //     var holdingId = JsonConvert.DeserializeObject<ResponseDto.MaterialResponse.Material[]>(material.Content);
        //     return holdingId[0].Id;
        // }

        // public Guid CreateInstructionMaterial(Guid collegialBodyId, string path, bool encrypt = false)
        // {
        //     var material = SendRequestFile
        //            ($"api/v2.0/instructions/drafts?collegialBodyId={collegialBodyId}&path=/&encrypt={encrypt}", path, Method.POST);
        //     var holdingId = JsonConvert.DeserializeObject<ResponseDto.MaterialResponse.Material[]>(material.Content);
        //     return holdingId[0].Id;
        // }

        // public Guid CreateIssue(Dto.Issue.Issue issue)
        // {
        //     var issueId = SendRequest<GuidId>
        //         ("api/v2.0/issues", Method.POST, issue);
        //     Log($"IssueId Create response content {issueId.Content} - Add. OK? - ");
        //     ColorResponse(issueId.IsSuccessful);
        //     var issueBlockId = SendRequest<IssueId>
        //         ($"api/v1.0/issueversions/{issueId.Data.Id}");
        //     return issueBlockId.Data.Issue.Id;
        // }

        // public bool ChangeStateIssue(Guid issueId, IssueStatus state, Dto.Issue.IssueWorkflow issueWorkflow)
        // {
        //     var response = SendRequest<Dto.Issue.IssueWorkflow>
        //         ($"api/v2.0/issues/{issueId}/status/{state}", Method.POST, issueWorkflow);
        //     Log($"Issue change status response content {response.Content} - Changed?. OK? - ");
        //     return ColorResponse(response.IsSuccessful);
        // }

        // public bool SendIssueOnReview(Guid issueId)
        // {
        //     var response = SendRequest($"api/v1.0/issues/{issueId}/review/status/ToActive", Method.POST);

        //     return ColorResponse(response.IsSuccessful);
        // }

        // public bool FinishIssueReview(Guid issueId)
        // {
        //     var response = SendRequest($"api/v1.0/issues/{issueId}/review/status/ToFinished", Method.POST);

        //     return ColorResponse(response.IsSuccessful);
        // }

        // public bool UnlockIssue(Guid issueId)
        // {
        //     var response = SendRequest
        //         ($"api/v2.0/issues/{issueId}/lock", Method.DELETE);
        //     return ColorResponse(response.IsSuccessful);
        // }
        // public void ModifyIssuesFromMeeting(Guid meetingId, IssuesList issues)
        // {
        //     var response = SendRequest($"api/v2.0/meetings/{meetingId}/agenda", Method.PUT, issues);
        //     Log($"Issue remove response content{response.Content} - Deleted. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        // }

        // public bool DeleteIssue(Guid issueId)
        // {
        //     var response = SendRequest($"/api/v2.0/issues/{issueId}", Method.DELETE);
        //     return ColorResponse(response.IsSuccessful);
        // }

        // public Guid CreateMeeting(Dto.Meeting.Meeting meeting)
        // {
        //     var response = SendRequest<GuidId>("api/v2.0/meetings", Method.POST, meeting);
        //     Log($"Meeting create {response.Data.Id} - Add. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        //     return response.Data.Id;
        // }

        // public Guid CreateAbsentiaMeeting(Dto.Meeting.MeetingAbsentia meeting)
        // {
        //     var response = SendRequest<GuidId>("api/v2.0/meetings", Method.POST, meeting);
        //     Log($"Meeting create {response.Data.Id} - Add. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        //     return response.Data.Id;
        // }

        // public void DeleteMeeting(Guid meetingId)
        // {
        //     var response = SendRequest($"api/v2.0/meetings/{meetingId}", Method.DELETE);
        //     Log($"Meeting delete response content {response.Content} - Deleted. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        // }

        // public bool SelectActionMeeting(Guid meetingId, MeetingActions action)
        // {
        //     var response = SendRequest
        //         ($"api/v2.0/meetings/{meetingId}/workflow/{action}", Method.POST);
        //     Log($"Meeting select action response content {response.Content} - selected. OK? - ");
        //     return ColorResponse(response.IsSuccessful);
        // }

        // public Guid GetIssueVersionId(Guid issueId)
        // {
        //     var response = SendRequest($"api/v2.0/issues/{issueId}");
        //     var issueVersionId = JsonConvert.DeserializeObject<IssueResponse>(response.Content);
        //     return issueVersionId.IssueVersion.Id;
        // }

        // public IssueStatusResponse GetIssueStatus(Guid issueVersionId)
        // {
        //     var response = SendRequest($"/api/v1.0/issueversions/{issueVersionId}");
        //     var issueStatus = JsonConvert.DeserializeObject<StatusResponse>(response.Content);
        //     return issueStatus.Status;
        // }

        // public bool RollBackMeeting(Guid meetingId)
        // {
        //     var response = SendRequest<MeetingStatusResponse>
        //         ($"api/v2.0/meetings/{meetingId}/moveBack", Method.POST);
        //     Log($"Meeting Move back response content {response.Content} - moved back. OK? - ");
        //     return response.IsSuccessful;
        // }

        // public MeetingDto GetMeeting(Guid meetingId)
        // {
        //     var response = SendRequest<MeetingDto>
        //            ($"api/v2.0/meetings/{meetingId}", Method.GET);
        //     return response.Data;
        // }

        // public Guid CreateHolding(HoldingName holding)
        // {
        //     var response = SendRequest<HoldingName>
        //     ($"/api/v2.0/holdings", Method.POST, holding);
        //     Log($"Holding Create Content {response.Content}   - Created. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        //     var holdingId = JsonConvert.DeserializeObject<GuidId>(response.Content);
        //     return holdingId.Id;
        // }

        // public bool RenameHolding(Guid holdingId, HoldingName holding)
        // {
        //     var response = SendRequest<HoldingName>
        //     ($"/api/v2.0/holdings/{holdingId}", Method.PUT, holding);
        //     return response.IsSuccessful;
        // }

        // public Guid CheckHoldingName(string name)
        // {
        //     var response = SendRequest($"/api/v2.0/orgUnits");
        //     var holdingsOrgUnits = JsonConvert.DeserializeObject<IList<Holdings>>(response.Content);
        //     foreach (var holdingData in holdingsOrgUnits)
        //     {
        //         if (name == holdingData.Name)
        //         {
        //             return (holdingData.Id);
        //         }
        //     }
        //     return Guid.Empty;
        // }

        // public bool CheckHoldingId(Guid id)
        // {
        //     var response = SendRequest($"/api/v2.0/orgUnits");
        //     var holdingsOrgUnits = JsonConvert.DeserializeObject<IList<Holdings>>(response.Content);

        //     foreach (var holdingData in holdingsOrgUnits)
        //     {
        //         if (id == holdingData.Id)
        //         {
        //             return true;
        //         }
        //     }
        //     return false;
        // }

        // public bool CheckHoldingNameById(Guid holdingId, string name)
        // {
        //     var response = SendRequest($"/api/v2.0/orgUnits");
        //     var holdingsOrgUnits = JsonConvert.DeserializeObject<IList<Holdings>>(response.Content);
        //     foreach (var holdingData in holdingsOrgUnits)
        //     {
        //         if (holdingId == holdingData.Id)
        //         {
        //             return (holdingData.Name == name);
        //         }
        //     }
        //     return false;
        // }

        // public Guid GetHoldingIdByCompanyId(Guid companyId)
        // {
        //     var response = SendRequest($"/api/v2.0/CollegialBodies/{companyId}");
        //     var holdingsId = JsonConvert.DeserializeObject<IList<HoldingId>>(response.Content);
        //     foreach (var holdingId in holdingsId)
        //     {
        //         return holdingId.Holding.Id;
        //     }
        //     return Guid.Empty;
        // }

        // public Guid CheckCompanyName(string name, Guid holdingId)
        // {
        //     var response = SendRequest($"/api/v2.0/Companies/{holdingId}");
        //     var companies = JsonConvert.DeserializeObject<IList<CompanyUnits>>(response.Content);
        //     foreach (var company in companies)
        //     {
        //         if (name == company.Name)
        //             return company.Id;
        //     }
        //     return Guid.Empty;
        // }

        // public bool CheckCompanyId(Guid companyId, Guid holdingId)
        // {
        //     var response = SendRequest($"/api/v2.0/Companies/{holdingId}");
        //     var companies = JsonConvert.DeserializeObject<IList<CompanyUnits>>(response.Content);
        //     foreach (var company in companies)
        //     {
        //         if (companyId == company.Id)
        //             return true;
        //     }
        //     return false;
        // }

        // public bool CheckCompanyNameById(string name, Guid companyId, Guid holdingId)
        // {
        //     var response = SendRequest($"/api/v2.0/Companies/{holdingId}");
        //     var companies = JsonConvert.DeserializeObject<IList<CompanyUnits>>(response.Content);
        //     foreach (var company in companies)
        //     {
        //         if (companyId == company.Id)
        //             return name == company.Name;
        //     }
        //     return false;
        // }

        // public Guid CreateCompany(Dto.Company.Company company)
        // {
        //     var response = SendRequest<Company>($"/api/v2.0/companies", Method.POST, company);
        //     Log($"Company Content {response.Content}   - Created. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        //     var companyId = JsonConvert.DeserializeObject<GuidId>(response.Content);
        //     return companyId.Id;
        // }

        // public void RenameCompany(Guid companyId, Dto.Company.Company company)
        // {
        //     var response = SendRequest<Company>
        //         ($"/api/v2.0/companies/{companyId}", Method.PUT, company);
        //     Log($"Company Content {response.Content}   - Created. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        // }

        // public Guid CheckCollegialBodyId(Guid collegialBodyId)
        // {
        //     var companyId = Guid.Empty;
        //     var response = SendRequest($"/api/v2.0/orgUnits/{collegialBodyId}");
        //     var collegialBodyResponse =
        //         JsonConvert.DeserializeObject<ResponseDto.CollegialBody>(response.Content);
        //     try
        //     {
        //         companyId = collegialBodyResponse.Parent.Id;
        //     }
        //     catch (Exception)
        //     { }
        //     return companyId;
        // }

        // public bool CheckCollegialBodyNameById(Guid collegialBodyId, string collegialBodyName)
        // {
        //     var response = SendRequest($"/api/v2.0/orgUnits/{collegialBodyId}");
        //     var collegialBody = JsonConvert.DeserializeObject<ResponseDto.CollegialBody>
        //         (response.Content);
        //     if (collegialBodyName == collegialBody.Name.Ru)
        //         return true;
        //     return false;
        // }

        // public Guid CreateCollegialBody(CollegialBody collegialBody)
        // {
        //     var response = SendRequest<CollegialBody>($"/api/v2.0/collegialBodies",
        //                                                 Method.POST, collegialBody);
        //     Log($"collegial Content {response.Content}   - Created. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        //     var companyId = JsonConvert.DeserializeObject<GuidId>(response.Content);
        //     return companyId.Id;
        // }

        // public Guid RenameCollegialBody(CollegialBody collegialBody)
        // {
        //     var response = SendRequest<CollegialBody>
        //     ($"/api/v2.0/collegialBodies/{collegialBody.Id}",
        //         Method.PUT, collegialBody);
        //     Log($"collegial Content {response.Content}   - Created. OK? - ");
        //     ColorResponse(response.IsSuccessful);
        //     var companyId = JsonConvert.DeserializeObject<GuidId>(response.Content);
        //     return companyId.Id;
        // }

        // public bool SetCollegialBodySettings(Object settingsDto, Guid collegialBodyId, Models.CollegialBodyModel.Settings.CollegialBodySettingsType settingsType)
        // {
        //     var response = SendRequest($"/api/v2.0/collegialBodies/{collegialBodyId}/settings/{settingsType.ToString()}",
        //               Method.PUT, settingsDto);
        //     return ColorResponseCode(response.StatusCode.ToString());
        // }

        // public Guid SetCollegialBodyMaterialTemplate(string path, MaterialTemplateCategory materialTemplate, Guid collegialBodyId)
        // {
        //     var response = SendRequestFile(
        //         $"/api/v2.0/collegialBodies/{collegialBodyId}/settings/{materialTemplate}", path, Method.POST);

        //     var responseId = JsonConvert.DeserializeObject<ResponseDto.MaterialResponse.Material>(response.Content);
        //     return responseId.Id;
        // }

        // public UserItem FetchUser(string login, string query = "")
        // {
        //     var page = 1;
        //     var size = 1000;
        //     FetchDto<UserItem> fetchUsers;
        //     var searchQuery = query == "" ? "" : "query=";
        //     var encodeQuery = HttpUtility.UrlEncode(query);

        //     do
        //     {
        //         var response = SendRequest
        //             ($"/api/v2.0/users/fetch?{searchQuery}{encodeQuery}&page={page}&size={size}");
        //         fetchUsers = JsonConvert.DeserializeObject<FetchDto<UserItem>>(response.Content);
        //         foreach (var item in fetchUsers.Items)
        //         {
        //             if (item.UserName == login.ToLower())
        //             {
        //                 return item;
        //             }
        //         }
        //         page++;
        //     } while (fetchUsers.CanFetchMore);
        //     return null;
        // }

        // public bool CreateMember(Members member, Guid collegialBodyId)
        // {
        //     var response = SendRequest<Members>
        //         ($"/api/v2.0/collegialBodies/{collegialBodyId}/members", Method.POST, member);
        //     Log($"Member Create Content {response.Content}   - Created. OK? - ");
        //     return ColorResponseCode(response.StatusCode.ToString());
        // }

        // public bool ModifyRoleMember(Dto.Member.Role role, Guid collegialBodyId,
        //                                                         Guid memberId)
        // {
        //     var response = SendRequest<Dto.Member.Role>
        //                                  ($"/api/v2.0/collegialBodies/{collegialBodyId}/members/{memberId}",
        //                                      Method.PUT, role);
        //     Log($"member Content {response.Content}   - Created. OK? - ");
        //     return ColorResponseCode(response.StatusCode.ToString());
        // }

        public Guid CreateUser(Dto.Person user)
        {
            var response = SendRequest<Dto.Person>
                ("api/v2.0/users/person", Method.POST, user);
            Log($"User Create Content {response.Content}   - Created. OK? - ");
            ColorResponse(response.IsSuccessful);
            var userId = JsonConvert.DeserializeObject<GuidId>(response.Content);
            return userId.Id;
        }

        // public bool SetUserInsiderFlags(Insider insider)
        // {
        //     var response = SendRequest<Insider>
        //         ("/api/v1.0/employees/insider", Method.PUT, insider);
        //     return ColorResponseCode(response.StatusCode.ToString());
        // }
        // private static bool CheckValidLoginPassword(LocalLogin user)
        // {
        //     return user.Login.Length > 0 && user.Password.Length > 5;
        // }

        // public bool SetInternalLoginPassword(LocalLogin user, Guid userId)
        // {
        //     if (!CheckValidLoginPassword(user))
        //     {
        //         throw new ArgumentOutOfRangeException
        //             ("Login will be > 0 characters," +
        //                     "Password will be > 5 characters");
        //     }

        //     var response = SendRequest<LocalLogin>
        //         ($"/api/v2.0/users/person/{userId}/locallogin", Method.POST, user);
        //     Log($"User Login Content {response.Content}   - Created. OK? - ");
        //     return ColorResponse(response.IsSuccessful);
        // }

        // public IEnumerable<ResponseDto.CollegialBodyDto.CollegialBodyMember> GetCollegialBodyMembers(Guid collegialBodyId)
        // {
        //     var response = SendRequest($"/api/v2.0/collegialBodies/{collegialBodyId}/members");

        //     var collegialBodyMember = JsonConvert.DeserializeObject
        //         <IEnumerable<ResponseDto.CollegialBodyDto.CollegialBodyMember>>(response.Content);

        //     return collegialBodyMember;
        // }

        // public IEnumerable<ResponseDto.CollegialBodyDto.CollegialBodyMember> GetReviewers(Guid collegialBodyId, User user)
        // {
        //     var response = SendRequest($"/api/v1.0/reviewDepartments/fetch?query={user.Surname}&collegialBodyId={collegialBodyId}&page=1&size=100");

        //     var collegialBodyMember = JsonConvert.DeserializeObject
        //         <ResponseDto.UserResponse.FetchReviewer>(response.Content);

        //     return collegialBodyMember.Items.Select(x=>x.Reviewer).ToList();
        // }

        // public IEnumerable<EmployeeItem> GetEmployees(string query)
        // {
        //     var customFields = new List<EmployeeItem>();
        //     FetchDto<EmployeeItem> employees;
        //     var size = 100;
        //     var page = 1;
            
        //     do
        //     {
        //         var response = SendRequest($"/api/v1.0/employees/fetch?query={query}&page={page++}&size={size}");
        //         employees = JsonConvert.DeserializeObject<FetchDto<EmployeeItem>>(response.Content);

        //         customFields = customFields.Concat(employees.Items).ToList();
        //     } while (page < 20 && employees.CanFetchMore);

        //     return customFields;
        // }

        // public IEnumerable<Guid> GetMeetingParticipants(Guid meetingId)
        // {
        //     var response = SendRequest($"/api/v2.0/meetings/{meetingId}/participants");

        //     var participantIds = JsonConvert.DeserializeObject
        //         <IEnumerable<ResponseDto.CollegialBodyDto.CollegialBodyMember>>(response.Content);

        //     return participantIds.Select(x => x.Id).ToList();
        // }

        // public bool MarkPresenceParticipant(Guid issueVersionId, Attendees attendees)
        // {
        //     var response = SendRequest<Attendees>($"/api/v2.0/issues/{issueVersionId}/attendees", Method.POST, attendees);

        //     return ColorResponse(response.IsSuccessful);
        // }

        // public bool AcceptIssue(Guid issueId, bool withForce)
        // {
        //     var response = SendRequest<IssueResponse>($"/api/v2.0/issues/{issueId}/Accept", Method.POST);

        //     return ColorResponse(response.IsSuccessful);
        // }

        // public Guid CreateInstruction(Dto.Instruction.Instruction instruction)
        // {
        //     var response = SendRequest<GuidId>
        //         ("api/v2.0/instructions", Method.POST, instruction);
        //     Log($"IssueId Create response content {response.Content} - Add. OK? - ");
        //     ColorResponse(response.IsSuccessful);

        //     var instructionId = JsonConvert.DeserializeObject<GuidId>(response.Content);
        //     return instructionId.Id;
        // }

        // public IEnumerable<Guid> GetDecisionIdsByMeeting(Guid meetingId)
        // {
        //     var response = SendRequest($"/api/v1.0/decisions?meetingId={meetingId}&page=1&size=1000", Method.GET);

        //     var participantIds = JsonConvert.DeserializeObject
        //         <DecisionId>(response.Content);

        //     return participantIds.Items.Select(x => x.Id).ToList();
        // }

        // public void SendOnApproveDecision(Guid decisionId, ApproveDecisionDTO approveDecisionDTO)
        // {
        //     var response = SendRequest($"/api/v1.0/decisions/{decisionId}", Method.PUT, approveDecisionDTO);
         
        //     ColorResponseCode(response.StatusCode.ToString());
        // }

        // public Guid GetVotingIdByIssueVersion(Guid issueVersionId)
        // {
        //     var response = SendRequest($"/api/v3.0/votings?issueVersionId={issueVersionId}&page=1&size=1000", Method.GET);

        //     var participantIds = JsonConvert.DeserializeObject
        //         <IEnumerable<GuidId>>(response.Content);

        //     return participantIds.Select(x => x.Id).ToList().First();
        // }

        // public void VoteIssue(Guid votingId, VoteDTO voteDTO)
        // {
        //     var response = SendRequest($"/api/v3.0/votings/{votingId}/votes", Method.POST, voteDTO);
                
        //     ColorResponseCode(response.StatusCode.ToString());
        // }

        // public void FormWrittenOpinion(Guid meetingId, WrittenOpinionDto opinionDto)
        // {
        //     var response = SendRequest($"/api/v2.0/meetings/{meetingId}/writtenOpinion", Method.POST, opinionDto);

        //     ColorResponseCode(response.StatusCode.ToString());
        // }

        // public void FormProtocol(Guid meetingId, Dto.Meeting.MeetingProtocolDto opinionDto)
        // {
        //     var response = SendRequest($"/api/v2.0/meetings/{meetingId}/protocol", Method.POST, opinionDto);

        //     ColorResponseCode(response.StatusCode.ToString());
        // }

        // public IEnumerable<ResponseDto.CustomField.CollegialBodyCustomFieldItem> GetCollegialBodyCustomField(Guid collegialBodyId)
        // {
        //     var customFields = new List<ResponseDto.CustomField.CollegialBodyCustomFieldItem>();
        //     FetchDto<ResponseDto.CustomField.CollegialBodyCustomFieldItem> customFieldSettings;
        //     var size = 100;
        //     var page = 1;

        //     do
        //     {
        //         var response = SendRequest($"/api/v2.0/collegialBodies/{collegialBodyId}/settings/customFieldSettings?page={page++}&size={size}");
        //         customFieldSettings = JsonConvert.DeserializeObject<FetchDto<ResponseDto.CustomField.CollegialBodyCustomFieldItem>>(response.Content);

        //         customFields = customFields.Concat(customFieldSettings.Items).ToList();
        //     } while (page < 20 && customFieldSettings.CanFetchMore);

        //     return customFields;
        // }

        // public IEnumerable<ResponseDto.CustomField.CustomFieldSetting> GetCustomField()
        // {
        //     var customFields = new List<ResponseDto.CustomField.CustomFieldSetting>();
        //     FetchDto<ResponseDto.CustomField.CustomFieldSetting> customFieldSettings;
        //     var size = 100;
        //     var page = 1;

        //     do
        //     {
        //         var response = SendRequest($"/api/v1.0/customFieldSettings?page={page++}&size={size}");
        //         customFieldSettings = JsonConvert.DeserializeObject<FetchDto<ResponseDto.CustomField.CustomFieldSetting>>(response.Content);

        //         customFields = customFields.Concat(customFieldSettings.Items).ToList();
        //     } while (page < 20 && customFieldSettings.CanFetchMore);

        //     return customFields;
        // }

        // public bool ApproveIssueInReview(Guid issueVersionId, Guid userId, RootCoordinationDto approveDecisionDTO)
        // {
        //     var steps = SendRequest(
        //         $"/api/v1.0/issueversions/{issueVersionId}/review/steps");
        //     var customFieldSettings = JsonConvert.DeserializeObject<IEnumerable<ResponseDto.StepResponse.StepItem>>(steps.Content);

        //     var issueRootCoordinationId = customFieldSettings.First(obj => obj.IsActive).Coordinations.First(x => x.Reviewer.Id == userId).RootCoordinationId;
        //     var response = SendRequest(
        //         $"/api/v1.0/issuerootcoordinations/{issueRootCoordinationId}/decision", Method.POST, approveDecisionDTO);
        //     return ColorResponse(response.IsSuccessful);
        // }

        // public void SignProtocol(Guid meetingId, Guid protocolId, int protocolVersion)
        // {
        //     var response = SendRequest(
        //         $"/api/v1.0/meeting/{meetingId}/protocol/{protocolId}/version/{protocolVersion}/signing", Method.PUT);
        // }

        // public bool CreateCustomField(CustomFieldDto customFieldDto)
        // {
        //     var response = SendRequest("/api/v1.0/customFieldSettings", Method.POST, customFieldDto);

        //     return ColorResponse(response.IsSuccessful);
        // }

        // public IEnumerable<ResponseDto.Notification.NotificationItem> GetNotifications(int count)
        // {
        //     var totalNotifications = new List<ResponseDto.Notification.NotificationItem>();
        //     FetchDto<ResponseDto.Notification.NotificationItem> receivedNotifications;
        //     var size = 100;
        //     var page = 1;

        //     do
        //     {
        //         var response = SendRequest($"/api/v3.0/notifications?page={page++}&size={size}");
        //         receivedNotifications = JsonConvert.DeserializeObject<FetchDto<ResponseDto.Notification.NotificationItem>>(response.Content);

        //         totalNotifications = totalNotifications.Concat(receivedNotifications.Items).ToList();
        //     } while (page < 20 && receivedNotifications.CanFetchMore && count < totalNotifications.Count);

        //     return totalNotifications;
        // }

        // public bool AddAlternates(Guid collegialBodyId, Guid MemberId, AlternatesDto alternate)
        // {
        //     var response = SendRequest($"/api/v2.0/collegialBodies/{collegialBodyId}/members/{MemberId}/alternates", Method.PUT, alternate);

        //     return response.IsSuccessful;
        // }

        // public IEnumerable<IssueReviewStep> GetReviewSteps(Guid issueVersionId)
        // {
        //     var response = SendRequest($"/api/v1.0/issueversions/{issueVersionId}/review/steps", Method.GET);

        //     var issueReviewSteps = JsonConvert.DeserializeObject
        //         <IEnumerable<IssueReviewStep>>(response.Content);

        //     return issueReviewSteps;
        // }

        // public bool SendReviewDecision(Guid coordinationId, IssueReviewDecisionDto issueReviewDecisionDto)
        // {
        //     var response = SendRequest($"/api/v1.0/issuerootcoordinations/{coordinationId}/decision", Method.POST, issueReviewDecisionDto);

        //     return response.IsSuccessful;
        // }

    }
}

