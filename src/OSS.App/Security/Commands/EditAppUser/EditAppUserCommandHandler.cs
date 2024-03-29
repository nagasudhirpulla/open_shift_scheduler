﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OSS.Domain.Entities;

namespace OSS.App.Security.Commands.EditAppUser;

public class EditAppUserCommandHandler : IRequestHandler<EditAppUserCommand, List<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<EditAppUserCommandHandler> _logger;

    public EditAppUserCommandHandler(UserManager<ApplicationUser> userManager, ILogger<EditAppUserCommandHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<List<string>> Handle(EditAppUserCommand request, CancellationToken cancellationToken)
    {
        List<string> errors = new List<string>();
        ApplicationUser user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            errors.Add($"Unable to find user with id {request.Id}");
        }
        List<IdentityError> identityErrors = new List<IdentityError>();
        // change password if not null
        string newPassword = request.Password;
        if (!string.IsNullOrWhiteSpace(newPassword))
        {
            string passResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passResetResult = await _userManager.ResetPasswordAsync(user, passResetToken, newPassword);
            if (passResetResult.Succeeded)
            {
                _logger.LogInformation("User password changed");
            }
            else
            {
                identityErrors.AddRange(passResetResult.Errors);
            }
        }

        // change username if changed
        if (user.UserName != request.Username)
        {
            IdentityResult usernameChangeResult = await _userManager.SetUserNameAsync(user, request.Username);
            if (usernameChangeResult.Succeeded)
            {
                _logger.LogInformation("Username changed");

            }
            else
            {
                identityErrors.AddRange(usernameChangeResult.Errors);
            }
        }

        // change email if changed
        if (user.Email != request.Email)
        {
            string emailResetToken = await _userManager.GenerateChangeEmailTokenAsync(user, request.Email);
            IdentityResult emailChangeResult = await _userManager.ChangeEmailAsync(user, request.Email, emailResetToken);
            if (emailChangeResult.Succeeded)
            {
                _logger.LogInformation("email changed");
            }
            else
            {
                identityErrors.AddRange(emailChangeResult.Errors);
            }
        }

        // change phone number if changed
        if (user.PhoneNumber != request.PhoneNumber)
        {
            string phoneChangeToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, request.PhoneNumber);
            IdentityResult phoneChangeResult = await _userManager.ChangePhoneNumberAsync(user, request.PhoneNumber, phoneChangeToken);
            if (phoneChangeResult.Succeeded)
            {
                _logger.LogInformation($"phone number of user {user.UserName} with id {user.Id} changed to {request.PhoneNumber}");
            }
            else
            {
                identityErrors.AddRange(phoneChangeResult.Errors);
            }
        }

        // change user role if not present in user
        bool isValidRole = SecurityConstants.GetRoles().Contains(request.UserRole);
        List<string> existingUserRoles = (await _userManager.GetRolesAsync(user)).ToList();
        bool isRoleChanged = !existingUserRoles.Any(r => r == request.UserRole);
        if (isValidRole)
        {
            if (isRoleChanged)
            {
                // remove existing user roles if any
                await _userManager.RemoveFromRolesAsync(user, existingUserRoles);
                // add new Role to user from VM
                await _userManager.AddToRoleAsync(user, request.UserRole);
            }
        }

        // check if two factor authentication to be changed
        if (user.TwoFactorEnabled != request.IsTwoFactorEnabled)
        {
            IdentityResult twoFactorChangeResult = await _userManager.SetTwoFactorEnabledAsync(user, request.IsTwoFactorEnabled);
            if (twoFactorChangeResult.Succeeded)
            {
                _logger.LogInformation($"two factor enabled = {request.IsTwoFactorEnabled}");
            }
            else
            {
                identityErrors.AddRange(twoFactorChangeResult.Errors);
            }
        }

        // update DisplayName
        if (user.DisplayName != request.DisplayName)
        {
            user.DisplayName = request.DisplayName;
            await _userManager.UpdateAsync(user);
        }

        // update OfficeId
        if (user.OfficeId != request.OfficeId)
        {
            user.OfficeId = request.OfficeId;
            await _userManager.UpdateAsync(user);
        }

        // update Designation
        if (user.Designation != request.Designation)
        {
            user.Designation = request.Designation;
            await _userManager.UpdateAsync(user);
        }

        // update GenderId
        if (user.GenderId != request.GenderId)
        {
            user.GenderId = request.GenderId;
            await _userManager.UpdateAsync(user);
        }

        // update IsActive
        if (user.IsActive != request.IsActive)
        {
            user.IsActive = request.IsActive;
            await _userManager.UpdateAsync(user);
        }

        // update ShiftRoleId
        if (user.ShiftRoleId != request.ShiftRoleId)
        {
            user.ShiftRoleId = request.ShiftRoleId;
            await _userManager.UpdateAsync(user);
        }

        // update ShiftGroupId
        if (user.ShiftGroupId != request.ShiftGroupId)
        {
            user.ShiftGroupId = request.ShiftGroupId;
            await _userManager.UpdateAsync(user);
        }

        foreach (IdentityError iError in identityErrors)
        {
            errors.Add(iError.Description);
        }
        return errors;
    }
}
