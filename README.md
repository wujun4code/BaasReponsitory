BaasReponsitory
===============

a library to help use Parse.com and AVOSCloud, and other restful BaaS service.


===============
This library is an open, flexible helper to use restful API from remote server.

Simple tutorial(Windows Console Application,for Windows Phone 8 version you can check out the code ,and run WP Demo.Thanks.):


## Configuration(IMPORTANT)

a.if you want to use BaaS as your data server,please register a acount from Parse(https://parse.com.)
  if you are in China mainland, please use AVOSCloud(https://cn.avoscloud.com/) as your BaaS server provider.

b.for General .NET Framework Application,there is always a "App.config" file in project root folder.
You can download the code, open "SampleDemo" project to check the config file.
for Windows Phone 8 app, you must add a xml file named "App.Config", and MUST set it path is "Resources/Config/App.Config", and set it build action is "Content".
"BRDemo.WindowsPhone" project from the code is a good sample.

this library need a structured config section to use.
just like this:
 ```xml
 <configuration>
  <configSections>
    <section name="BaaSConfigurationSection" type="BaaSReponsitory.BaaSConfigurationSection,BaaSReponsitory" />
  </configSections>

  <BaaSConfigurationSection>
    <BaaSHosts>
      <Host key="parse" name="Parse"  assemblyName="BaaSReponsitory" targetVersion="1.0" 
            appId="your Parse.com app id ,it can be got from the parse portal" restApiAppkey="your perse rest app key" 
            restApiAddress="https://api.parse.com" apiVersion="1"/>
      
      <Host key="avos" name="AVOSCloud" assemblyName="BaaSReponsitory" targetVersion="1.1" 
            appId="your AVOSCloud app id" restApiAppkey="your AVOSCloud app key" 
            restApiAddress="https://cn.avoscloud.com" apiVersion="1"/>
    </BaaSHosts>
    <CloudClasses> 
      <model ClassName="TestObject" assemblyName="SampleDemo" hostKey="avos"/>
      <model ClassName="Todo" assemblyName="SampleDemo" hostKey="parse"/>
    </CloudClasses>
  </BaaSConfigurationSection>
  
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
</configuration>
 ```
## Useage Example
1.define a Class like this:

```csharp
    /// <summary>
    /// a demo class for BR.
    /// </summary>
    [CloudObject(ClassName = "Todo")]//ClassName means the table name in Parse.com
    [DataContract]//for json serialize
    public class Todo
    {
        /// <summary>
        /// IMPORTANT:DO NOT add [DataMember] to Primary key property.just add  [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)] is OK!
        /// IsPrimaryKey= true means the logic PrimaryKey for your business,
        /// because Parse.com will create a Primary Key for you named "objectId", it looks like "xyk7Gd1",
        /// but when you get a object from server ,this Property will be replaced by objectId by BR.
        /// </summary>
        [CloudFiled(ColumnName = "Key", IsPrimaryKey = true)]
        public string ItemId { get; set; }


        /// <summary>
        /// general property.
        /// </summary>
        [DataMember]
        [CloudFiled(ColumnName = "Title")]
        public string Title { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "Status")]
        public int Status { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "StartTime")]
        public DateTime StartTime { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "Content")]
        public string Content { get; set; }

        [DataMember]
        [CloudFiled(ColumnName = "From")]
        public string From { get; set; }


        /// <summary>
        /// without [DataMember] attribute, it will NOT be sent to server.
        /// </summary>
        public bool NonSerialize { get; set; }
    }
	
```

2.Create a new Todo ,and send it to server.

```csharp
        Todo newItem = new Todo()
            {
                Content = "fork BR in github",
                StartTime = DateTime.Now.AddHours(1),
                Title = "say hello to BR",
                Status = 0
            };

		SimpleService ss = new SimpleService();//IMPORTANT:create a default service.
		
		var result = ss.Add<string, Todo>(newItem);//add it to cloud data server.it can get the new id of the newItem.
		
		Console.WriteLine("main thread ouptut:the newItem Id is:{0}, Content is {1}", result.ItemId, result.Content);
		
```

More demos can be got from code.

Thanks for spent time to read this.
