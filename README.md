ARIN-Whois.NET
==============

.NET client for ARIN's [Whois RESTful Web Service](https://www.arin.net/resources/whoisrws/index.html), the API to accessing ARIN's Whois data.


The [American Registry for Internet Numbers](https://www.arin.net/) (ARIN) is the Regional Internet Registry (RIR) for North America. Their API is a directory service to access data in their registration database, such as networks, organizations, and point of contacts.

This is a simple .NET client to access the RESTful API programmatically with a set of statically typed helper classes.

Install
============

Use the [NuGet](https://www.nuget.org/packages/ArinWhois) package, run the following command in the Package Manager Console:

    PM> Install-Package ArinWhois



Sample Usage
============

    var arinClient = new ArinClient();
    
    //Check single IP and populate organization along with point of contacts
    Response response = await arinClient.QueryIpAsync(IPAddress.Parse("104.131.68.92"));

	//Get Organizational info
	Organization organization = await QueryOrganizationAsync(response.Network.OrgRef.Handle);
	response.Organization = organization;

	//Get list of Point of contacts
	PointOfContacts pointOfContactRefs = await QueryPointOfContactsAsync(response.Network.OrgRef.Handle);

	IList<PointOfContact> pointOfContacts = new List<PointOfContact>();
	
	//Get Point of Contact details for each Point of Contact
	foreach (Poclinkref poclinkref in pointOfContactRefs.Pocs.PocLinkRefs)
	{
		//Check if PoC for the same type exist in the list. If so, no need to query again.
		PointOfContact pocCheck=pointOfContacts.Where(c => c?.poc?.Handle?.Value == poclinkref.Handle).FirstOrDefault();
		if(pocCheck == null)
		{
			PointOfContact pointOfContact = await QueryPointOfContactAsync(poclinkref.Handle);
			if(pointOfContact!=null)
			{
				pointOfContacts.Add(pointOfContact);
			}
		}  
	}

	if (response.Organization !=null)
	{
		response.Organization.PointOfContacts = pointOfContacts;
	}



Contributors
============
* [MaxHorstmann](https://github.com/MaxHorstmann) (Max Horstmann)
* [pwiens] (https://github.com/pwiens/ArinWhois.NET) (Paul Wiens)
* [aspnet4you] (https://github.com/aspnet4you/ArinWhois.NET) (Prodip Saha)



