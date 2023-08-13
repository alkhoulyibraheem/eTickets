using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestaurantStore.Core.Validation
{
	public class SafeText : ValidationAttribute
	{
		public string? Text { get; set; }

		public string GetErrorMessage()
		{
			Text = HttpUtility.HtmlEncode(Text);

			return $"This Text {Text} Not Safe !!!";
		}

		protected override ValidationResult? IsValid(
			object? value, ValidationContext validationContext)
		{
			if (value != null && value is string stringValue)
			{
				Text = stringValue;
				var safeText = Sanitizer.GetSafeHtmlFragment(stringValue);
				if (!CheckHtml(safeText)
					|| !CheckScript(safeText)
					|| !CheckSql(safeText)
					|| !safeText.Equals(stringValue))
				{
					return new ValidationResult(GetErrorMessage());
				}
			}


			return ValidationResult.Success;
		}

		private bool CheckScript(string text)
		{
			if (text.Contains("<script", StringComparison.OrdinalIgnoreCase)
				|| text.Contains("javascript:", StringComparison.OrdinalIgnoreCase)
				|| text.Contains("onload=", StringComparison.OrdinalIgnoreCase))
			{
				// Potentially malicious JavaScript found
				return false;
			}
			return true;
		}

		private bool CheckHtml(string text)
		{
			if (text.Contains("<", StringComparison.OrdinalIgnoreCase)
				|| text.Contains(">", StringComparison.OrdinalIgnoreCase))
			{
				// Potentially malicious HTML found
				return false;
			}
			return true;
		}

		private bool CheckSql(string text)
		{
			string[] sqlKeywords = { "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "--", ";" };
			foreach (string keyword in sqlKeywords)
			{
				if (text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
				{
					// Potentially malicious SQL injection found
					return false;
				}
			}
			return true;
		}
	}
}