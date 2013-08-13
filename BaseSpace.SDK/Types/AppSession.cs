﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using Illumina.BaseSpace.SDK.Extensions;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract( Name = "AppSession")]
    public class AppSessionCompact : AbstractResource
    {
        [DataMember(IsRequired = true)]
        public override string Id { get; set; }

        [DataMember(IsRequired = true)]
        public override Uri Href { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ApplicationCompact Application { get; set; }

        [DataMember]
        public UserCompact UserCreatedBy { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string StatusSummary { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }
    }


    [DataContract]
    public class AppSession : AppSessionCompact
    {
        [DataMember]
        public string OriginatingUri { get; set; }

        [DataMember]
        public IResource[] References { get; set; }

        [DataMember]
        public Uri HrefLogs { get; set; }
    }

    public enum AppSessionStatus { Running, Complete, NeedsAttention, Aborted }
    public enum AppSessionQueryParameters { Status, StatusSummary }
}
