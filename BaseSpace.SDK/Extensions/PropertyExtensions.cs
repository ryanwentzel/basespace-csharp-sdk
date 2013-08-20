﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illumina.BaseSpace.SDK.Types;

namespace Illumina.BaseSpace.SDK
{
    public static class PropertyContainerExtensions
    {
        public static bool TryGetProperty(this PropertyContainer propertyContainer, string name, out Property property)
        {
            property = propertyContainer.Items.FirstOrDefault(p => p.Name == name);
            return property != null;
        }

        /// <summary>
        /// Returns true if not all properties for the resource have been returned in this request due to paging
        /// </summary>
        public static bool IsTruncated(this PropertyContainer propertyContainer)
        {
            return propertyContainer.DisplayedCount != propertyContainer.TotalCount;
        }

        /// <summary>
        /// Returns true if not all property items for the property have been returned in this request due to paging
        /// </summary>
        public static bool IsTruncated(this Property property)
        {
            if (property.Content != null)
            {
                // single-value properties aren't truncated
                return false;
            }

            return property.ItemsDisplayedCount < property.ItemsTotalCount;
        }

        /// <summary>
        /// Returns the property's type for single-value properties, and the underlying type for multi-value properties.
        /// </summary>
        public static string GetUnderlyingType(this Property property)
        {
            return (property.Type ?? string.Empty).Replace(PropertyTypes.LIST_SUFFIX, string.Empty);
        }

        /// <summary>
        /// For properties of type 'string[]', returns property items as a string[].
        /// </summary>
        public static string[] ToStringArray(this Property property)
        {
            if ((property.Type ?? string.Empty) == PropertyTypes.STRING + PropertyTypes.LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToString()).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For properties of type 'string[]', returns property items that may be converted to int as a int[]. 
        /// </summary>
        public static int[] ToIntArray(this Property property)
        {
            if ((property.Type ?? string.Empty) == PropertyTypes.STRING + PropertyTypes.LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToInt()).Where(i => i.HasValue).Select(i => i.Value).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For properties of type 'string[]', returns property items that may be converted to long as a long[]. 
        /// </summary>
        public static long[] ToLongArray(this Property property)
        {
            if ((property.Type ?? string.Empty) == PropertyTypes.STRING + PropertyTypes.LIST_SUFFIX)
            {
                return property.Items.Select(i => i.ToLong()).Where(i => i.HasValue).Select(i => i.Value).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For multi-value properties containing resource references, returns property items referencing the given type as an array
        /// </summary>
        public static TResourceType[] ToResourceArray<TResourceType>(this Property property)
            where TResourceType : class, IPropertyContent
        {
            if (property.Items != null)
            {
                return property.Items.Select(i => i.ToResource<TResourceType>()).Where(i => i != null).ToArray();
            }
            return null;
        }

        /// <summary>
        /// For multi-value properties of type 'sample[]', returns property items as SampleCompact[]
        /// </summary>
        public static SampleCompact[] ToSampleArray(this Property property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.SAMPLE + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<SampleCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'appresult[]', returns property items as AppResultCompact[]
        /// </summary>
        public static AppResultCompact[] ToAppResultArray(this Property property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.APPRESULT + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<AppResultCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'project[]', returns property items as ProjectCompact[]
        /// </summary>
        public static ProjectCompact[] ToProjectArray(this Property property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.PROJECT + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<ProjectCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'appsession[]', returns property items as AppSessionCompact[]
        /// </summary>
        public static AppSessionCompact[] ToAppSessionArray(this Property property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.APPSESSION + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<AppSessionCompact>();
        }

        /// <summary>
        /// For multi-value properties of type 'run[]', returns property items as RunCompact[]
        /// </summary>
        public static RunCompact[] ToRunsArray(this Property property)
        {
            if ((property.Type ?? string.Empty) != PropertyTypes.RUN + PropertyTypes.LIST_SUFFIX)
            {
                return null;
            }
            return property.ToResourceArray<RunCompact>();
        }
    }

    public static class PropertyContentExtensions
    {
        public static bool IsLiteral(this IPropertyContent propertyContent)
        {
            return propertyContent as PropertyContentLiteral != null;
        }

        public static int? ToInt(this IPropertyContent propertyContent)
        {
            if (!propertyContent.IsLiteral())
            {
                return null;
            }
            return ((PropertyContentLiteral) propertyContent).ToInt();
        }

        public static long? ToLong(this IPropertyContent propertyContent)
        {
            if (!propertyContent.IsLiteral())
            {
                return null;
            }
            return ((PropertyContentLiteral) propertyContent).ToLong();
        }

        public static DateTime? ToDateTime(this IPropertyContent propertyContent)
        {
            if (!propertyContent.IsLiteral())
            {
                return null;
            }
            return ((PropertyContentLiteral) propertyContent).ToDateTime();
        }

        /// <summary>
        /// Convert to a compact resource
        /// </summary>
        /// <remarks>
        /// TResourceType must be a compact resource object 
        /// </remarks>
        public static TResourceType ToResource<TResourceType>(this IPropertyContent propertyContent)
            where TResourceType : class, IPropertyContent
        {
            if (propertyContent.IsLiteral())
            {
                return null;
            }
            return propertyContent as TResourceType;
        }
    }
}