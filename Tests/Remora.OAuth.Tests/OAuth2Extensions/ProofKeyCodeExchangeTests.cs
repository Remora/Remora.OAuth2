//
//  ProofKeyCodeExchangeTests.cs
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

using System.Collections.Generic;
using System.Web;
using Remora.OAuth2.OAuth2Extensions.ProofKeyCodeExchange;
using Xunit;

namespace Remora.OAuth.Tests.OAuth2Extensions;

/// <summary>
/// Tests the Proof Key Code Exchange extension.
/// </summary>
public class ProofKeyCodeExchangeTests
{
    /// <summary>
    /// Tests the <see cref="ProofKeyCodeExchangeAccessTokenRequest"/> class.
    /// </summary>
    public static class ProofKeyCodeExchangeAccessTokenRequestTests
    {
        /// <summary>
        /// Tests whether the extension correctly adds a field.
        /// </summary>
        [Fact]
        public static void AddsCorrectPropertyForCodeVerifier()
        {
            var expected = "ooga";
            var extension = new ProofKeyCodeExchangeAccessTokenRequest(expected);

            var container = new Dictionary<string, string>();
            extension.AddParameters(container);

            Assert.Contains("code_verifier", container as IReadOnlyDictionary<string, string>);
            Assert.Equal(expected, container["code_verifier"]);
        }
    }

    /// <summary>
    /// Tests the <see cref="ProofKeyCodeExchangeAuthorizationRequest"/> class.
    /// </summary>
    public static class ProofKeyCodeExchangeAuthorizationRequestTests
    {
        /// <summary>
        /// Tests whether the extension correctly adds a field.
        /// </summary>
        [Fact]
        public static void AddsCorrectPropertyForCodeChallenge()
        {
            var expected = "ooga";
            var extension = new ProofKeyCodeExchangeAuthorizationRequest(expected);

            var container = HttpUtility.ParseQueryString(string.Empty);
            extension.AddParameters(container);

            Assert.Contains("code_challenge", container.AllKeys);
            Assert.Equal(expected, container["code_challenge"]);
        }

        /// <summary>
        /// Tests whether the extension correctly adds a field.
        /// </summary>
        [Fact]
        public static void AddsCorrectPropertyForCodeChallengeMethod()
        {
            var expected = "ooga";
            var extension = new ProofKeyCodeExchangeAuthorizationRequest(string.Empty, expected);

            var container = HttpUtility.ParseQueryString(string.Empty);
            extension.AddParameters(container);

            Assert.Contains("code_challenge_method", container.AllKeys);
            Assert.Equal(expected, container["code_challenge_method"]);
        }
    }
}
