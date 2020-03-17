using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using OSS.Domain.Entities;
using AutoMapper;

namespace OSS.App.Security.Commands.CreateAppUser
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public CreateAppUserCommandHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IdentityResult> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                GenderId = request.GenderId,
                ShiftRoleId = request.ShiftRoleId,
                ShiftGroupId = request.ShiftGroupId,
                OfficeId = request.OfficeId,
                IsActive = request.IsActive
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                //TODO use logger here
                Console.WriteLine($"Created new account for {user.UserName} with id {user.Id}");
                // check if role string is valid
                bool isValidRole = SecurityConstants.GetRoles().Contains(request.UserRole);
                if (isValidRole)
                {
                    await _userManager.AddToRoleAsync(user, request.UserRole);
                    Console.WriteLine($"{request.UserRole} role assigned to new user {user.UserName} with id {user.Id}");
                }
                // send confirmation email to user
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = QueryHelpers.AddQueryString(request.BaseUrl, new Dictionary<string, string>() { { "code", code }, { "userId", user.Id } });
                try
                {
                    await _emailSender.SendEmailAsync(
                    user.Email,
                    "Please confirm your email for WRLDC Shift Roster web app",
                    $"Please confirm your account of WRLDC Shift Roster web app by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>");

                    Console.WriteLine($"Email address Confirmation mail sent to ${user.UserName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}
