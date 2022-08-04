//
//  ResourceOwnerPasswordCredentialsGrantTests.cs
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
/// Tests the Resource Owner Password Credentials grant flow.
/// </summary>
public static class ResourceOwnerPasswordCredentialsGrantTests
{
    /// <summary>
    /// Tests the <see cref="ResourceOwnerPasswordCredentialsAccessTokenRequest"/> class.
    /// </summary>
    public class ResourceOwnerPasswordCredentialsAccessTokenRequestTests :
        AccessTokenRequestTestsBase<ResourceOwnerPasswordCredentialsAccessTokenRequest>
    {
        /// <inheritdoc />
        protected override string GrantType => "password";

        /// <inheritdoc />
        protected override Func<ResourceOwnerPasswordCredentialsAccessTokenRequest> BlankRequestFactory =>
            () => new ResourceOwnerPasswordCredentialsAccessTokenRequest(string.Empty, string.Empty);

        /// <inheritdoc />
        protected override Func<IAccessTokenRequestExtension, ResourceOwnerPasswordCredentialsAccessTokenRequest> ExtendedRequestFactory =>
            e => new ResourceOwnerPasswordCredentialsAccessTokenRequest(string.Empty, string.Empty, Extensions: new(new[] { e } ));

        /// <summary>
        /// Tests the <see cref="ResourceOwnerPasswordCredentialsAccessTokenRequest.ToRequest"/> method.
        /// </summary>
        public static class ToRequest
        {
            /// <summary>
            /// Tests whether the created request includes the correct username.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectUsername()
            {
                var expected = "ooga";

                var request = new ResourceOwnerPasswordCredentialsAccessTokenRequest(expected, string.Empty);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "username", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct password.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectPassword()
            {
                var expected = "booga";

                var request = new ResourceOwnerPasswordCredentialsAccessTokenRequest(string.Empty, expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "password", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct scope.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectScope()
            {
                var expected = new[] { "some", "expected", "values" };

                var request = new ResourceOwnerPasswordCredentialsAccessTokenRequest(string.Empty, string.Empty, expected);
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
                var request = new ResourceOwnerPasswordCredentialsAccessTokenRequest("ooga", "booga", new[] { "some", "expected", "values" });
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData
                (
                    new Dictionary<string, string>
                    {
                        { "grant_type", "password" },
                        { "username", "ooga" },
                        { "password", "booga" },
                        { "scope", "some expected values" }
                    },
                    true
                );
            }
        }
    }
}
