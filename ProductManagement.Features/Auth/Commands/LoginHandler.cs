using MediatR;

using ProductManagement.Common.Dtos.Auth;
using ProductManagement.Common.Base.WebAPI;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Persistence;
using ProductManagement.Domain;
using ProductManagement.Common.Helpers;

namespace ProductManagement.Features.Auth.Commands
{
	public class LoginHandler(ProductManagementDbContext context) : IRequestHandler<LoginCommand, LoginResponse?>
	{
		public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			LoginResponse response = new();
			try
			{
				#region Validation
				if (request.Parameters == null)
				{
					response.SetValidationMessage("Parameters is null");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.UserName))
				{
					response.SetValidationMessage("Phone is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Password))
				{
					response.SetValidationMessage("Password is null or empty");
					return response;
				}				
				#endregion

				#region Check User
				MstUser? user = await context.MstUsers
					.FirstOrDefaultAsync(x => x.Username == request.Parameters.UserName && x.IsActive == true, cancellationToken);
				if (user == null || !HashHelper.Verify(request.Parameters.Password, user.PasswordHash))
				{
					response.SetValidationMessage("Phone or Password is incorrect");
					return response;
				}
				#endregion

				#region Generate Bearer Token
				response.BearerToken = BearerTokenHelper.GenerateBearerToken(user.Id);
				response.UserFullName = user.FullName;
				response.Message = "Login success";
				#endregion
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return response;
		}
	}
}
