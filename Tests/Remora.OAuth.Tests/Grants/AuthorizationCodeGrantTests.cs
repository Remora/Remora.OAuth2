//
//  AuthorizationCodeGrantTests.cs
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
using System.Web;
using Remora.OAuth.Tests.TestBases;
using Remora.OAuth2;
using Remora.OAuth2.Abstractions.OAuthExtensions;
using Remora.Rest.Xunit.Extensions;
using Xunit;

namespace Remora.OAuth.Tests.Grants;

/// <summary>
/// Tests the Authorization Code grant flow.
/// </summary>
public static class AuthorizationCodeGrantTests
{
    /// <summary>
    /// Tests the <see cref="AuthorizationCodeAccessTokenRequest"/> class.
    /// </summary>
    public class AuthorizationCodeAccessTokenRequestTests :
        AccessTokenRequestTestsBase<AuthorizationCodeAccessTokenRequest>
    {
        /// <inheritdoc />
        protected override string GrantType => "authorization_code";

        /// <inheritdoc />
        protected override Func<AuthorizationCodeAccessTokenRequest> BlankRequestFactory =>
            () => new AuthorizationCodeAccessTokenRequest(string.Empty);

        /// <inheritdoc />
        protected override Func<IAccessTokenRequestExtension, AuthorizationCodeAccessTokenRequest> ExtendedRequestFactory =>
            e => new AuthorizationCodeAccessTokenRequest(string.Empty, Extensions: new(new[] { e }));

        /// <summary>
        /// Tests the <see cref="AuthorizationCodeAccessTokenRequest.ToRequest"/> method.
        /// </summary>
        public static class ToRequest
        {
            /// <summary>
            /// Tests whether the created request includes the correct authorization code.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectCode()
            {
                var expected = "some code";

                var request = new AuthorizationCodeAccessTokenRequest(expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "code", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct redirect URI.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectRedirectUri()
            {
                var expected = new Uri("https://redirect-uri.net");

                var request = new AuthorizationCodeAccessTokenRequest(string.Empty, RedirectUri: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "redirect_uri", expected.ToString() }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct client ID.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectClientID()
            {
                var expected = "some client id";

                var request = new AuthorizationCodeAccessTokenRequest(string.Empty, ClientID: expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "client_id", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request does not include any additional properties.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithNoAdditionalProperties()
            {
                var request = new AuthorizationCodeAccessTokenRequest
                (
                    "some code",
                    RedirectUri: new Uri("https://redirect-uri.net"),
                    ClientID: "some client id"
                );

                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData
                (
                    new Dictionary<string, string>
                    {
                        { "grant_type", "authorization_code" },
                        { "code", "some code" },
                        { "redirect_uri", new Uri("https://redirect-uri.net").ToString() },
                        { "client_id", "some client id" }
                    },
                    true
                );
            }
        }
    }

    /// <summary>
    /// Tests the <see cref="AuthorizationCodeAuthorizationRequest"/> class.
    /// </summary>
    public class AuthorizationCodeAuthorizationRequestTests :
        AuthorizationRequestTestsBase<AuthorizationCodeAuthorizationRequest>
    {
        /// <inheritdoc />
        protected override string ResponseType => "code";

        /// <inheritdoc />
        protected override Func<AuthorizationCodeAuthorizationRequest> BlankRequestFactory =>
            () => new AuthorizationCodeAuthorizationRequest(string.Empty);

        /// <inheritdoc />
        protected override Func<IAuthorizationRequestExtension, AuthorizationCodeAuthorizationRequest> ExtendedRequestFactory =>
            e => new AuthorizationCodeAuthorizationRequest(string.Empty, Extensions: new(new[] { e }));

        /// <summary>
        /// Tests the <see cref="AuthorizationCodeAuthorizationRequest.ToRequest"/> method.
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

                var request = new AuthorizationCodeAuthorizationRequest(expected);
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

                var request = new AuthorizationCodeAuthorizationRequest(string.Empty, RedirectUri: expected);
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

                var request = new AuthorizationCodeAuthorizationRequest(string.Empty, Scope: expected);
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

                var request = new AuthorizationCodeAuthorizationRequest(string.Empty, State: expected);
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
                var request = new AuthorizationCodeAuthorizationRequest
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
                        { "response_type", "code" },
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

    /// <summary>
    /// Tests the <see cref="AuthorizationCodeAuthorizationResponse"/> class.
    /// </summary>
    public static class AuthorizationCodeAuthorizationResponseTests
    {
        /// <summary>
        /// Tests the <see cref="AuthorizationCodeAuthorizationResponse.TryParse"/> method.
        /// </summary>
        public static class TryParse
        {
            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseCodeCorrectly()
            {
                var expected = "some code";

                var value = new Uri($"https://my-uri.net?code={HttpUtility.UrlEncode(expected)}&state=booga");
                Assert.True(AuthorizationCodeAuthorizationResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.Code);
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseStateCorrectly()
            {
                var expected = "some state";

                var value = new Uri($"https://my-uri.net?code=ooga&state={HttpUtility.UrlEncode(expected)}");
                Assert.True(AuthorizationCodeAuthorizationResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.State);
            }

            /// <summary>
            /// Tests whether the method requires a certain property to be present.
            /// </summary>
            [Fact]
            public static void RequiresCode()
            {
                var value = new Uri("https://my-uri.net?state=booga");
                Assert.False(AuthorizationCodeAuthorizationResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method does not require a certain property to be present.
            /// </summary>
            [Fact]
            public static void DoesNotRequireState()
            {
                var value = new Uri($"https://my-uri.net?code=booga");
                Assert.True(AuthorizationCodeAuthorizationResponse.TryParse(value, out _));
            }
        }
    }
}
