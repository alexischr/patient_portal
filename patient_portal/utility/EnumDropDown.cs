using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PatientPortal.Utility
{
    public static class Utility
    {

        public static string RemoveSpace(this string str)
        {
            if (str == null)
                return null;
            var builder = new System.Text.StringBuilder();
            foreach (var character in str.ToCharArray())
            {
                if (!char.IsSeparator(character))
                    builder.Append(character);
            }
            return builder.ToString();
        }


    }

    public static class EnumDropDown
    {
        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "Select...", Value = "" } };

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Name;
            else
                return value.ToString();
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownListFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString EnumMultipleList<TModel>(this HtmlHelper<TModel> htmlHelper, ModelMetadata metadata, string name, object htmlAttributes)
        {
            Type enumType = metadata.ModelType.GetElementType();
            IEnumerable<Enum> values = Enum.GetValues(enumType).Cast<Enum>();


            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = GetEnumDescription(value),
                                                    Value = value.ToString(),
                                                    Selected = (metadata.Model == null ? false : Array.IndexOf((Array)metadata.Model, value) != -1)
                                                };

            return htmlHelper.ListBox(name, items, htmlAttributes);
        }


        public static MvcHtmlString EnumDropDownList<TModel>(this HtmlHelper<TModel> htmlHelper, ModelMetadata metadata, string name, object htmlAttributes)
        {
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<Enum> values = Enum.GetValues(enumType).Cast<Enum>();

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = GetEnumDescription(value),
                                                    Value = value.ToString(),
                                                    Selected = value.Equals(metadata.Model)
                                                };

            // If the enum is nullable, add an 'empty' item to the collection
            if (metadata.IsNullableValueType)
                items = SingleEmptyItem.Concat(items);

            return htmlHelper.DropDownList(name, items, htmlAttributes);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = GetEnumDescription(value),
                                                    Value = value.ToString(),
                                                    Selected = value.Equals(metadata.Model)
                                                };

            // If the enum is nullable, add an 'empty' item to the collection
            if (metadata.IsNullableValueType)
                items = SingleEmptyItem.Concat(items);

            return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
        }

    }

    public static class FormDisplay
    {
        public static MvcHtmlString RetainedDisplayFor<TModel, TObject>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TObject>> expression, object htmlAttributes = null)
        {
            var rs = htmlHelper.DisplayFor<TModel, TObject>(expression, htmlAttributes).ToHtmlString();
            rs = rs + htmlHelper.HiddenFor<TModel, TObject>(expression, htmlAttributes).ToHtmlString();
            return MvcHtmlString.Create(rs);
        }

    }

}


