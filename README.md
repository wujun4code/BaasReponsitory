BaasReponsitory
===============

a library to help use Parse.com and AVOSCloud, and other restful BaaS service.


===============
This library is an open, flexible helper to use restful API from remote server.

Simple tutorial(Windows Console Application,for Windows version you can check out the code ,and run WP Demo.Thanks.):

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
