using MediatR;

using Microsoft.EntityFrameworkCore;
using ProductManagement.Common.Dtos.Auth;
using ProductManagement.Common.Base.WebAPI;
using ProductManagement.Persistence;
using ProductManagement.Domain;
using ProductManagement.Common.Helpers;
using System.Text.Json;
using ProductManagement.Common.Extensions;

namespace ProductManagement.Features.Auth.Commands
{
	public class RegistrationHandler(ProductManagementDbContext context, NLog.ILogger logger) : IRequestHandler<RegistrationCommand, RegistrationResponse?>
	{
		public async Task<RegistrationResponse?> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			RegistrationResponse response = new();
			try
			{
				await context.Database.BeginTransactionAsync(cancellationToken);

				#region Validation
				if (request.Parameters == null)
				{
					response.SetValidationMessage("Parameters is null");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Username))
				{
					response.SetValidationMessage("Username is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Password))
				{
					response.SetValidationMessage("Password is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Email))
				{
					response.SetValidationMessage("Email is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Phone))
				{
					response.SetValidationMessage("Phone is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.FullName))
				{
					request.Parameters.FullName = request.Parameters.Username;
				}
				#endregion

				#region Check User
				MstUser? user = await context.MstUsers
					.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Parameters.Username.ToLower() ||
						x.Email.ToLower() == request.Parameters.Email.ToLower() ||
						x.Phone.ToLower() == request.Parameters.Phone.ToLower(), cancellationToken);
				if (user != null)
				{
					response.SetValidationMessage("Username or email or phone already exists");
					return response;
				}
				#endregion

				#region Create User
				MstUser newUser = new()
				{
					Username = request.Parameters.Username,
					FullName = request.Parameters.FullName,
					Email = request.Parameters.Email,
					Phone = request.Parameters.Phone,
					PasswordHash = HashHelper.DoHash(request.Parameters.Password),
					CreatedAt = DateTime.Now,
					LastModifiedAt = DateTime.Now
				};
				context.MstUsers.Add(newUser);
				await context.SaveChangesAsync(cancellationToken);
				response.Message = "Registration successful! Please log in to continue.";
				#endregion

				await context.Database.CommitTransactionAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				await context.Database.RollbackTransactionAsync(cancellationToken);
				logger.LogException(ex, nameof(this.Handle), JsonSerializer.Serialize(request.Parameters));
			}
			return response;
		}
	}
}
