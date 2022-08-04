//
//  RefreshAccessTokenTests.cs
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
using Remora.OAuth2;
using Remora.OAuth2.Abstractions.OAuthExtensions;
using Remora.Rest.Xunit.Extensions;
using Xunit;

namespace Remora.OAuth.Tests.Grants;

/// <summary>
/// Tests the access token refresh flow.
/// </summary>
public class RefreshAccessTokenTests
{
    /// <summary>
    /// Tests the <see cref="RefreshAccessTokenRequest"/> class.
    /// </summary>
    public class RefreshAccessTokenRequestTests : AccessTokenRequestTestsBase<RefreshAccessTokenRequest>
    {
        /// <inheritdoc />
        protected override string GrantType => "refresh_token";

        /// <inheritdoc />
        protected override Func<RefreshAccessTokenRequest> BlankRequestFactory =>
            () => new RefreshAccessTokenRequest(string.Empty);

        /// <inheritdoc />
        protected override Func<IAccessTokenRequestExtension, RefreshAccessTokenRequest> ExtendedRequestFactory =>
            e => new RefreshAccessTokenRequest(string.Empty, Extensions: new[] { e });

        /// <summary>
        /// Tests the <see cref="RefreshAccessTokenRequest.ToRequest"/> method.
        /// </summary>
        public static class ToRequest
        {
            /// <summary>
            /// Tests whether the created request includes the correct refresh token.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectRefreshToken()
            {
                var expected = "some token";

                var request = new RefreshAccessTokenRequest(expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "refresh_token", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct scope.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectScope()
            {
                var expected = new[] { "some", "expected", "values" };

                var request = new RefreshAccessTokenRequest(string.Empty, Scope: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "scope", string.Join(' ', expected) }
                });
            }

            /// <summary>
            /// Tests whether the created request does not include any additional properties.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithNoAdditionalProperties()
            {
                var request = new RefreshAccessTokenRequest
                (
                    "some token",
                    new[] { "some", "expected", "values" }
                );

                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData
                (
                    new Dictionary<string, string>
                    {
                        { "grant_type", "refresh_token" },
                        { "refresh_token", "some token" },
                        { "scope", "some expected values" }
                    },
                    true
                );
            }
        }
    }
}
