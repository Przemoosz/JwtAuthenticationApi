namespace JwtAuthenticationApi.Exceptions
{
	/// <summary>
	/// Represents one or more errors that occurs during reading settings from appsettings.json file.
	/// </summary>
	public sealed class SettingsNotProvidedException: Exception
	{
		/// <summary>
		/// Initializes new instance of <see cref="SettingsNotProvidedException"/> class.
		/// </summary>
		public SettingsNotProvidedException(): base()
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="SettingsNotProvidedException"/> class with error message.
		/// </summary>
		public SettingsNotProvidedException(string message): base(message)
		{
		}

		/// <summary>
		/// Initializes new instance of <see cref="SettingsNotProvidedException"/> class with error message and inner exception.
		/// </summary>
		public SettingsNotProvidedException(string message, Exception innerException): base(message, innerException) 
		{
		}

		/// <summary>
		/// Checks if provided setting occurs in appsettings.json and if ist value is not null or empty string.
		/// Throws <see cref="SettingsNotProvidedException"/> exception if any of this condition is not met.
		/// </summary>
		/// <param name="settingName">Setting name in appsettings.json.</param>
		/// <param name="settingFullName">Setting name from model class.</param>
		/// <exception cref="SettingsNotProvidedException"/>
		public static void ThrowIfSettingIsNullOrEmpty(string settingName, string settingFullName)
		{
			if (string.IsNullOrEmpty(settingName))
			{
				throw new SettingsNotProvidedException(
					$"Provided setting for {settingFullName} is null or empty. Provide this value in appsettings.json");
			}
		}
	}
}
