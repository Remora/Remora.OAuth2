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
using System.Linq;
using System.Web;
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

    /// <summary>
    /// Tests the <see cref="ImplicitAccessTokenErrorResponse"/> class.
    /// </summary>
    public static class ImplicitAccessTokenErrorResponseTests
    {
        /// <summary>
        /// Tests the <see cref="ImplicitAccessTokenErrorResponse.TryParse"/> method.
        /// </summary>
        public static class TryParse
        {
            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseErrorCorrectly()
            {
                var expected = "some error";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + $"error={HttpUtility.UrlEncode(expected)}&"
                    + "error_description=ooga&"
                    + $"error_uri={HttpUtility.UrlEncode("https://somewhere.org")}&"
                    + "state=booga"
                );

                Assert.True(ImplicitAccessTokenErrorResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.Error);
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseErrorDescriptionCorrectly()
            {
                var expected = "some description";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error=ooga&"
                    + $"error_description={HttpUtility.UrlEncode(expected)}&"
                    + $"error_uri={HttpUtility.UrlEncode("https://somewhere.org")}&"
                    + "state=booga"
                );

                Assert.True(ImplicitAccessTokenErrorResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.ErrorDescription);
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseErrorUriCorrectly()
            {
                var expected = "https://some-error-uri.com";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error=ooga&"
                    + "error_description=booga&"
                    + $"error_uri={HttpUtility.UrlEncode(expected)}&"
                    + "state=wooga"
                );

                Assert.True(ImplicitAccessTokenErrorResponse.TryParse(value, out var response));
                Assert.Equal(new Uri(expected), response.ErrorUri);
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseStateCorrectly()
            {
                var expected = "some state";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error=ooga&"
                    + "error_description=booga&"
                    + "error_uri=wooga&"
                    + $"state={HttpUtility.UrlEncode(expected)}"
                );

                Assert.True(ImplicitAccessTokenErrorResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.State);
            }

            /// <summary>
            /// Tests whether the method requires a fragment to be present.
            /// </summary>
            [Fact]
            public static void RequiresFragment()
            {
                var value = new Uri
                (
                    "https://my-uri.net"
                );

                Assert.False(ImplicitAccessTokenErrorResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method requires a certain property to be present.
            /// </summary>
            [Fact]
            public static void RequiresError()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error_description=ooga&"
                    + "error_uri=booga&"
                    + "state=wooga"
                );

                Assert.False(ImplicitAccessTokenErrorResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method does not require a certain property to be present.
            /// </summary>
            [Fact]
            public static void DoesNotRequireErrorDescription()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error=ooga&"
                    + "error_uri=wooga&"
                    + "state=wooga"
                );

                Assert.True(ImplicitAccessTokenErrorResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method does not require a certain property to be present.
            /// </summary>
            [Fact]
            public static void DoesNotRequireErrorUri()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error=ooga&"
                    + "error_description=booga&"
                    + "state=wooga"
                );

                Assert.True(ImplicitAccessTokenErrorResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method requires a certain property to be in a certain format.
            /// </summary>
            [Fact]
            public static void RequiresWellFormedErrorUri()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error=ooga&"
                    + "error_description=booga&"
                    + "error_uri=https://example.com<>&"
                    + "state=wooga"
                );

                Assert.False(ImplicitAccessTokenErrorResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method does not require a certain property to be present.
            /// </summary>
            [Fact]
            public static void DoesNotRequireState()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "error=ooga&"
                    + "error_description=booga&"
                    + "error_uri=wooga"
                );

                Assert.True(ImplicitAccessTokenErrorResponse.TryParse(value, out _));
            }
        }
    }

    /// <summary>
    /// Tests the <see cref="ImplicitAccessTokenResponse"/> class.
    /// </summary>
    public static class ImplicitAccessTokenResponseTests
    {
        /// <summary>
        /// Tests the <see cref="ImplicitAccessTokenResponse.TryParse"/> method.
        /// </summary>
        public static class TryParse
        {
            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseAccessTokenCorrectly()
            {
                var expected = "some token";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + $"access_token={HttpUtility.UrlEncode(expected)}&"
                    + "token_type=ooga&"
                    + "expires_in=3600&"
                    + "scope=wooga&"
                    + "state=booga"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.AccessToken);
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseTokenTypeCorrectly()
            {
                var expected = "some token type";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + $"access_token=ooga&"
                    + $"token_type={HttpUtility.UrlEncode(expected)}&"
                    + "expires_in=3600&"
                    + "scope=wooga&"
                    + "state=booga"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.TokenType);
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseExpiresInCorrectly()
            {
                var expected = TimeSpan.FromSeconds(3600);
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + $"access_token=ooga&"
                    + $"token_type=mooga&"
                    + $"expires_in={expected.TotalSeconds:0}&"
                    + "scope=wooga&"
                    + "state=booga"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.ExpiresIn);
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseScopeCorrectly()
            {
                var expected = "some valid scope";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + $"access_token=wooga&"
                    + "token_type=ooga&"
                    + "expires_in=3600&"
                    + $"scope={HttpUtility.UrlEncode(expected)}&"
                    + "state=booga"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out var response));
                Assert.Equal(expected.Split(' ').AsEnumerable(), response.Scope.Value.AsEnumerable());
            }

            /// <summary>
            /// Tests whether the method can parse a property correctly.
            /// </summary>
            [Fact]
            public static void CanParseStateCorrectly()
            {
                var expected = "some state";

                var value = new Uri
                (
                    "https://my-uri.net#"
                    + $"access_token=wooga&"
                    + "token_type=ooga&"
                    + "expires_in=3600&"
                    + $"scope=booga&"
                    + $"state={HttpUtility.UrlEncode(expected)}"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out var response));
                Assert.Equal(expected, response.State);
            }

            /// <summary>
            /// Tests whether the method requires a fragment to be present.
            /// </summary>
            [Fact]
            public static void RequiresFragment()
            {
                var value = new Uri
                (
                    "https://my-uri.net"
                );

                Assert.False(ImplicitAccessTokenResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method requires a certain property to be present.
            /// </summary>
            [Fact]
            public static void RequiresAccessToken()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "token_type=ooga&"
                    + "expires_in=3600&"
                    + "scope=booga&"
                    + "state=wooga"
                );

                Assert.False(ImplicitAccessTokenResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method requires a certain property to be present.
            /// </summary>
            [Fact]
            public static void RequiresTokenType()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "access_token=ooga&"
                    + "expires_in=3600&"
                    + "scope=booga&"
                    + "state=wooga"
                );

                Assert.False(ImplicitAccessTokenResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method does not require a certain property to be present.
            /// </summary>
            [Fact]
            public static void DoesNotRequireExpiresIn()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "access_token=ooga&"
                    + "token_type=booga&"
                    + "scope=booga&"
                    + "state=wooga"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method requires a certain property to be in a certain format.
            /// </summary>
            [Fact]
            public static void RequiresNumericalExpiresIn()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "access_token=ooga&"
                    + "token_type=booga&"
                    + "expires_in=notanumber&"
                    + "scope=booga&"
                    + "state=wooga"
                );

                Assert.False(ImplicitAccessTokenResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method does not require a certain property to be present.
            /// </summary>
            [Fact]
            public static void DoesNotRequireScope()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "access_token=ooga&"
                    + "token_type=booga&"
                    + "state=wooga"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out _));
            }

            /// <summary>
            /// Tests whether the method does not require a certain property to be present.
            /// </summary>
            [Fact]
            public static void DoesNotRequireState()
            {
                var value = new Uri
                (
                    "https://my-uri.net#"
                    + "access_token=ooga&"
                    + "token_type=booga&"
                    + "scope=wooga"
                );

                Assert.True(ImplicitAccessTokenResponse.TryParse(value, out _));
            }
        }
    }
}
