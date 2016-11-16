# lob-dotnet
Lob .Net API wrapper

Install
-----------

To get started install the package via nuget package manager.

```
PM > Install-Package Lob
```

Quick Start
-----------

Refernce the Library

```csharp
using LobClient;
```

Create an instance of the client with an API key

```csharp
string apiKey = "Your custom API key"
var lob = new Lob( apiKey );
```

Create resources

```csharp
lob.Letters.Create(new
{
  to = new
  {
      name = "John Smith",
      address_line1 = "ADDRESS LINE 1",
      address_line2 = "ADDRESS LINE 2",
      address_country = "COUNTRY CODE",
      address_city = "CITY",
      address_state = "STATE",
      address_zip = ZIP CODE
  },
  // Or use the address ID
  from = 5,
  color = true,
  file = "Contents",
  insert_blank_page = false
});
```

View a resources

```csharp
lobClient.Postcards.Get("id of postcard");
```

View all resources

```csharp
lobClient.Postcards.All();
```
Delete a resource

```csharp
lobClient.Postcards.Delete("id of postcard");
```

Resources:
- Addresses
- Areas
- BankAccounts
- Checks
- Countries
- Letters
- Postcards
- Routes
- States

See Lob api docs more info on the API [Lob.com/docs](https://lob.com/docs)


