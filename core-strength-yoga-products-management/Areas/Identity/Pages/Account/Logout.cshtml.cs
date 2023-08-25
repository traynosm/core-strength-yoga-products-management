﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using core_strength_yoga_products_management.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using core_strength_yoga_products_management.Interfaces;

namespace core_strength_yoga_products_management.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<core_strength_yoga_products_managementUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly ILoginService _loginService;

        public LogoutModel(SignInManager<core_strength_yoga_products_managementUser> signInManager, ILogger<LogoutModel> logger, ILoginService loginService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _loginService = loginService;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");
            _loginService.RevokeToken();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
