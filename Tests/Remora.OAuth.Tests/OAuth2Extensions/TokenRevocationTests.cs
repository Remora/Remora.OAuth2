//
//  TokenRevocationTests.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) Jarl Gullberg
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
using Remora.OAuth2.Abstractions.OAuthExtensions.TokenRevocation;
using Remora.OAuth2.OAuth2Extensions.TokenRevocation;
using Remora.Rest.Xunit.Extensions;
using Xunit;

namespace Remora.OAuth.Tests.OAuth2Extensions;

/// <summary>
/// Tests the token revocation flow.
/// </summary>
public static class TokenRevocationTests
{
    /// <summary>
    /// Tests the <see cref="TokenRevocationRequest"/> class.
    /// </summary>
    public class TokenRevocationRequestTests : TokenRevocationRequestTestsBase<TokenRevocationRequest>
    {
        /// <inheritdoc />
        protected override Func<TokenRevocationRequest> BlankRequestFactory =>
            () => new TokenRevocationRequest(string.Empty);

        /// <inheritdoc />
        protected override Func<ITokenRevocationRequestExtension, TokenRevocationRequest> ExtendedRequestFactory =>
            e => new TokenRevocationRequest(string.Empty, Extensions: new[] { e });

        /// <summary>
        /// Tests the <see cref="TokenRevocationRequest.ToRequest"/> method.
        /// </summary>
        public static class ToRequest
        {
            /// <summary>
            /// Tests whether the created request includes the correct token.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectToken()
            {
                var expected = "some token";

                var request = new TokenRevocationRequest(expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "token", expected }
                });
            }

            /// <summary>
            /// Tests whether the created request includes the correct token type hint.
            /// </summary>
            [Fact]
            public static void CreatesRequestWithCorrectTokenTypeHint()
            {
                var expected = "some hint";

                var request = new TokenRevocationRequest(string.Empty, expected);
                var message = request.ToRequest(new Uri("about:blank"));

                message.HasUrlEncodedFormData(new Dictionary<string, string>
                {
                    { "token_type_hint", expected }
                });
            }
        }
    }

    /// <summary>
    /// Tests the <see cref="TokenRevocationErrorResponse"/> class.
    /// </summary>
    public class DeviceAuthorizationAuthorizationResponseTests :
        JsonBackedTypeTestBase<TokenRevocationErrorResponse>;
}
