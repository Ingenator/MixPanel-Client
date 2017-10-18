# MixPanelHttpClient

MixPanelHttpClient is a simple MixPanel Http client for C#, it's built with NetStandard 1.4 so you can use it in your Xamarin.Android, Xamarin.iOS and Xamarin.Form.

It include a way to include push notifications tokens for Android and iOS.

## Features

This Client uses the [API Reference](https://mixpanel.com/help/reference/http) from Mixpanel.

For now it only implements the following methods, feel free to add your own!:

* /Track
* /Engage

And the Following operations:

* Set 
* setPushRegistrationId

## Methods

First you need to initialize the client with your Mixpanel Proyect Token and the OS of the host:

`MixPanelClient mpclient = MixPanelClient.GetInstance("[YOUR_MIXPANEL_PROJECT_ID", "[Enum MixPanelOSType]");`

Now your are ready to start tracking your events:

`mpclient = await mp.TrackAsync("My event");`

Also you can Track Events with Custom properties, usually this custom properties have to be bound with a particular User, if this is the case, you need to provide a distinct_id in the TrackAsync method with an anonymous object as follows:

`var result = await mp.TrackAsync<string, object>("theEvent", new { DistinctId = 1, CustomProperty = "Some Value" });`

Or you can use an Object that implements (I recommend this method)

`IBaseUserTrackingRequest`

or Inherits from 

`BaseUserTrackRequest`
As follows:

```
public class MyCustomObject : BaseUserTrackRequest
{
     public object CustomProperty { get; set; }

     public string CustomProperty2 { get; set; }
}

var toSend = new MyCustomObject() { DistinctId = testerID, CustomProperty = "Some value", CustomProperty2 = "AnotherValue" };

var result = await mp.TrackAsync("Some Event", toSend);
```

The DistinctId word is reserved for it will be replaced with the mixpanel reserved word "distinct_id". Reserved property names are at the Reserved Words secction.

TrackAsync properties (second parameter) support the following types:


Primitive types: string, int, bool etc. (will be passed toString() ),
Lists,
Key pair value objects like Dictionary<,> this objects will be added as root of the properties properties, lets say

```
var someDictionary = new Dictionary<string,string>();
someDictionary.Add("hello","world");
someDictionary.Add("goodBye","world");
mp.TrackAsync<string,object>("Some Event", new { MySimpleProperty = "someValue", ADictionary = someDictionary});
```


This will result in the following json for MixPanel:

```
{
    "event": "Some Event",
    "properties": {
        "MySimpleProperty": "someValue",
        "token": "e3bc4100330c35722740fb8c6f5abddc",
        "hello": "world",
        "goodbye": "world"
    }
}
```

***Note*:** The ADictionary (or the name it has) will be replaced with the dictionary properties as showed above.


## Operations

For Identifying your users you need to set some properties, for that use the GetPeople() method so you can access to the Set method.

The **Set** method will accept a predefined object  **MPUserProperties** or an object that inherits from it.


```
var person = mp.GetPeople();

person.Identify("[YOUR_USER_UNIQUE_ID]");    

result = await person.Set(new MPUserProperties() { Created = DateTime.Now, Name = "Jhon Doe", Email = "someone@somewhere.com", Phone = "+525500000000"  });                
```


The **setPushRegistrationId** is used to send push notifications with Mixpanel. First you need to identify the user so the token is stored in it's profile, if you had already identified it skip that step and just call the setPushRegistrationId. Note that the token will be stored based on the OS Type and this will only work with Android or iOS, so check in the GetInstance method the OS Type you are sending.


```
MixPanelClient mpclient = MixPanelClient.GetInstance("[YOUR_MIXPANEL_PROJECT_ID", "[Enum MixPanelOSType Android or iOS]");
var person = mpclient.GetPeople();
person.Identify("[YOUR_USER_UNIQUE_ID]");
var res = await person.setPushRegistrationId("[USER PUSH TOKEN]");
```

## Reserved Words

Reserved words are replaced with the Mixpanel reserved words, you can check Mixpanel reserved words [here](https://mixpanel.com/help/questions/articles/what-properties-do-mixpanels-libraries-store-by-default), properties with this names will be replaced so watch your naming.

Reserved words:

* DistinctId
* Created 
* Name 
* Email 
* Phone 
* Time 
* Ip 
* IgnoreTime 
* FirstName 
* LastName 
* Amount 
* City 
* Region 
* Country 
* Timezone 
* InitialReferrer 
* InitialReferringDomain 
* OperatingSystem 
* LastSeen
