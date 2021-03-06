﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OSS.App.Genders.Queries.GetGenders;
using OSS.App.Security;
using OSS.App.Security.Commands.CreateAppUser;
using OSS.App.Security.Commands.DeleteAppUser;
using OSS.App.Security.Commands.EditAppUser;
using OSS.App.Security.Queries.GetAppUserById;
using OSS.App.Security.Queries.GetAppUsers;
using OSS.App.Security.Queries.GetRawRawAppUserById;
using OSS.App.ShiftGroups.Queries.GetShiftGroups;
using OSS.App.ShiftRoles.Queries.GetShiftRoles;
using OSS.Domain.Entities;
using OSS.Web.Extensions;

namespace OSS.Web.Controllers
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class UsersController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var vm = await _mediator.Send(new GetAppUsersListQuery());
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["GenderId"] = new SelectList(await _mediator.Send(new GetGendersQuery()), "Id", "Name");
            ViewData["ShiftGroupId"] = new SelectList(await _mediator.Send(new GetShiftGroupsQuery()), "Id", "Name");
            ViewData["ShiftRoleId"] = new SelectList(await _mediator.Send(new GetShiftRolesQuery()), "Id", "RoleName");
            return View(new CreateAppUserCommand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppUserCommand vm)
        {
            vm.BaseUrl = new Uri(new Uri(Request.Scheme + "://" + Request.Host), "roster/Identity/Account/ConfirmEmail").ToString();
            IdentityResult result = await _mediator.Send(vm);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Created new account for {vm.Username}");
                return RedirectToAction(nameof(Index)).WithSuccess($"Created new user {vm.Username}");
            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            ViewData["GenderId"] = new SelectList(await _mediator.Send(new GetGendersQuery()), "Id", "Name");
            ViewData["ShiftGroupId"] = new SelectList(await _mediator.Send(new GetShiftGroupsQuery()), "Id", "Name");
            ViewData["ShiftRoleId"] = new SelectList(await _mediator.Send(new GetShiftRolesQuery()), "Id", "RoleName");
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _mediator.Send(new GetRawAppUserByIdQuery() { Id = id });
            if (user == null)
            {
                return NotFound();
            }

            EditAppUserCommand vm = _mapper.Map<EditAppUserCommand>(user);

            // If we got this far, something failed, redisplay form
            // ViewData["UserRole"] = new SelectList(SecurityConstants.GetRoles());
            ViewData["GenderId"] = new SelectList(await _mediator.Send(new GetGendersQuery()), "Id", "Name");
            ViewData["ShiftGroupId"] = new SelectList(await _mediator.Send(new GetShiftGroupsQuery()), "Id", "Name");
            ViewData["ShiftRoleId"] = new SelectList(await _mediator.Send(new GetShiftRolesQuery()), "Id", "RoleName");
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAppUserCommand vm)
        {
            List<string> errors = await _mediator.Send(vm);
            AddErrors(errors);

            // check if we have any errors and redirect if successful
            if (errors.Count == 0)
            {
                _logger.LogInformation("User edit operation successful");
                return RedirectToAction(nameof(Index)).WithSuccess("User Editing done");
            }

            // If we got this far, something failed, redisplay form
            // ViewData["UserRole"] = new SelectList(SecurityConstants.GetRoles());
            ViewData["GenderId"] = new SelectList(await _mediator.Send(new GetGendersQuery()), "Id", "Name");
            ViewData["ShiftGroupId"] = new SelectList(await _mediator.Send(new GetShiftGroupsQuery()), "Id", "Name");
            ViewData["ShiftRoleId"] = new SelectList(await _mediator.Send(new GetShiftRolesQuery()), "Id", "RoleName");
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            UserDTO vm = await _mediator.Send(new GetAppUserByIdQuery() { Id = id });

            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserDTO vm)
        {
            List<string> errs = await _mediator.Send(new DeleteAppUserCommand() { Id = vm.UserId });

            if (errs.Count == 0)
            {
                _logger.LogInformation("User deleted successfully");
                return RedirectToAction(nameof(Index)).WithSuccess("User deletion done");
            }

            AddErrors(errs);

            // If we got this far, something failed, redisplay form
            return View(vm);
        }

        // helper function
        private void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // helper function
        private void AddErrors(IEnumerable<string> errs)
        {
            foreach (string error in errs)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}