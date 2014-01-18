using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaaSReponsitory
{
#if FRAMEWORK
    public class SampleBaaSConfig : IBaaSConfig
    {
        public BaaSConfigurationSection BaaSConfig { get; set; }
        public void LoadConfig()
        {
            BaaSConfig = (BaaSConfigurationSection)ConfigurationManager.GetSection("BaaSConfigurationSection");
        }
        public IEnumerable<IBaaSHostConfig> HostConfigs
        {
            get
            {
                return BaaSConfig.HostConfigs;
            }
            set
            {

            }
        }
        public IEnumerable<IBaaSCloudClassConfig> ClassConfigs
        {
            get
            {
                return BaaSConfig.ClassConfigs;
            }
            set
            {

            }
        }
    }

    public class BaaSConfigurationSection : ConfigurationSection
    {

        [ConfigurationProperty(BRDic.BaaSHostskey, IsRequired = true)]
        public BaaSHostElmentsCollection HostConfigs
        {
            get
            {
                return base[BRDic.BaaSHostskey] as BaaSHostElmentsCollection;
            }
        }

        [ConfigurationProperty(BRDic.cloudObjectKey, IsRequired = true)]
        public CloudClassCollection ClassConfigs
        {
            get
            {
                return base[BRDic.cloudObjectKey] as CloudClassCollection;
            }
        }

    }

    [ConfigurationCollection(typeof(ModelElement), AddItemName = "model")]
    public class CloudClassCollection : ConfigurationElementCollection, IEnumerable<ModelElement>
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ModelElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as ModelElement;
            if (l_configElement != null)
                return l_configElement.ClassName;
            else
                return null;
        }

        public ModelElement this[int index]
        {
            get
            {
                return BaseGet(index) as ModelElement;
            }
        }

    #region IEnumerable<ConfigElement> Members

        IEnumerator<ModelElement> IEnumerable<ModelElement>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }
        #endregion
    }

    public class ModelElement : ConfigurationElement, IBaaSCloudClassConfig
    {
        [ConfigurationProperty(BRDic.ClassNameKey, IsKey = true, IsRequired = true)]
        public string ClassName
        {
            get
            {
                return base[BRDic.ClassNameKey] as string;
            }
            set
            {
                base[BRDic.ClassNameKey] = value;
            }
        }

        [ConfigurationProperty(BRDic.AssemblyNameKey, IsRequired = true)]
        public string assemblyName
        {
            get
            {
                return base[BRDic.AssemblyNameKey] as string;
            }
            set
            {
                base[BRDic.AssemblyNameKey] = value;
            }
        }


        [ConfigurationProperty(BRDic.HostKeyKey, IsRequired = true)]
        public string HostKey
        {
            get
            {
                return base[BRDic.HostKeyKey] as string;
            }
            set
            {
                base[BRDic.HostKeyKey] = value;
            }
        }

    }

    [ConfigurationCollection(typeof(BaaSHostElment), AddItemName = "Host")]
    public class BaaSHostElmentsCollection : ConfigurationElementCollection, IEnumerable<BaaSHostElment>
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new BaaSHostElment();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var l_configElement = element as BaaSHostElment;
            if (l_configElement != null)
                return l_configElement.key;
            else
                return null;
        }

        public BaaSHostElment this[int index]
        {
            get
            {
                return BaseGet(index) as BaaSHostElment;
            }
        }

    #region IEnumerable<ConfigSubElement> Members

        IEnumerator<BaaSHostElment> IEnumerable<BaaSHostElment>.GetEnumerator()
        {
            return (from i in Enumerable.Range(0, this.Count)
                    select this[i])
                    .GetEnumerator();
        }

        #endregion
    }

    public class BaaSHostElment : ConfigurationElement, IBaaSHostConfig
    {

        [ConfigurationProperty(BRDic.keyKey, IsKey = true, IsRequired = true)]
        public string key
        {
            get
            {
                return base[BRDic.keyKey] as string;
            }
            set
            {
                base[BRDic.keyKey] = value;
            }
        }

        [ConfigurationProperty(BRDic.nameKey, IsRequired = true)]
        public string name
        {
            get
            {
                return base[BRDic.nameKey] as string;
            }
            set
            {
                base[BRDic.nameKey] = value;
            }
        }

        [ConfigurationProperty(BRDic.assemblyNameKey, IsRequired = true)]
        public string assemblyName
        {
            get
            {
                return base[BRDic.assemblyNameKey] as string;
            }
            set
            {
                base[BRDic.assemblyNameKey] = value;
            }
        }

        [ConfigurationProperty(BRDic.targetVersionKey, IsRequired = true)]
        public string targetVersion
        {
            get
            {
                return base[BRDic.targetVersionKey] as string;
            }
            set
            {
                base[BRDic.targetVersionKey] = value;
            }
        }

        [ConfigurationProperty(BRDic.appIdKey, IsRequired = true)]
        public string appId
        {
            get
            {
                return base[BRDic.appIdKey] as string;
            }
            set
            {
                base[BRDic.appIdKey] = value;
            }
        }


        [ConfigurationProperty(BRDic.appkeyKey, IsRequired = true)]
        public string appKey
        {
            get
            {
                return base[BRDic.appkeyKey] as string;
            }
            set
            {
                base[BRDic.appkeyKey] = value;
            }
        }

        [ConfigurationProperty(BRDic.restApiAddressKey, IsRequired = true)]
        public string restApiAddress
        {
            get
            {
                return base[BRDic.restApiAddressKey] as string;
            }
            set
            {
                base[BRDic.restApiAddressKey] = value;
            }
        }

        [ConfigurationProperty(BRDic.apiVersionKey, IsRequired = true)]
        public string apiVersion
        {
            get
            {
                return base[BRDic.apiVersionKey] as string;
            }
            set
            {
                base[BRDic.apiVersionKey] = value;
            }
        }


    }
#endif

#if WINDOWS_PHONE

    public class BaaSSampleWPConfig : IBaaSConfig
    {

        private const string CONFIGURL = "Resources/Config/App.config";

        public void LoadConfig()
        {
            XDocument doc = XDocument.Load(CONFIGURL);

            var hostList = new List<SampleHostConfig>();

            var Configcontent = doc.Root.Elements(BRDic.ConfigSectionKey);

            var hostContent = Configcontent.Elements(BRDic.BaaSHostskey);
            var hosts = hostContent.Elements(BRDic.HostRootNameKey);

            foreach (var h in hosts)
            {
                var key = h.Attribute(BRDic.keyKey).Value;
                var name = h.Attribute(BRDic.nameKey).Value;
                var assemblyName = h.Attribute(BRDic.assemblyNameKey).Value;
                var targetVersion = h.Attribute(BRDic.targetVersionKey).Value;
                var appId = h.Attribute(BRDic.appIdKey).Value;
                var appKey = h.Attribute(BRDic.appkeyKey).Value;
                var restApiAddress = h.Attribute(BRDic.restApiAddressKey).Value;
                var apiVersion = h.Attribute(BRDic.apiVersionKey).Value;

                var th = new SampleHostConfig() 
                {
                    apiVersion = apiVersion,
                    restApiAddress = restApiAddress,
                    appKey = appKey,
                    appId = appId,
                    targetVersion = targetVersion,
                    assemblyName = assemblyName,
                    name = name,
                    key = key,

                };

                hostList.Add(th);

            }

            this.HostConfigs = hostList;

            var classList = new List<SampleCloudClassConfig>();

            var classContent = Configcontent.Elements(BRDic.cloudObjectKey);
            var classes = classContent.Elements(BRDic.modelKey);

            foreach (var cl in classes)
            {
                var ClassName = cl.Attribute(BRDic.ClassNameKey).Value;

                var assemblyName = cl.Attribute(BRDic.AssemblyNameKey).Value;

                var hostKey = cl.Attribute(BRDic.HostKeyKey).Value;

                var tc = new SampleCloudClassConfig() 
                {
                    ClassName = ClassName,
                    assemblyName = assemblyName,
                    HostKey = hostKey,
                };

                classList.Add(tc);

            }

            this.ClassConfigs = classList;

        }

        public IEnumerable<IBaaSHostConfig> HostConfigs { get; set; }

        public IEnumerable<IBaaSCloudClassConfig> ClassConfigs { get; set; }
    }

    public class SampleHostConfig : IBaaSHostConfig
    {
      public  string key { get; set; }

      public string name { get; set; }

      public string assemblyName { get; set; }

      public string targetVersion { get; set; }

      public string appId { get; set; }

      public string appKey { get; set; }

      public string restApiAddress { get; set; }

      public string apiVersion { get; set; }
    }

    public class SampleCloudClassConfig : IBaaSCloudClassConfig
    {
        public string ClassName { get; set; }

        public string assemblyName { get; set; }

        public string HostKey { get; set; }
    }
#endif
}

