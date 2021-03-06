﻿using System;
using System.Runtime.Serialization;

namespace Illumina.BaseSpace.SDK.Types
{
    [DataContract]
	public class ContentReferenceResource<T> : IContentReferenceResource<T> where T : AbstractResource
	{
        public ContentReferenceResource(T resource, string relation, string type=null)
        {
            Href = resource.Href;
            HrefContent = resource.Href;
            Rel = relation;
            Content = resource;
            
            Type = type ?? typeof(T).ToString().Replace("Compact", "").Replace("Illumina.BaseSpace.SDK.Types.", "");;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Rel { get; set; }

		[DataMember]
		public string Type { get; set; }

		[DataMember]
		public Uri Href { get; set; }

		[DataMember]
		public Uri HrefContent { get; set; }

		[DataMember]
		public T Content { get; set; }
	}
}
