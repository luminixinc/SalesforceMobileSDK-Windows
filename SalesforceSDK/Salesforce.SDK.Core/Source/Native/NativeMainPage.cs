﻿/*
 * Copyright (c) 2014, salesforce.com, inc.
 * All rights reserved.
 * Redistribution and use of this software in source and binary forms, with or
 * without modification, are permitted provided that the following conditions
 * are met:
 * - Redistributions of source code must retain the above copyright notice, this
 * list of conditions and the following disclaimer.
 * - Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 * - Neither the name of salesforce.com, inc. nor the names of its contributors
 * may be used to endorse or promote products derived from this software without
 * specific prior written permission of salesforce.com, inc.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Salesforce.SDK.Adaptation;
using Salesforce.SDK.App;
using Salesforce.SDK.Auth;

namespace Salesforce.SDK.Native
{
    public class NativeMainPage : Page, ISalesforcePage
    {
        /// <summary>
        ///     Helper method for handling logout.
        /// </summary>
        protected async void OnLogout()
        {
            await SalesforceApplication.GlobalClientManager.Logout();

            CheckIfLoginNeeded();
        }

        /// <summary>
        ///     When navigated to, we try to get a RestClient
        ///     If we are not already authenticated, this will kick off the login flow
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CheckIfLoginNeeded();
        }

        private void CheckIfLoginNeeded()
        {
            Account account = AccountManager.GetAccount();
            if (account == null)
            {
                PlatformAdapter.SendToCustomLogger("NativeMainPage.CheckIfLoginNeeded - account object is null, calling StartLoginFlow",
                                                          Windows.Foundation.Diagnostics.LoggingLevel.Verbose);
                PlatformAdapter.Resolve<IAuthHelper>().StartLoginFlow();
            }
        }
    }
}