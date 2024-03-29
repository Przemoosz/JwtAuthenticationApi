﻿namespace JwtAuthenticationApi.Models.Password
{
    using System.ComponentModel.DataAnnotations;
    using Abstraction.Models;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Password salt model, that will be saved in database. Inherits <see cref="ModelBase"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class PasswordSaltModel : ModelBase
    {
        /// <summary>
        /// Gets or sets Password salt.
        /// </summary>
        [Required]
        public string Salt { get; set; }

        /// <summary>
        /// Gets <see cref="int"/> value of user that is associated with this salt.
        /// </summary>
        [Required]
        public int UserId { get; init; }

		/// <summary>
		/// Initializes <see cref="PasswordSaltModel"/>
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="salt">Salt.</param>
		/// <param name="userId">User identifier associated with password salt.</param>
		public PasswordSaltModel(int id, string salt, int userId) : base(id)
        {
            Salt = salt;
            UserId = userId;
        }
    }
}