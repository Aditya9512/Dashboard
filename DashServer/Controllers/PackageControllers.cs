/*using ChilliDBApi.DataManager;

using DashServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.ServiceModel;
using System;
using System.Reflection;
using System.Linq;

namespace DashServer.Controllers


{

	[ApiController]
	[Route("[controller]")]
	public class PackageController
	{
		 // hardcoded values for now but these should be moved to a config file later
		string apiBaseUrl = "https://test.chillidb.com/ChilliDB_Main/";
		string systemId = "ChilliDB_Main";
		string systemUser = "Super_user"; // ADD CHILLIDB USERNAME HERE
		string systemPassword = "$Poly$"; // ADD CHILLIDB PASSWORD HERE BUT DO NOT CHECK IT IN TO GIT
		int apiMaxMessageSize = 1048576;
		int apiTimeOutSeconds = 600;
		int DataEntityIds = 5092;
 


		private readonly DashboardOptions _dashOptions;

		public PackageController(IOptions<DashboardOptions> unitOptions)
		{
			_dashOptions = unitOptions.Value;

		}




		// call the API

		[HttpGet(Name = "GetPackageController")]


		public async Task<DynamicDataField> GetTestAsync()
		{



			EndpointAddress endpointAddress = new(_dashOptions.apiBaseUrl + "MODULES/CRM_Integration_Services/DataExchange/DataManager.asmx");
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
            ChilliDBApi.DataManager.AuthenticationHeader authenticationHeader = new()
			{
				SystemIdentifier = _dashOptions.systemId,
				Username = _dashOptions.systemUser,
				Password = _dashOptions.systemPassword,
			};
			DataManagerSoapClient apiClient = await Task.Run(() =>
			new DataManagerSoapClient(channelBinding, endpointAddress));

			 IEnumerable<DynamicDataField> data = Array.Empty<DynamicDataField>();

			foreach (int i in _dashOptions.SystemEntityId*//**//*)
			{
				data = data.Union((await apiClient.GetBaseAndDynamicDataFieldsByEntityAsync
			(authenticationHeader, i)).GetBaseAndDynamicDataFieldsByEntityResult);
			} 
			*//*OrganisationCategorisationItem[] members = (await apiClient.GetOrgCategorisationsByEntityAsync
						(authenticationHeader, _dashOptions.SystemEntityId)).GetOrgCategorisationsByEntityResult;*//*


			return data.First();
		}



	}

	}


*/
