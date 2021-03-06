﻿using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{

    [DataContract( Name = "Application")]
    public class ApplicationCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string HrefLogo { get; set; }

        [DataMember]
        public string HomepageUri { get; set; }

        [DataMember]
        public string ShortDescription { get; set; }

        [DataMember]
        public string DateCreated { get; set; }
    }
    [DataContract]
    public class Application : ApplicationCompact
    {
        [DataMember]
        public string LongDescription { get; set; }

        [DataMember]
        public UserCompact UserOwnedBy { get; set; }

        [DataMember]
        public Uri HrefSettings { get; set; }
    }

    public enum ApplicationSortByParameters { Id, Name, DateCreated }
}
