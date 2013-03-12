using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Utility
{
    public class ExtendedDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        public const string Key_GroupName = "GroupName";

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            ModelMetadata modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            DisplayAttribute displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault();

            if (displayAttribute != null)
                modelMetadata.AdditionalValues[ExtendedDataAnnotationsModelMetadataProvider.Key_GroupName] = displayAttribute.GroupName.RemoveSpace();

            return modelMetadata;
        }

   
    }

    public static class ModelMetadataExtensions
    {
        public static string GetGroupName(this ModelMetadata modelMetadata)
        {
            if (modelMetadata.AdditionalValues.ContainsKey(ExtendedDataAnnotationsModelMetadataProvider.Key_GroupName))
                return (modelMetadata.AdditionalValues[ExtendedDataAnnotationsModelMetadataProvider.Key_GroupName] as string);

            return null;
        }

    }

    
}