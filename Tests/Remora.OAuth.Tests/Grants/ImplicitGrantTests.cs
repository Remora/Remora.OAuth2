//
//  ImplicitGrantTests.cs
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
/// Tests the Implicit grant flow.
/// </summary>
public static class ImplicitGrantTests
{
     /// <summary>
    /// Tests the <see cref="ImplicitAuthorizationRequest"/> class.
    /// </summary>
    public class ImplicitAuthorizationRequestTests :
        AuthorizationRequestTestsBase<ImplicitAuthorizationRequest>
    {
        /// <inheritdoc />
        protected override string ResponseType => "token";

        /// <inheritdoc />
        protected override Func<ImplicitAuthorizationRequest> BlankRequestFactory =>
            () => new ImplicitAuthorizationRequest(string.Empty);

        /// <inheritdoc />
        protected override Func<IAuthorizationRequestExtension, ImplicitAuthorizationRequest> ExtendedRequestFactory =>
            e => new ImplicitAuthorizationRequest(string.Empty, Extensions: new(new[] { e }));

        /// <summary>
        /// Tests the <see cref="ImplicitAuthorizationRequest.ToRequest"/> method.
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

                var request = new ImplicitAuthorizationRequest(expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasQueryParameters(new Dictionary<string, string>
                {
                    { "client_id", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct redirect URI.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectRedirectUri()
            {
                var expected = new Uri("https://redirect-uri.net");

                var request = new ImplicitAuthorizationRequest(string.Empty, RedirectUri: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasQueryParameters(new Dictionary<string, string>
                {
                    { "redirect_uri", expected.ToString() }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct scope.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectScope()
            {
                var expected = new[] { "some", "scope", "values" };

                var request = new ImplicitAuthorizationRequest(string.Empty, Scope: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasQueryParameters(new Dictionary<string, string>
                {
                    { "scope", string.Join(' ', expected) }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct state.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectState()
            {
                var expected = "some state";

                var request = new ImplicitAuthorizationRequest(string.Empty, State: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasQueryParameters(new Dictionary<string, string>
                {
                    { "state", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request does not include any additional properties.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithNoAdditionalProperties()
            {
                var request = new ImplicitAuthorizationRequest
                (
                    "some client id",
                    RedirectUri: new Uri("https://redirect-uri.net"),
                    Scope: new[] { "some", "scope", "values" },
                    State: "some state"
                );
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasQueryParameters
                (
                    new Dictionary<string, string>
                    {
                        { "response_type", "token" },
                        { "client_id", "some client id" },
                        { "redirect_uri", new Uri("https://redirect-uri.net").ToString() },
                        { "scope", "some scope values" },
                        { "state", "some state" }
                    },
                    true
                );
            }
        }
    }
}
