//
//  DeviceAuthorizationGrantTests.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using Remora.OAuth.Tests.TestBases;
using Remora.OAuth2.Abstractions.OAuthExtensions.DeviceAuthorizationGrant;
using Remora.OAuth2.OAuth2Extensions.DeviceAuthorizationGrant;
using Remora.Rest.Xunit.Extensions;
using Xunit;

namespace Remora.OAuth.Tests.OAuth2Extensions;

/// <summary>
/// Tests the device authorization grant flow.
/// </summary>
public class DeviceAuthorizationGrantTests
{
    /// <summary>
    /// Tests the <see cref="DeviceAuthorizationAccessTokenRequest"/> class.
    /// </summary>
    public class DeviceAuthorizationAccessTokenRequestTests :
        DeviceAuthorizationAccessTokenRequestTestsBase<DeviceAuthorizationAccessTokenRequest>
    {
        /// <inheritdoc />
        protected override string GrantType => "urn:ietf:params:oauth:grant-type:device_code";

        /// <inheritdoc />
        protected override Func<DeviceAuthorizationAccessTokenRequest> BlankRequestFactory =>
            () => new DeviceAuthorizationAccessTokenRequest(string.Empty);

        /// <inheritdoc />
        protected override Func<IDeviceAuthorizationAccessTokenRequestExtension, DeviceAuthorizationAccessTokenRequest> ExtendedRequestFactory =>
            e => new DeviceAuthorizationAccessTokenRequest(string.Empty, Extensions: new[] { e });

        /// <summary>
        /// Tests the <see cref="DeviceAuthorizationAccessTokenRequest.ToRequest"/> method.
        /// </summary>
        public static class ToRequest
        {
            /// <summary>
            /// Tests whether the created request includes the correct device code.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectDeviceCode()
            {
                var expected = "some code";

                var request = new DeviceAuthorizationAccessTokenRequest(expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "device_code", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct client ID.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectClientID()
            {
                var expected = "some client id";

                var request = new DeviceAuthorizationAccessTokenRequest(string.Empty, ClientID: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "client_id", expected }
                });
            }
        }
    }

    /// <summary>
    /// Tests the <see cref="DeviceAuthorizationAuthorizationRequest"/> class.
    /// </summary>
    public class DeviceAuthorizationAuthorizationRequestTests :
        DeviceAuthorizationAuthorizationRequestTestsBase<DeviceAuthorizationAuthorizationRequest>
    {
        /// <inheritdoc />
        protected override Func<DeviceAuthorizationAuthorizationRequest> BlankRequestFactory =>
            () => new DeviceAuthorizationAuthorizationRequest();

        /// <inheritdoc />
        protected override Func<IDeviceAuthorizationAuthorizationRequestExtension, DeviceAuthorizationAuthorizationRequest> ExtendedRequestFactory =>
            e => new DeviceAuthorizationAuthorizationRequest(Extensions: new[] { e });

        /// <summary>
        /// Tests the <see cref="DeviceAuthorizationAuthorizationRequest.ToRequest"/> method.
        /// </summary>
        public static class ToRequest
        {
            /// <summary>
            /// Tests whether the created request includes the correct client ID.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectClientID()
            {
                var expected = "some client id";

                var request = new DeviceAuthorizationAuthorizationRequest(expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "client_id", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct scope.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectScope()
            {
                var expected = new[] { "some", "scope", "values" };

                var request = new DeviceAuthorizationAuthorizationRequest(string.Empty, Scope: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "scope", string.Join(' ', expected) }
                });
            }
        }
    }

    /// <summary>
    /// Tests the <see cref="DeviceAuthorizationAccessTokenErrorResponse"/> class.
    /// </summary>
    public class DeviceAuthorizationAccessTokenErrorResponseTests :
        JsonBackedTypeTestBase<DeviceAuthorizationAccessTokenErrorResponse>
    {
    }

    /// <summary>
    /// Tests the <see cref="DeviceAuthorizationAccessTokenResponse"/> class.
    /// </summary>
    public class DeviceAuthorizationAccessTokenResponseTests :
        JsonBackedTypeTestBase<DeviceAuthorizationAccessTokenResponse>
    {
    }

    /// <summary>
    /// Tests the <see cref="DeviceAuthorizationAuthorizationErrorResponse"/> class.
    /// </summary>
    public class DeviceAuthorizationAuthorizationErrorResponseTests :
        JsonBackedTypeTestBase<DeviceAuthorizationAuthorizationErrorResponse>
    {
    }

    /// <summary>
    /// Tests the <see cref="DeviceAuthorizationAuthorizationResponse"/> class.
    /// </summary>
    public class DeviceAuthorizationAuthorizationResponseTests :
        JsonBackedTypeTestBase<DeviceAuthorizationAuthorizationResponse>
    {
    }
}
