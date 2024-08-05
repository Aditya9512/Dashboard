using ChilliDBApi.MembershipManager;
using DashServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ServiceModel;

namespace DashServer.Controllers


{

	[ApiController]
	[Route("[controller]")]
	public class ClientsController
	{
		private readonly DashboardOptions _dashOptions;

		public ClientsController(IOptions<DashboardOptions> unitOptions)
		{
			_dashOptions = unitOptions.Value;

		}





		[HttpGet(Name = "GetClientsController")]
		public async Task<ClientsResponse> GetAsync(DateTime DT)

		{


			EndpointAddress endpointAddress = new(_dashOptions.apiBaseUrl + "MODULES/CRM_Integration_Services/DataExchange/MembershipManager.asmx");
			TimeSpan apiTimeOutSpan = new(_dashOptions.apiTimeOutSeconds * new TimeSpan(0, 0, 1).Ticks);
			BasicHttpBinding channelBinding = new(endpointAddress.Uri.Scheme.ToLower() == "http"
		? BasicHttpSecurityMode.None
		: BasicHttpSecurityMode.Transport)
			{
				MaxReceivedMessageSize = _dashOptions.apiMaxMessageSize,
				OpenTimeout = apiTimeOutSpan,
				CloseTimeout = apiTimeOutSpan,
				ReceiveTimeout = apiTimeOutSpan,
				SendTimeout = apiTimeOutSpan,
			};
			AuthenticationHeader authenticationHeader = new()
			{
				SystemIdentifier = _dashOptions.systemId,
				Username = _dashOptions.systemUser,
				Password = _dashOptions.systemPassword,
			};
			MembershipManagerSoapClient apiClient = await Task.Run(() =>
			new MembershipManagerSoapClient(channelBinding, endpointAddress));
			/*MembershipMemberInfo[] members = (await apiClient.GetMemberDetailsByPackageAsync
			   (authenticationHeader, _dashOptions.membershipPackageId)).GetMemberDetailsByPackageResult;*/

			IEnumerable<MembershipMemberInfo> members = Array.Empty<MembershipMemberInfo>();


			foreach (int i in _dashOptions.membershipPackageIds)
			{
				members = members.Union((await apiClient.GetMemberDetailsByPackageAsync
			(authenticationHeader, i)).GetMemberDetailsByPackageResult);
			}
			ClientsResponse response = new ClientsResponse();


			response.NewSignups = members.Where(member => member.Status.Description == "Member" && member.CreatedDate > DT).
				Select(k => new Client
				{
					Member = k.Organisation.Name,
					Package = k.MembershipPackage.Description
				});
			DateTime date1 = DateTime.Today.AddDays(-365);
			DateTime date2 = DateTime.Today.AddDays(-30);
			DateTime date3 = DateTime.Today.AddDays(-7);
			response.Total = members.Count(member => member.Status.Description == "Member");
			response.Year = members.Count(member => member.Status.Description == "Member" && member.CreatedDate > date1);
			response.Month = members.Count(member => member.Status.Description == "Member" && member.CreatedDate > date2);
			response.Week = members.Count(member => member.Status.Description == "Member" && member.CreatedDate > date3);
			/*response.premium = members.Count(member => member.MembershipPackage.Description == "Premium Membership");
			response.platinum = members.Count(member => member.MembershipPackage.Description == "Platinum Membership");*/
			var grouped = members.GroupBy(member => new DateTime(member.CreatedDate.Year, member.CreatedDate.Month,  1))
				.OrderBy(group => group.Key);
			var memberCount = members.GroupBy(member => member.MembershipPackage.Identifier).Select(package => new Package
			{
				Id = package.Key,
				Count = package.Count(),
				Name = package.First().MembershipPackage.Description,
			});
			response.Packages= memberCount;
			//var x = grouped.Select(group => group.Key); // gets list of the months
			//var y = grouped.Select(group => group.Select(member => member)); // gets list of list of members
			//var firstGroup = grouped.First();
			//var month = firstGroup.Key;
			//var firstMember = firstGroup.First();
			foreach (var member in memberCount)
			{
				Console.WriteLine(member.Id);
				//foreach (var member in group) { Console.WriteLine("member"+member.Key); }
				Console.WriteLine("  Count = " + member.Count.ToString());
			}
			foreach (var group in grouped)
			{ Console.WriteLine( group.Key.ToString("MMMM dd"));
				//foreach (var member in group) { Console.WriteLine("member"+member.Key); }
				Console.WriteLine("  Count = " + group.Count().ToString());
			}
			Console.WriteLine("Today = {0}", DateTime.Today);
			foreach (var group in grouped)
			{
				var x = group.Key.ToString("MMMM dd");
				var y = group.Count().ToString();
			}
			//DateTime.Now
			//DateTime.Today
			//DateTime.Today.AddDays(-7)
			List<Graph> graphs = new();

			
			{
				Graph graph = new Graph
				{
					Title = string.Format("Member Sign-ups "),
					XAxis = new(),
					DataSets = new(),
				};
				foreach (var group in grouped)
				{
					DateTime x = group.Key;
					graph.XAxis.Add(x);
				}

				for (int k = 1; k <= 1; ++k)
				{
					GraphDataSet dataSet = new()
					{
						Label = string.Format("GraphDataSet {0} ", k),
						YAxis = new(),
						//Colour = new byte[]{ 255, 99, 132 },
					};
					if (k == 1)
					{
						dataSet.borderColor = new int[] { 75, 192, 192 };
						dataSet.backgroundColor = new double[] { 75, 192, 192, 0.5 };
					}
					else
					{
						dataSet.borderColor = new int[] { 53, 162, 235 };
						dataSet.backgroundColor = new double[] { 53, 162, 235, 0.5 };
					}

					foreach (var group in grouped)
					{
						var y = group.Count();
						dataSet.YAxis.Add(y);
					}

					//...
					graph.DataSets.Add(dataSet);
				}

				graphs.Add(graph);
			}
			response.Graphs = graphs;
			return response;


			//return (ClientsResponse)members.Where(member => member.Status.Description == "Member" && member.CreatedDate > DT).
			//	Select(k => new Client {
			//		Member = k.Organisation.Name,
			//	 Package = k.MembershipPackage.Description });

		}
		private static IEnumerable<MembershipMemberInfo> GetMembers(IEnumerable<MembershipMemberInfo> members)
		{
			return members;
		}
		/*NewSignups(ClientsResponse)members.Where(member => member.Status.Description == "Member" && member.CreatedDate > DT).
				Select(k => new Client {
			Member = k.Organisation.Name,
				 Package = k.MembershipPackage.Description });*/
		[HttpGet("test")]
		public async Task<MembershipMemberInfo> GetTestAsync()
		{
			EndpointAddress endpointAddress = new(_dashOptions.apiBaseUrl + "MODULES/CRM_Integration_Services/DataExchange/MembershipManager.asmx");
			TimeSpan apiTimeOutSpan = new(_dashOptions.apiTimeOutSeconds * new TimeSpan(0, 0, 1).Ticks);
			BasicHttpBinding channelBinding = new(endpointAddress.Uri.Scheme.ToLower() == "http"
		? BasicHttpSecurityMode.None
		: BasicHttpSecurityMode.Transport)
			{
				MaxReceivedMessageSize = _dashOptions.apiMaxMessageSize,
				OpenTimeout = apiTimeOutSpan,
				CloseTimeout = apiTimeOutSpan,
				ReceiveTimeout = apiTimeOutSpan,
				SendTimeout = apiTimeOutSpan,
			};
			AuthenticationHeader authenticationHeader = new()
			{
				SystemIdentifier = _dashOptions.systemId,
				Username = _dashOptions.systemUser,
				Password = _dashOptions.systemPassword,
			};
			MembershipManagerSoapClient apiClient = await Task.Run(() =>
					new MembershipManagerSoapClient(channelBinding, endpointAddress));
			IEnumerable<MembershipMemberInfo> members = Array.Empty<MembershipMemberInfo>();

			foreach (int i in _dashOptions.membershipPackageIds)
			{
				members = members.Union((await apiClient.GetMemberDetailsByOrganisationAsync
			(authenticationHeader, i)).GetMemberDetailsByOrganisationResult);
			}

			return members.First();
		}


		/*[HttpGet(Name = "Count")]


		public async Task<int> Getsync(DateTime DT)
		{

			EndpointAddress endpointAddress = new(_dashOptions.apiBaseUrl + "MODULES/CRM_Integration_Services/DataExchange/MembershipManager.asmx");
			TimeSpan apiTimeOutSpan = new(_dashOptions.apiTimeOutSeconds * new TimeSpan(0, 0, 1).Ticks);
			BasicHttpBinding channelBinding = new(endpointAddress.Uri.Scheme.ToLower() == "http"
		? BasicHttpSecurityMode.None
		: BasicHttpSecurityMode.Transport)
			{
				MaxReceivedMessageSize = _dashOptions.apiMaxMessageSize,
				OpenTimeout = apiTimeOutSpan,
				CloseTimeout = apiTimeOutSpan,
				ReceiveTimeout = apiTimeOutSpan,
				SendTimeout = apiTimeOutSpan,
			};
			AuthenticationHeader authenticationHeader = new()
			{
				SystemIdentifier = _dashOptions.systemId,
				Username = _dashOptions.systemUser,
				Password = _dashOptions.systemPassword,
			};
			MembershipManagerSoapClient apiClient = await Task.Run(() =>
			new MembershipManagerSoapClient(channelBinding, endpointAddress));
			IEnumerable<MembershipMemberInfo> Stat = Array.Empty<MembershipMemberInfo>();

			foreach (int i in _dashOptions.membershipPackageIds)
			{
				Stat = Stat.Union((await apiClient.GetMemberDetailsByOrganisationAsync
			(authenticationHeader, i)).GetMemberDetailsByOrganisationResult);
			}

			
			//return  members.Count(member => member.Status.Description == "Member");
			return  Stat.Count(ss => ss.Status.Description == "Member" && ss.CreatedDate > DT);
		}*/

	}
}
